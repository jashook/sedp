////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net/hw11
// Proxy: lyle.smu.edu/~jshook/cgi-bin/proxy.py
//
// Module: AIProgram.cs
//
// 30-Sep-14: Version 1.0: Created
// 15-Oct-14: Version 1.1: Changed to accept the entire board as JSON
// 20-Oct-14: Version 1.1: Refactored
//
// Notes: 
//
// Simple class to start the server
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
using JS;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace A1
{
   public class AIProgram
   {
      //////////////////////////////////////////////////////////////////////////
      // Function: on_get_request
      //
      // Parameters:
      //
      // Dictionary<string, string> parameters: all of the get parameters
      //                                        passed to the function
      //
      // Return:
      //
      // string: the string to be serviced in the HTTP Response
      //////////////////////////////////////////////////////////////////////////
      public static string on_get_request(Dictionary<string, string> parameters)
      {
         // Use the default service to handle get requests

         return "";
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: on_post_request
      //
      // Parameters:
      //
      // Dictionary<string, string> parameters: all of the post parameters
      //                                        passed to the function
      //
      // Return:
      //
      // string: the string to be serviced in the HTTP Response
      //
      // Note:
      //
      // Unnamed key value pairs for post are inserted as "default"
      // all post information transfered in a way that is not url-x-encoded
      // is stored in they key "raw_data"
      //////////////////////////////////////////////////////////////////////////
      public static string on_post_request(Dictionary<string, string> parameters)
      {
         string json_input;

         // JSON is beingsent without a key
         // therefore it will be stored with the key "default"
         if (parameters.TryGetValue("default", out json_input) == false)
         {
            // Incorrect parameters
            
            return "";
         }

         InputJsonHalma2 input = JSON<InputJsonHalma2>.Parse(json_input);

         Piece[] pieces = input.pieces;
         Destination[] destinations = input.destinations;

         Piece[] enemy_pieces = input.enemy;
         Destination[] enemy_destinations = input.enemydestinations;

         Piece[] all_pieces = new Piece[pieces.Length + enemy_pieces.Length];

         int index = 0;

         foreach (Piece piece in pieces) all_pieces[index++] = piece;
         foreach (Piece piece in enemy_pieces) all_pieces[index++] = piece;

         Board board = new Board(input.boardSize, all_pieces, destinations, enemy_destinations);
         AI ai = new AI(board);

         Move move = ai.GetNextMove(pieces, all_pieces);

         return JSON<Move>.ToJSONString(move);

      }

      //////////////////////////////////////////////////////////////////////////
      // Function: Main
      //
      // Parameters:
      //
      // string[] args: command line arguments
      //
      // Return:
      //
      // void
      //////////////////////////////////////////////////////////////////////////
      static void Main(string[] args)
      {
         if (args.Length != 3)
         {
            Console.Write("Unexpected arguments provided, usage <exe> ");
            Console.Write("<path to listen on> ");
            Console.Write("<path of html file> ");
            Console.WriteLine("<path of resource folder>");

            return;
         }
           
         ServiceFile html_file = new ServiceFile(args[0], args[1]);
         ServiceFolder resource_folder = new ServiceFolder(args[2]);

         // Service the html file "/", and the folder on any get request
         // Only servicing these two locations guarentees security
         var collection = new ServiceCollection(html_file, resource_folder);

         // Start the server, listening at "/"
         new ev9.HttpEndPoint(collection, on_get_request, on_post_request);
      }

   } // end of class(AIProgram)
      
} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
