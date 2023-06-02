using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class Teachers
    {
        DataRow dr;
        private string teacherId;
        private string teacherPrivateName;
        private string teacherFamilyName;
        private string teacherStreet;
        private string teacherNumHouse;
        private int teacherCity;
        private string teacherPhoneNumber;
        private string teacherMail;
        private string teacherSex;

        public DataRow Dr { get => dr; set => dr = value; }
        public string TeacherId { get => teacherId; set { 
                if (Validation.CheckId(value)) teacherId = value; 
                else throw new Exception("ת\"ז לא תקינה"); } 
        }
        public string TeacherPrivateName { get => teacherPrivateName; set => teacherPrivateName = value; }
        public string TeacherFamilyName { get => teacherFamilyName; set => teacherFamilyName = value; }
        public string TeacherStreet { get => teacherStreet; set => teacherStreet = value; }
        public string TeacherNumHouse { get => teacherNumHouse; set => teacherNumHouse = value; }
        public int TeacherCity { get => teacherCity; set => teacherCity = value; }
        public string TeacherPhoneNumber { get => teacherPhoneNumber; set { 
                if (Validation.IsPelepon(value)) teacherPhoneNumber = value; 
                else throw new Exception("מספר פלאפון לא תקין"); } 
        }
        public string TeacherMail { get => teacherMail; set { 
                if (Validation.IsMail(value)) teacherMail = value; 
                else throw new Exception("כתובת מייל לא תקינה"); } 
        }
        public string TeacherSex { get => teacherSex; set => teacherSex = value; }
        public Teachers()
        {

        }
        public Teachers(DataRow dr)
        {
            Dr = dr;
            TeacherId = dr["TeacherId"].ToString();
            TeacherPrivateName = dr["TeacherPrivateName"].ToString();
            TeacherFamilyName = dr["TeacherFamilyName"].ToString();
            TeacherStreet = dr["TeacherStreet"].ToString();
            TeacherNumHouse = dr["TeacherNumHouse"].ToString();
            TeacherCity = Convert.ToInt32(dr["TeacherCity"]);
            TeacherPhoneNumber = dr["TeacherPhoneNumber"].ToString();
            TeacherMail = dr["TeacherMail"].ToString();
            TeacherSex = dr["TeacherSex"].ToString();
        }
        public void FillDataRow()
        {
            Dr["TeacherId"] = TeacherId;
            Dr["TeacherPrivateName"] = TeacherPrivateName;
            Dr["TeacherFamilyName"] = TeacherFamilyName;
            Dr["TeacherStreet"] = TeacherStreet;
            Dr["TeacherNumHouse"] = TeacherNumHouse;
            Dr["TeacherCity"] = TeacherCity;
            Dr["TeacherPhoneNumber"] = TeacherPhoneNumber;
            Dr["TeacherMail"] = TeacherMail;
            Dr["TeacherSex"] = TeacherSex;
        }

        public Teachers(Teachers t)
        {
            this.TeacherId = t.TeacherId;
            this.TeacherPrivateName = t.TeacherPrivateName;
            this.TeacherFamilyName = t.TeacherFamilyName;
            this.TeacherStreet = t.TeacherStreet;
            this.TeacherNumHouse = t.TeacherNumHouse;
            this.TeacherCity = t.TeacherCity;
            this.TeacherPhoneNumber = t.TeacherPhoneNumber;
            this.TeacherMail = t.TeacherMail;
            this.TeacherSex = t.TeacherSex;
        }
        public City ThisCity()
        {
            CityDB cdb = new CityDB();
            return cdb.Find(this.TeacherCity);
        }
        public override string ToString()
        {
            return teacherPrivateName + " " + teacherFamilyName;
        }
    }
}
