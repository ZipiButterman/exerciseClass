using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class PaymentsDB:GeneralDB
    {
        public PaymentsDB() : base("Payments") { }
        protected List<Payments> lst = new List<Payments>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new Payments(dr));
            }
        }
        public List<Payments> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(Payments p)
        {
            p.Dr = table.NewRow();
            p.FillDataRow();
            this.Add(p.Dr);
        }
        public Payments Find(int cod)
        {
            return this.GetList().Find(x => x.PaymentsCode == cod);
        }
        public void UpdateRow(Payments p)
        {
            p.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int code)
        {
            Payments p = this.Find(code);
            if (p != null)
            {
                p.Dr.Delete();
                this.Update();
            }
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.PaymentsCode) + 1;
        }
    }
}
