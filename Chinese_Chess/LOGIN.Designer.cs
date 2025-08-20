namespace Chinese_Chess
{
    partial class LOGIN
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
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            txtUSER = new TextBox();
            txtPASS = new TextBox();
            label3 = new Label();
            cbLevel = new ComboBox();
            dataGridView1 = new DataGridView();
            btnExit = new Button();
            label4 = new Label();
            lv1 = new Label();
            lv2 = new Label();
            btnSign = new Button();
            btnUPDATE = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(199, 240);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(103, 45);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 82);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 1;
            label1.Text = "User";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(69, 124);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // txtUSER
            // 
            txtUSER.Location = new Point(141, 79);
            txtUSER.Name = "txtUSER";
            txtUSER.Size = new Size(164, 27);
            txtUSER.TabIndex = 3;
            // 
            // txtPASS
            // 
            txtPASS.Location = new Point(141, 124);
            txtPASS.Name = "txtPASS";
            txtPASS.Size = new Size(164, 27);
            txtPASS.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(69, 167);
            label3.Name = "label3";
            label3.Size = new Size(43, 20);
            label3.TabIndex = 5;
            label3.Text = "Level";
            // 
            // cbLevel
            // 
            cbLevel.FormattingEnabled = true;
            cbLevel.Location = new Point(141, 167);
            cbLevel.Name = "cbLevel";
            cbLevel.Size = new Size(161, 28);
            cbLevel.TabIndex = 6;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(339, 7);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(494, 301);
            dataGridView1.TabIndex = 7;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(11, 303);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(96, 45);
            btnExit.TabIndex = 8;
            btnExit.Text = "Thoat";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 163);
            label4.Location = new Point(351, 323);
            label4.Name = "label4";
            label4.Size = new Size(124, 23);
            label4.TabIndex = 9;
            label4.Text = "%AI win (lv):";
            // 
            // lv1
            // 
            lv1.AutoSize = true;
            lv1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 163);
            lv1.Location = new Point(508, 311);
            lv1.Name = "lv1";
            lv1.Size = new Size(62, 46);
            lv1.TabIndex = 10;
            lv1.Text = "lv1";
            // 
            // lv2
            // 
            lv2.AutoSize = true;
            lv2.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 163);
            lv2.Location = new Point(641, 311);
            lv2.Name = "lv2";
            lv2.Size = new Size(62, 46);
            lv2.TabIndex = 11;
            lv2.Text = "lv2";
            // 
            // btnSign
            // 
            btnSign.Location = new Point(77, 240);
            btnSign.Name = "btnSign";
            btnSign.Size = new Size(103, 45);
            btnSign.TabIndex = 15;
            btnSign.Text = "Sign In";
            btnSign.UseVisualStyleBackColor = true;
            btnSign.Click += btnSign_Click;
            // 
            // btnUPDATE
            // 
            btnUPDATE.Location = new Point(240, 12);
            btnUPDATE.Name = "btnUPDATE";
            btnUPDATE.Size = new Size(93, 31);
            btnUPDATE.TabIndex = 16;
            btnUPDATE.Text = "UPDATE";
            btnUPDATE.UseVisualStyleBackColor = true;
            btnUPDATE.Click += btnUPDATE_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(113, 201);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(50, 24);
            radioButton1.TabIndex = 17;
            radioButton1.Text = "Đỏ";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Location = new Point(199, 201);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(57, 24);
            radioButton2.TabIndex = 18;
            radioButton2.TabStop = true;
            radioButton2.Text = "Đen";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // LOGIN
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 360);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(btnUPDATE);
            Controls.Add(btnSign);
            Controls.Add(lv2);
            Controls.Add(lv1);
            Controls.Add(label4);
            Controls.Add(btnExit);
            Controls.Add(dataGridView1);
            Controls.Add(cbLevel);
            Controls.Add(label3);
            Controls.Add(txtPASS);
            Controls.Add(txtUSER);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Name = "LOGIN";
            Text = "LOGIN";
            Load += LOGIN_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Label label1;
        private Label label2;
        private TextBox txtUSER;
        private TextBox txtPASS;
        private Label label3;
        private ComboBox cbLevel;
        private DataGridView dataGridView1;
        private Button btnExit;
        private Label label4;
        private Label lv1;
        private Label lv2;
        private Button btnSign;
        private Button btnUPDATE;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
    }
}