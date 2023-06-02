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
    public partial class FrmAttendSubscriberCourse : Form
    {
        CourseTime ct;
        CourseTimeDB ctdb;
        CourseSubscription cs;
        CourseSubscriptionDB csdb;
        Attendance a;
        AttendanceDB adb;
        FrmAttend fa;
        FormProject fp;
        public FrmAttendSubscriberCourse(FormProject f,FrmAttend f1, string id)
        {
            InitializeComponent();
            DoToolTip();       
            fa = f1;
            fp = f;
            textBox1.Text = id;
            ct = new CourseTime();
            ctdb = new CourseTimeDB();
            cs = new CourseSubscription();
            csdb = new CourseSubscriptionDB();
            a = new Attendance();   
            adb = new AttendanceDB();   
            dataGridView1.DataSource = csdb.GetList().FindAll(x => x.StudentId == id).Select(x => new {קוד_קורס = x.CourseCode, מספר_סידורי = x.SerialNumber,קורס = x.ThisCourse().Kind}).ToList();
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(btnClose, "סגור טופס");
            toolTip1.SetToolTip(btnDisplay, "הצג נוכחות עבור המנוי הנבחר לקורס הנבחר");
            toolTip1.SetToolTip(label17, "חזרה לטופס הקודם");
        }


        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                cs = csdb.GetList().FindAll(x => x.StudentId == textBox1.Text).Find(x => x.CourseCode == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value) && x.SerialNumber == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value));
                lbl1.Text = cs.AttendanceCourse.ToString();
                lbl2.Text = cs.EnrolledCourse.ToString();
            }
        }

        
        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            fa.Show();
            fa.Activate(); 
        }

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            fa.Close();
            this.Close();
            fp.Show();
            fp.Activate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
