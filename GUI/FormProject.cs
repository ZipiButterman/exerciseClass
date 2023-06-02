using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseClass.GUI
{
    
    public partial class FormProject : Form
    {
        public FormProject()
        {
            InitializeComponent();
            this.BringToFront();
            DoToolTip();
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(btnAttendence, "סימון או בדיקת נוכחות עבור קורסים המתקיימים היום");
            toolTip1.SetToolTip(btnSubscribers, "הוסף, מחק או עדכן מנויים");
            toolTip1.SetToolTip(btnTeachers, "הוסף, מחק או עדכן מורים");
            toolTip1.SetToolTip(btnCourse, "ניהול קורסים");
        }
        private void btnTeachers_Click(object sender, EventArgs e)
        {
            FrmTeachers ft = new FrmTeachers(this, null);
            ft.Show();
        }

        private void btnSubscribers_Click(object sender, EventArgs e)
        {
            FrmSubscriber fs = new FrmSubscriber(this);
            fs.Show();
        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            FrmAttend fa = new FrmAttend(this);
            fa.Show();
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            AddLessonKind fc = new AddLessonKind(this);
            fc.Show();
        }
    }
}
    
        
