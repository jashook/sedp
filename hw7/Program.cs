using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace hw7
{
   class Program
   {
      static void Main(string[] args)
      {
         Socket connecting_socket = null;
         IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 8080);
         connecting_socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

         Socket listening_socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

         bool failed = false;
         int port = 6780;

         IPEndPoint listening_endpoint = new IPEndPoint(IPAddress.Loopback, port);

         try
         {
            do
            {
               listening_socket.Bind(listening_endpoint);

            } while (failed);
         }

         catch(Exception e)
         {               
            listening_endpoint = new IPEndPoint(IPAddress.Loopback, ++port);

            failed = true;
         }

         string message = port.ToString() + ":/";

         Byte[] sent_message = Encoding.ASCII.GetBytes(message);

         // Max of 1024 waiting connections
         listening_socket.Listen(1024);

         connecting_socket.Connect(endpoint);
         connecting_socket.Send(sent_message, sent_message.Length, 0);
         connecting_socket.Disconnect(false);

         while(true)
         {
            Socket accepting_socket = listening_socket.Accept();

            string html = "<h1>Hello World :)</h1>";

            byte[] html_message = Encoding.ASCII.GetBytes(html);

            accepting_socket.Send(html_message, html_message.Length, 0);

            accepting_socket.Disconnect(false);

         }

      }
   }
}
