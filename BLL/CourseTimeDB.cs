using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class CourseTimeDB:GeneralDB
    {
        public CourseTimeDB() : base("CourseTime") { }
        protected List<CourseTime> lst = new List<CourseTime>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new CourseTime(dr));
            }
        }
        public List<CourseTime> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(CourseTime c)
        {
            c.Dr = table.NewRow();
            c.FillDataRow();
            this.Add(c.Dr);
        }
        public CourseTime Find(int serial, int code)
        {
            return this.GetList().Find(x => x.SerialNumber == serial && x.Code == code);
        }
        public CourseTime Find(int serial)
        {
            return this.GetList().Find(x => x.SerialNumber == serial);
        }

        public CourseTime Find2(int code)
        {
            return this.GetList().Find(x => x.Code == code);
        }
        public void UpdateRow(CourseTime c)
        {
            c.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int serial, int code)
        {
            CourseTime c = this.Find(serial, code);
            if (c != null)
            {
                c.Dr.Delete();
                this.Update();
            }
        }
        public int GetNextKey(int code)
        {
            
            if (this.Find2(code) == null)
                return 1;
            return this.GetList().FindAll(x => x.ThisLessonKind().LessonCode == code).Max(x => x.SerialNumber) + 1;
        }
    }
}
