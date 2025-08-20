using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Tot : Chess_Piece
    {
        public Tot(string color, Point position, Image image)
            : base("Tốt", color, position, image) { }

        public override bool IsValidMove(Point newPosition, Chess_Piece[,] board)
        {
            int deltaY = newPosition.Y - Vitri.Y; // Phân tích chiều dọc
            int deltaX = Math.Abs(newPosition.X - Vitri.X); // Phân tích chiều ngang

            // Kiểm tra vị trí mới có nằm trong giới hạn của mảng không
            if (newPosition.X < 0 || newPosition.X >= board.GetLength(0) || // 9 cột
                newPosition.Y < 0 || newPosition.Y >= board.GetLength(1)) // 10 hàng
            {
                return false;
            }

            // Tốt Đỏ chỉ di chuyển về phía 0 (xuống), Tốt Đen chỉ di chuyển về phía 9 (trên)
            if (Color == "Đỏ")
            {
                // Di chuyển thẳng 1 ô xuống
                if (deltaY == -1 && deltaX == 0)
                {
                    return board[newPosition.X, newPosition.Y] == null || !IsSameColor(board[newPosition.X, newPosition.Y]);
                }
                // Nếu đã vượt sông (y = 4), có thể di chuyển ngang
                else if (Vitri.Y <= 4 && deltaY == 0 && deltaX == 1)
                {
                    return board[newPosition.X, newPosition.Y] == null || !IsSameColor(board[newPosition.X, newPosition.Y]);
                }
            }
            else // Tốt Đen
            {
                // Di chuyển thẳng 1 ô lên
                if (deltaY == 1 && deltaX == 0)
                {
                    return board[newPosition.X, newPosition.Y] == null || !IsSameColor(board[newPosition.X, newPosition.Y]);
                }
                // Nếu đã vượt sông (y = 5), có thể di chuyển ngang
                else if (Vitri.Y >= 5 && deltaY == 0 && deltaX == 1)
                {
                    return board[newPosition.X, newPosition.Y] == null || !IsSameColor(board[newPosition.X, newPosition.Y]);
                }
            }

            return false; // Không hợp lệ
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Nếu quân Tốt chưa vượt sông
            if ((Color == "Đỏ" && Vitri.Y >= 5) || (Color == "Đen" && Vitri.Y <= 4))
            {
                // Di chuyển thẳng về phía trước
                Point forwardMove = new Point(Vitri.X, Vitri.Y - (Color == "Đỏ" ? 1 : -1));
                if (IsValidMove(forwardMove, board))
                {
                    validMoves.Add(forwardMove);
                }
            }
            else
            {
                // Nếu quân Tốt đã vượt sông, có thể di chuyển thẳng hoặc sang ngang
                Point forwardMove = new Point(Vitri.X, Vitri.Y - (Color == "Đỏ" ? 1 : -1)); // Di chuyển thẳng
                Point leftMove = new Point(Vitri.X - 1, Vitri.Y); // Di chuyển sang trái
                Point rightMove = new Point(Vitri.X + 1, Vitri.Y); // Di chuyển sang phải

                // Kiểm tra di chuyển thẳng
                if (IsValidMove(forwardMove, board))
                {
                    validMoves.Add(forwardMove);
                }

                // Kiểm tra di chuyển sang trái
                if (Vitri.X > 0) // Kiểm tra xem quân cờ không vượt ngoài biên trái
                {
                    if (IsValidMove(leftMove, board))
                    {
                        validMoves.Add(leftMove);
                    }
                }

                // Kiểm tra di chuyển sang phải
                if (Vitri.X < 8) // Kiểm tra xem quân cờ không vượt ngoài biên phải
                {
                    if (IsValidMove(rightMove, board))
                    {
                        validMoves.Add(rightMove);
                    }
                }
            }

            return validMoves;
        }


    }
}