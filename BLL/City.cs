using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class City
    {
        DataRow dr;
        private int cityCode;
        private string cityName;

        public DataRow Dr { get => dr; set => dr = value; }
        public int CityCode { get => cityCode; set => cityCode = value; }
        public string CityName { get => cityName; set => cityName = value; }

        public City()
        {

        }
        public City(DataRow dr)
        {
            Dr = dr;
            CityCode = Convert.ToInt32(dr["CityCode"]);
            CityName = dr["CityName"].ToString();
        }


        public void FillDataRow()
        {
            Dr["CityCode"] = CityCode;
            Dr["CityName"] = CityName;
        }



        public override string ToString()
        {
            return cityName;
        }
    }
}
