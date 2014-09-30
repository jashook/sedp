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
      public static string on_get_request()
      {
         return "<h1>Hello World, this is a test.</h1>";
      }

      public static string on_post_request()
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
