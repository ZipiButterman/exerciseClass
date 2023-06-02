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
    public partial class FrmSubscriber : Form
    {
        Subscribers s;
        SubscribersDB sdb;
        Subscribers s2;
        CourseSubscription cs;
        CourseSubscriptionDB csdb;
        CityDB cdb;
        City c;
        FormProject fp;
        bool flagAdd = false;
        bool flagUpdate = false;
        bool flagError = false;
        bool flageOK = false;
        public FrmSubscriber(FormProject f)
        {
            InitializeComponent();
            DoToolTip();
            s = new Subscribers();
            sdb = new SubscribersDB();
            cs = new CourseSubscription();
            csdb = new CourseSubscriptionDB();
            cdb = new CityDB();
            c = new City();
            fp = f;
            string[] arr = { "050", "052", "053", "054", "055", "057", "058" };
            for (int i = 0; i < 7; i++)
                cmbNumPhone.Items.Add(arr[i]);
            cmbCity.DataSource = cdb.GetList();
            cmbSex.SelectedIndex = 0;
            dataGridView1.DataSource = sdb.GetList().Select(x => new { תז=x.StudentId, שם_פרטי=x.StudentPrivateName, שם_משפחה=x.StudentFamilyName, רחוב=x.StudentStreet, מספר_בית=x.StudentNumHouse, טלפון=x.StudentPhoneNumber, עיר=x.ThisCity().CityName }).ToList();
            NotPossible();
            NotVisible();
        }

        public void NotVisible()
        {
            cmbSearchSex.Visible = false;
            label13.Visible = false;
            firstn.Visible = false;
            city.Visible = false;
            id.Visible = false;
            family.Visible = false;
        }
        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(txtId, "הכנס ת\"ז");
            toolTip1.SetToolTip(txtPrivateName, "הכנס שם פרטי");
            toolTip1.SetToolTip(txtFamilyName, "הכנס שם משפחה");
            toolTip1.SetToolTip(cmbCity, "בחר עיר");
            toolTip1.SetToolTip(txtCity, "הכנס רחוב");
            toolTip1.SetToolTip(txtNumHouse, "הכנס מספר בית");
            toolTip1.SetToolTip(txtNumber, "הכנס מספר נייד");
            toolTip1.SetToolTip(cmbNumPhone, "בחר קידומת");
            toolTip1.SetToolTip(dateTimePicker1, "בחר תאריך לידה");
            toolTip1.SetToolTip(cmbSex, "בחר מין");
            toolTip1.SetToolTip(txtMail, "הכנס כתובת מייל");
            toolTip1.SetToolTip(txtDebt, "חוב המנוי");
            toolTip1.SetToolTip(button1, "סגור טופס");
            toolTip1.SetToolTip(btnAdd, "הוסף מנוי חדש");
            toolTip1.SetToolTip(btnSave, "שמור שינויים");
            toolTip1.SetToolTip(btnUpdate, "עדכן מנוי נבחר");
            toolTip1.SetToolTip(btnAddCity, "הוסף עיר חדשה");
            toolTip1.SetToolTip(btnAddCourse, "רשום מנוי נבחר לקורס");
            toolTip1.SetToolTip(btnCancelErease, "בטל מחיקה אחרונה");
            toolTip1.SetToolTip(btnEarese, "מחק מנוי נבחר");
            toolTip1.SetToolTip(btnErease, "הוצא מנוי נבחר מקורס");
            toolTip1.SetToolTip(btnPay, "גש לדף התשלום");
            toolTip1.SetToolTip(firstn, "הכנס שם פרטי לחיפוש");
            toolTip1.SetToolTip(city, "הכנס עיר לחיפוש");
            toolTip1.SetToolTip(id, "הכנס ת\"ז לחיפוש");
            toolTip1.SetToolTip(family, "הכנס שם משפחה לחיפוש");
            toolTip1.SetToolTip(rbSName, "חפש מנוי לפי שם משפחה");
            toolTip1.SetToolTip(rbId, "חפש מנוי לפי ת\"ז");
            toolTip1.SetToolTip(rbFName, "חפש מנוי לפי שם פרטי");
            toolTip1.SetToolTip(rbCity, "חפש מנוי לפי עיר");
            toolTip1.SetToolTip(rbSex, "חפש מנוי לפי מין");
            toolTip1.SetToolTip(cmbSearchSex, "בחר מין לחיפוש");
        }

        private void NotPossible()
        {
            txtId.Enabled = false;
            txtPrivateName.Enabled = false;
            txtFamilyName.Enabled = false;
            txtCity.Enabled = false;
            txtNumHouse.Enabled = false;
            txtNumber.Enabled = false;
            txtMail.Enabled = false;
            cmbCity.Enabled = false;
            dateTimePicker1.Enabled = false;
            btnAddCity.Enabled = false;
            cmbNumPhone.Enabled = false;
            btnPay.Visible = false;
            cmbSex.Enabled = false;
        }
        private void Possible()
        {            
            txtId.Enabled = true;
            txtPrivateName.Enabled = true;
            txtFamilyName.Enabled = true;
            txtCity.Enabled = true;
            txtNumHouse.Enabled = true;
            txtNumber.Enabled = true;
            txtMail.Enabled = true;
            cmbCity.Enabled = true;
            dateTimePicker1.Enabled = true;
            btnAddCity.Enabled = true;
            cmbNumPhone.Enabled = true;
            cmbSex.Enabled = true;
        }

        private void Fill(Subscribers s)
        {
            string str = s.StudentPhoneNumber;
            string str1 = str.Substring(0, 3);
            string str2 = str.Substring(3, 7);
            txtId.Text = s.StudentId;
            txtPrivateName.Text = s.StudentPrivateName;
            txtFamilyName.Text = s.StudentFamilyName;
            txtNumber.Text = str2;
            cmbNumPhone.Text = str1;
            cmbCity.Text = s.ThisCity().CityName;
            txtNumHouse.Text = s.StudentNumHouse;
            txtMail.Text = s.StudentMail;
            txtDebt.Text = s.StudentDebt.ToString();
            cmbSex.Text = s.StudentSex;
            dateTimePicker1.Value = s.StudentDateOfBirth;
            txtCity.Text = s.StudentStreet;
        }

        private bool CreateFieled(Subscribers s)
        {
            flageOK = true;
            string str = cmbNumPhone.Text;
            string str1 = txtNumber.Text;
            string str2 = str + str1;
            //Subscribers s1 = new Subscribers();
            //CourseSubscription 
             
            try
            {
                if (txtId.Text == "")
                    throw new Exception("יש להכניס ת\"ז");
                else
                    s.StudentId = txtId.Text;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            try
            {
                if (txtPrivateName.Text == "")
                    throw new Exception("יש להכניס שם פרטי");
                else
                    s.StudentPrivateName = txtPrivateName.Text;
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
                    s.StudentFamilyName = txtFamilyName.Text;
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
                    s.StudentCity = ((City)cmbCity.SelectedItem).CityCode;
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
                    s.StudentStreet = txtCity.Text;

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
                    s.StudentNumHouse = txtNumHouse.Text;

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
                    s.StudentPhoneNumber = str2;
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
                    s.StudentMail = txtMail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            s.StudentDebt = 0;
            try
            {
                if (cmbSex.Text == "")
                    throw new Exception("יש לבחור מין מהרשימה");
                else
                    s.StudentSex = cmbSex.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            try
            {
                if (dateTimePicker1.Text == "")
                    throw new Exception("יש לבחור תאריך לידה");
                else
                    s.StudentDateOfBirth = Convert.ToDateTime(dateTimePicker1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                return false;
            }
            return flageOK;
        }
        private void btnAddCity_Click(object sender, EventArgs e)
        {
            FrmCity fc = new FrmCity(fp,this,null);
            fc.ShowDialog();
            cdb.Update();
            cmbCity.DataSource = cdb.GetList();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            flagUpdate = true;
            flagAdd = false;
            Possible();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string tz = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                s = sdb.Find(tz);
                Fill(s);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            Subscribers s1 = new Subscribers();
            s1 = sdb.Find(txtId.Text);
            
            if (flagError)
            {
                MessageBox.Show("אחד או יותר מהערכים שהכנסת שגוי", "שגיאת ערכים", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);//, );
                return;
            }
            if (flagAdd)
            {
                if (s1 != null)
                {
                    MessageBox.Show("מנוי זה כבר רשום", "מנוי קיים", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);//, );
                    return;
                }
                if (CreateFieled(s))
                {
                    DialogResult r1 = MessageBox.Show("האם להוסיף לקוח זה?", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r1 == DialogResult.Yes)
                    {
                        sdb.AddNew(s);
                        ClearText();
                        NotPossible();
                    }
                }
            }
            else
            {
                if (flagUpdate)
                {
                    if (CreateFieled(s))
                    {
                        DialogResult r1 = MessageBox.Show("האם לעדכן לקוח זה?", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r1 == DialogResult.Yes)
                        {
                            sdb.UpdateRow(s);
                            NotPossible();
                            ClearText();
                        }
                    }
               }
            }
            dataGridView1.DataSource = sdb.GetList().Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearText();
            Possible();
            flagAdd = true;
            flagUpdate = false;
            panel2.Visible = true;
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            FrmPayments fp1 = new FrmPayments(fp,this,Convert.ToDouble(txtDebt.Text), txtId.Text);
            fp1.ShowDialog();
            dataGridView1.DataSource = sdb.GetList().Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void btnEarese_Click(object sender, EventArgs e)
        {
            s2 = new Subscribers();      
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                cs = csdb.Find(id);
                if(cs != null)
                {
                    DialogResult r = MessageBox.Show("מנוי זה רשום לחוג, לא ניתן למחוק אותו", "מחיקת מנוי", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                s = sdb.Find(id);
                s2 = new Subscribers(s);
                sdb.DeleteRow(id);
                dataGridView1.DataSource = sdb.GetList().Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            flagUpdate = true;
            flagAdd = false;
            Possible();
            if (dataGridView1.SelectedRows.Count > 0)
            {                
                string tz = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                s = sdb.Find(tz);
                if(s.StudentDebt == 0)
                    btnPay.Visible = false;
                else
                    btnPay.Visible = true;  
                Fill(s);
            }
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                FrmCoursesList fcl = new FrmCoursesList(fp,this,dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                fcl.ShowDialog();
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
        
        private void txtPrivateName_TextChanged(object sender, EventArgs e)
        {
            txtPrivateName.BackColor = Color.White;
            lblAName.Text = "";
            flagError = false;
            if (!Validation.IsHebrew(txtPrivateName.Text) && !Validation.IsEnglish(txtPrivateName.Text))
            {
                flagError = true;
                txtPrivateName.BackColor = Color.Lavender;
                lblAName.Text = "יש להכניס אותיות בשפה אחת בלבד";
                txtPrivateName.Focus();
            }
        }


        private void txtFamilyName_TextChanged(object sender, EventArgs e)
        {
            flagError = false;
            txtFamilyName.BackColor = Color.White;
            lblBName.Text = "";
            if (!Validation.IsHebrew(txtFamilyName.Text) && !Validation.IsEnglish(txtFamilyName.Text))
            {
                flagError = true;
                txtFamilyName.BackColor = Color.Lavender;
                lblBName.Text = "יש להכניס אותיות בשפה אחת בלבד";
                txtFamilyName.Focus();
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

        private void txtCity_TextChanged(object sender, EventArgs e)
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
        private void txtNumHouse_TextChanged(object sender, EventArgs e)
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

        private void cmbNumPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            flagError = false;
            if (cmbNumPhone.SelectedIndex < 0)
            {
                lblPhone.Text = "חובה לבחור קידומת לפלאפון";
                flagError = true;
            }
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
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


        private void btnErease_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CourseSubscription ss = new CourseSubscription();
                CourseSubscriptionDB ssd = new CourseSubscriptionDB();
                ss = ssd.Find(id);
                if(ss == null)
                {
                    MessageBox.Show("מנוי זה לא רשום עדיין לקורסים", "אין קורסים",  MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
                    return;
                }
                FrmDelete fd = new FrmDelete(fp,this,dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                fd.ShowDialog();
            }
        }

    

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            fp.Show();
            fp.Activate();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelErease_Click(object sender, EventArgs e)
        {
            if (s2 != null)
            {
                sdb.AddNew(s2);
                sdb.Update();
                dataGridView1.DataSource = sdb.GetList().Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
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

        private void rbFName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFName.Checked)
            {
                label13.Visible = true;
                city.Visible = false;
                id.Visible = false;
                family.Visible = false;
                firstn.Visible = true;
                cmbSearchSex.Visible = false;
                label13.Text = "הכנס שם פרטי לחיפוש מנוי";
            }
        }

        private void rbSName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSName.Checked)
            {
                label13.Visible = true;
                firstn.Visible = false;
                city.Visible = false;
                id.Visible = false;
                cmbSearchSex.Visible = false;
                label13.Text = "הכנס שם משפחה לחיפוש מנוי";
                family.Visible = true;
            }
        }

        private void rbId_CheckedChanged(object sender, EventArgs e)
        {
            if (rbId.Checked)
            {
                label13.Visible = true;
                firstn.Visible = false;
                city.Visible = false;
                family.Visible = false;
                id.Visible = true;
                cmbSearchSex.Visible = false;
                label13.Text = "הכנס ת\"ז לחיפוש מנוי";
            }
        }

        private void rbCity_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCity.Checked)
            {
                label13.Visible = true;
                firstn.Visible = false;
                id.Visible = false;
                family.Visible = false;
                cmbSearchSex.Visible = false;
                label13.Text = "הכנס עיר לחיפוש מנוי";
                city.Visible = true;

            }
        }

        private void firstn_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sdb.GetList().FindAll(x => x.StudentPrivateName.Contains(firstn.Text)).Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sdb.GetList().FindAll(x => x.StudentId.Contains(id.Text)).Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void city_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sdb.GetList().FindAll(x => x.ThisCity().CityName.Contains(city.Text)).Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void family_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sdb.GetList().FindAll(x => x.StudentFamilyName.Contains(family.Text)).Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
        }

        private void rbSex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSex.Checked)
            {
                label13.Visible = true;
                firstn.Visible = false;
                id.Visible = false;
                family.Visible = false;
                cmbSearchSex.Visible = true;
                label13.Text = "בחר מין לחיפוש מנוי";
                city.Visible = true;
            }
        }

        private void cmbSearchSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSearchSex.SelectedIndex == 0)
            {
                dataGridView1.DataSource = sdb.GetList().FindAll(x => x.StudentSex == "זכר").Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
            }
            else
            {
                dataGridView1.DataSource = sdb.GetList().FindAll(x => x.StudentSex == "נקבה").Select(x => new { תז = x.StudentId, שם_פרטי = x.StudentPrivateName, שם_משפחה = x.StudentFamilyName, רחוב = x.StudentStreet, מספר_בית = x.StudentNumHouse, טלפון = x.StudentPhoneNumber, עיר = x.ThisCity().CityName }).ToList();
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
