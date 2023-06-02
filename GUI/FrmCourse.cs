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
    public partial class FrmCourse : Form
    {
        bool flagAdd = false;
        bool flagUpdate = false;
        bool flageOK = false;
        CourseTime c;
        CourseTime ct1;
        CourseTimeDB cdb;
        LessonKindDB ldb;
        Teachers t;
        TeachersDB tdb;
        CourseSubscriptionDB csdb;
        CourseSubscription cs;
        LessonKind l;
        FormProject fp;
        AddLessonKind alk;

        public FrmCourse(FormProject f, AddLessonKind a, string code)
        {
            InitializeComponent();
            DoToolTip(); 
            t = new Teachers();
            c = new CourseTime();
            cdb = new CourseTimeDB();
            tdb = new TeachersDB();
            ldb = new LessonKindDB();
            csdb = new CourseSubscriptionDB();
            cs = new CourseSubscription();
            l = new LessonKind();
            fp = f;
            alk = a;
            label4.Visible = false;
            txtTeacher.Visible = false;
            txtKind.Visible = false;
            textBox4.Visible = false;
            cmbSex.Visible = false;
            textBox1.Text = code;
            l = ldb.Find(Convert.ToInt32(code));
            textBox2.Text = l.Kind;
            string[] arr = { "ראשון", "שני", "שלישי", "רביעי", "חמישי" };
            string[] arr1 = { "9.00", "10.00", "11.00", "12.00", "13.00", "14.00", "20.00", "21.00", "22.00", "23.00" };
            for (int i = 0; i < 5; i++)
                cmbDay.Items.Add(arr[i]);
            for (int i = 0; i < arr1.Length; i++)
                cmbHour.Items.Add(arr1[i]);
            dataGridView1.DataSource = cdb.GetList().Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
            if(l.Audience == "גברים" || l.Audience == "בנים")
                cmbTeacher.DataSource = tdb.GetList().FindAll(x => x.TeacherSex == "זכר");
            if (l.Audience == "נשים" || l.Audience == "בנות")
                cmbTeacher.DataSource = tdb.GetList().FindAll(x => x.TeacherSex == "נקבה");
            NotPossible();
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(btnSave, "שמור שינויים");
            toolTip1.SetToolTip(cmbDay, "בחר יום");
            toolTip1.SetToolTip(cmbHour, "בחר שעה");
            toolTip1.SetToolTip(cmbTeacher, "בחר מורה");
            toolTip1.SetToolTip(btnUpdate, "עדכן קורס קיים");
            toolTip1.SetToolTip(btnEarese, "מחק קורס קיים");
            toolTip1.SetToolTip(btnAdd, "הוסף קורס חדש");
            toolTip1.SetToolTip(button1, "סגור טופס");
            toolTip1.SetToolTip(numericUpDown1, "בחר מספר שיעורים");
            toolTip1.SetToolTip(btnAddTeacher, "הוסף מורה חדש");
            toolTip1.SetToolTip(btnCanael, "בטל מחיקה אחרונה");
            toolTip1.SetToolTip(label17, "חזור לדף הקודם");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            flagAdd = true;
            ClearText();
            Possible();
        }
        private bool CreateFieled(CourseTime c)
        {
            CourseTime c1 = new CourseTime();
            flageOK = true;
            c1 = cdb.GetList().FindAll(x => ((Teachers)cmbTeacher.SelectedItem).TeacherId == x.TeacherId).ToList().FindAll(x => x.Day == cmbDay.Text).ToList().Find(x => x.Hour == Convert.ToDouble(cmbHour.SelectedItem));
            if (c1 != null)
            {
                MessageBox.Show("למורה " + cmbTeacher.Text + " \nקיים כבר קורס קורס ביום: " + cmbDay.Text + " \nבשעה: " + cmbHour.Text + " ועל כן לא ניתן\n לקבוע עבורו קורס נוסף!", "קורס",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                return false;
            }                
            c.NumberOfLesson = Convert.ToInt32(numericUpDown1.Value);
            try
            {
                if (cmbDay.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Day = cmbDay.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }           
            try
            {
                if (cmbHour.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Hour = Convert.ToDouble(cmbHour.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            c.Code = Convert.ToInt32(textBox1.Text);
            try
            {
                if (cmbTeacher.Text == "")
                    throw new Exception("שדה חובה");
                 else
                    c.TeacherId = ((Teachers)cmbTeacher.SelectedItem).TeacherId; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }          
            return flageOK;
        }

        private void NotPossible()
        {
            cmbDay.Enabled = false;
            cmbHour.Enabled = false;
            cmbTeacher.Enabled = false;
            btnAddTeacher.Enabled = false;
            numericUpDown1.Enabled = false;
        }
        private void Possible()
        {
            cmbDay.Enabled = true;
            cmbHour.Enabled = true;
            cmbTeacher.Enabled = true;
            btnAddTeacher.Enabled = true;
            numericUpDown1.Enabled = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flagAdd)
            {
                if (CreateFieled(c))
                {
                    c.SerialNumber = cdb.GetNextKey(Convert.ToInt32(textBox1.Text));
                    DialogResult r = MessageBox.Show("האם להוסיף קורס זה?", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        cdb.AddNew(c);
                        cdb.UpdateRow(c);
                        NotPossible();
                        ClearText();
                    }
                }
            }
            else
            {
                if (flagUpdate)
                {
                    if (CreateFieled(c))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן לקוח זה?", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            cdb.UpdateRow(c);
                            NotPossible();
                            ClearText();
                        }
                    }
                }
            }
            dataGridView1.DataSource = cdb.GetList().Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
        }

        public void ClearText()
        {
            cmbDay.Text = "";
            cmbHour.Text = "";
            cmbTeacher.Text = "";
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            flagUpdate = true;
            flagAdd = false;
            Possible();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int tz = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                c = cdb.Find(tz);
                Fill(c);
            }
        }
        private void Fill(CourseTime c)
        {
            cmbDay.Text = c.Day;
            cmbHour.Text = c.Hour.ToString();
            cmbTeacher.Text = c.TeacherId;
            numericUpDown1.Value = c.NumberOfLesson;
        }

        private void btnEarese_Click(object sender, EventArgs e)
        {
            Attendance a = new Attendance();
            AttendanceDB adb = new AttendanceDB();
            int serialN = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            cs = csdb.Find2(serialN, code);
            if (cs != null)
            {
                DialogResult r = MessageBox.Show("לקורס זה רשומים תלמידים, לא ניתן למחוק אותו", "מחיקת קורס", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            ct1 = new CourseTime();
            c = cdb.Find(serialN);
            ct1 = new CourseTime(c);
            cdb.DeleteRow(serialN, code);
            cdb.Update();
            dataGridView1.DataSource = cdb.GetList().Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
        }
        private void lblBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            alk.Close();
            fp.Show();
            fp.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddTeacher_Click_1(object sender, EventArgs e)
        {
            FrmTeachers ft = new FrmTeachers(fp, this);
            ft.ShowDialog();
            cmbTeacher.DataSource = tdb.GetList();
            cmbTeacher.Update();
        }

        private void btnCanael_Click(object sender, EventArgs e)
        {
            if(ct1 != null)
            {
                cdb.AddNew(ct1);
                cdb.UpdateRow(ct1);
                dataGridView1.DataSource = cdb.GetList().Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbTeacher_CheckedChanged(object sender, EventArgs e)
        {
            if(rbTeacher.Checked)
            {
                label4.Visible = true;
                label4.Text = "הכנס שם מורה לחיפוש קורסים";
                txtTeacher.Visible = true;
                txtKind.Visible = false;
                textBox4.Visible = false;
                cmbSex.Visible = false;
            }
        }

        private void rbKind_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKind.Checked)
            {
                label4.Visible = true;
                label4.Text = "הכנס סוג שיעור לחיפוש קורסים עבור אותו סוג שיעור";
                txtTeacher.Visible = false;
                txtKind.Visible = true;
                textBox4.Visible = false;
                cmbSex.Visible = false;
            }
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDay.Checked)
            {
                label4.Visible = true;
                label4.Text = "הכנס יום לחיפוש קורסים של אותו יום";
                txtTeacher.Visible = false;
                txtKind.Visible = false;
                textBox4.Visible = true;
                cmbSex.Visible = false;
            }
        }

        private void rbSearchSex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSearchSex.Checked)
            {
                label4.Visible = true;
                label4.Text = "בחר מין לחיפוש קורסים עבור אותו מין";
                txtTeacher.Visible = false;
                txtKind.Visible = false;
                textBox4.Visible = false;
                cmbSex.Visible = true;
            }
        }

        private void txtTeacher_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = cdb.GetList().FindAll(x => x.ThisTeacher().TeacherPrivateName.Contains(txtTeacher.Text)).Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = cdb.GetList().FindAll(x => x.Day.Contains(textBox4.Text)).Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
        }

        private void txtKind_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = cdb.GetList().FindAll(x => x.ThisLessonKind().Kind.Contains(txtKind.Text)).Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
        }

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSex.SelectedIndex == 0)
            {
                dataGridView1.DataSource = cdb.GetList().FindAll(x => x.ThisLessonKind().Audience == "בנים" || x.ThisLessonKind().Audience == "גברים").Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();
            }
            else
            {
                dataGridView1.DataSource = cdb.GetList().FindAll(x => x.ThisLessonKind().Audience == "בנות" || x.ThisLessonKind().Audience == "נשים").Select(x => new { מספר_סידורי = x.SerialNumber, קוד = x.Code, יום = x.Day, שעה = x.Hour, מורה = x.ThisTeacher(), סוג = x.ThisLessonKind().Kind, מספר_שיעורים = x.NumberOfLesson }).ToList();            
            }
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            FrmTeachers f = new FrmTeachers(fp, this);
            f.ShowDialog();
        }
    }
}
