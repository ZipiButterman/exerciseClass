using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class LessonKindDB:GeneralDB
    {
        public LessonKindDB() : base("LessonKind") { }
        protected List<LessonKind> lst = new List<LessonKind>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                lst.Add(new LessonKind(dr));
            }
        }
        public List<LessonKind> GetList()
        {
            lst.Clear();
            DataTableToList();
            return lst;
        }
        public void AddNew(LessonKind l)
        {
            l.Dr = table.NewRow();
            l.FillDataRow();
            this.Add(l.Dr);
        }
        public LessonKind Find(int cod)
        {
            return this.GetList().Find(x => x.LessonCode == cod);
        }
        public LessonKind Find(string cod)
        {
            return this.GetList().Find(x => x.Kind == cod);
        }
        public void UpdateRow(LessonKind l)
        {
            l.FillDataRow();
            this.Update();
        }
        public void DeleteRow(int code)
        {
            LessonKind l = this.Find(code);
            if (l != null)
            {
                l.Dr.Delete();
                this.Update();
            }
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.LessonCode) + 1;
        }
    }
}
