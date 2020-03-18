using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulatorApp.Model
{
    class ClientSide
    {

        public static string Read(Socket sender)
        {
            // Data buffer 
            byte[] messageReceived = new byte[1024];
            // We receive the messagge using  
            // the method Receive(). This  
            // method returns number of bytes 
            // received, that we'll use to  
            // convert them to string 
            int byteRecv = sender.Receive(messageReceived);
            string message = Encoding.ASCII.GetString(messageReceived,0, byteRecv);

            Console.WriteLine("Message from Server -> {0}", message);
            return message;
        }
        public static void CloseSocket(Socket sender)
        {
            // Close Socket using  
            // the method Close() 
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        public static string Write(Socket sender, string message)
        {
            // Creation of messagge that 
            // we will send to Server
            message = "get '/controls/engines/current-engine/throttle'";
            byte[] messageSent = Encoding.ASCII.GetBytes(message);
            int byteSent = sender.Send(messageSent);
            return Read(sender);
        }
        // ExecuteClient() Method 
        public static Socket ExecuteClient(string ip, int port)
        {

            try
            {

                // Establish the remote endpoint  
                // for the socket. This  
                // uses a given port on a given ip. 
                IPAddress ipAddr = IPAddress.Parse(ip);
                IPEndPoint endPoint = new IPEndPoint(ipAddr, port);

                // Creation TCP/IP Socket using  
                // Socket Class Costructor 
                Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote  
                    // endpoint using method Connect() 
                    sender.Connect(endPoint);

                    // We print EndPoint information  
                    // that we are connected 
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());
                    //**************************************************//


                   


                }

                // Manage of Socket's Exceptions 
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
                return sender;
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}