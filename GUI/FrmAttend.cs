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
    public partial class FrmAttend : Form
    {
        Attendance a;
        AttendanceDB adb;
        CourseSubscription cs;
        CourseSubscriptionDB csdb;
        LessonKind l;
        LessonKindDB ldb;
        CourseTime c;
        CourseTimeDB cdb;
        SubscribersDB sdb;
        FormProject fp;
        string course;
        public FrmAttend(FormProject f)
        {
            InitializeComponent();
            DoToolTip();           
            fp = f;
            cs = new CourseSubscription();
            csdb = new CourseSubscriptionDB();
            a = new Attendance();
            adb = new AttendanceDB();
            l = new LessonKind();
            ldb = new LessonKindDB();
            c = new CourseTime();
            cdb = new CourseTimeDB();
            sdb = new SubscribersDB();
            dataGridView3.Visible = false;
            dataGridView2.Visible = false;
            dataGridView1.DataSource = ldb.GetList().Select(x => new { קוד_קורס = x.LessonCode, סוג_קורס = x.Kind }).ToList();
            foreach (Attendance item in adb.GetList())
            {
                item.Status = false;
            }
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(btnSave, "סגור טופס");
            toolTip1.SetToolTip(btnBack, "חזור לטבלה קודמת");
            toolTip1.SetToolTip(btnShowAttend, "הצג נוכחות למנוי הנבחר");
            toolTip1.SetToolTip(checkBox1, "תלמיד נוכח");
            toolTip1.SetToolTip(checkBox2, "תלמיד אינו נוכח");
        }
        private void btnShowAttend_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count > 0)
            {
                FrmAttendSubscriberCourse fasc = new FrmAttendSubscriberCourse(fp,this, dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                fasc.ShowDialog();
            }            
        }


        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            course = ldb.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)).Kind;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                l = ldb.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                dataGridView3.DataSource = cdb.GetList().FindAll(x => l.LessonCode == x.Code).ToList().FindAll(x => Translate(x.Day) == DateTime.Now.DayOfWeek.ToString()).Select(x => new { קוד_קורס = x.Code, מספר_סידורי = x.SerialNumber, יום = x.Day, שעה = x.Hour, תז_מורה = x.TeacherId }).ToList();
                if (dataGridView3.Rows.Count == 0)
                {
                    DialogResult r = MessageBox.Show("היום לא מתקיים חוג " + course, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    title.Text = "חוגי " + course  + " שמתקיימים היום";
                    btnBack.Visible = true;
                    dataGridView3.Visible = true;
                    label2.Text = "לחץ פעמיים על קורס נבחר כדי לקבל את רשימת המנויים לקורס זה";
                }
            }
        }
        public string Translate(string day)
        {
            switch (day)
            {
                case "ראשון":
                    return "Sunday";
                case "שני":
                    return "Monday";
                case "שלישי":
                    return "Tuesday";
                case "רביעי":
                    return "Wednesday";
                case "חמישי":
                    return "Thursday"; 

            }
            return "";
        }
        int serialNumber;
        int code;
        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnBack.Visible = true;
            if (dataGridView3.SelectedRows.Count > 0)
            {
                serialNumber = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value);
                code = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
                cs = csdb.Find2(serialNumber, code);
                if(cs == null)
                {
                    DialogResult r = MessageBox.Show("לא נרשמו תלמידים לקורס זה", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    dataGridView1.Visible = true;
                    dataGridView3.Visible = false;
                    return;
                }
                dataGridView2.DataSource = csdb.GetList().FindAll(x => x.SerialNumber == cs.SerialNumber && x.CourseCode == cs.CourseCode).Select(x => new { תז = x.StudentId, שם_פרטי = x.ThisStudent().StudentPrivateName, שם_משפחה = x.ThisStudent().StudentFamilyName, נוכחות = x.AttendanceCourse, מספר_שיעורים_כללי = x.EnrolledCourse }).ToList();
                dataGridView2.Visible = true;
                btnShowAttend.Visible = true;
                //btnShowAttend.Visible = true;
                dataGridView3.Visible = false;
                CourseTime c1 = new CourseTime();
                c1 = cdb.Find(Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value));
                title.Text = "מנויים לחוג " + course + " ביום " + c1.Day + " שעה " + c1.Hour;
                label2.Text = "לחץ פעמיים על מנוי כדי לסמן נוכחות";
            }
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            if (dataGridView2.SelectedRows.Count > 0)
            {
                a = adb.Find(Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value), dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), DateTime.Now.Date);
                if(a == null)
                {
                    a = new Attendance();
                    a.CodeCourse = cs.CourseCode;
                    a.Id = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    a.SerialNumber = cs.SerialNumber;
                    a.Code = adb.GetNextKey();
                    adb.AddNew(a);
                }
                a.DateOfCourse = DateTime.Now.Date;
                panel1.Visible = true;
              
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                cs = csdb.Find(dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value));//.GetList().FindAll(x => x.SerialNumber == Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value)).Find(x => x.StudentId == dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                if(a.Status)
                {
                    MessageBox.Show("למנוי זה כבר סומן נוכחות בשיעור זה","סימון נוכחות כפולה",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                    return;
                }
                cs.AttendanceCourse++;
                a.Status = true;
                adb.UpdateRow(a);
                csdb.UpdateRow(cs);
                panel1.Visible = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                dataGridView2.DataSource = csdb.GetList().FindAll(x => x.SerialNumber == cs.SerialNumber && x.CourseCode == cs.CourseCode).Select(x => new { תז = x.StudentId, שם_פרטי = x.ThisStudent().StudentPrivateName, שם_משפחה = x.ThisStudent().StudentFamilyName, נוכחות = x.AttendanceCourse, מספר_שיעורים_כללי = x.EnrolledCourse }).ToList();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                cs = csdb.Find(dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value));//.GetList().FindAll(x => x.SerialNumber == Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value)).Find(x => x.StudentId == dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                //cs = csdb.GetList().FindAll(x => x.SerialNumber == Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[1].Value)).Find(x => x.StudentId == dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                if (a.Status && a.DateOfCourse.Date == DateTime.Now.Date)
                {
                    cs.AttendanceCourse--;
                    csdb.UpdateRow(cs);
                    a.Status = false;
                    adb.UpdateRow(a);
                }
                else
                    MessageBox.Show("למנוי זה לא סומן נוכחות בשיעור זה", "אין סימון נוכחות", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                panel1.Visible = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                dataGridView2.DataSource = csdb.GetList().FindAll(x => x.SerialNumber == cs.SerialNumber && x.CourseCode == cs.CourseCode).Select(x => new { תז = x.StudentId, שם_פרטי = x.ThisStudent().StudentPrivateName, שם_משפחה = x.ThisStudent().StudentFamilyName, נוכחות = x.AttendanceCourse, מספר_שיעורים_כללי = x.EnrolledCourse }).ToList();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Visible == true)
            {
                dataGridView2.Visible = false;
                //btnShowAttend.Enabled = false;
                dataGridView3.Visible = false;
                dataGridView1.Visible = true;
                btnBack.Visible = false;
            }
            else if (dataGridView2.Visible == true)
            {
                dataGridView1.Visible = false;
                btnShowAttend.Visible = false;
                dataGridView2.Visible = false;
                //btnShowAttend.Visible = false;
                dataGridView3.Visible = true;
            }
            
        }

        private void lblBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            fp.Show();
            fp.Activate();
        }
    }
}
