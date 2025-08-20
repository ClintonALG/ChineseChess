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
    public partial class PLAYER : Form
    {
        private Board chessBoard;
        private DataGridView dgvMoveHistory;
        public PLAYER()
        {
            InitializeComponent();
            InitializeMoveHistoryGrid();
        }

        //==================================================================================================================================================
        //==================================================================================================================================================

        private void PLAYER_Load(object sender, EventArgs e)
        {
            chessBoard = new Board(panelBoard, true, true, null, 0, null, dgvMoveHistory);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReStart_Click(object sender, EventArgs e)
        {
            chessBoard = new Board(panelBoard, true, true, null, 0, null, dgvMoveHistory);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            chessBoard.Undo(1);
        }

        private void dgvMoveHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
