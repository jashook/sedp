////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: AI.cs
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

class AI
{
   private AI() { }

   public static Move NextMove(Board board, Piece[] pieces)
   {
      Cell upper_left = board.DestinationUpperLeft;
      Cell lower_right = board.DestinationLowerRight;

      // Total list of destinations
      List<Cell> destinations = new List<Cell>();
     
      for (int i = upper_left.X; i < board.Cells.GetLength(0); ++i)
      {
         for (int j = upper_left.Y; j < lower_right.Y + 1; ++j)
         {
            if (board.Cells[i, j].Occupied() == false)
            {
               destinations.Add(board.Cells[i, j]);
            }
         }
      }

      Cell best_destination = GetBestDestination(destinations);

      Piece moving_piece = GetDesiredMovingPiece(pieces, best_destination);

      if (moving_piece == null)
      {
         return null; // game is finished
      }

      Move move = DoMove(moving_piece, best_destination);

      return move;
   }

   static Move DoMove(Piece piece, Cell destination)
   {
      Cell old_cell = new Cell(piece.X, piece.Y);

      if (piece.X < destination.X)
      {
         piece.X = piece.X + 1;
      }
      if (piece.Y > destination.Y)
      {
         piece.Y = piece.Y - 1;
      }

      return new Move(old_cell, destination);
   }

   private static Cell GetBestDestination(List<Cell> destinations)
   {
      Cell best_destination = destinations[0];

      foreach (Cell destination in destinations)
      {
         // if the row is smaller and the column is larger
         // then it is a better destination
         if (destination.X < best_destination.X && destination.Y > best_destination.Y)
         {
            best_destination = destination;
         }
      }

      return best_destination;
   }

   private static Piece GetDesiredMovingPiece(Piece[] pieces, Cell destination)
   {
      // For now return the first piece

      foreach (Piece piece in pieces)
      {
         if (piece.X > destination.X && piece.Y < destination.Y)
         {
            return piece;
         }
      }

      return null;
   }

} // end of class(AI)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(hw8)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
