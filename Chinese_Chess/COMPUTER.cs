using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    public partial class COMPUTER : Form
    {
        //==================================================================================================================================================
        //==================================================================================================================================================

        private Board chessBoard;
        private string username;
        private int lv_depth;
        private string AI_color;
        private bool player1 = false;
        private bool player2 = false;

        private DataGridView dgvMoveHistory;

        //==================================================================================================================================================
        //==================================================================================================================================================
        public COMPUTER(string username, int lv_depth, string AI_color)
        {
            InitializeComponent();
            this.username = username;
            this.lv_depth = lv_depth;
            this.AI_color = AI_color;
            if (AI_color == "Đen")
            {
                this.player2 = true;
            }
            else this.player1 = true;
            MyDB db = new MyDB();
            string query = "SELECT username FROM ketqua WHERE username = '" + username + "' AND lv_depth = " + lv_depth;
            if (username != db.getScalarString(query))
            {
                string query_1 = "INSERT INTO ketqua(username,lv_depth) VALUES('" + username + "','" + lv_depth + "')";
                int add = db.getNonQuery(query_1);
            }
            InitializeMoveHistoryGrid();
        }
        private void COMPUTER_Load(object sender, EventArgs e)
        {
            chessBoard = new Board(panelBoard, player1, player2, username, lv_depth, AI_color, dgvMoveHistory);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            chessBoard.Undo(2);
        }

        private void btnReStart_Click(object sender, EventArgs e)
        {
            chessBoard = new Board(panelBoard, player1, player2, username, lv_depth, AI_color, dgvMoveHistory);
        }

        private void InitializeMoveHistoryGrid()
        {
            dgvMoveHistory = new DataGridView();
            dgvMoveHistory.ColumnCount = 4;
            dgvMoveHistory.Location = new Point(780, 57);
            dgvMoveHistory.Size = new Size(550, 807);

            dgvMoveHistory.Columns[0].Name = "Phe";
            dgvMoveHistory.Columns[1].Name = "Quân cờ";
            dgvMoveHistory.Columns[2].Name = "Vị trí cũ";
            dgvMoveHistory.Columns[3].Name = "Vị trí mới";

            dgvMoveHistory.DefaultCellStyle.Font = new Font("Arial", 14); // Thay "Arial" bằng font bạn muốn, "14" là kích cỡ chữ
            dgvMoveHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 16, FontStyle.Bold); // Font chữ cho tiêu đề cột

            dgvMoveHistory.CellFormatting += DgvMoveHistory_CellFormatting;

            dgvMoveHistory.AllowUserToAddRows = false;

            this.Controls.Add(dgvMoveHistory);
        }
        private void DgvMoveHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMoveHistory.Columns[e.ColumnIndex].Name == "Phe") // Kiểm tra cột Phe
            {
                string side = e.Value?.ToString();
                if (side == "Đen")
                {
                    dgvMoveHistory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray; // Màu nền đen nhạt
                }
                else if (side == "Đỏ")
                {
                    dgvMoveHistory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral; // Màu nền đỏ nhạt
                }
            }
        }
    }
}
