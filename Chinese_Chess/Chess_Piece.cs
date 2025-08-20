using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    public abstract class Chess_Piece
    {
        public string Name { get; set; }      // Tên quân cờ
        public string Color { get; set; }     // Màu quân cờ ("Đỏ" hoặc "Đen")
        public Point Vitri { get; set; }      // Vị trí hiện tại trên bàn cờ
        public Image Image { get; set; }      // Hình ảnh của quân cờ

        public Chess_Piece(string name, string color, Point vitri, Image image)
        {
            Name = name;
            Color = color;
            Vitri = vitri;
            Image = image;
        }

        // Phương thức kiểm tra cho quy tắc di chuyển của quân cờ
        public abstract bool IsValidMove(Point newPosition, Chess_Piece[,] board);

        // Phương thức để lấy danh sách các vị trí hợp lệ
        public virtual List<Point> GetValidMoves(Chess_Piece[,] board)
        {
            return new List<Point>();
        }

        // Thay đổi vị trí quân cờ
        public void Move(Point newPosition)
        {
            Vitri = newPosition;
        }

        // Kiểm tra màu sắc của quân cờ
        public bool IsSameColor(Chess_Piece other)
        {
            return this.Color == other.Color;
        }

        // Phương thức kiểm tra Tướng có bị chiếu không
        public bool TuongInCheck(Chess_Piece[,] board)
        {
            Point tuongPosition = Point.Empty;

            // Tìm vị trí của Tướng
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] != null && board[x, y] is Tuong && IsSameColor(board[x, y])) // Kiểm tra màu
                    {
                        tuongPosition = new Point(x, y);
                        break;
                    }
                }
            }

            // Nếu không tìm thấy Tướng, trả về false (hoặc bạn có thể xử lý trường hợp này theo cách khác)
            if (tuongPosition == Point.Empty)
            {
                return false;
            }

            // Duyệt qua tất cả quân cờ của đối thủ
            foreach (var piece in board)
            {
                if (piece != null && !IsSameColor(piece))
                {
                    // Lấy danh sách các nước đi hợp lệ của quân cờ đối thủ
                    List<Point> validMoves = piece.GetValidMoves(board);

                    // Kiểm tra xem vị trí Tướng có nằm trong danh sách các nước đi hợp lệ không
                    if (validMoves.Contains(tuongPosition))
                    {
                        return true; // Tướng bị chiếu
                    }
                }
            }

            return false; // Không có quân cờ nào có thể tấn công Tướng
        }
        public virtual List<Point> GetSafeMoves(Chess_Piece[,] board)
        {
            List<Point> safeMoves = new List<Point>();

            // Lấy danh sách các nước đi hợp lệ
            List<Point> validMoves = GetValidMoves(board);

            // Duyệt qua từng nước đi hợp lệ
            foreach (var move in validMoves)
            {
                // Tạo bản sao của bàn cờ
                Chess_Piece[,] tempBoard = (Chess_Piece[,])board.Clone();

                // Di chuyển quân cờ tạm thời đến vị trí mới
                tempBoard[Vitri.X, Vitri.Y] = null; // Xóa quân cờ khỏi vị trí hiện tại
                tempBoard[move.X, move.Y] = this; // Di chuyển quân cờ đến vị trí mới

                // Kiểm tra xem Tướng có bị chiếu sau khi di chuyển không
                if (!TuongInCheck(tempBoard))
                {
                    safeMoves.Add(move); // Thêm nước đi vào danh sách nếu Tướng không bị chiếu
                }
            }

            return safeMoves;
        }
    }

}
