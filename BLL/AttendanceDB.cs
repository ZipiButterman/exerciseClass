using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace ExerciseClass.BLL
{
    class AttendanceDB:GeneralDB
    {
        public AttendanceDB() : base("Attendance") { }
        protected List<Attendance> lst = new List<Attendance>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new Attendance(dr));
            }
        }
        public List<Attendance> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(Attendance a)
        {
            a.Dr = table.NewRow();
            a.FillDataRow();
            this.Add(a.Dr);
        }
        public Attendance Find2(int cod)
        {
            return this.GetList().Find(x => x.Code == cod);
        }
        public Attendance Find(int cod, string id)
        {
            return this.GetList().Find(x => x.SerialNumber == cod && x.Id == id);
        }

        public Attendance Find(int cod, string id, DateTime dt)
        {
            return this.GetList().Find(x => x.SerialNumber == cod && x.Id == id && x.DateOfCourse.Date == dt.Date );
        }
        public Attendance Find3(string cod)
        {
            return this.GetList().Find(x => x.Id == cod);
        }
        public void UpdateRow(Attendance a)
        {
            a.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int code,string id, DateTime dt)
        {
            Attendance a = this.Find(code, id, dt);
            if (a != null)
            {
                a.Dr.Delete();
                this.Update();
            }
        }
        public void DeleteRow1(int code, string id)
        {
            AttendanceDB a = new AttendanceDB(); 
            foreach (Attendance item in a.GetList())
            {
                if(item.SerialNumber == code && item.Id == id)
                {
                    item.Dr.Delete();
                    this.Update();
                }              
            }
           
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Code) + 1;
        }

    }
}
