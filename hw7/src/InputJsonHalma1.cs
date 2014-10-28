////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net/hw11
//
// Module: InputJsonHalma1.cs
//
// 30-Sep-14: Version 1.0: Created
// 20-Oct-14: Version 1.0: Refactored
//
// Notes:
//
// Only used to parse the incomming Json.
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace A1
{
   public class InputJsonHalma1
   {
      // Member Variables

      public Piece[] pieces { get; set; }
      public Piece[] destinations { get; set; }
      public int boardSize { get; set; }

      // Constructors

      public InputJsonHalma1()
      {

      }

      public InputJsonHalma1 (Piece[] pieces, Piece[] destinations, int board_size)
      {
         this.pieces = pieces;
         this.destinations = destinations;

         this.boardSize = board_size;
      }

   } // end of class (InputJson)

} // end of namespace (A1)

