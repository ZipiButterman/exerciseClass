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
    public partial class FrmCoursesList : Form
    {
        LessonKind l;
        CourseTime ct;
        LessonKindDB ldb;
        CourseTimeDB ctdb;
        CourseSubscription cs;
        CourseSubscription cs1;
        CourseSubscriptionDB csdb;
        FormProject fp;
        FrmSubscriber fc;
        Subscribers s;
        SubscribersDB sdb;
        public FrmCoursesList(FormProject f,FrmSubscriber f1,string id)
        {
            InitializeComponent();
            DoToolTip();          
            l = new LessonKind();
            ldb = new LessonKindDB();
            ct = new CourseTime();
            ctdb = new CourseTimeDB();
            cs = new CourseSubscription();
            cs1 = new CourseSubscription();
            csdb = new CourseSubscriptionDB();
            s = new Subscribers();
            sdb = new SubscribersDB();
            fp = f;
            fc = f1;
            s = sdb.Find(id);
            if(s.StudentSex == "זכר")
                kind.DataSource = ldb.GetList().FindAll(x => x.Audience == "בנים" || x.Audience == "גברים").Select(x => new { קוד_קורס = x.LessonCode, סוג = x.Kind, קהל_יעד = x.Audience, מחיר_חודשי = x.PricePerMonth, מחיר_רבעון = x.QuarterlyPrice }).ToList();
            else
                kind.DataSource = ldb.GetList().FindAll(x => x.Audience == "בנות" || x.Audience == "נשים").Select(x => new { קוד_קורס = x.LessonCode, סוג = x.Kind, קהל_יעד = x.Audience, מחיר_חודשי = x.PricePerMonth, מחיר_רבעון = x.QuarterlyPrice }).ToList();
            course.Visible = false;
            textBox1.Text = id;
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(label2, "חזרה לדף הבית");
            toolTip1.SetToolTip(label17, "חזרה לטופס הקודם");
            toolTip1.SetToolTip(btnChoose, "חזרה לטבלה הקודמת");
            toolTip1.SetToolTip(button1, "סגור טופס");
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (course.Visible)
            {
                kind.Visible = true;
                course.Visible = false;
            }
        }

     
        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            fc.Show();
            fc.Activate();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            fc.Close();
            fp.Show();
            fp.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kind_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (kind.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(kind.SelectedRows[0].Cells[0].Value);
                course.DataSource = ctdb.GetList().FindAll(x => x.Code == Convert.ToInt32(id)).Select(x => new { קוד_קורס = x.Code, מספר_סידורי = x.SerialNumber, יום = x.Day, שעה = x.Hour, מורה = x.TeacherId }).ToList();
                if (course.Rows.Count == 0)
                {
                    MessageBox.Show("אין קורסים לסוג שיעור זה", "אין קורסים", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                course.Visible = true;
                kind.Visible = false;
                lblData.Text = "לחץ פעמיים על הקורס הרצוי כדי לרשום את המנוי לקורס זה";
            }
        }

        private void course_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (course.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(kind.SelectedRows[0].Cells[0].Value);
                s = new Subscribers();
                s = sdb.Find(textBox1.Text);
                cs1 = csdb.GetList().FindAll(x => x.StudentId == textBox1.Text).Find(x => x.CourseCode == id);
                if (cs1 != null)
                {
                    MessageBox.Show("מנוי זה כבר רשום לקורס זה", "אזהרה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                string day = course.SelectedRows[0].Cells[2].Value.ToString();
                double hour = Convert.ToDouble(course.SelectedRows[0].Cells[3].Value);
                cs = csdb.GetList().FindAll(x => (s.StudentId == x.StudentId)).ToList().FindAll(x => x.ThisCourseTime().Day == day).ToList().Find(x => x.ThisCourseTime().Hour == hour);
                if (cs != null)
                {
                    MessageBox.Show("לסטודנט " + s + " \nקיים כבר קורס קורס ביום: " + day + " \nבשעה: " + hour.ToString() + " ועל כן לא ניתן\n לקבוע עבורו קורס נוסף!", "קורס", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                DialogResult r = MessageBox.Show("האם להוסיף קורס זה?", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    l = ldb.Find(id);
                    ct = ctdb.Find(Convert.ToInt32(course.SelectedRows[0].Cells[1].Value), id);
                    cs = new CourseSubscription();
                    cs.AttendanceCourse = 0;
                    cs.EnrolledCourse = ct.NumberOfLesson;
                    cs.CourseCode = id;
                    cs.SerialNumber = Convert.ToInt32(course.SelectedRows[0].Cells[1].Value);
                    cs.StudentId = textBox1.Text;
                    s.StudentDebt += l.QuarterlyPrice * 4.0;
                    sdb.UpdateRow(s);
                    csdb.AddNew(cs);
                    csdb.UpdateRow(cs);
                    r = MessageBox.Show("האם ברצונך להוסיף קורס נוסף?", "רישום לקורס", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        kind.Visible = true;
                        course.Visible = false;
                    }
                    else
                        this.Close();
                }
            }
        }
    }
}
