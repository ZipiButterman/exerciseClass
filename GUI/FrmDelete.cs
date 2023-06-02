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
    public partial class FrmDelete : Form
    {
        CourseSubscription cs;
        CourseSubscriptionDB csdb;
        Attendance a;
        AttendanceDB adb;
        Subscribers s;
        SubscribersDB sdb;
        FormProject fp;
        FrmSubscriber fs;

        public FrmDelete(FormProject f1,FrmSubscriber f2,string id)
        {
            InitializeComponent();
            DoToolTip();
            cs = new CourseSubscription();
            csdb = new CourseSubscriptionDB();
            a = new Attendance();
            adb = new AttendanceDB();
            s = new Subscribers();
            sdb = new SubscribersDB();
            fp = f1;
            fs = f2;
            textBox1.Text = id;
            dataGridView1.DataSource = csdb.GetList().FindAll(x => x.StudentId == id).Select(x => new {קוד_קורס = x.CourseCode, מספר_סידורי = x.SerialNumber, שם_קורס = x.ThisCourse().Kind }).ToList();
        }
        
        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(label17, "חזרה לטופס הקודם");
            toolTip1.SetToolTip(button1, "סגור טופס");
        }

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            fs.Show();
            fs.Activate();
        }

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            fs.Close();
            this.Close();
            fp.Show();
            fp.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                cs = csdb.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                adb.DeleteRow1(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value), textBox1.Text);
                DialogResult r = new DialogResult();
                r = MessageBox.Show("האם למחוק קורס?", "מחיקת קורס", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    csdb.DeleteRow(textBox1.Text, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    csdb.Update();
                    s = sdb.Find(cs.ThisStudent().StudentId);
                    s.StudentDebt -= cs.ThisCourse().QuarterlyPrice * 4;
                    sdb.UpdateRow(s);
                    dataGridView1.DataSource = csdb.GetList().FindAll(x => x.StudentId == textBox1.Text).Select(x => new { קוד_קורס = x.CourseCode, מספר_סידורי = x.SerialNumber, שם_קורס = x.ThisCourse().Kind }).ToList();
                    if (count > 1)
                    {
                        r = MessageBox.Show("האם למחוק קורס נוסף?", "מחיקת קורס", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.No)
                        {
                            this.Close();
                        }
                    }
                    else
                        this.Close();
                }
            }
        }
    }
}
