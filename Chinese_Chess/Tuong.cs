using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Tuong : Chess_Piece
    {
        public Tuong(string color, Point position, Image image)
            : base("Tướng", color, position, image) { }

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

            // Tướng chỉ di chuyển ngang hoặc dọc 1 ô và phải ở trong cung
            bool isInPalace = (newPosition.X >= 3 && newPosition.X <= 5 &&
                              ((Color == "Đỏ" && newPosition.Y >= 7) ||
                               (Color == "Đen" && newPosition.Y <= 2)));

            // Kiểm tra nước đi cơ bản của Tướng (di chuyển ngang hoặc dọc 1 ô trong cung)
            if ((deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1))
            {
                if (isInPalace)
                {
                    // Kiểm tra ô mới không có quân cờ cùng màu
                    Chess_Piece targetPiece = board[newPosition.X, newPosition.Y];
                    if (targetPiece == null || !IsSameColor(targetPiece))
                    {
                        return true; // Nước đi hợp lệ
                    }
                }
            }
            return false;
        }

        private void CheckTouchingTuong(Chess_Piece[,] board, List<Point> validMoves)
        {
            // Kiểm tra vị trí Tướng đối phương trong cùng cột
            for (int y = 0; y < board.GetLength(1); y++)
            {
                Chess_Piece pieceAtPosition = board[Vitri.X, y];
                if (pieceAtPosition is Tuong && !IsSameColor(pieceAtPosition))
                {
                    // Kiểm tra không có quân cờ chắn giữa hai Tướng
                    bool isClearPath = true;
                    int startY = Math.Min(Vitri.Y, y) + 1; // Bắt đầu từ ô ngay dưới/ trên Tướng hiện tại
                    int endY = Math.Max(Vitri.Y, y); // Ô Tướng đối phương

                    for (int checkY = startY; checkY < endY; checkY++)
                    {
                        if (board[Vitri.X, checkY] != null)
                        {
                            isClearPath = false; // Có quân chắn
                            break;
                        }
                    }

                    // Nếu không có quân nào chắn thì thêm vị trí Tướng đối phương vào validMoves
                    if (isClearPath)
                    {
                        validMoves.Add(new Point(Vitri.X, y)); // Thêm vào danh sách điểm hợp lệ để ăn Tướng
                    }
                }
            }
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Các điểm di chuyển hợp lệ cho quân Tướng
            Point[] possibleMoves = new Point[]
            {
        new Point(Vitri.X - 1, Vitri.Y), // lên
        new Point(Vitri.X + 1, Vitri.Y), // xuống
        new Point(Vitri.X, Vitri.Y - 1), // trái
        new Point(Vitri.X, Vitri.Y + 1)  // phải
            };

            foreach (var move in possibleMoves)
            {
                // Kiểm tra nước đi có hợp lệ không
                if (IsValidMove(move, board))
                {
                    validMoves.Add(move); // Thêm vào danh sách nước đi hợp lệ
                }
            }

            // Kiểm tra xem có Tướng đối phương chạm mặt không
            CheckTouchingTuong(board, validMoves);

            return validMoves;
        }
    }
}



