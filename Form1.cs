using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ServerProvaTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int player_number = 1;
            Task taskServingClient = Task.Run(() => { Listener.StartListening(player_number, textboxinutileAEEW, label_output, label_output_2, label_output_ball_top, label_output_ball_left); });
        }

        private void label_output_Click(object sender, EventArgs e)
        {

        }
    }
}
