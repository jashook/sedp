﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net/hw11
// Proxy: lyle.smu.edu/~jshook/cgi-bin/proxy.py
//
// Module: Destination.cs
//
// 07-Oct-14: Version 1.0: Created
// 20-Oct-14: Version 1.0: Refactored
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace A1
{
   public class Destination : Cell
   {
      public int team { get; set; }

      public Destination() : base(0,0)
      {
         
      }

      public Destination(int x, int y, int team) : base(x, y)
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
      public virtual bool Occupied()
      {
         return false;
      }

   } // end of class(Cell)

} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
