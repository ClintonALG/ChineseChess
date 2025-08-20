using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Voi : Chess_Piece
    {
        public Voi(string color, Point position, Image image)
            : base("Voi", color, position, image) { }

        public override bool IsValidMove(Point newPosition, Chess_Piece[,] board)
        {
            int deltaX = Math.Abs(newPosition.X - Vitri.X);
            int deltaY = Math.Abs(newPosition.Y - Vitri.Y);
            bool move = false;

            // Kiểm tra vị trí mới có nằm trong giới hạn của mảng không
            if (newPosition.X < 0 || newPosition.X >= board.GetLength(0) ||
                newPosition.Y < 0 || newPosition.Y >= board.GetLength(1))
            {
                return false;
            }
            // Kiểm tra quân Voi không vượt sông
            if (Vitri.Y < 5 && newPosition.Y >= 5 || Vitri.Y >= 5 && newPosition.Y < 5)
            {
                return false; // Nếu quân Voi di chuyển qua sông, nước đi không hợp lệ
            }

            // Voi chỉ di chuyển 2 ô chéo và không có quân cờ chắn giữa
            if (deltaX == 2 && deltaY == 2)
            {
                Point middlePosition = new Point((Vitri.X + newPosition.X) / 2, (Vitri.Y + newPosition.Y) / 2);
                if (board[middlePosition.X, middlePosition.Y] == null)
                {
                    // Nước đi hợp lệ nếu ô mới không có quân cờ cùng màu
                    Chess_Piece targetPiece = board[newPosition.X, newPosition.Y];
                    if (targetPiece == null || !IsSameColor(targetPiece))
                    {
                        move = true; // Nước đi hợp lệ
                    }
                }
            }

            return move;
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Các điểm di chuyển hợp lệ cho quân Voi
            Point[] possibleMoves = new Point[]
            {
        new Point(Vitri.X - 2, Vitri.Y - 2), // lên trái
        new Point(Vitri.X - 2, Vitri.Y + 2), // lên phải
        new Point(Vitri.X + 2, Vitri.Y - 2), // xuống trái
        new Point(Vitri.X + 2, Vitri.Y + 2)  // xuống phải
            };

            foreach (var move in possibleMoves)
            {
                // Kiểm tra nước đi có hợp lệ không
                if (IsValidMove(move, board))
                {
                    validMoves.Add(move); // Thêm nước đi hợp lệ vào danh sách
                }
            }

            return validMoves;
        }
    }
}
