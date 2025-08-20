using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Chinese_Chess;

namespace Chinese_Chess
{
    public class Max_Min
    {
        private int MaxDepth; // Độ sâu tối đa cho thuật toán Minimax
        private Board board; // Tham chiếu đến đối tượng Board
        private Dictionary<ulong, int> transpositionTable = new Dictionary<ulong, int>(); // Bảng Zobrist
        private Dictionary<Chess_Piece, Button> Box_piece;
        public string AI_color;
        public string opColor;
        private ulong[,] zobristTable; // Bảng Zobrist Hashing

        public Max_Min(Board board, int depth, Dictionary<Chess_Piece, Button> piecebox, string ai_color, string op_color)
        {
            this.board = board;
            this.MaxDepth = depth;
            this.Box_piece = piecebox;
            this.AI_color = ai_color;
            this.opColor = op_color;
            InitializeZobrist(); // Khởi tạo bảng Zobrist
        }

        public struct MoveResult
        {
            public Chess_Piece Piece; // Quân cờ cần di chuyển
            public Point BestMove;    // Nước đi tốt nhất

            public MoveResult(Chess_Piece piece, Point bestMove)
            {
                Piece = piece;
                BestMove = bestMove;
            }
        }

        public MoveResult GetBestMove()
        {
            int bestScore = int.MinValue;
            Chess_Piece bestPiece = null;
            Point bestMove = new Point(-1, -1);

            var pieces = GetPiecesByColor(board.board, AI_color);

            // Trộn các quân cờ trước khi tính toán
            Random rand = new Random();
            pieces = pieces.OrderBy(x => rand.Next()).ToList();

            foreach (var piece in pieces)
            {
                // Trộn các nước đi hợp lệ của quân cờ
                var moves = SortMoves(piece);
                moves = moves.OrderBy(x => rand.Next()).ToList(); // Trộn nước đi

                foreach (var move in moves)
                {
                    var originalPosition = piece.Vitri;
                    var targetPiece = board.board[move.X, move.Y];

                    SimulateMove(piece, move);

                    int score = MaxMin(MaxDepth - 1, int.MinValue, int.MaxValue, opColor);

                    UndoMove(piece, originalPosition, targetPiece, move);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                        bestPiece = piece;
                    }
                }
            }

            return new MoveResult(bestPiece, bestMove);
        }

        private int MaxMin(int depth, int alpha, int beta, string color)
        {
            ulong boardHash = ComputeZobristHash();

            // Kiểm tra nếu trạng thái này đã được tính toán trước
            if (transpositionTable.ContainsKey(boardHash))
                return transpositionTable[boardHash];

            if (depth == 0 || board.CheckForGameOver(color) != -1)
            {
                int eval = EvaluateBoard(color);
                transpositionTable[boardHash] = eval; 
                return eval;
            }

            if (color == AI_color)
            {
                int maxEval = int.MinValue;

                foreach (var piece in GetPiecesByColor(board.board, color))
                {
                    foreach (var move in SortMoves(piece))
                    {
                        var originalPosition = piece.Vitri;
                        var targetPiece = board.board[move.X, move.Y];

                        SimulateMove(piece, move);

                        int eval = MaxMin(depth - 1, alpha, beta, opColor);

                        UndoMove(piece, originalPosition, targetPiece, move);

                        maxEval = Math.Max(maxEval, eval);
                        alpha = Math.Max(alpha, eval);

                        if (beta <= alpha)
                            break;
                    }
                }

                transpositionTable[boardHash] = maxEval;
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;

                foreach (var piece in GetPiecesByColor(board.board, color))
                {
                    foreach (var move in SortMoves(piece))
                    {
                        var originalPosition = piece.Vitri;
                        var targetPiece = board.board[move.X, move.Y];

                        SimulateMove(piece, move);

                        int eval = MaxMin(depth - 1, alpha, beta, AI_color);

                        UndoMove(piece, originalPosition, targetPiece, move);

                        minEval = Math.Min(minEval, eval);
                        beta = Math.Min(beta, eval);

                        if (beta <= alpha)
                            break;
                    }
                }

                transpositionTable[boardHash] = minEval;
                return minEval;
            }
        }

        private void SimulateMove(Chess_Piece piece, Point move)
        {
            var originalPosition = piece.Vitri;
            var targetPiece = board.board[move.X, move.Y];

            board.board[move.X, move.Y] = piece;
            board.board[originalPosition.X, originalPosition.Y] = null;
            piece.Move(move);
        }

        private void UndoMove(Chess_Piece piece, Point originalPosition, Chess_Piece targetPiece, Point move)
        {
            piece.Move(originalPosition);
            board.board[originalPosition.X, originalPosition.Y] = piece;
            board.board[move.X, move.Y] = targetPiece;
        }

        private int EvaluateBoard(string color)
        {
            int score = 0;

            foreach (var piece in GetAllPieces(board.board))
                score += GetPieceValue(piece);

            if (board.CheckForGameOver(color) == 1)
                score -= 10000;

            return color == AI_color ? score : -score;
        }

        private int GetPieceValue(Chess_Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Tuong" => 1000,
                "Si" => 200,
                "Voi" => 250,
                "Ma" => 300,
                "Xe" => 600,
                "Phao" => 450,
                "Tot" => 200,
                _ => 0
            };
        }

        private List<Chess_Piece> GetPiecesByColor(Chess_Piece[,] boardState, string color)
        {
            var pieces = new List<Chess_Piece>();
            foreach (var piece in GetAllPieces(boardState))
                if (piece.Color == color)
                    pieces.Add(piece);
            return pieces;
        }

        private IEnumerable<Chess_Piece> GetAllPieces(Chess_Piece[,] boardState)
        {
            foreach (var piece in boardState)
                if (piece != null)
                    yield return piece;
        }

        private List<Point> SortMoves(Chess_Piece piece)
        {
            var moves = piece.GetSafeMoves(board.board);
            moves.Sort((a, b) => EvaluateMove(piece, b).CompareTo(EvaluateMove(piece, a)));
            return moves;
        }

        private int EvaluateMove(Chess_Piece piece, Point move)
        {
            var targetPiece = board.board[move.X, move.Y];
            return targetPiece != null ? GetPieceValue(targetPiece) : 0;
        }

        private void InitializeZobrist()
        {
            zobristTable = new ulong[9 * 10, 12];
            var rand = new Random();
            for (int i = 0; i < 9 * 10; i++)
                for (int j = 0; j < 12; j++)
                    zobristTable[i, j] = (ulong)rand.Next();
        }

        private ulong ComputeZobristHash()
        {
            ulong hash = 0;

            foreach (var piece in GetAllPieces(board.board))
            {
                int x = piece.Vitri.X;
                int y = piece.Vitri.Y;
                int pieceType = GetPieceTypeIndex(piece);
                hash ^= zobristTable[x * 10 + y, pieceType];
            }

            return hash;
        }

        private int GetPieceTypeIndex(Chess_Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Tuong" => 0,
                "Si" => 1,
                "Voi" => 2,
                "Ma" => 3,
                "Xe" => 4,
                "Phao" => 5,
                "Tot" => 6,
                _ => 7
            };
        }

        public MoveResult GetRandomMove(Chess_Piece piece)
        {
            Random random = new Random();
            var moves = piece.GetSafeMoves(board.board);
            var randomMove = moves[random.Next(moves.Count)];

            return new MoveResult(piece, randomMove);
        }
    }

    public class Nega_Max
    {
        private int MaxDepth; // Độ sâu tối đa
        private Board board; // Đối tượng bàn cờ
        private Dictionary<ulong, TTEntry> transpositionTable = new(); // Bảng ghi nhớ Zobrist
        private Dictionary<Chess_Piece, Button> Box_piece;
        public string AI_color;
        public string opColor;
        private ulong[,] zobristTable; // Bảng Zobrist Hashing
        private Stack<MoveState> moveHistory = new(); // Lịch sử trạng thái để Undo

        public Nega_Max(Board board, int depth, Dictionary<Chess_Piece, Button> piecebox, string ai_color, string op_color)
        {
            this.board = board;
            this.MaxDepth = depth;
            this.Box_piece = piecebox;
            this.AI_color = ai_color;
            this.opColor = op_color;
            InitializeZobrist(); // Khởi tạo bảng Zobrist
        }

        private struct TTEntry
        {
            public int Score;
            public int Depth;
        }

        private struct MoveState
        {
            public Chess_Piece Piece;
            public Point OriginalPosition;
            public Chess_Piece CapturedPiece;
            public Point TargetPosition;

            public MoveState(Chess_Piece piece, Point originalPosition, Chess_Piece capturedPiece, Point targetPosition)
            {
                Piece = piece;
                OriginalPosition = originalPosition;
                CapturedPiece = capturedPiece;
                TargetPosition = targetPosition;
            }
        }

        public struct MoveResult
        {
            public Chess_Piece Piece; // Quân cờ cần di chuyển
            public Point BestMove;    // Nước đi tốt nhất

            public MoveResult(Chess_Piece piece, Point bestMove)
            {
                Piece = piece;
                BestMove = bestMove;
            }
        }

        public MoveResult GetBestMove()
        {
            int bestScore = int.MinValue;
            Chess_Piece bestPiece = null;
            Point bestMove = new Point(-1, -1);

            foreach (var piece in GetPiecesByColor(board.board, AI_color))
            {
                foreach (var move in SortMoves(piece))
                {
                    SimulateMove(piece, move);

                    int score = -Negamax(MaxDepth - 1, int.MinValue, int.MaxValue, opColor);

                    UndoMove();

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                        bestPiece = piece;
                    }
                }
            }

            return new MoveResult(bestPiece, bestMove);
        }

        private int Negamax(int depth, int alpha, int beta, string color)
        {
            ulong boardHash = ComputeZobristHash();

            // Kiểm tra trạng thái trong bảng Zobrist
            if (transpositionTable.TryGetValue(boardHash, out TTEntry entry) && entry.Depth >= depth)
                return entry.Score;

            if (depth == 0 || board.CheckForGameOver(color) != -1)
            {
                int eval = EvaluateBoard(color);

                // Thêm randomness vào việc đánh giá
                RandomizeEvaluation(ref eval);

                transpositionTable[boardHash] = new TTEntry { Score = eval, Depth = depth };
                return eval;
            }

            int maxEval = int.MinValue;

            foreach (var piece in GetPiecesByColor(board.board, color))
            {
                foreach (var move in SortMoves(piece))
                {
                    SimulateMove(piece, move);

                    int eval = -Negamax(depth - 1, -beta, -alpha, opColor);

                    // Thêm randomness vào việc đánh giá
                    RandomizeEvaluation(ref eval);

                    UndoMove();

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);

                    if (alpha >= beta)
                        break;
                }

                if (alpha >= beta)
                    break;
            }

            transpositionTable[boardHash] = new TTEntry { Score = maxEval, Depth = depth };
            return maxEval;
        }

        private void SimulateMove(Chess_Piece piece, Point move)
        {
            var originalPosition = piece.Vitri;
            var targetPiece = board.board[move.X, move.Y];

            moveHistory.Push(new MoveState(piece, originalPosition, targetPiece, move));

            board.board[move.X, move.Y] = piece;
            board.board[originalPosition.X, originalPosition.Y] = null;
            piece.Move(move);
        }

        private void UndoMove()
        {
            if (moveHistory.Count == 0) return;

            var lastMove = moveHistory.Pop();

            lastMove.Piece.Move(lastMove.OriginalPosition);
            board.board[lastMove.OriginalPosition.X, lastMove.OriginalPosition.Y] = lastMove.Piece;
            board.board[lastMove.TargetPosition.X, lastMove.TargetPosition.Y] = lastMove.CapturedPiece;
        }

        private int EvaluateBoard(string color)
        {
            int score = 0;

            foreach (var piece in GetAllPieces(board.board))
            {
                score += GetPieceValue(piece);
                score += GetPositionValue(piece);
            }

            if (board.CheckForGameOver(color) == 1)
                score -= 10000;

            return color == AI_color ? score : -score;
        }

        private int GetPieceValue(Chess_Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Tuong" => 1000,
                "Si" => 200,
                "Voi" => 250,
                "Ma" => 300,
                "Xe" => 600,
                "Phao" => 450,
                "Tot" => 200,
                _ => 0
            };
        }

        private int GetPositionValue(Chess_Piece piece)
        {
            // Lấy giá trị quân cờ từ bảng điểm
            int value = 0;

            // Lấy tọa độ của quân cờ
            int x = piece.Vitri.X;
            int y = piece.Vitri.Y;

            // Đánh giá vị trí trung tâm (tạo vùng trung tâm là các ô từ (3,3) đến (6,6) cho bàn cờ 9x10)
            bool isCenter = (x >= 3 && x <= 6) && (y >= 3 && y <= 6);

            // Các yếu tố ảnh hưởng đến giá trị vị trí quân cờ
            switch (piece.GetType().Name)
            {
                case "Tuong":
                    // Tướng có giá trị cao nếu ở gần trung tâm
                    value += isCenter ? 100 : -100;
                    break;

                case "Voi":
                    // Voi có giá trị cao ở các vị trí gần trung tâm
                    value += isCenter ? 30 : -30;
                    break;

                case "Ma":
                    // Mã có giá trị cao nếu gần trung tâm, vì Mã có thể di chuyển linh hoạt
                    value += isCenter ? 30 : -30;
                    break;

                case "Xe":
                    // Xe mạnh ở trung tâm và biên, nên tính toán cả 2
                    value += (x == 0 || x == 8 || y == 0 || y == 9) ? 70 : 40;
                    break;

                case "Phao":
                    // Phao mạnh khi có thể tấn công từ xa, nhưng cũng cần ở trung tâm
                    value += (x == 0 || x == 8 || y == 0 || y == 9) ? 50 : 30;
                    break;

                case "Tot":
                    // Tốt có giá trị cao hơn khi tiến gần phía đối phương
                    value += (y < 5) ? 20 : -10;
                    break;
            }

            return value;
        }



        private List<Chess_Piece> GetPiecesByColor(Chess_Piece[,] boardState, string color)
        {
            var pieces = new List<Chess_Piece>();
            foreach (var piece in GetAllPieces(boardState))
                if (piece.Color == color)
                    pieces.Add(piece);
            return pieces;
        }

        private IEnumerable<Chess_Piece> GetAllPieces(Chess_Piece[,] boardState)
        {
            foreach (var piece in boardState)
                if (piece != null)
                    yield return piece;
        }

        private List<Point> SortMoves(Chess_Piece piece)
        {
            var moves = piece.GetSafeMoves(board.board);
            moves.Sort((a, b) => EvaluateMove(piece, b).CompareTo(EvaluateMove(piece, a)));
            return moves;
        }

        private int EvaluateMove(Chess_Piece piece, Point move)
        {
            var targetPiece = board.board[move.X, move.Y];
            return targetPiece != null ? GetPieceValue(targetPiece) : 0;
        }

        private void InitializeZobrist()
        {
            zobristTable = new ulong[9 * 10, 12];
            var rand = new Random();
            for (int i = 0; i < 9 * 10; i++)
                for (int j = 0; j < 12; j++)
                    zobristTable[i, j] = (ulong)rand.Next() << 32 | (ulong)rand.Next();
        }

        private ulong ComputeZobristHash()
        {
            ulong hash = 0;

            for (int x = 0; x < 9; x++) // 9 hàng
            {
                for (int y = 0; y < 10; y++) // 10 cột
                {
                    var piece = board.board[x, y];
                    if (piece != null)
                    {
                        int pieceType = GetPieceType(piece);
                        hash ^= zobristTable[x * 10 + y, pieceType];
                    }
                }
            }

            return hash;
        }

        private int GetPieceType(Chess_Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Tuong" => 0,
                "Si" => 1,
                "Voi" => 2,
                "Ma" => 3,
                "Xe" => 4,
                "Phao" => 5,
                "Tot" => 6,
                _ => -1
            };
        }

        private void RandomizeEvaluation(ref int evaluation)
        {
            // Thêm yếu tố ngẫu nhiên vào đánh giá
            Random rand = new Random();
            // Tạo một sự thay đổi ngẫu nhiên trong phạm vi nhỏ, chẳng hạn ±10
            evaluation += rand.Next(-10, 11);
        }
    }
}



