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
    public partial class FrmCity : Form
    {
        CityDB cdb;
        City c;
        FrmSubscriber fs;
        FrmTeachers ft;
        FormProject fp;
        bool flagError = false;
        public FrmCity(FormProject f, FrmSubscriber f1, FrmTeachers f2)
        {
            InitializeComponent();
            DoToolTip();
            cdb = new CityDB();
            c = new City();
            fs = f1;
            ft = f2;
            fp = f;
            dataGridView1.DataSource = cdb.GetList().Select(x => new { קוד_עיר = x.CityCode, שם_עיר = x.CityName }).ToList();
        }
        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(button2, "סגור טופס");
            toolTip1.SetToolTip(button1, "הוסף עיר");
            toolTip1.SetToolTip(label17, "חזרה לטופס הקודם");
            toolTip1.SetToolTip(textBox1, "הכנס שם עיר");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("חובה להכניס שם עיר","שדה חובה",MessageBoxButtons.OK,MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if(flagError)
            {
                MessageBox.Show("הערך שהזנת לא תקין", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            City c1 = new City();
            c1 = cdb.GetList().Find(x => x.CityName == name);
            if(c1 != null)
            {
                MessageBox.Show("עיר זו כבר קיימת ברשימה", "אזהרה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                textBox1.Text = "";
                return;
            }
            c.CityName = name;
            c.CityCode = cdb.GetNextKey();
            cdb.AddNew(c);
            cdb.UpdateRow(c);
            dataGridView1.DataSource = cdb.GetList().Select(x => new { קוד_עיר = x.CityCode, שם_עיר = x.CityName }).ToList();
        }



        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            if (ft == null)
            {
                fs.Show();
                fs.Activate();
            }
            else
            {
                ft.Show();
                ft.Activate();
            }
        }

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            if (fs == null)
                ft.Close();
            else fs.Close();
            this.Close();
            fp.Show();
            fp.Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            lblCity.Text = "";
            flagError = false;
            if (!Validation.IsEnglish(textBox1.Text) && !Validation.IsHebrew(textBox1.Text))
            {
                flagError = true;
                textBox1.BackColor = Color.Lavender;
                lblCity.Text = "יש להכניס אותיות בלבד";
                textBox1.Focus();
            }
        }
    }
}
