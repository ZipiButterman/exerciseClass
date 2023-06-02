using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CorseTime
    {
        DataRow dr;
        private int serialNumber;
        private int code;
        private string day;
        private double hour;
        private string teacherId;

        public DataRow Dr { get => dr; set => dr = value; }
        public int SerialNumber { get => serialNumber; set => serialNumber = value; }
        public int Code { get => code; set => code = value; }
        public string Day { get => day; set => day = value; }
        public double Hour { get => hour; set => hour = value; }
        public string TeacherId { get => teacherId; set => teacherId = value; }

        public CorseTime(DataRow dr)
        {
            Dr = dr;
            Code = Convert.ToInt32(dr["Code"]);
            SerialNumber = Convert.ToInt32(dr["SerialNumber"]);
            Hour = Convert.ToDouble(dr["Hour"]);
            TeacherId = dr["TeacherId"].ToString();
            Day = dr["Day"].ToString();
        }
        public void FillDataRow()
        {
            Dr["Code"] = Code;
            Dr["SerialNumber"] = SerialNumber;
            Dr["Hour"] = Hour;
            Dr["TeacherId"] = TeacherId;
            Dr["Day"] = Day;
        }
    }
}
