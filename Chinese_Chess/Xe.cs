using Chinese_Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public class Xe : Chess_Piece
    {
        public Xe(string color, Point position, Image image)
            : base("Xe", color, position, image) { }

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

            // Xe di chuyển theo hàng ngang hoặc dọc
            if (deltaX == 0 || deltaY == 0)
            {
                // Kiểm tra có quân cờ chắn giữa hai vị trí không
                if (IsPathClear(newPosition, board))
                {
                    Chess_Piece targetPiece = board[newPosition.X, newPosition.Y];
                    // Nước đi hợp lệ nếu ô trống hoặc có quân đối phương
                    return targetPiece == null || !IsSameColor(targetPiece);
                }
            }

            return false; // Nước đi không hợp lệ
        }

        private bool IsPathClear(Point newPosition, Chess_Piece[,] board)
        {
            int stepX = (newPosition.X - Vitri.X) == 0 ? 0 : (newPosition.X - Vitri.X) / Math.Abs(newPosition.X - Vitri.X);
            int stepY = (newPosition.Y - Vitri.Y) == 0 ? 0 : (newPosition.Y - Vitri.Y) / Math.Abs(newPosition.Y - Vitri.Y);

            // Kiểm tra các ô giữa vị trí hiện tại và vị trí mới
            int x = Vitri.X + stepX;
            int y = Vitri.Y + stepY;

            if (newPosition.X < 0 || newPosition.X >= 9 || newPosition.Y < 0 || newPosition.Y >= 10)
            {
                return false;
            }
            while (x != newPosition.X || y != newPosition.Y)
            {
                if (board[x, y] != null)
                {
                    return false; // Có quân cờ chắn
                }
                x += stepX;
                y += stepY;
            }

            return true; // Không có quân cờ chắn
        }

        public override List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            List<Point> validMoves = new List<Point>();

            // Các điểm di chuyển hợp lệ cho quân Xe
            for (int i = 0; i < board.GetLength(1); i++)
            {
                // Di chuyển theo hàng ngang
                Point horizontalMove = new Point(i, Vitri.Y);
                if (IsValidMove(horizontalMove, board))
                {
                    validMoves.Add(horizontalMove); // Thêm nước đi hợp lệ vào danh sách
                }

                // Di chuyển theo cột dọc
                Point verticalMove = new Point(Vitri.X, i);
                if (IsValidMove(verticalMove, board))
                {
                    validMoves.Add(verticalMove); // Thêm nước đi hợp lệ vào danh sách
                }
            }

            return validMoves;
        }

    }
}
