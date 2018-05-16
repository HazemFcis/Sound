using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sound
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Source = src.Text;
            string Dest = dest.Text;
            int SizeFolder = int.Parse(sizeFolder.Text);
            sound obj = new sound(comboBox1.SelectedIndex, Source, Dest, SizeFolder);
            obj.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
