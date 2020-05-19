using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoveLock
{
    public partial class NotifyIconForm : Form
    {
    
        public NotifyIconForm()
        {
            InitializeComponent();
        }


        private void NotifyIconForm_FormClosing(object sender, FormClosingEventArgs e)
        {
          Form1.isOpenOrClose = false;
        }

        private void NotifyIconForm_Load(object sender, EventArgs e)
        {
          switch (Form1.level)
            {
                case 1:
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    break;
            }



        }

        private void NotifyIconForm_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form1.level = 1;
            }
            else if (radioButton2.Checked) {
                Form1.level = 2;
            }
            else if (radioButton3.Checked)
            {
                Form1.level = 3;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
