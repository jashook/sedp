﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: Program.cs
//
// 30-Oct-14: Version 1.0: Created
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
using ev9;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace hw7 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class Program
{
   // Service Files

   private static ServiceCollection files =
      new ServiceCollection(Permission.READ_ONLY_ALL,
                            new ServiceFolder(@"C:\Users\Shook\Source\Repos\sedp\hw7\Eric C. Larson_files"),
                            new ServiceFile("/", @"C:\Users\Shook\Source\Repos\sedp\hw7\Eric C. Larson.htm")
                           );

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
      new ev9.HttpEndPoint(files, on_get_request, on_post_request);
   }

} // end of class(Program)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////