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
            public static void StartClient(int numPlayer,Panel player1, Panel player2, Label score_p1, Label lbl_input_p1, Label score_p2, Label lbl_input_p2, Panel panel_ball, Label label_score_p1, Label label_score_p2, Panel panel_fine_partita, Label label1)
            {
                byte[] bytes = new byte[1024];
                int count = 0;

                try
                {
                    string data = "";
                    bool incScore = true;
                    bool nf = true;

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
                        stringa_da_inviare = score_p2.Text + lbl_input_p1.Text + Convert.ToString(player1.Top) + Convert.ToString(player2.Top) + Convert.ToString(player1.Top).Length + Convert.ToString(player2.Top).Length + "$";
                        if (label_score_p1.Text == "5" || label_score_p2.Text == "5")
                        {
                            stringa_da_inviare = "Quit$";
                            count = 0;

                            if ((score_p2.Text == "1" && label_score_p1.Text == "5") || (score_p2.Text == "2" && label_score_p2.Text == "5"))
                                label1.Text = "VITTORIA";
                            else
                                label1.Text = "SCONFITTA";  
                            panel_fine_partita.Visible = true;

                            System.Threading.Thread.Sleep(3000);
                            if (nf == true)
                            {
                                nf = false;
                                Form1 f1 = new Form1();
                                f1.ShowDialog();
                            }
                        }
                        byte[] msg = Encoding.ASCII.GetBytes(stringa_da_inviare);   //score_p2 = numero giocatore (1 o 2)   lbl_input_p1 = input del client

                        int bytesSent = sender.Send(msg);
                        data = "";

                        while (data.IndexOf("$") == -1)
                        {
                            int bytesRec = sender.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);      //"data" ora contiene il messaggio del server
                        }
                        string prova = "";

                        prova = data.Substring(0, data.IndexOf("$"));

                        if (prova != "1" && prova != "2")
                        {
                            int l = data.Length;
                            string lt1 = data.Substring(l - 3, 1);      //substring di "data" nelle posizioni dove sono presenti le lunghezze dei dati significativi del messaggio
                            string lt2 = data.Substring(l - 2, 1);      //es: lt2 conterrà la lunghezza del valore ".Top" del player2
                            string lb1 = data.Substring(l - 5, 1);
                            string lb2 = data.Substring(l - 4, 1);


                            player1.Top = Convert.ToInt32(data.Substring(0, Convert.ToInt32(lt1)));     //grazie alle lunghezze prima estrapolate ora si possono usare i dati inviati dal server per aggiornare la form del client
                            player2.Top = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1), Convert.ToInt32(lt2)));

                            //lbl_input_p2.Text = data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2), Convert.ToInt32(lb1));
                            panel_ball.Left = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2), Convert.ToInt32(lb1)));
                            panel_ball.Top = Convert.ToInt32(data.Substring(Convert.ToInt32(lt1) + Convert.ToInt32(lt2) + Convert.ToInt32(lb1), Convert.ToInt32(lb2)));
                        }

                        if (panel_ball.Left > 200 && panel_ball.Left < 400)
                            incScore = true;

                        if (panel_ball.Left > 5 && panel_ball.Left < 25 && incScore == true)        //incremento dello score
                        {
                            label_score_p2.Text = Convert.ToString(Convert.ToInt32(label_score_p2.Text) + 1);
                            incScore = false;
                        }
                        if (panel_ball.Left > 550 && panel_ball.Left < 570 && incScore == true)
                        {
                            label_score_p1.Text = Convert.ToString(Convert.ToInt32(label_score_p1.Text) + 1);
                            incScore = false;
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
                        System.Threading.Thread.Sleep(1);
                        count++;
                        if (data == "Quit$")
                        {
                            count = 0;
                        }
                    }
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    //label_score_p1.Text = "ciao";
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