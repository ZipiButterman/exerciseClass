using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class Attendance
    {
        DataRow dr;
        private int code;
        private int serialNumber;
        private int codeCourse;
        private DateTime dateOfCourse;
        private string id;
        private bool status;

        public DataRow Dr { get => dr; set => dr = value; }
        public int Code { get => code; set => code = value; }
        public int SerialNumber { get => serialNumber; set => serialNumber = value; }
        public int CodeCourse { get => codeCourse; set => codeCourse = value; }
        public DateTime DateOfCourse { get => dateOfCourse; set => dateOfCourse = value; }
        public string Id { get => id; set { if (Validation.CheckId(value)) id = value; else throw new Exception("ת\"ז לא תקינה"); } }
        public bool Status { get => status; set => status = value; }

        public Attendance()
        {

        }
        public Attendance(DataRow dr)
        {
            Dr = dr;
            Code = Convert.ToInt32(dr["Code"]);
            SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
            CodeCourse = Convert.ToInt32(dr["CodeCourse"]);
            DateOfCourse = Convert.ToDateTime(dr["DateOfCourse"]);
            Id = dr["Id"].ToString();
            Status = Convert.ToBoolean(dr["Status"]);
        }

        public void FillDataRow()
        {
            Dr["Code"] = Code;
            Dr["SerialNumber"] = SerialNumber;
            Dr["CodeCourse"] = CodeCourse;
            Dr["DateOfCourse"] = DateOfCourse;
            Dr["Id"] = Id;
            Dr["Status"] = Status;
        }



        public Subscribers ThisStudent()
        {
            SubscribersDB sdb = new SubscribersDB();
            return sdb.Find(this.id);
        }
    }
}
