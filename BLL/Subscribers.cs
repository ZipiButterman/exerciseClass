using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class Subscribers
    {
        DataRow dr;
        private string studentId;
        private string studentPrivateName;
        private string studentFamilyName;
        private string studentStreet;
        private string studentNumHouse;
        private int studentCity;
        private string studentPhoneNumber;
        private DateTime studentDateOfBirth;
        private string studentSex;
        private double studentDebt;
        private string studentMail;

        public DataRow Dr { get => dr; set => dr = value; }
        public string StudentId { get => studentId; set { 
                if (Validation.CheckId(value)) studentId = value; 
                else throw new Exception("ת\"ז לא תקינה");} 
        }
        public string StudentFamilyName { get => studentFamilyName; set => studentFamilyName = value; }
        public string StudentPrivateName { get => studentPrivateName; set => studentPrivateName = value; }
        public string StudentStreet { get => studentStreet; set => studentStreet = value; }
        public string StudentNumHouse { get => studentNumHouse; set => studentNumHouse = value; }
        public int StudentCity { get => studentCity; set => studentCity = value; }
        public string StudentPhoneNumber { get => studentPhoneNumber; set { 
                if (Validation.IsPelepon(value)) studentPhoneNumber = value; 
                else throw new Exception("מספר פלאפון לא תקין"); } 
        }
        public DateTime StudentDateOfBirth { get => studentDateOfBirth; set => studentDateOfBirth = value; }
        public string StudentSex { get => studentSex; set => studentSex = value; }
        public double StudentDebt { get => studentDebt; set => studentDebt = value; }
        public string StudentMail { get => studentMail; set { 
                if (Validation.IsMail(value)) studentMail = value; 
                else throw new Exception("כתובת מייל לא תקינה"); } 
        }

        public Subscribers(DataRow dr)
        {
            Dr = dr;
            StudentFamilyName = dr["StudentFamilyName"].ToString();
            StudentId = dr["StudentId"].ToString();
            StudentPrivateName = dr["StudentPrivateName"].ToString();
            StudentStreet = dr["StudentStreet"].ToString();
            StudentNumHouse = dr["StudentNumHouse"].ToString();
            StudentCity = Convert.ToInt32(dr["StudentCity"]);
            StudentPhoneNumber = dr["StudentPhoneNumber"].ToString();
            StudentSex = dr["StudentSex"].ToString();
            StudentDebt = Convert.ToDouble(dr["StudentDebt"]);
            StudentDateOfBirth = Convert.ToDateTime(dr["StudentDateOfBirth"]);
            StudentMail = dr["StudentMail"].ToString();
        }

        public Subscribers()
        {

        }
        public Subscribers(Subscribers s)
        {
            this.studentId = s.studentId;
            this.StudentPrivateName = s.studentPrivateName;
            this.StudentStreet = s.studentStreet;   
            this.StudentNumHouse = s.studentNumHouse; 
            this.StudentCity = s.studentCity;   
            this.StudentPhoneNumber = s.studentPhoneNumber; 
            this.StudentSex = s.studentSex; 
            this.StudentDebt = s.studentDebt;   
            this.StudentDateOfBirth = s.studentDateOfBirth; 
            this.StudentMail = s.studentMail;   
            this.StudentFamilyName = s.studentFamilyName;
        }

        public void FillDataRow()
        {
            Dr["StudentFamilyName"] = StudentFamilyName;
            Dr["StudentId"] = StudentId;
            Dr["StudentPrivateName"] = StudentPrivateName;
            Dr["StudentStreet"] = StudentStreet;
            Dr["StudentNumHouse"] = StudentNumHouse;
            Dr["StudentCity"] = StudentCity;
            Dr["StudentPhoneNumber"] = StudentPhoneNumber;
            Dr["StudentSex"] = StudentSex;
            Dr["StudentDebt"] = StudentDebt;
            Dr["StudentDateOfBirth"] = StudentDateOfBirth;
            Dr["StudentMail"] = StudentMail;
        }

        public City ThisCity()
        {
            CityDB cdb = new CityDB();
            return cdb.Find(this.studentCity);
        }
        public override string ToString()
        {
            return studentPrivateName + " " + studentFamilyName;
        }
    }
}
