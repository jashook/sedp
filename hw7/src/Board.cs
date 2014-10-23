////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net
//
// Module: Board.cs
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
   public class Board
   {
      // Member Variables

      public Cell[,] Cells { get; private set; }
      public Cell[] Destinations { get; set; }
      public Cell LowerLeftDestination { get; set; }

      public int Size { get; set; }

      // Constructor

      public Board(int size, Piece[] pieces, Piece[] destinations)
      {
         Cells = new Cell[size, size];
         Size = size;
         Destinations = new Cell[destinations.Length];

         int index = 0;

         foreach (Piece piece in destinations)
         {
            Destinations[index++] = new Cell(piece.x, piece.y);

            if (LowerLeftDestination == null)
            {
               LowerLeftDestination = Destinations[index - 1];
            }

            else if (Destinations[index - 1].x <= LowerLeftDestination.x && Destinations[index - 1].y >= LowerLeftDestination.y)
            {
               LowerLeftDestination = Destinations[index - 1];
            }
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

         foreach (Piece piece in pieces)
         {
            AddPiece(piece);
         }
      }

      // Public member Functions

      //////////////////////////////////////////////////////////////////////////
      // Function: AddPiece
      //
      // Parameters:
      //
      // Piece point: piece to add to the board
      //
      // Return:
      //
      // void
      //////////////////////////////////////////////////////////////////////////
      public void AddPiece(Piece point)
      {
         Cells[point.x, point.y] = point;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsDestination
      //
      // Parameters:
      //
      // Piece piece: piece to check against destinations
      //
      // Return:
      //
      // bool: true if is a destination, false if not
      //////////////////////////////////////////////////////////////////////////
      public bool IsDestination(Piece piece)
      {
         foreach (Cell cell in Destinations)
         {
            if (piece.x == cell.x && piece.y == cell.y)
            {
               return true;
            }
         }

         return false;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsEmptyLocation
      //
      // Parameters:
      //
      // Piece piece: piece to check
      //
      // Return:
      //
      // bool: true if there is no piece in this cell, false if occupied
      //
      // Note:
      //
      // Occupied is an overloaded function in both cell and piece.
      // If the cell is a piece, it will return true, else it is false.
      //////////////////////////////////////////////////////////////////////////
      public bool IsEmptyLocation(Piece piece)
      {
         return !Cells[piece.x, piece.y].Occupied();
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsPreferredLocation
      //
      // Parameters:
      //
      // Piece piece: piece to check
      //
      // Return:
      //
      // bool: true if the piece is not past the destination, false otherwise
      //////////////////////////////////////////////////////////////////////////
      public bool IsPreferredLocation(Piece piece)
      {
         // Make sure pieces do not go past the destinations
         if (IsValidLocation(piece))
         {

            return IsDestination(piece) || 
                  (piece.x <= 
                   LowerLeftDestination.x && 
                   piece.y >= LowerLeftDestination.y
                  );
         }

         else
         {
            return false;
         }
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsValidLocation
      //
      // Parameters:
      //
      // Piece piece: piece to check
      //
      // Return:
      //
      // bool: true if the cell is inside the board, false otherwise
      //////////////////////////////////////////////////////////////////////////
      public bool IsValidLocation(Piece piece)
      {
         return (piece.x >= 0 && piece.x <= Size && piece.y <= Size && piece.y >= 0);
      }

   } // end of class(Board)

} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
