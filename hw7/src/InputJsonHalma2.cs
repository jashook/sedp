﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net
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
      public Piece[] destinations { get; set; }
      public int boardSize { get; set; }
      public Piece[] enemy { get; set; }
      public Piece[] enemydestinations { get; set; }

      // Constructors

      public InputJsonHalma2()
      {

      }

      public InputJsonHalma2 (Piece[] pieces, 
                              Piece[] destinations, 
                              int board_s,
                              Piece[] enemy,
                              Piece[] enemydesinations
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

