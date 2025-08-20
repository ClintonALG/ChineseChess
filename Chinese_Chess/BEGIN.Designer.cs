namespace Chinese_Chess
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnComputer = new Button();
            btnPLAYER = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // btnComputer
            // 
            btnComputer.Location = new Point(117, 71);
            btnComputer.Name = "btnComputer";
            btnComputer.Size = new Size(225, 88);
            btnComputer.TabIndex = 0;
            btnComputer.Text = "CHƠI VỚI MÁY ";
            btnComputer.UseVisualStyleBackColor = true;
            btnComputer.Click += btnComputer_Click;
            // 
            // btnPLAYER
            // 
            btnPLAYER.Location = new Point(117, 193);
            btnPLAYER.Name = "btnPLAYER";
            btnPLAYER.Size = new Size(225, 77);
            btnPLAYER.TabIndex = 1;
            btnPLAYER.Text = "CHƠI VỚI NGƯỜI";
            btnPLAYER.UseVisualStyleBackColor = true;
            btnPLAYER.Click += btnPLAYER_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(146, 302);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(162, 50);
            btnThoat.TabIndex = 2;
            btnThoat.Text = "THOÁT";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(479, 429);
            Controls.Add(btnThoat);
            Controls.Add(btnPLAYER);
            Controls.Add(btnComputer);
            Name = "FormMain";
            Text = "Chinese_Chess";
            ResumeLayout(false);
        }

        #endregion

        private Button btnComputer;
        private Button btnPLAYER;
        private Button btnThoat;
    }
}
