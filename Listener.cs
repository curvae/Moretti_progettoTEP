using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ServerProvaTask
{
    internal class Listener
    {
        public static string data = null;
        public static byte[] bytes = new Byte[1024];

        public static async void StartListening(int player_number, Label textboxinutileAEEW, Label label_output, Label label_output_2)
        {

            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);

            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Timeout : {0}", listener.ReceiveTimeout);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    ClientManager clientThread = new ClientManager(handler);

                    Task taskServingClient = Task.Run(() => { clientThread.doClient(handler, player_number, textboxinutileAEEW, label_output, label_output_2); });

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        
    }


    public class ClientManager
    {

        Socket clientSocket;
        byte[] bytes = new Byte[1024];
        String data = "";

        public ClientManager(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public void doClient(Socket handler, int player_number, Label textboxinutileAEEW, Label label_output, Label label_output_2)
        {

            int count = 0;
            byte[] msg;
            //handler.Send(Encoding.ASCII.GetBytes(Convert.ToString(player_number)));
            while (data != "Quit$")
            {
                
                data = "";
                while (data.IndexOf("$") == -1)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                }
                
                int l = data.Length;
                string lt1 = data.Substring(l - 3, 1);
                string lt2 = data.Substring(l - 2, 1);
                int l1 = 0;
                int l2 = 0;

                // 0 1 2 3 4 5 6 7 8 09 10
                // 1 2 3 4 5 6 7 8 9 10 11
                //                -3 -2 -1

                msg = Encoding.ASCII.GetBytes("");
                if (data.Substring(1, 1) == "w")
                {
                    if (data.Substring(0, 1) == "1")
                    {
                        //label_output.Text = "-5";
                        if(Convert.ToInt32(data.Substring(2, Convert.ToInt32(lt1)))>5)
                            label_output.Text = Convert.ToString(Convert.ToInt32(data.Substring(2, Convert.ToInt32(lt1))) - 5);
                    }
                    if (data.Substring(0, 1) == "2")
                    {
                        //label_output_2.Text = "-5";
                        if(Convert.ToInt32(data.Substring(2 + Convert.ToInt32(lt1), Convert.ToInt32(lt2)))>5)
                            label_output_2.Text = Convert.ToString(Convert.ToInt32(data.Substring(2 + Convert.ToInt32(lt1), Convert.ToInt32(lt2))) - 5);
                    }
                    l1 = label_output.Text.Length;
                    l2 = label_output_2.Text.Length;
                    msg = Encoding.ASCII.GetBytes(label_output.Text + label_output_2.Text + Convert.ToString(l1) + Convert.ToString(l2) + "$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text+"$");
                }
                if (data.Substring(1, 1) == "s")
                {
                    if (data.Substring(0, 1) == "1")
                    {
                        //label_output.Text = "+5";
                        if(Convert.ToInt32(data.Substring(2, Convert.ToInt32(lt1)))<300)
                            label_output.Text = Convert.ToString(Convert.ToInt32(data.Substring(2, Convert.ToInt32(lt1))) + 5);
                    }
                    if (data.Substring(0, 1) == "2")
                    {
                        //label_output_2.Text = "+5";
                        if(Convert.ToInt32(data.Substring(2 + Convert.ToInt32(lt1), Convert.ToInt32(lt2)))<300)
                            label_output_2.Text = Convert.ToString(Convert.ToInt32(data.Substring(2 + Convert.ToInt32(lt1), Convert.ToInt32(lt2))) + 5);
                    }
                    l1 = label_output.Text.Length;
                    l2 = label_output_2.Text.Length;
                    msg = Encoding.ASCII.GetBytes(label_output.Text + label_output_2.Text + Convert.ToString(l1) + Convert.ToString(l2) + "$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text + "$");
                }
                Console.WriteLine("Messaggio ricevuto : {0}", data);
                
                if (count == 0)
                {
                    msg = Encoding.ASCII.GetBytes(textboxinutileAEEW.Text + "$");
                    if (textboxinutileAEEW.Text == "2")
                        textboxinutileAEEW.Text = "3";
                    else
                        textboxinutileAEEW.Text = "2";
                    count++;
                }

                
                handler.Send(msg);
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            data = "";

        }
    }
}















/*
 int count = 0;
            byte[] msg;
            //handler.Send(Encoding.ASCII.GetBytes(Convert.ToString(player_number)));
            while (data != "Quit$")
            {
                count++;
                data = "";
                while (data.IndexOf("$") == -1)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                }

                
                msg = Encoding.ASCII.GetBytes("");
                if (data == "w$")
                {
                    msg = Encoding.ASCII.GetBytes("-5$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text+"$");
                }
                if (data == "s$")
                {
                    msg = Encoding.ASCII.GetBytes("+5$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text + "$");
                }
                Console.WriteLine("Messaggio ricevuto : {0}", data);
                
                if (count == 1)
                {
                    msg = Encoding.ASCII.GetBytes(textboxinutileAEEW.Text + "$");
                    textboxinutileAEEW.Text = "2";
                }


                handler.Send(msg);
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            data = "";
*/


/*  
         public void doClient(Socket handler, int player_number, Label textboxinutileAEEW, Label label_output, Label label_output_2)
        {

            int count = 0;
            byte[] msg;
            //handler.Send(Encoding.ASCII.GetBytes(Convert.ToString(player_number)));
            while (data != "Quit$")
            {
                
                data = "";
                while (data.IndexOf("$") == -1)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                }

                
                msg = Encoding.ASCII.GetBytes("");
                if (data == "w$")
                {
                    msg = Encoding.ASCII.GetBytes("-5+5$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text+"$");
                }
                if (data == "s$")
                {
                    msg = Encoding.ASCII.GetBytes("+5-5$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text + "$");
                }
                Console.WriteLine("Messaggio ricevuto : {0}", data);
                
                if (count == 0)
                {
                    msg = Encoding.ASCII.GetBytes(textboxinutileAEEW.Text + "$");
                    textboxinutileAEEW.Text = "2";
                    count++;
                }


                handler.Send(msg);
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            data = "";

        }
*/






/*
 public void doClient(Socket handler, int player_number, Label textboxinutileAEEW, Label label_output, Label label_output_2)
        {

            int count = 0;
            byte[] msg;
            //handler.Send(Encoding.ASCII.GetBytes(Convert.ToString(player_number)));
            while (data != "Quit$")
            {
                
                data = "";
                while (data.IndexOf("$") == -1)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                }

                
                msg = Encoding.ASCII.GetBytes("");
                if (data.Substring(1, 2) == "w$")
                {
                    if (data.Substring(0, 1) == "1")
                    {
                        label_output.Text = "-5";
                    }
                    if (data.Substring(0, 1) == "2")
                    {
                        label_output_2.Text = "-5";
                    }
                    msg = Encoding.ASCII.GetBytes(label_output.Text + label_output_2.Text + "$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text+"$");
                }
                if (data.Substring(1, 2) == "s$")
                {
                    if (data.Substring(0, 1) == "1")
                    {
                        label_output.Text = "+5";
                    }
                    if (data.Substring(0, 1) == "2")
                    {
                        label_output_2.Text = "+5";
                    }
                    msg = Encoding.ASCII.GetBytes(label_output.Text+label_output_2.Text+"$");
                    //msg = Encoding.ASCII.GetBytes(label_output.Text + "$");
                }
                Console.WriteLine("Messaggio ricevuto : {0}", data);
                
                if (count == 0)
                {
                    msg = Encoding.ASCII.GetBytes(textboxinutileAEEW.Text + "$");
                    if (textboxinutileAEEW.Text == "2")
                        textboxinutileAEEW.Text = "3";
                    textboxinutileAEEW.Text = "2";
                    count++;
                }

                
                handler.Send(msg);
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            data = "";

        }
*/