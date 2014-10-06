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
      // Service Files

      const Tuple<string, string>[] service_files = new Tuple<string, string>[] {
         new Tuple<string, string>("hello.js", "C:\\Users\\Shook\\Source\\Repos\\cde\\data\\hello.js")
      };

      public static string on_get_request(Dictionary<string, string> parameters)
      {
         return "<h1>Hello World, this is a test.</h1>";
      }

      public static string on_post_request(Dictionary<string, string> parameters)
      {
         return "";
      }

      static void Main(string[] args)
      {
         // start the server, listening on "/"
         new HttpEndPoint("/", on_get_request, on_post_request);
      }
   }
}
