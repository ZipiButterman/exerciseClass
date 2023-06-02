using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CourseSubscriptionDB:GeneralDB
    {
        public CourseSubscriptionDB() : base("CourseSubscription") { }
        protected List<CourseSubscription> lst = new List<CourseSubscription>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new CourseSubscription(dr));
            }
        }
        public List<CourseSubscription> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(CourseSubscription c)
        {
            c.Dr = table.NewRow();
            c.FillDataRow();
            this.Add(c.Dr);
        }
        public CourseSubscription Find(int cod)
        {
            return this.GetList().Find(x => x.CourseCode == cod);
        }
        public CourseSubscription Find2(int serial, int cod)
        {
            return this.GetList().Find(x => x.SerialNumber == serial && x.CourseCode == cod);
        }
        public CourseSubscription Find(string cod)
        {
            return this.GetList().Find(x => x.StudentId == cod);
        }
        public CourseSubscription Find(string id, int cod)
        {
            return this.GetList().Find(x => x.StudentId == id && x.CourseCode == cod);
        }
        public void UpdateRow(CourseSubscription c)
        {
            c.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int code)
        {
            CourseSubscription c = this.Find(code);
            if (c != null)
            {
                c.Dr.Delete();
                this.Update();
            }
        }
        public void DeleteRow(string id, int code)
        {
            CourseSubscription c = this.Find(id, code);
            if (c != null)
            {
                c.Dr.Delete();
                this.Update();
            }
        }
    }
}
