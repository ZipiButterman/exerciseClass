using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExerciseClass.GUI
{
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent();
            label5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.Text == "1234")
            {
                FormProject p = new FormProject();
                p.Show();
            }
            else
            {
                label5.Visible = true;
            }
            maskedTextBox1.Text = "";
        }
    }
}
