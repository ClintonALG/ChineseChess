using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Ma : Chess_Piece
    {
        public Ma(string color, Point vitri, Image image)
            : base("Ma", color, vitri, image) { }

        public override bool IsValidMove(Point newPosition, Chess_Piece[,] board)
        {
            int deltaX = Math.Abs(newPosition.X - Vitri.X);
            int deltaY = Math.Abs(newPosition.Y - Vitri.Y);

            // Ma di chuyển theo hình chữ "L", nghĩa là đi 2 ô theo 1 hướng và 1 ô theo hướng còn lại
            if ((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2))
            {
                // Kiểm tra xem có quân nào chặn đường của quân Mã không (nước cản)
                if (deltaX == 2)
                {
                    // Kiểm tra nước cản theo trục X
                    int blockX = (newPosition.X + Vitri.X) / 2;
                    if (board[blockX, Vitri.Y] != null) return false;
                }
                else if (deltaY == 2)
                {
                    // Kiểm tra nước cản theo trục Y
                    int blockY = (newPosition.Y + Vitri.Y) / 2;
                    if (board[Vitri.X, blockY] != null) return false;
                }

                // Kiểm tra vị trí mới không có quân cờ cùng màu
                Chess_Piece targetPiece = board[newPosition.X, newPosition.Y];
                if (targetPiece == null || !IsSameColor(targetPiece))
                {
                    return true;
                }

            }

            return false;
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Các nước đi theo hình chữ "L" của quân Mã
            Point[] potentialMoves = {
        new Point(Vitri.X + 2, Vitri.Y + 1),
        new Point(Vitri.X + 2, Vitri.Y - 1),
        new Point(Vitri.X - 2, Vitri.Y + 1),
        new Point(Vitri.X - 2, Vitri.Y - 1),
        new Point(Vitri.X + 1, Vitri.Y + 2),
        new Point(Vitri.X + 1, Vitri.Y - 2),
        new Point(Vitri.X - 1, Vitri.Y + 2),
        new Point(Vitri.X - 1, Vitri.Y - 2)
    };

            foreach (var move in potentialMoves)
            {
                if (move.X >= 0 && move.X < 9 && move.Y >= 0 && move.Y < 10) // Giới hạn trong bàn cờ
                {
                    if (IsValidMove(move, board))
                    {
                        // Thêm nước đi vào danh sách hợp lệ
                        validMoves.Add(move);
                    }
                }
            }

            return validMoves;
        }

    }
}
