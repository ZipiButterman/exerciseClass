using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CityDB : GeneralDB
    {
        public CityDB() : base("City") { }
        protected List<City> lst = new List<City>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new City(dr));
            }
        }
        public List<City> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(City c)
        {
            c.Dr = table.NewRow();
            c.FillDataRow();
            this.Add(c.Dr);
        }
        public City Find(int cod)
        {
            return this.GetList().Find(x => x.CityCode == cod);
        }
        public void UpdateRow(City c)
        {
            c.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int code)
        {
            City c = this.Find(code);
            if (c != null)
            {
                c.Dr.Delete();
                this.Update();
            }
        }
        public override string ToString()
        {
            City c = new City();
            return c.CityName;
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.CityCode) + 1;
        }
    }

    
    
}
