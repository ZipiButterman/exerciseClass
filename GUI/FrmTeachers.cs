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
    public partial class FrmTeachers : Form
    {
        Teachers t;
        Teachers t2;
        TeachersDB tdb;
        CityDB cdb;
        CourseTime ct;
        CourseTimeDB ctdb;
        FormProject f;
        FrmCourse fc;
        bool flagAdd = false;
        bool flagUpdate = false;
        bool flagError = false;
        bool flageOK = false;

        public FrmTeachers(FormProject fp, FrmCourse fc2)
        {
            InitializeComponent();
            DoToolTip();
            t = new Teachers();
            tdb = new TeachersDB();
            cdb = new CityDB();
            ct = new CourseTime();
            ctdb = new CourseTimeDB();
            fc = fc2;
            f = fp;
            if (fc != null)
                label17.Visible = true;
            string[] arr = { "050", "052", "053", "054", "055", "057", "058" };
            for (int i = 0; i < 7; i++)
                cmbNumPhone.Items.Add(arr[i]);
            dataGridView1.DataSource = tdb.GetList().Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            cmbCity.DataSource = cdb.GetList();
            NotPossible();
            NotVisible();
        }
        private void ClearText()
        {
            txtCity.Text = "";
            txtFamilyName.Text = "";
            txtId.Text = "";
            txtMail.Text = "";
            txtNumber.Text = "";
            txtNumHouse.Text = "";
            txtPrivateName.Text = "";
            cmbSex.Text = "";
            cmbCity.Text = "";
            cmbNumPhone.Text = "";
        }
        public void NotVisible()
        {
            textBox1.Visible = false;
            fmaily.Visible = false;
            city.Visible = false;
            id.Visible = false;
            label11.Visible = false;
            cmbSearchSex.Visible = false;
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(txtId, "הכנס ת\"ז");
            toolTip1.SetToolTip(txtPrivateName, "הכנס שם פרטי");
            toolTip1.SetToolTip(txtFamilyName, "הכנס שם משפחה");
            toolTip1.SetToolTip(cmbCity, "בחר עיר");
            toolTip1.SetToolTip(cmbSex, "בחר מין");
            toolTip1.SetToolTip(txtCity, "הכנס רחוב");
            toolTip1.SetToolTip(txtNumHouse, "הכנס מספר בית");
            toolTip1.SetToolTip(button1, "סגור טופס");
            toolTip1.SetToolTip(txtNumber, "הכנס מספר נייד");
            toolTip1.SetToolTip(cmbNumPhone, "בחר קידומת");
            toolTip1.SetToolTip(txtMail, "הכנס כתובת מייל");
            toolTip1.SetToolTip(btnAdd, "הוסף מנוי חדש");
            toolTip1.SetToolTip(btnAddCity, "הוסף עיר חדשה");
            toolTip1.SetToolTip(button2, "בטל מחיקה אחרונה");
            toolTip1.SetToolTip(btnEarese, "מחק מנוי נבחר");
            toolTip1.SetToolTip(btnSave, "שמור שינויים");
            toolTip1.SetToolTip(btnUpdate, "עדכן מנוי נבחר");
            toolTip1.SetToolTip(textBox1, "הכנס שם פרטי לחיפוש");
            toolTip1.SetToolTip(city, "הכנס עיר לחיפוש");
            toolTip1.SetToolTip(id, "הכנס ת\"ז לחיפוש");
            toolTip1.SetToolTip(fmaily, "הכנס שם משפחה לחיפוש");
            toolTip1.SetToolTip(rbSName, "חפש מנוי לפי שם משפחה");
            toolTip1.SetToolTip(rbId, "חפש מנוי לפי ת\"ז");
            toolTip1.SetToolTip(rbFName, "חפש מנוי לפי שם פרטי");
            toolTip1.SetToolTip(rbCity, "חפש מנוי לפי עיר");
            toolTip1.SetToolTip(rbSearchSex, "חפש מנוי לפי מין");
            toolTip1.SetToolTip(cmbSearchSex, "בחר מין לחיפוש");
        }

        private void NotPossible()
        {
            label1.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;
            txtId.Enabled = false;
            txtPrivateName.Enabled = false;
            txtFamilyName.Enabled = false;
            txtCity.Enabled = false;
            txtNumHouse.Enabled = false;
            txtNumber.Enabled = false;
            txtMail.Enabled = false;
            cmbSex.Enabled = false;
            cmbCity.Enabled = false;
            cmbNumPhone.Enabled = false;
        }
        private void Possible()
        {
            label1.Visible = true;
            panel2.Visible = true;
            panel1.Visible = true;
            txtId.Enabled = true;
            txtPrivateName.Enabled = true;
            txtFamilyName.Enabled = true;
            txtCity.Enabled = true;
            txtNumHouse.Enabled = true;
            txtNumber.Enabled = true;
            txtMail.Enabled = true;
            cmbSex.Enabled = true;
            cmbCity.Enabled = true;
            cmbNumPhone.Enabled = true;
        }

        private void Fill(Teachers t)
        {
            string str = t.TeacherPhoneNumber;
            string str1 = str.Substring(0,3);
            string str2 = str.Substring(3, 7);
            txtPrivateName.Text = t.TeacherPrivateName;
            txtFamilyName.Text = t.TeacherFamilyName;
            txtId.Text = t.TeacherId;
            txtNumber.Text = str2;
            cmbNumPhone.Text = str1;
            cmbCity.Text = t.ThisCity().CityName;
            txtNumHouse.Text = t.TeacherNumHouse;
            txtMail.Text = t.TeacherMail;
            txtCity.Text = t.TeacherStreet;
            cmbSex.Text = t.TeacherSex;
        }

        private bool CreateFieled(Teachers t)
        {
            flageOK = true;
            string str = cmbNumPhone.Text;
            string str1 = txtNumber.Text;
            string str2 = str + str1;
            t.TeacherSex = cmbSex.Text;
            try
            {
                if (txtId.Text == "")
                    throw new Exception("יש להכניס ת\"ז");
                else
                    t.TeacherId = txtId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            try
            {
                if (txtPrivateName.Text == "")
                    throw new Exception("יש להכניס שם פרטי");
                else
                    t.TeacherPrivateName = txtPrivateName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            try
            {
                if (txtFamilyName.Text == "")
                    throw new Exception("יש להכניס שם משפחה");
                else
                    t.TeacherFamilyName = txtFamilyName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (cmbCity.Text == "")
                    throw new Exception("יש לבחור עיר מהרשימה");
                else
                    t.TeacherCity = ((City)cmbCity.SelectedItem).CityCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtCity.Text == "")
                    throw new Exception("יש להכניס רחוב");
                else
                    t.TeacherStreet = txtCity.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtNumHouse.Text == "")
                    throw new Exception("יש להכניס מספר בית");
                else
                    t.TeacherNumHouse = txtNumHouse.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtNumber.Text == "")
                    throw new Exception("יש להכניס מספר פלאפון");
                else
                    t.TeacherPhoneNumber = str2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtMail.Text == "")
                    throw new Exception("יש להכניס כתובת מייל");
                else
                    t.TeacherMail = txtMail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (cmbSex.Text == "")
                    throw new Exception("יש לבחור מין מהרשימה");
                else
                    t.TeacherSex = cmbSex.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }           
            return flageOK;
        }
        

        private  void btnAddCity_Click(object sender, EventArgs e)
        {
           FrmCity fc = new FrmCity(f,null,this);
           fc.ShowDialog();
           cmbCity.DataSource = cdb.GetList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            flagUpdate = true;
            flagAdd = false;
            Possible();
            ClearText();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string tz = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                t = tdb.Find(tz);
                Fill(t);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            flagAdd = true;
            ClearText();
            Possible();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Teachers t1 = new Teachers();
            t1 = tdb.Find(txtId.Text);
            if (flagError)
                return;
            if (flagAdd)
            {
                if (t1 != null)
                {
                    MessageBox.Show("מורה זה כבר קיים", "מורה קיים", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (CreateFieled(t))
                {
                    DialogResult r = MessageBox.Show("האם להוסיף מורה זה?", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        tdb.AddNew(t);
                        tdb.UpdateRow(t);
                        NotPossible();
                        ClearText();
                    }
                }
            }
            else
            {
                if (flagUpdate)
                {
                    if (CreateFieled(t))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן מורה זה?", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tdb.UpdateRow(t);
                            ClearText();
                            NotPossible();
                        }
                    }
                }
            }         
            dataGridView1.DataSource = tdb.GetList().Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void btnEarese_Click(object sender, EventArgs e)
        {
            t2 = new Teachers();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                ct = ctdb.GetList().Find(x => x.TeacherId == id);
                if (ct != null)
                {
                    DialogResult r = MessageBox.Show("ישנם קורסים המועברים על ידי מורה זה, לא ניתן למחוק אותו", "מחיקת מורה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                } 
                t = tdb.Find(id);
                t2 = new Teachers(t);
                tdb.DeleteRow(id);
                dataGridView1.DataSource = tdb.GetList().Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
        }

      
        private void lblBack_Click(object sender, EventArgs e)
        {
            f.Show();
            f.Activate();
            this.Close();
            if(fc != null)
                fc.Close();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            if(fc != null)
            {
                fc.Show();
                fc.Activate(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (t2 != null)
            {
                tdb.AddNew(t2);
                tdb.Update();
                dataGridView1.DataSource = tdb.GetList().Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
        }

        

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagError = false;
            if (cmbCity.SelectedIndex < 0)
            {
                lblCity.Text = "'חובה לבחור עיר מהרשימה או להוסיף עיר בלחצן 'הוסף עיר";
                flagError = true;
            }
        }

        

        private void cmbNumPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagError = false;
            if (cmbNumPhone.SelectedIndex < 0)
            {
                lblPhone.Text = "חובה לבחור קידומת לפלאפון";
                flagError = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tdb.GetList().FindAll(x => x.TeacherPrivateName.Contains(textBox1.Text)).Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void rbFName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFName.Checked)
            {
                label11.Visible = true;
                city.Visible = false;
                id.Visible = false;
                fmaily.Visible = false;
                textBox1.Visible = true;
                cmbSearchSex.Visible = false;

                label11.Text = "הכנס שם פרטי לחיפוש מורה";
            }
        }

        private void rbSName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSName.Checked)
            {
                label11.Visible = true;
                city.Visible = false;
                id.Visible = false;
                textBox1.Visible = true;
                label11.Text = "הכנס שם משפחה לחיפוש מורה";
                fmaily.Visible = true;
                cmbSearchSex.Visible = false;

            }
        }

        private void rbId_CheckedChanged(object sender, EventArgs e)
        {
            if (rbId.Checked)
            {
                label11.Visible = true;
                city.Visible = false;
                fmaily.Visible = false;
                textBox1.Visible = true;
                id.Visible = true;
                cmbSearchSex.Visible = false;
                label11.Text = "הכנס ת\"ז לחיפוש מורה";

            }
        }

        private void rbCity_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCity.Checked)
            {
                label11.Visible = true;
                id.Visible = false;
                fmaily.Visible = false;
                textBox1.Visible = true;
                label11.Text = "הכנס עיר לחיפוש מורה";
                city.Visible = true;
                cmbSearchSex.Visible = false;
            }
        }

        private void city_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tdb.GetList().FindAll(x => x.ThisCity().CityName.Contains(city.Text)).Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tdb.GetList().FindAll(x => x.TeacherId.Contains(id.Text)).Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void fmaily_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tdb.GetList().FindAll(x => x.TeacherFamilyName.Contains(fmaily.Text)).Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void rbSearchSex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSearchSex.Checked)
            {
                label11.Visible = true;
                textBox1.Visible = false;
                id.Visible = false;
                fmaily.Visible = false;
                cmbSearchSex.Visible = true;
                label11.Text = "בחר מין לחיפוש מנוי";
                city.Visible = true;
            }
        }

        private void cmbSearchSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchSex.SelectedIndex == 0)
            {
                dataGridView1.DataSource = tdb.GetList().FindAll(x => x.TeacherSex == "זכר").Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
            else
            {
                dataGridView1.DataSource = tdb.GetList().FindAll(x => x.TeacherSex == "נקבה").Select(x => new { תז = x.TeacherId, שם_פרטי = x.TeacherPrivateName, שם_משפחה = x.TeacherFamilyName, רחוב = x.TeacherStreet, מספר_בית = x.TeacherNumHouse, טלפון = x.TeacherPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
        }

        private void txtId_TextChanged_1(object sender, EventArgs e)
        {
            txtId.BackColor = Color.White;
            lblId.Text = "";
            flagError = false;
            if (!Validation.IsNum(txtId.Text))
            {
                flagError = true;
                txtId.BackColor = Color.Lavender;
                lblId.Text = "יש להכניס ספרות בלבד";
                txtId.Focus();
            }
        }

        private void txtPrivateName_TextChanged_1(object sender, EventArgs e)
        {
            txtPrivateName.BackColor = Color.White;
            lblAName.Text = "";
            flagError = false;
            if (!Validation.IsHebrew(txtPrivateName.Text) && !Validation.IsEnglish(txtPrivateName.Text))
            {
                flagError = true;
                txtPrivateName.BackColor = Color.Lavender;
                lblAName.Text = "יש להכניס אותיות בלבד";
                txtPrivateName.Focus();
            }
        }

        private void txtFamilyName_TextChanged_1(object sender, EventArgs e)
        {
            flagError = false;
            txtFamilyName.BackColor = Color.White;
            lblBName.Text = "";
            if (!Validation.IsHebrew(txtFamilyName.Text) && !Validation.IsEnglish(txtFamilyName.Text))
            {
                flagError = true;
                txtFamilyName.BackColor = Color.Lavender;
                lblBName.Text = "יש להכניס אותיות בלבד";
                txtFamilyName.Focus();
            }
        }

        private void txtCity_TextChanged_1(object sender, EventArgs e)
        {
            flagError = false;
            txtCity.BackColor = Color.White;
            lblStreet.Text = "";

            if (!Validation.IsHebrew(txtCity.Text) && !Validation.IsEnglish(txtCity.Text))
            {
                flagError = true;
                txtCity.BackColor = Color.Lavender;
                lblStreet.Text = "יש להכניס אותיות בלבד";
                txtCity.Focus();
            }
        }

        private void txtNumHouse_TextChanged_1(object sender, EventArgs e)
        {
            flagError = false;
            txtNumHouse.BackColor = Color.White;
            lblHome.Text = "";

            if (!Validation.IsNum(txtNumHouse.Text))
            {
                flagError = true;
                txtNumHouse.BackColor = Color.Lavender;
                lblHome.Text = "יש להכניס ספרות בלבד";
                txtNumHouse.Focus();
            }
        }

        private void txtNumber_TextChanged_1(object sender, EventArgs e)
        {
            flagError = false;
            txtNumber.BackColor = Color.White;
            lblPhone.Text = "";

            if (!Validation.IsNum(txtNumber.Text))
            {
                flagError = true;
                txtNumber.BackColor = Color.Lavender;
                lblPhone.Text = "יש להכניס ספרות בלבד";
                txtNumber.Focus();
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Text.Length != 9 && txtId.Text != "")
            {
                flagError = true;
                txtId.BackColor = Color.Lavender;
                lblId.Text = "ת''ז חייבת להכיל 9 ספרות";
                txtId.Focus();
            }
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            if (txtNumber.Text.Length != 7 && txtNumber.Text != "")
            {
                flagError = true;
                txtNumber.BackColor = Color.Lavender;
                lblPhone.Text = "מספר פלאפון חייב להכיל 10 ספרות";
                txtNumber.Focus();
            }
        }
    }
}
