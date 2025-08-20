using Chinese_Chess;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    public class Board
    {
        public bool Turn_Den = true; //Biến xác định lượt
        private Chess_Piece selectedPiece; // Biến lưu quân cờ được chọn
        public bool Computermode = false;
        public string AI_color;
        public string opColor;
        public bool player_1 = false;
        public bool player_2 = false;
        public bool computerlose = false;

        private List<Button> possibleMoveButtons = new List<Button>(); // Lưu trữ các nút nước đi có thể

        public Chess_Piece[,] board;                        // Ma trận bàn cờ 9x10
        private Panel panelBoard;                            // Panel chứa bàn cờ và các quân cờ
        private Dictionary<Chess_Piece, Button> pieceBoxes; // Lưu trữ các PictureBox của quân cờ
        /*public Stack<(Chess_Piece piece, Point oldPosition, Point newPosition, Chess_Piece capturedPiece)> moveHistory;*/  // Lưu trữ các nước đi trước của quân cờ
        public Stack<(Chess_Piece piece, Point oldPosition, Point newPosition, Chess_Piece capturedPiece)> moveHistory = new Stack<(Chess_Piece, Point, Point, Chess_Piece)>();


        private string username;
        private int lv_depth;

        private DataGridView dgvMoveHistory = new DataGridView();

        MyDB db = new MyDB();

        public Board(Panel panel, bool player_1, bool player_2, string user, int level, string AI_color, DataGridView dgvMoveHistory)
        {
            this.dgvMoveHistory = dgvMoveHistory;

            this.panelBoard = panel;

            // Khởi tạo bàn cờ 9 cột và 10 hàng
            board = new Chess_Piece[9, 10];
            pieceBoxes = new Dictionary<Chess_Piece, Button>();
            moveHistory = new Stack<(Chess_Piece piece, Point oldPosition, Point newPosition, Chess_Piece capturedPiece)>();

            this.username = user;
            this.lv_depth = level;

            panelBoard.Controls.Clear();
            LoadBoardImage(); // Tải ảnh bàn cờ
            InitializeBoard(); // Khởi tạo quân cờ
            this.player_1 = player_1;
            this.player_2 = player_2;
            if (player_1 && player_2) { Computermode = false; }
            else
            {
                Computermode = true;
                this.AI_color = AI_color;
                if (AI_color == "Đen")
                    opColor = "Đỏ";
                else opColor = "Đen";

            }
            if (Computermode) // Chỉ thực hiện nếu lượt của máy
            {
                if (Turn_Den != player_1)
                {
                    ComputerMove();
                }
            }
        }

        // Tải ảnh bàn cờ
        public Chess_Piece GetPieceAt(int x, int y)
        {
            //Xác định vị trí 
            return board[x, y];
        }

        private void LoadBoardImage()
        {
            PictureBox boardPictureBox = new PictureBox
            {
                Size = new Size(716, 796), // Kích thước của bàn cờ
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent // Nếu bạn cần nền trong suốt
            };

            Image boardImage = Image.FromFile("..\\..\\..\\Images\\board_chinese_chess.jpg"); // Đường dẫn tới ảnh bàn cờ
            boardPictureBox.Image = boardImage;
            panelBoard.Controls.Add(boardPictureBox); // Thêm vào panel
        }

        // Khởi tạo quân cờ và đặt chúng vào các vị trí trên bàn cờ
        public void InitializeBoard()
        {
            //Thêm bên đỏ 

            Image tuong_doImage = Image.FromFile("..\\..\\..\\Images\\tuong_do.png");
            Chess_Piece tuongRed = new Tuong("Đỏ", new Point(4, 9), tuong_doImage);
            board[4, 9] = tuongRed;
            AddPieceToBoard(tuongRed);// Thêm quân cờ vào bàn cờ

            Image xe_doImage = Image.FromFile("..\\..\\..\\Images\\xe_do.png");
            Chess_Piece xeRed1 = new Xe("Đỏ", new Point(0, 9), xe_doImage);
            board[0, 9] = xeRed1;
            AddPieceToBoard(xeRed1);
            Chess_Piece xeRed2 = new Xe("Đỏ", new Point(8, 9), xe_doImage);
            board[8, 9] = xeRed2;
            AddPieceToBoard(xeRed2);

            Image ma_doImage = Image.FromFile("..\\..\\..\\Images\\ma_do.png");
            Chess_Piece maRed1 = new Ma("Đỏ", new Point(1, 9), ma_doImage);
            board[1, 9] = maRed1;
            AddPieceToBoard(maRed1);
            Chess_Piece maRed2 = new Ma("Đỏ", new Point(7, 9), ma_doImage);
            board[7, 9] = maRed2;
            AddPieceToBoard(maRed2);

            Image voi_doImage = Image.FromFile("..\\..\\..\\Images\\voi_do.png");
            Chess_Piece voiRed1 = new Voi("Đỏ", new Point(2, 9), voi_doImage);
            board[2, 9] = voiRed1;
            AddPieceToBoard(voiRed1);
            Chess_Piece voiRed2 = new Voi("Đỏ", new Point(6, 9), voi_doImage);
            board[6, 9] = voiRed2;
            AddPieceToBoard(voiRed2);

            Image si_doImage = Image.FromFile("..\\..\\..\\Images\\si_do.png");
            Chess_Piece siRed1 = new Si("Đỏ", new Point(3, 9), si_doImage);
            board[3, 9] = siRed1;
            AddPieceToBoard(siRed1);
            Chess_Piece siRed2 = new Si("Đỏ", new Point(5, 9), si_doImage);
            board[5, 9] = siRed2;
            AddPieceToBoard(siRed2);

            Image phao_doImage = Image.FromFile("..\\..\\..\\Images\\phao_do.png");
            Chess_Piece phaoRed1 = new Phao("Đỏ", new Point(1, 7), phao_doImage);
            board[1, 7] = phaoRed1;
            AddPieceToBoard(phaoRed1);
            Chess_Piece phaoRed2 = new Phao("Đỏ", new Point(7, 7), phao_doImage);
            board[7, 7] = phaoRed2;
            AddPieceToBoard(phaoRed2);

            Image tot_doImage = Image.FromFile("..\\..\\..\\Images\\tot_do.png");
            for (int i = 0; i < 10; i += 2)
            {
                Chess_Piece totRed = new Tot("Đỏ", new Point(i, 6), tot_doImage);
                board[i, 6] = totRed;
                AddPieceToBoard(totRed);
            }

            //Thêm bên đen

            Image tuong_denImage = Image.FromFile("..\\..\\..\\Images\\tuong_den.png");
            Chess_Piece tuongBlack = new Tuong("Đen", new Point(4, 0), tuong_denImage);
            board[4, 0] = tuongBlack;
            AddPieceToBoard(tuongBlack);// Thêm quân cờ vào bàn cờ

            Image xe_denImage = Image.FromFile("..\\..\\..\\Images\\xe_den.png");
            Chess_Piece xeBlack1 = new Xe("Đen", new Point(0, 0), xe_denImage);
            board[0, 0] = xeBlack1;
            AddPieceToBoard(xeBlack1);
            Chess_Piece xeBlack2 = new Xe("Đen", new Point(8, 0), xe_denImage);
            board[8, 0] = xeBlack2;
            AddPieceToBoard(xeBlack2);

            Image ma_denImage = Image.FromFile("..\\..\\..\\Images\\ma_den.png");
            Chess_Piece maBlack1 = new Ma("Đen", new Point(1, 0), ma_denImage);
            board[1, 0] = maBlack1;
            AddPieceToBoard(maBlack1);
            Chess_Piece maBlack2 = new Ma("Đen", new Point(7, 0), ma_denImage);
            board[7, 0] = maBlack2;
            AddPieceToBoard(maBlack2);

            Image voi_denImage = Image.FromFile("..\\..\\..\\Images\\voi_den.png");
            Chess_Piece voiBlack1 = new Voi("Đen", new Point(2, 0), voi_denImage);
            board[2, 0] = voiBlack1;
            AddPieceToBoard(voiBlack1);
            Chess_Piece voiBlack2 = new Voi("Đen", new Point(6, 0), voi_denImage);
            board[6, 0] = voiBlack2;
            AddPieceToBoard(voiBlack2);

            Image si_denImage = Image.FromFile("..\\..\\..\\Images\\si_den.png");
            Chess_Piece siBlack1 = new Si("Đen", new Point(3, 0), si_denImage);
            board[3, 0] = siBlack1;
            AddPieceToBoard(siBlack1);
            Chess_Piece siBlack2 = new Si("Đen", new Point(5, 0), si_denImage);
            board[5, 0] = siBlack2;
            AddPieceToBoard(siBlack2);

            Image phao_denImage = Image.FromFile("..\\..\\..\\Images\\phao_den.png");
            Chess_Piece phaoBlack1 = new Phao("Đen", new Point(1, 2), phao_denImage);
            board[1, 2] = phaoBlack1;
            AddPieceToBoard(phaoBlack1);
            Chess_Piece phaoBlack2 = new Phao("Đen", new Point(7, 2), phao_denImage);
            board[7, 2] = phaoBlack2;
            AddPieceToBoard(phaoBlack2);

            Image tot_denImage = Image.FromFile("..\\..\\..\\Images\\tot_den.png");
            for (int i = 0; i < 10; i += 2)
            {
                Chess_Piece totBlack = new Tot("Đen", new Point(i, 3), tot_denImage);
                board[i, 3] = totBlack;
                AddPieceToBoard(totBlack);
            }

        }

        private void HighlightValidMoves(Chess_Piece piece)
        {
            // Lấy danh sách các vị trí an toàn
            var safeMoves = piece.GetSafeMoves(board); // Giả sử bạn có biến board đại diện cho bàn cờ

            // Duyệt qua danh sách các vị trí an toàn
            foreach (var move in safeMoves)
            {
                Button moveButton = new Button
                {
                    Size = new Size(80, 80),
                    Location = new Point(move.X * 80, (9 - move.Y) * 80),
                    BackColor = Color.Green, // Màu để chỉ ra các vị trí có thể di chuyển
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Tag = move // Lưu vị trí của nước đi trong thuộc tính Tag
                };

                moveButton.Click += (sender, e) =>
                {
                    var btn = sender as Button;
                    Point targetPosition = (Point)btn.Tag;

                    MovePiece(piece.Vitri.X, piece.Vitri.Y, targetPosition.X, targetPosition.Y, dgvMoveHistory); // Di chuyển quân cờ

                    ClearMoveHints(); // Xóa các gợi ý di chuyển
                    selectedPiece = null; // Bỏ chọn quân cờ
                };

                panelBoard.Controls.Add(moveButton); // Thêm nút vào bảng
                moveButton.BringToFront(); // Đưa nút lên phía trước
            }
        }


        // Phương thức xóa các nút chỉ ra nước đi có thể
        private void ClearMoveHints()
        {
            var moveButtons = panelBoard.Controls.OfType<Button>().Where(b => b.BackColor == Color.Green).ToList();
            foreach (var btn in moveButtons)
            {
                panelBoard.Controls.Remove(btn); // Xóa các nút gợi ý di chuyển
                btn.Dispose();
            }
        }

        //Tạo quân cờ mới
        private Button CreatePieceButton(Chess_Piece piece)
        {
            Button pieceButton = new Button
            {
                Size = new Size(80, 80),
                Location = new Point(piece.Vitri.X * 80, (9 - piece.Vitri.Y) * 80),
                BackgroundImage = piece.Image,
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                FlatAppearance = { BorderSize = 0 }
            };

            pieceButton.Paint += (sender, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, pieceButton.Width, pieceButton.Height);
                    pieceButton.Region = new Region(path);
                }

                if (selectedPiece == piece)
                {
                    using (Pen pen = new Pen(Color.Yellow, 3))
                    {
                        e.Graphics.DrawEllipse(pen, 0, 0, pieceButton.Width, pieceButton.Height);
                    }
                }
            };

            pieceButton.Click += (sender, e) =>
            {
                if (selectedPiece == piece)
                {
                    ClearMoveHints();
                    selectedPiece = null;
                }
                else
                {
                    ClearMoveHints();
                    selectedPiece = piece;
                    if ((Turn_Den && piece.Color == "Đen") || (!Turn_Den && piece.Color == "Đỏ"))
                    {
                        HighlightValidMoves(piece);
                    }
                }

                foreach (var button in pieceBoxes.Values)
                {
                    button.Invalidate();
                }
                pieceButton.Invalidate();
            };

            return pieceButton;
        }


        // Thêm quân cờ vào Panel (hiển thị quân cờ)
        private void AddPieceToBoard(Chess_Piece piece)
        {
            Button pieceButton = CreatePieceButton(piece);
            panelBoard.Controls.Add(pieceButton);
            pieceBoxes[piece] = pieceButton;
            pieceButton.BringToFront();
        }


        // Phương thức di chuyển và ăn quân cờ
        public void MovePiece(int startX, int startY, int endX, int endY, DataGridView dgvMoveHistory)
        {
            Chess_Piece piece = board[startX, startY];
            Chess_Piece targetPiece = board[endX, endY]; // Quân cờ tại vị trí đích

            if (piece != null)
            {
                // Lấy danh sách các nước đi an toàn cho quân cờ
                List<Point> safeMoves = piece.GetSafeMoves(board);

                // Kiểm tra nếu nước đi đích nằm trong danh sách nước đi an toàn
                if (safeMoves.Contains(new Point(endX, endY)))
                {
                    // Nếu vị trí đích có quân cờ đối phương
                    if (targetPiece != null && targetPiece.Color != piece.Color)
                    {
                        // Xóa quân cờ bị ăn
                        panelBoard.Controls.Remove(pieceBoxes[targetPiece]);
                        pieceBoxes.Remove(targetPiece);
                    }

                    // Lưu lại bước di chuyển trước đó vào stack
                    moveHistory.Push((piece, new Point(startX, startY), new Point(endX, endY), targetPiece));

                    // Di chuyển quân cờ
                    piece.Move(new Point(endX, endY));
                    board[endX, endY] = piece;
                    board[startX, startY] = null; // Xóa quân cờ khỏi vị trí cũ

                    // Cập nhật giao diện (di chuyển quân cờ trên Panel)
                    UpdatePieceOnBoard(piece);
                    Turn_Den = !Turn_Den; // Đổi lượt 

                    // Xác định phe hiện tại
                    string playerSide = Turn_Den ? "Đỏ" : "Đen";

                    // Cập nhật lịch sử nước đi vào DataGridView
                    string pieceName = piece.GetType().Name; // Hoặc phương thức nào đó để lấy tên quân cờ
                    string oldPosition = $"{(char)(startX + 65)}{startY + 1}"; // Tạo vị trí cũ (ví dụ: A5)
                    string newPosition = $"{(char)(endX + 65)}{endY + 1}"; // Tạo vị trí mới (ví dụ: G5)
                    if (dgvMoveHistory != null)
                    {
                        dgvMoveHistory.Rows.Add(playerSide, pieceName, oldPosition, newPosition);
                    }

                    // Kiểm tra xem bên đối phương đã hết nước đi chưa
                    if (CheckForGameOver(piece.Color) == 1) // Kiểm tra xem bên đối phương đã hết nước đi
                    {
                        MessageBox.Show($"Bên {piece.Color} đã thắng!");
                        // Kết thúc trò chơi && thực hiện hành động cần thiết
                        if (Computermode)
                        {
                            if (piece.Color == "Đen")
                            {
                                string query = "UPDATE ketqua SET win = win + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
                                int add = db.getNonQuery(query);
                            }
                            else
                            {
                                string query = "UPDATE ketqua SET lose = lose + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
                                int add = db.getNonQuery(query);
                            }
                        }
                    }
                    // Kiểm tra xem hòa chưa
                    if (CheckForGameOver(piece.Color) == 0)
                    {
                        MessageBox.Show("HÒA <3");
                        // Kết thúc trò chơi && thực hiện hành động cần thiết
                        if (Computermode)
                        {
                            string query = "UPDATE ketqua SET draw = draw + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
                            int add = db.getNonQuery(query);
                        }
                    }
                }
            }
            if (Computermode) // Chỉ thực hiện nếu lượt của máy
            {
                if (AI_color == "Đen")
                {
                    if (Turn_Den == true)
                    {
                        ComputerMove();
                    }
                }
                if (AI_color == "Đỏ")
                {
                    if (Turn_Den == false)
                    {
                        ComputerMove();
                    }
                }
            }
        }

        //public void MovePiece(int startX, int startY, int endX, int endY)
        //{
        //    Chess_Piece piece = board[startX, startY];
        //    Chess_Piece targetPiece = board[endX, endY]; // Quân cờ tại vị trí đích

        //    if (piece != null)
        //    {
        //        // Lấy danh sách các nước đi an toàn cho quân cờ
        //        List<Point> safeMoves = piece.GetSafeMoves(board);

        //        // Kiểm tra nếu nước đi đích nằm trong danh sách nước đi an toàn
        //        if (safeMoves.Contains(new Point(endX, endY)))
        //        {
        //            // Nếu vị trí đích có quân cờ đối phương
        //            if (targetPiece != null && targetPiece.Color != piece.Color)
        //            {
        //                // Xóa quân cờ bị ăn
        //                panelBoard.Controls.Remove(pieceBoxes[targetPiece]);
        //                pieceBoxes.Remove(targetPiece);
        //            }

        //            // Lưu lại bước di chuyển trước đó vào stack
        //            moveHistory.Push((piece, new Point(startX, startY), new Point(endX, endY), targetPiece));

        //            // Di chuyển quân cờ
        //            piece.Move(new Point(endX, endY));
        //            board[endX, endY] = piece;
        //            board[startX, startY] = null; // Xóa quân cờ khỏi vị trí cũ

        //            // Cập nhật giao diện (di chuyển quân cờ trên Panel)
        //            UpdatePieceOnBoard(piece);
        //            Turn_Den = !Turn_Den; // Đổi lượt 

        //            // Kiểm tra xem bên đối phương đã hết nước đi chưa
        //            if (CheckForGameOver(piece.Color) == 1) // Kiểm tra xem bên đối phương đã hết nước đi
        //            {
        //                MessageBox.Show($"Bên {piece.Color} đã thắng!");
        //                // Kết thúc trò chơi && thực hiện hành động cần thiết
        //                if (Computermode)
        //                {
        //                    if (piece.Color == "Đen")
        //                    {
        //                        string query = "UPDATE ketqua SET win = win + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
        //                        int add = db.getNonQuery(query);
        //                    }
        //                    else
        //                    {
        //                        string query = "UPDATE ketqua SET lose = lose + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
        //                        int add = db.getNonQuery(query);
        //                    }
        //                }
        //            }
        //            // Kiểm tra xem hòa chưa
        //            if (CheckForGameOver(piece.Color) == 0)
        //            {
        //                MessageBox.Show("HÒA <3");
        //                // Kết thúc trò chơi && thực hiện hành động cần thiết
        //                if (Computermode)
        //                {
        //                    string query = "UPDATE ketqua SET draw = draw + 1 WHERE username = '" + username + "' AND lv_depth =" + lv_depth;
        //                    int add = db.getNonQuery(query);
        //                }
        //            }
        //        }
        //    }
        //    if (Computermode) // Chỉ thực hiện nếu lượt của máy
        //    {
        //        if (AI_color == "Đen")
        //        {
        //            if (Turn_Den == true)
        //            {
        //                ComputerMove();
        //            }
        //        }
        //        if (AI_color == "Đỏ")
        //        {
        //            if (Turn_Den == false)
        //            {
        //                ComputerMove();
        //            }
        //        }
        //    }
        //}

        public bool IsMovablePiece(Chess_Piece piece)
        {
            // Kiểm tra các quân loại Tốt, Xe, Pháo, Mã có thể qua sông
            return piece is Tot || piece is Xe || piece is Phao || piece is Ma;
        }
        public int CheckForGameOver(string color)
        {
            // Kiểm tra xem bên đối phương có còn nước đi hợp lệ không
            string opponentColor = (color == "Đỏ") ? "Đen" : "Đỏ";
            bool opponentHasMoves = false;
            bool playerHasMoves = false;
            bool opponentHasMovablePieces = false;
            bool playerHasMovablePieces = false;

            foreach (var piece in pieceBoxes.Keys)
            {
                if (piece.Color == opponentColor)
                {
                    // Kiểm tra quân đối phương có thuộc loại có thể qua sông không
                    if (IsMovablePiece(piece))
                    {
                        opponentHasMovablePieces = true;
                    }
                }
                else if (piece.Color == color)
                {
                    // Kiểm tra quân người chơi có thuộc loại có thể qua sông không
                    if (IsMovablePiece(piece))
                    {
                        playerHasMovablePieces = true;
                    }
                }
            }

            // Nếu cả hai bên đều không còn quân có thể qua sông, kết thúc với hòa
            if (!opponentHasMovablePieces && !playerHasMovablePieces)
            {
                return 0; // Trò chơi kết thúc với hòa
            }

            foreach (var piece in pieceBoxes.Keys)
            {
                if (piece.Color == opponentColor)
                {
                    var safeMoves = piece.GetSafeMoves(board);
                    if (safeMoves.Count > 0) // Nếu quân cờ đối phương còn nước đi hợp lệ
                    {
                        opponentHasMoves = true;
                    }
                }
                else if (piece.Color == color)
                {
                    var safeMoves = piece.GetSafeMoves(board);
                    if (safeMoves.Count > 0) // Nếu quân cờ của người chơi còn nước đi hợp lệ
                    {
                        playerHasMoves = true;
                    }
                }
            }

            if (!opponentHasMoves && !playerHasMoves)
            {
                return 0; // Trò chơi kết thúc với hòa
            }
            else if (!opponentHasMoves)
            {
                return 1; // Người chơi thắng
            }

            return -1; // Trò chơi chưa kết thúc
        }




        // Phương thức đi lại quân cờ
        public void Undo(int sobuoc)
        {
            if (moveHistory.Count == 0)
            {
                MessageBox.Show("Không có bước nào để quay lại!");
                return;
            }

            for (int i = 0; i < sobuoc; i++)
            {
                if (moveHistory.Count > 0)
                {
                    var lastMove = moveHistory.Pop();

                    Chess_Piece piece = lastMove.piece;
                    Point oldPosition = lastMove.oldPosition;
                    Point newPosition = lastMove.newPosition;
                    Chess_Piece capturedPiece = lastMove.capturedPiece;

                    // Khôi phục trạng thái bàn cờ
                    board[oldPosition.X, oldPosition.Y] = piece;
                    board[newPosition.X, newPosition.Y] = capturedPiece;

                    piece.Move(oldPosition);

                    if (capturedPiece != null)
                    {
                        capturedPiece.Move(newPosition);

                        if (!pieceBoxes.ContainsKey(capturedPiece))
                        {
                            Button capturedPieceButton = CreatePieceButton(capturedPiece);
                            pieceBoxes[capturedPiece] = capturedPieceButton;
                        }

                        panelBoard.Controls.Add(pieceBoxes[capturedPiece]);
                        pieceBoxes[capturedPiece].BringToFront();
                    }

                    UpdatePieceOnBoard(piece);
                    Turn_Den = !Turn_Den;

                    // Xóa dòng cuối trong DataGridView
                    if (dgvMoveHistory.Rows.Count > 0)
                    {
                        // Kết thúc trạng thái chỉnh sửa nếu có
                        if (dgvMoveHistory.IsCurrentCellInEditMode)
                        {
                            dgvMoveHistory.EndEdit(); // Kết thúc chỉnh sửa
                            dgvMoveHistory.CommitEdit(DataGridViewDataErrorContexts.Commit); // Commit dữ liệu
                        }

                        // Xóa dòng cuối
                        dgvMoveHistory.Rows.RemoveAt(dgvMoveHistory.Rows.Count - 1);
                    }
                }
                else
                {
                    MessageBox.Show("Không còn bước nào để quay lại!");
                    break;
                }
            }
        }

        // Cập nhật vị trí quân cờ trên bảng
        private void UpdatePieceOnBoard(Chess_Piece piece)
        {
            if (pieceBoxes.TryGetValue(piece, out Button pieceBox))
            {
                pieceBox.Location = new Point(piece.Vitri.X * 80, (9 - piece.Vitri.Y) * 80); // Cập nhật vị trí
            }
        }


        // Phương thức di chuyển của máy tính
        public void ComputerMove()
        {
            if (moveHistory.Count == 0)
            {
                // Thực hiện nước đi "Đen Dô Pháo Đầu" nếu là nước đi đầu tiên
                MovePiece(7, 2, 4, 2, dgvMoveHistory);
                return;
            }
            if (moveHistory.Count == 1)
            {
                // Thực hiện nước đi "Mã Thủ Tốt" nếu là nước đi thứ 2
                MovePiece(1, 9, 2, 7, dgvMoveHistory);
                return;
            }
            int lv = 3;
            if (lv_depth == 1)
            {
                Max_Min minimax = new Max_Min(this, lv, pieceBoxes, AI_color, opColor);

                // Tìm quân cờ và nước đi tốt nhất
                Max_Min.MoveResult result = minimax.GetBestMove();
                if (result.Piece != null)
                {
                    // Thực hiện di chuyển quân cờ
                    MovePiece(result.Piece.Vitri.X, result.Piece.Vitri.Y, result.BestMove.X, result.BestMove.Y, dgvMoveHistory);
                }
                else computerlose = true;
            }
            else
            {
                Nega_Max minimax = new Nega_Max(this, lv, pieceBoxes, AI_color, opColor);

                // Tìm quân cờ và nước đi tốt nhất
                Nega_Max.MoveResult result = minimax.GetBestMove();
                if (result.Piece != null)
                {
                    // Thực hiện di chuyển quân cờ
                    MovePiece(result.Piece.Vitri.X, result.Piece.Vitri.Y, result.BestMove.X, result.BestMove.Y, dgvMoveHistory);
                }
                else computerlose = true;
            }
            if (computerlose) MessageBox.Show($"Máy thua vì hết nước đi!");

        }
    }
}


