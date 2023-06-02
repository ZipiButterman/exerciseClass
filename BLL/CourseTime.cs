using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CourseTime
    {
        DataRow dr;
        private int serialNumber;
        private int code;
        private string day;
        private double hour;
        private string teacherId;
        private int numberOfLesson;

        public DataRow Dr { get => dr; set => dr = value; }
        public int SerialNumber { get => serialNumber; set => serialNumber = value; }
        public int Code { get => code; set => code = value; }
        public string Day { get => day; set => day = value; }
        public double Hour { get => hour; set => hour = value; }
        public string TeacherId { get => teacherId; set { 
                if (Validation.CheckId(value)) teacherId = value;
                else throw new Exception("ת\"ז לא תקינה"); } 
        }
        public int NumberOfLesson { get => numberOfLesson; set => numberOfLesson = value; } 

        public CourseTime(DataRow dr)
        {
            Dr = dr;
            Code = Convert.ToInt32(dr["Code"]);
            SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
            Hour = Convert.ToDouble(dr["Hour1"]);
            TeacherId = dr["TeacherId"].ToString();
            Day = dr["Day1"].ToString();
            NumberOfLesson = Convert.ToInt32(dr["NumberOfLessons"]);
        }
        public CourseTime()
        {

        }

        public CourseTime(CourseTime ct)
        {
            this.SerialNumber = ct.SerialNumber;
            this.Hour = ct.Hour;
            this.TeacherId = ct.TeacherId;
            this.Day = ct.Day;
            this.NumberOfLesson = ct.NumberOfLesson;
            this.code = ct.Code;

        }
        public void FillDataRow()
        {
            Dr["Code"] = Code;
            Dr["SerialNumber"] = SerialNumber;
            Dr["Hour1"] = Hour;
            Dr["TeacherId"] = TeacherId;
            Dr["Day1"] = Day;
            Dr["NumberOfLessons"] = NumberOfLesson;
        }



        public Teachers ThisTeacher()
        {
            TeachersDB tdb = new TeachersDB();
            return tdb.Find(this.TeacherId);
        }
        public LessonKind ThisLessonKind()
        {
            LessonKindDB ldb = new LessonKindDB();
            return ldb.Find(this.code);
        }
    }
}