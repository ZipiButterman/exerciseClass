using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExerciseClass.BLL
{
    class Payments
    {
        DataRow dr;
        private int paymentsCode;
        private string subscriptionID;
        private DateTime dateOfPayment;
        private double amountToPay;
        private string methodsOfPayment;
        private string creditCardNumber;
        private DateTime validit;
        private string threeDigits;   
        private string cardId;
        private int numberOfPayments;
        private string checkNumber;
        private int bankNumber;
        private int branchNumber;
        private DateTime dateOfMaturity;
        
        public DataRow Dr { get => dr; set => dr = value; }
        public int PaymentsCode { get => paymentsCode; set => paymentsCode = value; }
        public string SubscriptionID { get => subscriptionID; set { 
                if (Validation.CheckId(value)) subscriptionID = value; 
                else throw new Exception("ת\"ז לא תקינה"); } 
        }
        public DateTime DateOfPayment { get => dateOfPayment; set => dateOfPayment = value; }
        public double AmountToPay { get => amountToPay; set => amountToPay = value; }
        public string MethodsOfPayment { get => methodsOfPayment; set => methodsOfPayment = value; }
        public string CreditCardNumber { get => creditCardNumber; set { if (Validation.checkVisa(value)) creditCardNumber = value; else throw new Exception("מספר אשראי לא תקין"); } }
        public DateTime Validit { get => validit; set => validit = value; }
        public string ThreeDigits { get => threeDigits; set => threeDigits = value; }
        public string CardId { get => cardId; set { 
                if (Validation.CheckId(value) || value == "") cardId = value; 
                else throw new Exception("ת\"ז לא תקינה"); } 
        }
        public int NumberOfPayments { get => numberOfPayments; set => numberOfPayments = value; }
        public string CheckNumber { get => checkNumber; set => checkNumber = value; }
        public int BankNumber { get => bankNumber; set => bankNumber = value; }
        public int BranchNumber { get => branchNumber; set => branchNumber = value; }
        public DateTime DateOfMaturity { get => dateOfMaturity; set => dateOfMaturity = value; }

        public Payments(DataRow dr)
        {
            Dr = dr;
            PaymentsCode = Convert.ToInt32(dr["PaymentsCode"]);
            SubscriptionID = dr["SubscriptionID"].ToString();
            DateOfPayment = Convert.ToDateTime(dr["DateOfPayment"]);
            AmountToPay = Convert.ToDouble(dr["AmountToPay"]);
            MethodsOfPayment = dr["MethodsOfPayment"].ToString();
            CreditCardNumber = dr["CreditCardNumber"].ToString();
            Validit = Convert.ToDateTime(dr["Validit"]);
            ThreeDigits = dr["ThreeDigits"].ToString();
            CardId = dr["CardId"].ToString();
            NumberOfPayments = Convert.ToInt32(dr["NumberOfPayments"]);
            CheckNumber = dr["CheckNumber"].ToString();
            BankNumber = Convert.ToInt32(dr["BankNumber"]);
            BranchNumber = Convert.ToInt32(dr["BranchNumber"]);
            DateOfMaturity = Convert.ToDateTime(dr["DateOfMaturity"]);
        }

        public Payments()
        {

        }

        public void FillDataRow()
        {
            Dr["PaymentsCode"] = PaymentsCode;
            Dr["SubscriptionID"] = SubscriptionID;
            Dr["DateOfPayment"] = DateOfPayment;
            Dr["AmountToPay"] = AmountToPay;
            Dr["MethodsOfPayment"] = MethodsOfPayment;
            Dr["CreditCardNumber"] = CreditCardNumber;
            Dr["Validit"] = Validit;
            Dr["ThreeDigits"] = ThreeDigits;
            Dr["CardId"] = CardId;
            Dr["NumberOfPayments"] = NumberOfPayments;
            Dr["CheckNumber"] = CheckNumber;
            Dr["BankNumber"] = BankNumber;
            Dr["BranchNumber"] = BranchNumber;
            Dr["DateOfMaturity"] = DateOfMaturity;
        }
    }
}
