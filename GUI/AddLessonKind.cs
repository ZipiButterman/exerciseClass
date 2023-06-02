using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExerciseClass.BLL;

namespace ExerciseClass.GUI
{
    public partial class AddLessonKind : Form
    {
        LessonKind l;
        LessonKindDB ldb;
        CourseTime c;
        CourseTimeDB cdb;
        LessonKind l1;
        FormProject fp;
        bool flagUpdate = false;
        bool flagAdd = false;
        bool flagOK = false;
        bool flagError = false;
        public AddLessonKind(FormProject f1)
        {
            InitializeComponent();
            DoToolTip();          
            fp = f1;
            ldb = new LessonKindDB();
            l = new LessonKind();
            c = new CourseTime();
            cdb = new CourseTimeDB();   
            string[] arr = { "גברים", "נשים", "בנים", "בנות" };
            dataGridView1.DataSource = ldb.GetList().Select(x => new { סוג_שיעור = x.Kind, קוד_שיעור = x.LessonCode, קהל_יעד=x.Audience, תשלום_חודשי=x.PricePerMonth, רבעון=x.QuarterlyPrice }).ToList();
            for (int i = 0; i < arr.Length; i++)
                cmbAudience.Items.Add(arr[i]);
            NotPossible();
            NotVisible();
        }

        public void NotVisible()
        {
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            btnBackData.Visible = false;
        }
        public void DoToolTip()
        {
            toolTip1.SetToolTip(txtKind, "הכנס סוג שיעור");
            toolTip1.SetToolTip(txtMPrice, "הכנס מחיר חודשי");
            toolTip1.SetToolTip(txtQPrice, "הכנס מחיר רבעון");
            toolTip1.SetToolTip(cmbAudience, "בחר קהל יעד");
            toolTip1.SetToolTip(btnAdd, "הוסף חוג חדש");
            toolTip1.SetToolTip(btnUpdate, "עדכן חוג קיים");
            toolTip1.SetToolTip(btnEarese, "מחק חוג קיים");
            toolTip1.SetToolTip(btnSave, "שמור שינויים");
            toolTip1.SetToolTip(button1, "סגור טופס");
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(btnAddKind, "הוסף, מחק או עדכן קורסים לסוג שיעור נבחר");
            toolTip1.SetToolTip(button2, "בטל מחיקה אחרונה");
            toolTip1.SetToolTip(btnBackData, "חזרה לטבלה הקודמת");
        }
        private bool CreateFields(LessonKind l)
        {
            flagOK = true;
            try
            {
                if (txtKind.Text == "")
                    throw new Exception("יש להכניס סוג שיעור");
                else
                    l.Kind = txtKind.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);   
                return false;
            }
            try
            {
                if (cmbAudience.Text == "")
                    throw new Exception("יש לבחור קהל יעד");
                else
                    l.Audience = cmbAudience.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtMPrice.Text == "")
                    throw new Exception("יש להכניס מחיר חודשי");
                else
                    l.PricePerMonth = Convert.ToDouble(txtMPrice.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtQPrice.Text == "")
                    throw new Exception("יש להכניס מחיר רבעון");
                else
                    l.QuarterlyPrice = Convert.ToDouble(txtQPrice.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                flagOK = false;
            }
            return flagOK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LessonKind l1 = new LessonKind();
            l1 = ldb.GetList().Find(x => x.Kind == txtKind.Text);
            if (l1 != null)
            {
                MessageBox.Show("סוג שיעור זה כבר קיים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if(flagError)
            {
                MessageBox.Show("אחד או יותר מהערכים שהזנת לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if(flagAdd)
            {
                l.LessonCode = ldb.GetNextKey();
                if (CreateFields(l))
                {
                    DialogResult r = MessageBox.Show("האם ברצונך להוסיף סוג שיעור זה", "הוספת סוג שיעור", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if(r == DialogResult.Yes)
                    {
                        ldb.AddNew(l);
                        ldb.UpdateRow(l);
                        NotPossible();
                        ClearText();
                    }                    
                }
                
                dataGridView1.DataSource = ldb.GetList().Select(x => new { סוג_שיעור = x.Kind, קוד_שיעור = x.LessonCode, קהל_יעד = x.Audience, תשלום_חודשי = x.PricePerMonth, רבעון = x.QuarterlyPrice }).ToList();
            }
            if(flagUpdate)
            {
                if (CreateFields(l))
                {
                    DialogResult r = MessageBox.Show("האם ברצונך לעדכן סוג שיעור זה", "עדכון סוג שיעור", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        ldb.UpdateRow(l);
                        NotPossible();
                        ClearText();
                    }
                }
                dataGridView1.DataSource = ldb.GetList().Select(x => new { סוג_שיעור = x.Kind, קוד_שיעור = x.LessonCode, קהל_יעד = x.Audience, תשלום_חודשי = x.PricePerMonth, רבעון = x.QuarterlyPrice }).ToList();
            }
        }
        private void NotPossible()
        {
            txtKind.Enabled = false;
            txtMPrice.Enabled = false;
            txtQPrice.Enabled = false;
            cmbAudience.Enabled = false;          
        }
        private void Possible()
        {
            txtKind.Enabled = true;
            txtMPrice.Enabled = true;
            txtQPrice.Enabled = true;
            cmbAudience.Enabled = true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Possible();
            flagUpdate = true;
            flagAdd = false;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
                l = ldb.Find(id);
                Fill(l);
            }
        }
        private void Fill(LessonKind l)
        {
            txtKind.Text = l.Kind;
            txtMPrice.Text = l.PricePerMonth.ToString();
            txtQPrice.Text = l.QuarterlyPrice.ToString();
            cmbAudience.SelectedItem = l.Audience;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            flagAdd = true;
            flagUpdate = false;
            Possible();
            ClearText();
        }

        public void ClearText()
        {
            txtKind.Text = "";
            txtMPrice.Text = "";
            cmbAudience.Text = "";
            txtQPrice.Text = "";
        }
        private void btnEarese_Click(object sender, EventArgs e)
        {

            string kind = ldb.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value)).Kind;
            int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            if (dataGridView1.SelectedRows.Count > 0)
            {
                c = cdb.Find2(code);
                if (c != null)
                {
                    DialogResult r = MessageBox.Show("לסוג שיעור " + kind + " קיימים קורסים במהלך השבוע\nכדי לבצע מחיקה יש למחוק את הקורסים הקיימים.", "מחיקת סוג שיעור", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                LessonKind l2 = new LessonKind();
                l2 = ldb.Find(code);
                l1 = new LessonKind(l2);
                ldb.DeleteRow(code);
                dataGridView1.DataSource = ldb.GetList().Select(x => new { סוג_שיעור = x.Kind, קוד_שיעור = x.LessonCode, קהל_יעד = x.Audience, תשלום_חודשי = x.PricePerMonth, רבעון = x.QuarterlyPrice }).ToList();
            }            
        }

     

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            fp.Show();
            fp.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKind_TextChanged(object sender, EventArgs e)
        {
            txtKind.BackColor = Color.White;
            lblKind.Text = "";
            flagError = false;
            if (!Validation.IsHebrew(txtKind.Text) && !Validation.IsEnglish(txtKind.Text))
            {
                flagError = true;
                txtKind.BackColor = Color.Lavender;
                lblKind.Text = "יש להכניס אותיות בלבד";
                txtKind.Focus();
            }
        }
      
        private void txtMPrice_TextChanged(object sender, EventArgs e)
        {
            txtMPrice.BackColor = Color.White;
            lblMP.Text = "";
            flagError = false;
            if (!Validation.IsNum(txtMPrice.Text))
            {
                flagError = true;
                txtMPrice.BackColor = Color.Lavender;
                lblMP.Text = "יש להכניס ספרות בלבד";
                txtMPrice.Focus();
            }
        }

        private void txtQPrice_TextChanged(object sender, EventArgs e)
        {
            txtQPrice.BackColor = Color.White;
            lblQP.Text = "";
            flagError = false;
            if (!Validation.IsNum(txtQPrice.Text))
            {
                flagError = true;
                txtQPrice.BackColor = Color.Lavender;
                lblQP.Text = "יש להכניס ספרות בלבד";
                txtQPrice.Focus();
            }
        }

        private void btnAddKind_Click(object sender, EventArgs e)
        {
            string code;
            if(dataGridView1.SelectedRows.Count > 0)
            {
                code = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                FrmCourse fc = new FrmCourse(fp, this, code);
                fc.ShowDialog();
            }            
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            CourseTimeDB ctdb = new CourseTimeDB();
            CourseTime cc = new CourseTime();
            cc = ctdb.GetList().Find(x => x.Code == code);
            if (cc == null)
            {
                MessageBox.Show("אין קורסים לסוג שיעור זה", "אין קורסים", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            dataGridView2.DataSource = ctdb.GetList().FindAll(x => x.Code == code).Select(x => new { קוד = x.Code, מספר_סידור = x.SerialNumber, מורה = x.ThisTeacher().ToString(), יום = x.Day, שעה = x.Hour, מספר_שיעורים = x.NumberOfLesson }).ToList();            
            dataGridView1.Visible = false;
            label6.Text = "לחץ פעמיים על קורס כדי לראות את המנויים אליו";
            panel1.Visible = false;
            btnBackData.Visible = true;
            dataGridView2.Visible = true;
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int serial = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[1].Value);
            int codeCourse = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
            CourseSubscriptionDB csdb = new CourseSubscriptionDB();
            CourseSubscription cs = new CourseSubscription();
            cs = csdb.GetList().Find(x => x.SerialNumber == serial && codeCourse == x.CourseCode);
            if (cs == null)
            {
                MessageBox.Show("אין מנויים לקורס זה", "אין מנויים", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            dataGridView3.DataSource = csdb.GetList().FindAll(x => x.SerialNumber == serial && codeCourse == x.CourseCode).Select(x => new { מנוי = x.StudentId, שם_מנוי = x.ThisStudent().ToString() }).ToList();            
            label6.Text = "";
            dataGridView2.Visible = false;
            dataGridView3.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(l1 != null)
            {
                ldb.AddNew(l1);
                ldb.UpdateRow(l1);
                dataGridView1.DataSource = ldb.GetList().Select(x => new { סוג_שיעור = x.Kind, קוד_שיעור = x.LessonCode, קהל_יעד = x.Audience, תשלום_חודשי = x.PricePerMonth, רבעון = x.QuarterlyPrice }).ToList();
            }
        }

        private void btnBackData_Click(object sender, EventArgs e)
        {
            if(dataGridView3.Visible)
            {
                dataGridView2.Visible = true;
                dataGridView3.Visible = false;
                label6.Text = "לחץ פעמיים על קורס כדי לראות את המנויים אליו";
            }
            else
            {
                dataGridView1.Visible = true;
                btnBackData.Visible = false;
                label6.Text = "לחץ פעמים על סוג שיעור לראות את הקורסים שלו";
            }
        }
    }
}
