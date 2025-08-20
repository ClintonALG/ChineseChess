namespace Chinese_Chess
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        public void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void btnComputer_Click(object sender, EventArgs e)
        {
            LOGIN lOGIN = new LOGIN();
            this.Hide();
            lOGIN.Show();
            lOGIN.FormClosed += (s, args) => this.Show();
        }
        public void btnPLAYER_Click(object sender, EventArgs e)
        {
            PLAYER Player_Form = new PLAYER();
            this.Hide();
            Player_Form.Show();
            Player_Form.FormClosed += (s, args) => this.Show();
        }
        private void GameForm_FormClosed(object? sender, FormClosedEventArgs? e)
        {
            this.Show();
        }
    }
}
