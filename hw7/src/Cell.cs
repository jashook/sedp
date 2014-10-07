﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: Cell.cs
//
// 7-Oct-14: Version 1.0: Created
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace hw8 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class Cell
{
   // Member Variables

   public int X { get; set; }
   public int Y { get; set; }

   public Cell()
   {
      X = 0;
      Y = 0;
   }

   public Cell(int x, int y)
   {
      X = x;
      Y = y;
   }

   public virtual bool Occupied()
   {
      return false;
   }

} // end of class(Cell)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(hw8)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////