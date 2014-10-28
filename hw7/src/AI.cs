////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// Author: Jarret Shook
//
// URL: ev9.cloudapp.net
//
// Module: AI.cs
//
// 07-Oct-14: Version 1.0: Created
// 14-Oct-14: Version 1.0: Changed to namespace A1
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace A1
{
   public class AI
   {
      // Private Delegates
      private delegate void MovePieceDelegate(Piece piece);

      // Member Variables
      public Board GameBoard { get; set; }
      public Destination[] Destination { get; set; }

      // Constructor

      public AI(Board board) 
      {
         GameBoard = board;
      }

      // Public Member Functions

      //////////////////////////////////////////////////////////////////////////
      // Function: GetNextMove
      //
      // Parameters:
      //
      // Piece[] pieces: all the pieces of the AI
      //
      // Return:
      //
      // Move: the move that the AI has chosen
      //////////////////////////////////////////////////////////////////////////
      public Move GetNextMove(Piece[] pieces, Piece[] all_pieces)
      {
         int team = pieces[0].team;

         Destination[] game_board_destinations;

         game_board_destinations = GameBoard.Team0Destinations;

         Destination[] all_destinations = game_board_destinations;

         List<Destination> unoccupied_destinations;
         Move move;

         // Loop while the move is not ideal
         do
         {
            unoccupied_destinations = new List<Destination>();

            foreach (Destination cell in all_destinations)
            {
               if (cell.Occupied() == false)
               {
                  unoccupied_destinations.Add(cell);
               }
            }

            Destination best_destination;
            best_destination = GetBestDestination(unoccupied_destinations);

            Piece moving_piece = GetPreferredMovingPiece(pieces, 
                                                         best_destination,
                                                         all_pieces
                                                        );

            if (moving_piece == null)
            {
               moving_piece = GetMovingPiece(pieces, 
                                             best_destination,
                                             all_pieces
                                            );
            }

            if (moving_piece == null)
            {
               return null; // game is finished
            }

            move = DoMove(moving_piece, best_destination, all_pieces);

         } while(move == null && unoccupied_destinations.Count > 0);

         return move;
      }

      // Private member functions

      //////////////////////////////////////////////////////////////////////////
      // Function: DoMove
      //
      // Parameters:
      //
      // Piece piece: the piece the AI is trying to move
      // Cell destination: the destination the piece is moving towards
      // Piece[] pieces: array of all the pieces
      //
      // Return:
      //
      // Move: The best possible move of this piece
      //////////////////////////////////////////////////////////////////////////
      private Move DoMove(Piece piece, Destination destination, Piece[] pieces)
      {
         // Prefer a jump, therefore check for it first

         Piece moving_piece = new Piece(piece.x, piece.y, piece.team);
         List<Piece> possible_jumps = GetJump(pieces, piece, destination);

         Move move = null;

         if (possible_jumps != null)
         {
            move = new Move(piece, possible_jumps.ToArray());
         }

         if (possible_jumps == null)
         {
            moving_piece = DoMoveWithoutJump(pieces, piece, destination);

            move = new Move(piece, new Cell[] { moving_piece });
         }

         return move;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: DoMoveWithoutJump
      //
      // Parameters:
      //
      // Piece[] pieces: array of all the pieces
      // Piece current_piece: piece that will be moving
      // Cell destination: 
      //
      // Return:
      //
      // Piece: the new piece's location
      //
      // Note:
      //
      // Attempts to move the piece in three different possible directions
      // Then chooses the best possible move
      //////////////////////////////////////////////////////////////////////////
      private Piece DoMoveWithoutJump(Piece[] pieces, 
                                      Piece current_piece, 
                                      Cell destation
                                     )
      {
         Piece up_piece = null; 
         Piece diagnal_piece = null;
         Piece horizontal_piece = null;

         int team = pieces[0].team;

         if (current_piece.y > destation.y)
         {
            up_piece = new Piece(current_piece.x, 
                                 current_piece.y - 1, 
                                 current_piece.team
                                );
         }

         if (current_piece.x < destation.x)
         {
            if (team == 0)
            {
               horizontal_piece = new Piece(current_piece.x + 1, 
                                            current_piece.y, 
                                            current_piece.team
                                           );
            }
            else
            {
               horizontal_piece = new Piece(current_piece.x - 1, 
                                            current_piece.y, 
                                            current_piece.team
                                           );
            }

            // If can move in both locations then up_piece will not be null
            if (up_piece!= null)
            {
               int new_x;
               int new_y;

               if (team == 0)
               {
                  new_x = current_piece.x + 1;
                  new_y = current_piece.y - 1;
               }

               else
               {
                  new_x = current_piece.x + 1;
                  new_y = current_piece.y - 1;
               }

               diagnal_piece = new Piece(new_x, new_y, current_piece.team);
            }
         }

         if (diagnal_piece != null)
         {
            if (GameBoard.IsPreferredLocation(diagnal_piece) && 
                GameBoard.IsEmptyLocation(diagnal_piece)
               )
            {
               return diagnal_piece;
            }
         }

         if (up_piece != null)
         {
            if (GameBoard.IsPreferredLocation(up_piece) && 
                GameBoard.IsEmptyLocation(up_piece)
               )
            {
               return up_piece;
            }
         }

         if (horizontal_piece != null)
         {
            if (GameBoard.IsPreferredLocation(horizontal_piece) && 
                GameBoard.IsEmptyLocation(horizontal_piece)
               )
            {
               return horizontal_piece;
            }
         }

         return null;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: GetBestDestination
      //
      // Parameters:
      //
      // List<Cell> destinations
      //
      // Return:
      //
      // Cell: the prefered location
      //
      // Note:
      //
      // Returns the lowest-left destination
      //////////////////////////////////////////////////////////////////////////
      private Destination GetBestDestination(List<Destination> destinations)
      {
         Destination best_destination = destinations[0];

         int team = best_destination.team;

         best_destination = GameBoard.BestDestination;

         return best_destination;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: GetPreferredMovingPiece
      //
      // Parameters:
      //
      // Piece[] pieces: all the pieces
      // Cell destination: where the piece will be moving to
      //
      // Return:
      //
      // Piece: the piece with the best move
      ////////////////////////////////////////////////////////////////////////// 
      private Piece GetPreferredMovingPiece(Piece[] pieces, 
                                            Destination destination,
                                            Piece[] all_pieces
                                           )
      {
         Piece piece_with_largest_jumps = null;
         List<Piece> largest_possible_jump = null;

         // Return the piece with the most possible jumps
         foreach (Piece piece in pieces)
         {
            List<Piece> jumping_piece = GetJump(all_pieces, piece, destination);

            if (jumping_piece != null)
            {
               if (largest_possible_jump != null && 
                   largest_possible_jump.Count < jumping_piece.Count
                  )
               {
                  largest_possible_jump = jumping_piece;

                  piece_with_largest_jumps = piece;
               }

               else if (largest_possible_jump == null)
               {
                  piece_with_largest_jumps = piece;
                  largest_possible_jump = jumping_piece;
               }
            }
         }

         return piece_with_largest_jumps;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: GetMovingPiece
      //
      // Parameters:
      //
      // Piece[] pieces: all the pieces
      // Cell destination: where the piece will be moving to
      //
      // Return:
      //
      // Piece: a piece that can move
      //////////////////////////////////////////////////////////////////////////
      private Piece GetMovingPiece(Piece[] pieces, 
                                   Cell destination,
                                   Piece[] all_pieces
                                  )
      {
         // For now return the first piece that can move
         foreach (Piece piece in pieces)
         {
            if (IsPossibleToMove(all_pieces, piece, destination))
            {
               return piece;
            }
         }

         return null;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: GetJump
      //
      // Parameters:
      //
      // Piece[] pieces: all the pieces
      // Piece piece: the moving piece
      //
      // Return:
      //
      // List<Piece>: All of the jumps done, null if impossibe to jump
      //////////////////////////////////////////////////////////////////////////
      private List<Piece> GetJump(Piece[] pieces, Piece piece, Destination dest)
      {
         Piece up_piece = new Piece(piece.x, piece.y - 1, piece.team);
         Piece diagnal_piece;
         Piece horizontal_piece;

         if (IsUpperRight(dest))
         {
            diagnal_piece = new Piece(piece.x + 1, piece.y - 1, piece.team);
            horizontal_piece = new Piece(piece.x + 1, piece.y, piece.team);
         }

         else
         {
            diagnal_piece = new Piece(piece.x - 1, piece.y - 1, piece.team);
            horizontal_piece = new Piece(piece.x - 1, piece.y, piece.team);
         }

         List<Piece> jump_list = new List<Piece>();

         MovePieceDelegate do_diagnal_move;
         MovePieceDelegate do_up_move;
         MovePieceDelegate do_horizontal_move;

         do_up_move = (current_piece) => { --current_piece.y; };

         if (IsUpperRight(dest))
         {
            do_diagnal_move = 
               (current_piece) => { ++current_piece.x; --current_piece.y; };
               

            do_horizontal_move = (current_piece) => { ++current_piece.x; };
         }

         else
         {
            do_diagnal_move = 
               (current_piece) => { --current_piece.x; --current_piece.y; };


            do_horizontal_move = (current_piece) => { --current_piece.x; };
         }

         bool is_valid_jump = false;

         do
         {
            // Reset all the pieces
            if (jump_list.Count > 0)
            {
               int new_x = jump_list[jump_list.Count - 1].x;
               int new_y = jump_list[jump_list.Count - 1].y;

               diagnal_piece.x = up_piece.x = horizontal_piece.x = new_x;
               diagnal_piece.y = up_piece.y = horizontal_piece.y = new_y;

               do_diagnal_move(diagnal_piece);
               do_up_move(up_piece);
               do_horizontal_move(horizontal_piece);
            }

            is_valid_jump = false;

            if (IsValidJump(pieces, diagnal_piece, do_diagnal_move))
            {
               jump_list.Add(new Piece(diagnal_piece.x, 
                                       diagnal_piece.y, 
                                       piece.team)
                            );

               is_valid_jump = true;

               continue;
            }

            if (IsValidJump(pieces, up_piece, do_up_move))
            {
               jump_list.Add(new Piece(up_piece.x, up_piece.y, piece.team));

               is_valid_jump = true;

               continue;
            }

            if (IsValidJump(pieces, horizontal_piece, do_horizontal_move))
            {
               jump_list.Add(new Piece(horizontal_piece.x, 
                                       horizontal_piece.y, 
                                       piece.team)
                            );

               is_valid_jump = true;

               continue;
            }

         } while (is_valid_jump);

         return jump_list.Count == 0 ? null : jump_list;
      }

      //////////////////////////////////////////////////////////////////////////
      // Function: IsPossibleToMove
      //
      // Parameters:
      //
      // Piece[] pieces: all the pieces
      // Piece current_piece: the moving piece
      // Cell destination: the destination cell
      //
      // Return:
      //
      // bool: returns whether it is possible to move or not
      //////////////////////////////////////////////////////////////////////////
      private bool IsPossibleToMove(Piece[] pieces, 
                                    Piece current_piece, 
                                    Cell destation
                                   )
      {
         if (DoMoveWithoutJump(pieces, current_piece, destation) != null)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      private bool IsUpperRight(Destination destination)
      {
         return destination.x > 3;
      }

      private bool IsValidJump(Piece[] pieces, 
                               Piece current_piece,
                               MovePieceDelegate do_move,
                               bool start = true
                              )
      {
         foreach(Piece jumping_piece in pieces)
         {
            if (jumping_piece.x == current_piece.x && 
                jumping_piece.y == current_piece.y
               )
            {
               if (start)
               {
                  do_move(current_piece);

                  return IsValidJump(pieces, current_piece, do_move, false);
               }

               else
               {
                  // On top of a piece, cannot jump.

                  return false;
               }
            }
         }

         if (start)
         {
            return false;
         }

         else if(GameBoard.IsValidLocation(current_piece) && 
                 GameBoard.IsPreferredLocation(current_piece)
                )
         {
            return true;
         }

         else
         {
            return false;
         }

      }

   } // end of class(AI)

} // end of namespace(A1)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
