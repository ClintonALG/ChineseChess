namespace Chinese_Chess
{
    partial class PLAYER
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLAYER));
            panelBoard = new Panel();
            btnUndo = new Button();
            btnExit = new Button();
            btnStart = new Button();
            SuspendLayout();
            // 
            // panelBoard
            // 
            resources.ApplyResources(panelBoard, "panelBoard");
            panelBoard.Name = "panelBoard";
            // 
            // btnUndo
            // 
            resources.ApplyResources(btnUndo, "btnUndo");
            btnUndo.Name = "btnUndo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnExit
            // 
            resources.ApplyResources(btnExit, "btnExit");
            btnExit.Name = "btnExit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnStart
            // 
            resources.ApplyResources(btnStart, "btnStart");
            btnStart.Name = "btnStart";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnReStart_Click;
            // 
            // PLAYER
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelBoard);
            Controls.Add(btnUndo);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            Name = "PLAYER";
            Load += PLAYER_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBoard;
        private Button btnUndo;
        private Button btnExit;
        private Button btnStart;
    }
}