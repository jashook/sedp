////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: HttpEndPoint.cs
//
// 30-Oct-14: Version 1.0: Created
// 30-Oct-14: Version 1.0: Support for creating an HttpEndpoint
//
// Notes:
//
// Create a new endpoint and start listening on that EndPoint
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace ev9 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class HttpEndPoint
{
   public delegate string OnRequest(Dictionary<string, string> parameters);

   private OnRequest m_get_request_function;
   private OnRequest m_post_request_function;
   private string m_url;

   private ServiceFile[] m_files;

   public HttpEndPoint(string url, OnRequest get_request, OnRequest post_request, ServiceFile[] files)
   {
      m_get_request_function = get_request;
      m_post_request_function = post_request;

      m_url = url;
      m_files = files;

      Thread t = new Thread(new ThreadStart(Connect));

      t.Start();
      t.Join();
   }

   public void Connect()
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

      catch
      {
         listening_endpoint = new IPEndPoint(IPAddress.Loopback, ++port);

         failed = true;
      }

      string message = port.ToString() + ":" + m_url;

      foreach (ServiceFile file in m_files)
      {
         message = message + "&" + port.ToString() + ":" + file.GetName();
      }

      Byte[] sent_message = Encoding.ASCII.GetBytes(message);

      // Max of 1024 waiting connections
      listening_socket.Listen(1024);

      try
      {
         connecting_socket.Connect(endpoint);
      }

      catch
      {
         Console.WriteLine("Unable to Connect to Server, please check if it is running.");
      }
         
      connecting_socket.Send(sent_message, sent_message.Length, 0);
      connecting_socket.Disconnect(false);

      while (true)
      {
         Socket accepting_socket = listening_socket.Accept();

         byte[] pre_run_information_buffer = new byte[256];

         int pre_run_amount_read = 0;

         string pre_run_information = "";

         do
         {
            pre_run_amount_read = accepting_socket.Receive(pre_run_information_buffer, pre_run_information_buffer.Length, 0);

            pre_run_information = pre_run_information + Encoding.ASCII.GetString(pre_run_information_buffer, 0, pre_run_amount_read);

         } while (pre_run_amount_read > 0);

         Dictionary<string, string> parameters;
         string url;

         string raw_output = "";

         int method = ParseType(pre_run_information, out parameters, out url, ref raw_output);

         parameters.Add("raw_output", raw_output);

         string service_file = ServiceFile(url);

         string html = "";

         if (service_file != "")
         {
            html = service_file;
         }

         else
         {
            html = method == 0 ? m_get_request_function(parameters) : m_post_request_function(parameters);
         }

         byte[] html_message = Encoding.ASCII.GetBytes(html);

         accepting_socket = listening_socket.Accept();

         accepting_socket.Send(html_message, html_message.Length, 0);

         accepting_socket.Disconnect(false);
      }
   }

   private int ParseType(string transfered_information, out Dictionary<string, string> parameters, out string url, ref string raw_ouput)
   {
      parameters = new Dictionary<string, string>();

      int count;

      for (count = 0; transfered_information[count] != '?' && count < transfered_information.Length; ++count);

      url = transfered_information.Substring(0, count);

      transfered_information = transfered_information.Substring(count + 1);

      char method_char = transfered_information[0];

      int method = method_char - '0';

      transfered_information = transfered_information.Substring(1);

      char raw_char = transfered_information[0];

      int raw = raw_char - '0';

      transfered_information = transfered_information.Substring(1);

      if (transfered_information.Length > 0) return method;

      if (raw == 1)
      {
         raw_ouput = transfered_information;

         return method;
      }

      if (transfered_information == "")
      {
         return method;
      }

      string[] split_information = transfered_information.Split(new char[1] { '&' });

      foreach (string key_value in split_information)
      {
         string[] key_values = key_value.Split(new char[1] { '=' });

         parameters.Add(key_values[0], key_values[1]);
      }

      return method;
   }

   private string ReadFile(ServiceFile file)
   {
      return System.IO.File.ReadAllText(file.GetFilePath());
   }

   private string ServiceFile(string file)
   {
      ServiceFile s_file = null;
      bool found = false;

      foreach (ServiceFile p_file in m_files)
      {
         if (p_file.GetName() == file)
         {
            found = true;

            s_file = p_file;

            break;
         }
      }

      if (found == false) return "";

      string return_file;

      switch (s_file.GetFilePermission())
      {
         case ev9.ServiceFile.FilePermission.READ_ONLY_ALL:
            return_file = ReadFile(s_file);

            break;
         
         case ev9.ServiceFile.FilePermission.READ_ONLY_AUTHORIZED:
            return_file = "";

            break;
         case ev9.ServiceFile.FilePermission.READ_ONLY_LOCAL:
            return_file = "";

            break;
         case ev9.ServiceFile.FilePermission.READ_WRITE_ALL:
            return_file = ReadFile(s_file);

            break;
         case ev9.ServiceFile.FilePermission.READ_WRITE_AUTHORIZED:
            return_file = "";

            break;
         case ev9.ServiceFile.FilePermission.READ_WRITE_LOCAL:
            return_file = "";

            break;
         case ev9.ServiceFile.FilePermission.HIDDEN:
            return_file = "";

            break;
         default:
            return_file = "";

            break;
      }

      return return_file;
   }

} // end of class(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////