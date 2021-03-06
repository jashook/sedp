﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net/hw11
// Proxy: lyle.smu.edu/~jshook/cgi-bin/proxy.py
//
// Module: Piece.cs
//
// 7-Oct-14: Version 1.0: Created
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace A1 
{
   public class Piece : Cell
   {
      public int team { get; set; }

      public Piece() : base(0,0)
      {

      }

      public Piece(int x, int y, int team) : base(x,y)
      {
         this.team = team;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: Occupied
      //
      // Return:
      //
      // bool: returns false
      //
      // Note:
      //
      // Anything inside a cell is expected to derive the cell class.
      // Therefore, their occupied function will be true.
      //////////////////////////////////////////////////////////////////////////
      public override bool Occupied()
      {
         return true;
      }

   } // end of class(Piece)

} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
/// 