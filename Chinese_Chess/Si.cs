using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Si : Chess_Piece
    {
        public Si(string color, Point vitri, Image image)
            : base("Si", color, vitri, image) { }

        public override bool IsValidMove(Point newPosition, Chess_Piece[,] board)
        {
            int deltaX = Math.Abs(newPosition.X - Vitri.X);
            int deltaY = Math.Abs(newPosition.Y - Vitri.Y);

            // Sĩ chỉ di chuyển chéo 1 ô và phải ở trong cung
            bool isInPalace = (newPosition.X >= 3 && newPosition.X <= 5 && ((Color == "Đỏ" && newPosition.Y >= 7) || (Color == "Đen" && newPosition.Y <= 2)));

            if (deltaX == 1 && deltaY == 1 && isInPalace)
            {
                // Kiểm tra ô mới không có quân cờ cùng màu
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

            // Các nước đi chéo 1 ô của quân Sĩ
            Point[] potentialMoves = {
        new Point(Vitri.X + 1, Vitri.Y + 1),
        new Point(Vitri.X + 1, Vitri.Y - 1),
        new Point(Vitri.X - 1, Vitri.Y + 1),
        new Point(Vitri.X - 1, Vitri.Y - 1)
    };

            foreach (var move in potentialMoves)
            {
                // Kiểm tra quân Sĩ có trong phạm vi cung hay không
                if (move.X >= 3 && move.X <= 5 &&
                    ((Color == "Đỏ" && move.Y >= 7 && move.Y <= 9) ||
                     (Color == "Đen" && move.Y >= 0 && move.Y <= 2)))
                {
                    // Kiểm tra nước đi có hợp lệ không
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
