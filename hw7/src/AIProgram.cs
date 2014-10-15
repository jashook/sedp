////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: AIProgram.cs
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
using JS;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace hw8 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class AIProgram
{
   private static Board s_board = new Board();
   private static Piece[] s_pieces = new Piece[9];

   public static string on_get_request(Dictionary<string, string> parameters)
   {
      // Get request should not happen

      return "";
   }

   public static string on_post_request(Dictionary<string, string> parameters)
   {
      string move_as_json;

      if (parameters.TryGetValue("move", out move_as_json) == false)
      {
         // Incorrect parameters
         
         return "";
      }

      //Move move = JSON<Move>.Parse(move_as_json);

      //s_board.UpdateBoard(move);

      Move move = AI.NextMove(s_board, s_pieces);

      return JSON<Move>.ToJSONString(move);

   }

   static void Main(string[] args)
   {
      //int count = 0;

      //for (int x = 0; x < 3; ++x)
      //{
      //   for (int y = 6; y < 9; ++y)
      //   {
      //      s_pieces[count++] = new Piece(x, y, 0);
      //   }
      //}

      //test();

      ServiceCollection collection =
            new ServiceCollection(new ServiceFile("/", @"/Users/jarret/Projects/hw11/hw11/Halma UI.htm"),
                                  new ServiceFolder(@"/Users/jarret/Projects/hw11/hw11/ui_files")
                                 );

      new ev9.HttpEndPoint(collection, on_get_request, on_post_request);
   }

   public static void test()
   {
      Move move;

      Dictionary<string, string> parameters = null;

      do
      {
         move = JSON<Move>.Parse(on_post_request(parameters));

         Console.WriteLine(move.OriginalLocation);

      } while (move != null);
   }

} // end of class(Program)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
