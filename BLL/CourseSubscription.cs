using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CourseSubscription
    {
        DataRow dr;
        private string studentId;
        private int courseCode;
        private int serialNumber;
        private int enrolledCourse;
        private int attendanceCourse;

        public DataRow Dr { get => dr; set => dr = value; }
        public int CourseCode { get => courseCode; set => courseCode = value; }
        public int SerialNumber { get => serialNumber; set => serialNumber = value; }
        public int EnrolledCourse { get => enrolledCourse; set => enrolledCourse = value; }
        public int AttendanceCourse { get => attendanceCourse; set => attendanceCourse = value; }
        public string StudentId { get => studentId; set { 
                if (Validation.CheckId(value)) studentId = value; 
                else throw new Exception("ת\"ז לא תקינה"); } 
        }

        public CourseSubscription(DataRow dr)
        {
            Dr = dr;
            CourseCode = Convert.ToInt32(dr["CourseCode"]);
            SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
            EnrolledCourse = Convert.ToInt32(dr["EnrolledCourse"]);
            AttendanceCourse = Convert.ToInt32(dr["AttendanceCourse"]);
            StudentId = dr["StudentId"].ToString();
        }

        public CourseSubscription()
        {

        }

        public void FillDataRow()
        {
            Dr["CourseCode"] = CourseCode;
            Dr["SerialNumber"] = SerialNumber;
            Dr["EnrolledCourse"] = EnrolledCourse;
            Dr["AttendanceCourse"] = AttendanceCourse;
            Dr["StudentId"] = StudentId;
        }
        
        
        public Subscribers ThisStudent()
        {
            SubscribersDB sdb = new SubscribersDB();
            return sdb.Find(this.StudentId);
        }

        public LessonKind ThisCourse()
        {
            LessonKindDB ldb = new LessonKindDB();
            return ldb.Find(this.CourseCode);
        }
        public CourseTime ThisCourseTime()
        {
            CourseTimeDB ldb = new CourseTimeDB();
            return ldb.Find(this.SerialNumber, this.CourseCode);
        }
    }
}
