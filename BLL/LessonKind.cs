using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class LessonKind
    {
        DataRow dr;
        private int lessonCode;
        private string audience;
        private string kind;
        private double pricePerMonth;
        private double quarterlyPrice;

        public DataRow Dr { get => dr; set => dr = value; }
        public int LessonCode { get => lessonCode; set => lessonCode = value; }
        public string Audience { get => audience; set => audience = value; }
        public string Kind { get => kind; set => kind = value; }
        public double PricePerMonth { get => pricePerMonth; set => pricePerMonth = value; }
        public double QuarterlyPrice { get => quarterlyPrice; set => quarterlyPrice = value; }

        public LessonKind(DataRow dr)
        {
            Dr = dr;
            LessonCode = Convert.ToInt32(dr["LessonCode"]);
            PricePerMonth = Convert.ToDouble(dr["PricePerMonth"]);
            QuarterlyPrice = Convert.ToDouble(dr["QuarterlyPrice"]);
            Audience = dr["Audience"].ToString();
            Kind = dr["Kind"].ToString();
        }

        public LessonKind()
        {

        }

        public LessonKind (LessonKind l)
        {
            this.kind = l.kind;
            this.audience = l.audience;
            this.quarterlyPrice = l.quarterlyPrice;
            this.pricePerMonth = l.pricePerMonth;
            this.lessonCode = l.lessonCode;
        }

        public void FillDataRow()
        {
            Dr["LessonCode"] = LessonCode;
            Dr["PricePerMonth"] = PricePerMonth;
            Dr["QuarterlyPrice"] = QuarterlyPrice;
            Dr["Audience"] = Audience;
            Dr["Kind"] = Kind;
        }
        public override string ToString()
        {
            return kind;
        }
    }
}
