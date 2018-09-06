using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding
{
    public partial class Form2 : Form
    {
        Form1 parent;

        public Form2(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.textBox1.Text) % 2 != 0 && int.Parse(this.textBox2.Text) % 2 != 0)
            {
                this.parent.DrawGrid(int.Parse(this.textBox1.Text), int.Parse(this.textBox2.Text));

                this.Close();
            }
        }
    }
}
