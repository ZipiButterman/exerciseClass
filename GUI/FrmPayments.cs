using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExerciseClass.BLL;
using ExerciseClass.GUI;


namespace ExerciseClass.GUI
{
    public partial class FrmPayments : Form
    {
        Payments p;
        PaymentsDB pdb;
        Subscribers s;
        SubscribersDB sdb;
        FrmSubscriber ffs;
        FormProject ffp;
        bool flagOK = false;
        bool flagError = false;
        public FrmPayments(FormProject fp, FrmSubscriber fs, double money, string id)
        {
            InitializeComponent();
            DoToolTip();


            string[] arr = { "ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "ספטמבר", "אוקטובר", "נובמבר", "דצמבר" };
            pdb = new PaymentsDB();
            p = new Payments();
            s = new Subscribers();
            sdb = new SubscribersDB();
            ffs = fs;
            ffp = fp;
            textBox7.Text = id;
            s = sdb.Find(id);
            textBox8.Text = money.ToString();
            for (int i = DateTime.Today.Year; i < DateTime.Today.AddYears(20).Year; i++)
                cmbYear.Items.Add(i);
            for (int i = 0; i < arr.Length; i++)
                cmbMonth.Items.Add(arr[i]);
            for (int i = 1; i < 6; i++)
                cmbPayments.Items.Add(i);
            rbCash.Checked = false;
            rbCheck.Checked = false;
            rbCreditCard.Checked = false;
            radioButton1.Checked = true;
            cmbPayments.SelectedIndex = 0;
        }

        public void DoToolTip()
        {
            toolTip1.SetToolTip(lblBack, "חזרה לדף הבית");
            toolTip1.SetToolTip(label17, "חזור לטופס הקודם");
            toolTip1.SetToolTip(rbCheck, "תשלום ע\"י צ'ק");
            toolTip1.SetToolTip(rbCash, "תשלום ע\"י מזומן");
            toolTip1.SetToolTip(rbCreditCard, "תשלום ע\"י אשראי");
            toolTip1.SetToolTip(btnContinue, "אישור והמשך");
            toolTip1.SetToolTip(textBox6, "הכנס מספר אשראי");
            toolTip1.SetToolTip(cmbMonth, "בחר חודש");
            toolTip1.SetToolTip(cmbYear, "בחר שנה");
            toolTip1.SetToolTip(cmbPayments, "בחר מספר תשלומים");
            toolTip1.SetToolTip(textBox5, "הכנס 3 ספרות בגב הכרטיס");
            toolTip1.SetToolTip(textBox4, "הכנס ת\"ז של בעל הכרטיס");
            toolTip1.SetToolTip(textBox1, "הכנס מספר צ'ק");
            toolTip1.SetToolTip(comboBox1, "בחר בנק");
            toolTip1.SetToolTip(dateTimePicker1, "בחר תאריך פרעון");
        }
        private void rbCheck_CheckedChanged(object sender, EventArgs e)
        {
            string[] arr = { "יהב", "לאומי", "דיסקונט", "הפועלים", "איגוד", "מזרחי טפחות", "citibank", "הבינלאומי", "ירושלים", "ישראל" };
            if (rbCheck.Checked)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    comboBox1.Items.Add(arr[i]);
                }             
                panelCheck.Visible = true;
                panelCard.Visible = false;
                lblPay.Visible = false;
                label14.Visible = true;
                cmbPayments.Visible = true;
                cmbPayments.SelectedIndex = 0;
            }
                
        }

        private void rbCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCreditCard.Checked)
            {
                panelCard.Visible = true;
                panelCheck.Visible = false;
                lblPay.Visible = false;
                label14.Visible = true;
                cmbPayments.Visible = true;
                cmbPayments.SelectedIndex = 0;
            }

        }

        private void rbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCash.Checked)
            {
                panelCard.Visible = false;
                panelCheck.Visible = false;
                lblPay.Visible = true;
                label14.Visible = false;
                cmbPayments.Items.Clear();
                cmbPayments.Items.Add(1);
                cmbPayments.SelectedIndex = 0;
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (!rbCash.Checked && !rbCheck.Checked && !rbCreditCard.Checked)
            {
                MessageBox.Show("יש לבחור אמצעי תשלום", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if(flagError)
            {
                MessageBox.Show("אחד או יותר מהערכים שהזנת שגוי", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if(CreateFieled(p))
            {
                DialogResult r = MessageBox.Show("אישור", "אישור תשלום סופי", MessageBoxButtons.OKCancel);
                if (r == DialogResult.OK)
                {
                    p.PaymentsCode = pdb.GetNextKey();
                    p.SubscriptionID = textBox7.Text;
                    p.AmountToPay = Convert.ToDouble(textBox8.Text);
                    p.DateOfPayment = DateTime.Now.Date;
                    p.NumberOfPayments = Convert.ToInt32(cmbPayments.Text);
                    s.StudentDebt -= (Convert.ToDouble(textBox8.Text) / p.NumberOfPayments);
                    this.Close();
                    sdb.UpdateRow(s);
                    sdb.Update();
                    pdb.AddNew(p);
                    pdb.UpdateRow(p);
                    pdb.Update();
                }
            }                
        }
        private bool CreateFieled(Payments p)
        {
            flagOK = true;
            int[] arr = { 4, 10, 11, 12, 13, 20, 22, 31, 54, 99 };
            if (rbCheck.Checked)
            {
                p.MethodsOfPayment = "צ'ק";
                p.CreditCardNumber = "";
                p.Validit = new DateTime(1, 1, 1);
                p.ThreeDigits = "";
                p.CardId = "";
                try
                {
                    if (textBox1.Text == "")
                        throw new Exception("יש להכניס מספקר צ'ק");
                    else
                        p.CheckNumber = textBox1.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (comboBox1.Text == "")
                        throw new Exception("יש לבחור בנק מהרשימה");
                    else
                        p.BankNumber = Convert.ToInt32(arr[comboBox1.SelectedIndex]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (textBox2.Text == "")
                        throw new Exception("יש להכניס מספר סניף");
                    else
                        p.BranchNumber = Convert.ToInt32(textBox2.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (dateTimePicker1.Text == "")
                        throw new Exception("יש לבחור תאריך פירעון");
                    else
                        p.DateOfMaturity = dateTimePicker1.Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            if (rbCreditCard.Checked)
            {
                p.MethodsOfPayment = "אשראי";
                p.CheckNumber = "";
                p.BankNumber = 0;
                p.BranchNumber = 0;
                p.DateOfMaturity = new DateTime(1, 1, 1);
                try
                {
                    if (textBox6.Text == "")
                        throw new Exception("יש להכניס מספר אשראי");
                    else
                        p.CreditCardNumber = textBox6.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (cmbYear.Text == "" || cmbMonth.Text == "")
                        throw new Exception("יש לבחור תוקף מהרשימה");
                    else
                        p.Validit = new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex) + 1, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (textBox5.Text == "")
                        throw new Exception("יש להכניס 3 ספרות בגב הכרטיס");
                    else
                        p.ThreeDigits = textBox5.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
                try
                {
                    if (textBox4.Text == "")
                        throw new Exception("יש להכניס ת\"ז של בעל הכרטיס");
                    else
                        p.CardId = textBox4.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            if (rbCash.Checked)
            {
                p.MethodsOfPayment = "מזומן";
                p.CreditCardNumber = "";
                p.Validit = new DateTime(1, 1, 1);
                p.ThreeDigits = "";
                p.CardId = "";
                p.CheckNumber = "";
                p.BankNumber = 0;
                p.BranchNumber = 0;
                p.DateOfMaturity = new DateTime(1, 1, 1);
                flagOK = true;
            }
            return flagOK;
        }


        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
            ffs.Show();
            ffs.Activate();
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            this.Close();
            ffs.Close();
            ffp.Show();
            ffp.Activate(); 
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.BackColor = Color.White;
            lblCard.Text = "";
            flagError = false;
            if (!Validation.IsNum(textBox6.Text))
            {
                flagError = true;
                textBox6.BackColor = Color.Lavender;
                lblCard.Text = "יש להכניס ספרות בלבד";
                textBox6.Focus();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.BackColor = Color.White;
            lblDigit.Text = "";
            flagError = false;
            if (!Validation.IsNum(textBox5.Text))
            {
                flagError = true;
                textBox5.BackColor = Color.Lavender;
                lblDigit.Text = "יש להכניס ספרות בלבד";
                textBox5.Focus();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
            lblId.Text = "";
            flagError = false;
            if (!Validation.IsNum(textBox4.Text))
            {
                flagError = true;
                textBox4.BackColor = Color.Lavender;
                lblId.Text = "יש להכניס ספרות בלבד";
                textBox4.Focus();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Length != 3)
            {
                lblDigit.Text = "יש להכניס 3 ספרות";
                flagError = true;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 9)
            {
                lblId.Text = "יש להכניס 9 ספרות";
                flagError = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            lblBranch.Text = "";
            flagError = false;
            if (!Validation.IsNum(textBox2.Text))
            {
                flagError = true;
                textBox2.BackColor = Color.Lavender;
                lblBranch.Text = "יש להכניס ספרות בלבד";
                textBox2.Focus();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length != 3)
            {
                lblBranch.Text = "יש להכניס 3 ספרות";
                flagError = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            lblCheck.Text = "";
            flagError = false;
            if (!Validation.IsNum(textBox1.Text))
            {
                flagError = true;
                textBox1.BackColor = Color.Lavender;
                lblCheck.Text = "יש להכניס ספרות בלבד";
                textBox1.Focus();
            }
        }
    }
}
