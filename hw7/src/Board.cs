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

   public Cell[,] Cells { get; private set; }
   public Cell DestinationUpperLeft { get; private set; }
   public Cell DestinationLowerRight { get; private set; }

   public Board(int size = 9, Cell upper_left_destination = null, Cell lower_right_destination = null)
   {
      Cells = new Cell[size, size];

      if (upper_left_destination == null)
      {
         // Default destination is first row, last column
         upper_left_destination = new Cell(0, 6);

         lower_right_destination = new Cell(2, 8);
      }

      // Set up the board as a collection of cells
      // Assign each cell its own number
      for (int x = 0; x < size; ++x)
      {
         for (int y = 0; y < size; ++y)
         {
            Cells[x, y] = new Cell(x, y);
         }
      }

      DestinationUpperLeft = upper_left_destination;
      DestinationLowerRight = lower_right_destination;
   }

   public void AddPiece(Piece point)
   {
      Cells[point.X, point.Y] = point;
   }

   public void MovePiece(Piece point, Cell destination)
   {
      if (IsValidMove(point, destination) == false) return;      

      // Reset the cell (Removing the point)
      Cells[point.X, point.Y] = new Cell(point.X, point.Y);

      Cells[destination.X, destination.Y] = point;

      // Update the point
      point.X = destination.X;
      point.Y = destination.Y;
   }

   //public void UpdateBoard(Move move)
   //{
   //  MovePiece((Piece)move.OriginalLocation, move.DestinationLocation);
   //}

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

      if (destination.X < 0 || destination.Y > Cells.GetLength(0))
      {
         // Not inside the board
         return false;
      }

      if (destination.Y < 0 || destination.Y > Cells.GetLength(1))
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
