using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chinese_Chess
{
    public class Phao : Chess_Piece
    {
        public Phao(string color, Point position, Image image)
            : base("Pháo", color, position, image) { }

        public override bool IsValidMove(Point newPosition, Chess_Piece[,] board)
        {
            int deltaX = Math.Abs(newPosition.X - Vitri.X);
            int deltaY = Math.Abs(newPosition.Y - Vitri.Y);

            // Kiểm tra vị trí mới có nằm trong giới hạn của mảng không
            if (newPosition.X < 0 || newPosition.X >= board.GetLength(0) ||
                newPosition.Y < 0 || newPosition.Y >= board.GetLength(1))
            {
                return false;
            }

            // Pháo có thể di chuyển thẳng (ngang hoặc dọc)
            if ((deltaX > 0 && deltaY == 0) || (deltaX == 0 && deltaY > 0))
            {
                // Biến đếm quân cờ ở giữa
                int countBetween = 0;
                Chess_Piece targetPiece = board[newPosition.X, newPosition.Y];

                // Kiểm tra hướng di chuyển
                if (deltaX > 0) // Di chuyển ngang
                {
                    for (int x = Math.Min(Vitri.X, newPosition.X) + 1; x < Math.Max(Vitri.X, newPosition.X); x++)
                    {
                        if (board[x, Vitri.Y] != null)
                        {
                            countBetween++; // Tăng đếm nếu có quân cờ chắn
                        }
                    }

                    // Nếu không có quân cờ chắn
                    if (countBetween == 0)
                    {
                        return targetPiece == null; // Nếu ô đích trống
                    }
                    else
                    {
                        // Nếu có quân cờ chắn, chỉ cho phép ăn quân đối phương
                        if (targetPiece != null && !IsSameColor(targetPiece) && countBetween == 1)
                        {
                            return true; // Nước đi hợp lệ để ăn quân cờ
                        }
                    }
                }
                else // Di chuyển dọc
                {
                    for (int y = Math.Min(Vitri.Y, newPosition.Y) + 1; y < Math.Max(Vitri.Y, newPosition.Y); y++)
                    {
                        if (board[Vitri.X, y] != null)
                        {
                            countBetween++; // Tăng đếm nếu có quân cờ chắn
                        }
                    }

                    // Nếu không có quân cờ chắn
                    if (countBetween == 0)
                    {
                        return targetPiece == null; // Nếu ô đích trống
                    }
                    else
                    {
                        // Nếu có quân cờ chắn, chỉ cho phép ăn quân đối phương
                        if (targetPiece != null && !IsSameColor(targetPiece) && countBetween == 1)
                        {
                            return true; // Nước đi hợp lệ để ăn quân cờ
                        }
                    }
                }
            }

            return false; // Nước đi không hợp lệ
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Các điểm di chuyển hợp lệ cho quân Pháo
            for (int y = 0; y < board.GetLength(1); y++)
            {
                // Di chuyển ngang
                Point moveHorizontal = new Point(Vitri.X, y);
                if (IsValidMove(moveHorizontal, board))
                {
                    // Thêm nước đi vào danh sách hợp lệ
                    validMoves.Add(moveHorizontal);
                }
            }

            for (int x = 0; x < board.GetLength(0); x++)
            {
                // Di chuyển dọc
                Point moveVertical = new Point(x, Vitri.Y);
                if (IsValidMove(moveVertical, board))
                {
                    // Thêm nước đi vào danh sách hợp lệ
                    validMoves.Add(moveVertical);
                }
            }

            return validMoves;
        }
    }
}
