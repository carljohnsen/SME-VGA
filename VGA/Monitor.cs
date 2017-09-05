using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VGA
{
    public partial class Monitor : Form
    {
        public Monitor(int width, int height)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public void aoeu()
        { 
            Graphics graphics = CreateGraphics();
            Rectangle rect = new Rectangle(10, 10, 640, 480);
            graphics.DrawRectangle(Pens.Black, rect);
            button1.Text = "aoeu";
        }

        
    }
}
