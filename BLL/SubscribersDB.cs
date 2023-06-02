using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class SubscribersDB:GeneralDB
    {
        public SubscribersDB() : base("Subscribers") { }
        protected List<Subscribers> lst = new List<Subscribers>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new Subscribers(dr));
            }
        }
        public List<Subscribers> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(Subscribers s)
        {
            s.Dr = table.NewRow();
            s.FillDataRow();
            this.Add(s.Dr);
        }
        public Subscribers Find(string cod)
        {
            return this.GetList().Find(x => x.StudentId == cod);
        }
        public void UpdateRow(Subscribers s)
        {
            s.FillDataRow();
            this.Update();
        }
        public void DeleteRow(string code)
        {
            Subscribers s = this.Find(code);
            if (s != null)
            {
                s.Dr.Delete();
                this.Update();
            }
        }
        City c;
        public City ThisCity()
        {
            c = new City();
            return c;
        }
        public override string ToString()
        {
            Subscribers s = new Subscribers();
            return s.StudentPrivateName + " " + s.StudentFamilyName;
        }
    }
}
