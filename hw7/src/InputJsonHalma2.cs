////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net/hw11
// Proxy: lyle.smu.edu/~jshook/cgi-bin/proxy.py
//
// Module: InputJsonHalma2.cs
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
   public class InputJsonHalma2
   {
      // Member Variables

      public Piece[] pieces { get; set; }
      public Destination[] destinations { get; set; }
      public int boardSize { get; set; }
      public Piece[] enemy { get; set; }
      public Destination[] enemydestinations { get; set; }

      // Constructors

      public InputJsonHalma2()
      {

      }

      public InputJsonHalma2 (Piece[] pieces, 
                              Destination[] destinations, 
                              int board_size,
                              Piece[] enemy,
                              Destination[] enemydesinations
                             )
      {
         this.pieces = pieces;
         this.destinations = destinations;

         this.boardSize = board_size;
         this.enemy = enemy;
         this.enemydestinations = enemydestinations;
      }

   } // end of class (InputJson)

} // end of namespace (A1)

