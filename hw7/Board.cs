////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: Board.cs
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

class Board
{
   // Member Variables

   private Cell[,] Board;
   private Cell Destination;  // Default destination is first row, last column

   public Board(int size = 9, Cell Destination = new Cell(0,8))
   {
      Board = new Cell[size,size];

      // Set up the board as a collection of cells
      // Assign each cell its own number
      for (int x = 0; x < size; ++x)
      {
         for (int y = 0; y < size; ++y)
         {
            Board[x][y] = new Cell(x, y);
         }
      }
   }

   public void AddPiece(Piece point)
   {
      Board[point.X, point.Y] = point;
   }

   public void MovePiece(Piece point, Cell destination)
   {
      if (IsValidMove(point, destination) == false) return;      

      // Reset the cell (Removing the point)
      Board[point.X, point.Y] = new Cell(point.X, point.Y);

      Board[destination.X, destination.Y] = point;

      // Update the point
      point.X = destination.X;
      point.Y = destination.Y;
   }

   private bool IsValidMove(Piece point, Cell destination)
   {
      if (destination.X != point.X && destination.X + 1 != point.X)
      {
         // Not a valid move, return without moving
         return false;
      }
   
      if (destination.Y != point.Y && destination.Y + 1 != point.Y)
      {
         // Not a valid move, return without moving
         return false;
      }

      if (destination.X < 0 || destination.Y > Board.GetLength(0))
      {
         // Not inside the board
         return false;
      }

      if (destination.Y < 0 || destination.Y > Board.GetLength(1))
      {
         // Not inside the board
         return false;
      }

      return true;

   }

} // end of class(Cell)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(hw8)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
