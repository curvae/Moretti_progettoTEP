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

namespace provaTask
{
    internal class SocketClient
    {
            public static void StartClient(int numPlayer,Panel player1, Panel player2, Label score_p1, Label lbl_input_p1, Label score_p2, Label lbl_input_p2, Panel panel_ball, Label label_score_p1, Label label_score_p2)
            {
                byte[] bytes = new byte[1024];
                int count = 0;

                try
                {
                    string data = "";

                    IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                    Random stringa_casuale = new Random();
                    string stringa_da_inviare = "";

                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    while (data != "Quit$")
                    {
                        stringa_da_inviare = score_p2.Text+lbl_input_p1.Text + Convert.ToString(player1.Top) + Convert.ToString(player2.Top) + Convert.ToString(player1.Top).Length + Convert.ToString(player2.Top).Length + "$";

                        byte[] msg = Encoding.ASCII.GetBytes(stringa_da_inviare);

                        int bytesSent = sender.Send(msg);
                        data = "";

                        while (data.IndexOf("$") == -1)
                        {
                            int bytesRec = sender.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }
                        string prova = "";

                        prova = data.Substring(0, data.IndexOf("$"));

                        if (prova != "1" && prova != "2")
                        {
                            int l = data.Length;
                            string lt1 = data.Substring(l - 3, 1);
                            string lt2 = data.Substring(l - 2, 1);
                            string lb1 = data.Substring(l - 5, 1);
                            string lb2 = data.Substring(l - 4, 1);

                            //prova = data.Substring(0,data.IndexOf(";"));
                            //prova2 = data.Substring(data.IndexOf(";")+1, data.IndexOf("$"));
                            //prova = data.Substring(0, 2);
                            //prova2 = data.Substring(2, 2);
                            player1.Top = Convert.ToInt32(data.Substring(0, Convert.ToInt32(lt1)));//player2.Top + Convert.ToInt32(prova2);
                            player2.Top = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1), Convert.ToInt32(lt2)));//player1.Top + Convert.ToInt32(prova);
                            //panel_ball.Left = Convert.ToInt32(data.Substring(Convert.ToInt32(lt2), Convert.ToInt32(lb1)));
                            lbl_input_p2.Text = data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2), Convert.ToInt32(lb1));
                            panel_ball.Left = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2), Convert.ToInt32(lb1)));
                            panel_ball.Top = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2) + Convert.ToInt32(lb1), Convert.ToInt32(lb2)));
                        }

                        if(panel_ball.Left > - 5 && panel_ball.Left < 5)
                            label_score_p2.Text = Convert.ToString(Convert.ToInt32(label_score_p2.Text) + 1);
                        if (panel_ball.Right > -5 && panel_ball.Right < 5)
                            label_score_p1.Text = Convert.ToString(Convert.ToInt32(label_score_p1.Text) + 1);

                        if (prova == "1")
                        {
                            score_p2.Text = prova;
                        }
                        if (prova == "2")
                        {
                            score_p2.Text = prova;
                        }
                        score_p1.Text = prova;
                        Console.WriteLine("Messaggio ricevuto: " + data);
                        System.Threading.Thread.Sleep(1);
                        count++;
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    /*
                     * catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }*/

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }


            public static void Receive(Socket sender, int bytesRec, string data, byte []bytes)
            {

            }





    }
}










/*
public static void StartClient(int numPlayer,Panel player1, Panel player2, Label score_p1, Label lbl_input_p1, Label score_p2, Label lbl_input_p2)
            {
                byte[] bytes = new byte[1024];
                int count = 0;

                try
                {
                    string data = "";

                    IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                    Random stringa_casuale = new Random();
                    string stringa_da_inviare = "";

                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    while (data != "Quit$")
                    {
                        stringa_da_inviare = lbl_input_p1.Text + "$";

                        byte[] msg = Encoding.ASCII.GetBytes(stringa_da_inviare);

                        int bytesSent = sender.Send(msg);
                        data = "";

                        while (data.IndexOf("$") == -1)
                        {
                            int bytesRec = sender.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }
                        string prova = "";

                        prova = data.Substring(0, data.IndexOf("$"));
                        
                        

                        player2.Top = player2.Top + Convert.ToInt32(prova);

                        player1.Top = player1.Top + Convert.ToInt32(prova);
                        
                        if (prova == "1")
                        {
                            score_p2.Text = prova;
                        }
                        if (prova == "2")
                        {
                            score_p2.Text = prova;
                        }
                        score_p1.Text = prova;
                        Console.WriteLine("Messaggio ricevuto: " + data);
                        System.Threading.Thread.Sleep(100);
                        count++;
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
*/





/*
 * public static void StartClient(int numPlayer,Panel player1, Panel player2, Label score_p1, Label lbl_input_p1, Label score_p2, Label lbl_input_p2)
            {
                byte[] bytes = new byte[1024];
                int count = 0;

                try
                {
                    string data = "";

                    IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                    Random stringa_casuale = new Random();
                    string stringa_da_inviare = "";

                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    while (data != "Quit$")
                    {
                        stringa_da_inviare = lbl_input_p1.Text + "$";

                        byte[] msg = Encoding.ASCII.GetBytes(stringa_da_inviare);

                        int bytesSent = sender.Send(msg);
                        data = "";

                        while (data.IndexOf("$") == -1)
                        {
                            int bytesRec = sender.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }
                        string prova = "";
                        string prova2 = "";

                        prova = data.Substring(0, data.IndexOf("$"));

                        if (prova != "1" && prova != "2")
                        {
                            prova = data.Substring(0,2);
                            prova2 = data.Substring(2,2);
                            player2.Top = player2.Top + Convert.ToInt32(prova2);
                            player1.Top = player1.Top + Convert.ToInt32(prova);
                        }
                        
                        if (prova == "1")
                        {
                            score_p2.Text = prova;
                        }
                        if (prova == "2")
                        {
                            score_p2.Text = prova;
                        }
                        score_p1.Text = prova;
                        Console.WriteLine("Messaggio ricevuto: " + data);
                        System.Threading.Thread.Sleep(100);
                        count++;
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
*/











/*
  public static void StartClient(int numPlayer,Panel player1, Panel player2, Label score_p1, Label lbl_input_p1, Label score_p2, Label lbl_input_p2)
            {
                byte[] bytes = new byte[1024];
                int count = 0;

                try
                {
                    string data = "";

                    IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);
                    Random stringa_casuale = new Random();
                    string stringa_da_inviare = "";

                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    while (data != "Quit$")
                    {
                        stringa_da_inviare = score_p2.Text+lbl_input_p1.Text + "$";

                        byte[] msg = Encoding.ASCII.GetBytes(stringa_da_inviare);

                        int bytesSent = sender.Send(msg);
                        data = "";

                        while (data.IndexOf("$") == -1)
                        {
                            int bytesRec = sender.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }
                        string prova = "";
                        string prova2 = "";

                        prova = data.Substring(0, data.IndexOf("$"));

                        if (prova != "1" && prova != "2")
                        {
                            prova = data.Substring(0,2);
                            prova2 = data.Substring(2,2);
                            player2.Top = player2.Top + Convert.ToInt32(prova2);
                            player1.Top = player1.Top + Convert.ToInt32(prova);
                        }
                        
                        if (prova == "1")
                        {
                            score_p2.Text = prova;
                        }
                        if (prova == "2")
                        {
                            score_p2.Text = prova;
                        }
                        score_p1.Text = prova;
                        Console.WriteLine("Messaggio ricevuto: " + data);
                        System.Threading.Thread.Sleep(100);
                        count++;
                    }

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
*/