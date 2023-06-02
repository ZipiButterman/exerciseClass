using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class TeachersDB:GeneralDB
    {
        public TeachersDB() : base("Teachers") { }
        protected List<Teachers> lst = new List<Teachers>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new Teachers(dr));
            }
        }
        public List<Teachers> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(Teachers t)
        {
            t.Dr = table.NewRow();
            t.FillDataRow();
            this.Add(t.Dr);
        }
        public Teachers Find(string cod)
        {
            return this.GetList().Find(x => x.TeacherId == cod);
        }
        
        public void UpdateRow(Teachers t)
        {
            t.FillDataRow();
            this.Update();
        }
        public void DeleteRow(string code)
        {
            Teachers t = this.Find(code);
            if (t != null)
            {
                t.Dr.Delete();
                this.Update();
            }
        }
        City c;
        public City ThisCity()
        {
            c = new City();
            return c;
        }
    }
}
