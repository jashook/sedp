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
      public Destination[] Team0Destinations { get; set; }
      public Destination[] Team1Destinations { get; set; }
      public Cell LowerLeftDestination { get; set; }
      public Cell LowerRightDestination { get; set; }
      public int Size { get; set; }

      // Constructor

      public Board(int size, 
                   Piece[] pieces, 
                   Destination[] first_team_destinations,
                   Destination[] second_team_destinations
                  )
      {
         Cells = new Cell[size, size];
         Size = size;
         Team0Destinations = first_team_destinations;

         int index = 0;

         Destination first_team_destination = first_team_destinations[0];

         if (first_team_destination.x < 3)
         {
            foreach (Destination piece in first_team_destinations)
            {
               if (LowerRightDestination == null)
               {
                  LowerRightDestination = Team0Destinations[index - 1];
               }

               else if (Team0Destinations[index - 1].x <= LowerLeftDestination.x &&
                        Team0Destinations[index - 1].y >= LowerLeftDestination.y
                       )
               {
                  LowerRightDestination = Team0Destinations[index - 1];
               }
            }
         }

         else
         {
            foreach (Destination piece in second_team_destinations)
            {
               if (LowerLeftDestination == null)
               {
                  LowerLeftDestination = Team0Destinations[index - 1];
               }

               else if (Team0Destinations[index - 1].x <= LowerLeftDestination.x && 
                        Team0Destinations[index - 1].y >= LowerLeftDestination.y
                       )
               {
                  LowerLeftDestination = Team0Destinations[index - 1];
               }
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
      // Function: IsTeam0Destination
      //
      // Parameters:
      //
      // Piece piece: piece to check against destinations
      //
      // Return:
      //
      // bool: true if is a destination of team 0, false if not
      //////////////////////////////////////////////////////////////////////////
      public bool IsTeam0Destination(Piece piece)
      {
         foreach (Destination cell in Team0Destinations)
         {
            if (piece.x == cell.x && piece.y == cell.y)
            {
               return true;
            }
         }

         return false;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsTeam1Destination
      //
      // Parameters:
      //
      // Piece piece: piece to check against destinations
      //
      // Return:
      //
      // bool: true if is a destination of team 1, false if not
      //////////////////////////////////////////////////////////////////////////
      public bool IsTeam1Destination(Piece piece)
      {
         foreach (Destination cell in Team1Destinations)
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
         int team = piece.team;

         // Make sure pieces do not go past the destinations
         if (IsValidLocation(piece))
         {

            if (team == 0)
            {
               return IsTeam0Destination(piece) ||
                      (piece.x <= LowerLeftDestination.x &&
                       piece.y >= LowerLeftDestination.y
                      );
            }

            else
            {
               return IsTeam1Destination(piece) ||
                      (piece.x >= LowerRightDestination.x &&
                       piece.y >= LowerRightDestination.y
                      );
            }
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
         return (piece.x >= 0 && piece.x 
                 <= Size && 
                 piece.y <= Size && 
                 piece.y >= 0
                );
      }

   } // end of class(Board)

} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
