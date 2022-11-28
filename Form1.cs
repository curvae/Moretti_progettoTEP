using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace provaTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task t = Task.Run(() => { movePanel(); });
        }

        public void movePanel()
        {
            
            Invoke(new MethodInvoker(() =>
            {
                panel1.Top = 100;
                for(int i=0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    panel1.Top = panel1.Top + i;
                }
            }));
        }
    }
    


    
}
