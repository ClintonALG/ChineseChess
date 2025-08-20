namespace Chinese_Chess
{
    partial class COMPUTER
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnReStart = new Button();
            btnExit = new Button();
            btnUndo = new Button();
            panelBoard = new Panel();
            SuspendLayout();
            // 
            // btnReStart
            // 
            btnReStart.Location = new Point(294, 12);
            btnReStart.Name = "btnReStart";
            btnReStart.Size = new Size(139, 36);
            btnReStart.TabIndex = 1;
            btnReStart.Text = "ReStart";
            btnReStart.UseVisualStyleBackColor = true;
            btnReStart.Click += btnReStart_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(530, 12);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(139, 36);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(61, 12);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(139, 36);
            btnUndo.TabIndex = 3;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // panelBoard
            // 
            panelBoard.Location = new Point(5, 54);
            panelBoard.Name = "panelBoard";
            panelBoard.Size = new Size(722, 807);
            panelBoard.TabIndex = 5;
            // 
            // COMPUTER
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 880);
            Controls.Add(panelBoard);
            Controls.Add(btnUndo);
            Controls.Add(btnExit);
            Controls.Add(btnReStart);
            Name = "COMPUTER";
            Text = "COMPUTER";
            Load += COMPUTER_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button btnReStart;
        private Button btnExit;
        private Button btnUndo;
        private Panel panelBoard;
    }
}