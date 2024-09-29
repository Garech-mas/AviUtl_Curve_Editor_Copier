namespace MoveCurveEditorData
{
    partial class Form1
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
            openFileDialog1 = new OpenFileDialog();
            lblToAup = new Label();
            txtToAup = new TextBox();
            lblFromAup = new Label();
            txtFromAup = new TextBox();
            btnToAup = new Button();
            btnFromAup = new Button();
            btnRun = new Button();
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblToAup
            // 
            lblToAup.AutoSize = true;
            lblToAup.Location = new Point(14, 57);
            lblToAup.Name = "lblToAup";
            lblToAup.Size = new Size(43, 15);
            lblToAup.TabIndex = 0;
            lblToAup.Text = "移動先";
            // 
            // txtToAup
            // 
            txtToAup.AllowDrop = true;
            txtToAup.Location = new Point(58, 54);
            txtToAup.Name = "txtToAup";
            txtToAup.Size = new Size(465, 23);
            txtToAup.TabIndex = 1;
            txtToAup.DragDrop += txtToAup_DragDrop;
            txtToAup.DragEnter += txtToAup_DragEnter;
            // 
            // lblFromAup
            // 
            lblFromAup.AutoSize = true;
            lblFromAup.Location = new Point(14, 15);
            lblFromAup.Name = "lblFromAup";
            lblFromAup.Size = new Size(43, 15);
            lblFromAup.TabIndex = 0;
            lblFromAup.Text = "移動元";
            // 
            // txtFromAup
            // 
            txtFromAup.AllowDrop = true;
            txtFromAup.Location = new Point(58, 12);
            txtFromAup.Name = "txtFromAup";
            txtFromAup.Size = new Size(465, 23);
            txtFromAup.TabIndex = 1;
            txtFromAup.DragDrop += txtFromAup_DragDrop;
            txtFromAup.DragEnter += txtFromAup_DragEnter;
            // 
            // btnToAup
            // 
            btnToAup.Location = new Point(529, 54);
            btnToAup.Name = "btnToAup";
            btnToAup.Size = new Size(23, 23);
            btnToAup.TabIndex = 2;
            btnToAup.Text = "..";
            btnToAup.UseVisualStyleBackColor = true;
            btnToAup.Click += btnToAup_Click;
            // 
            // btnFromAup
            // 
            btnFromAup.Location = new Point(529, 15);
            btnFromAup.Name = "btnFromAup";
            btnFromAup.Size = new Size(23, 23);
            btnFromAup.TabIndex = 2;
            btnFromAup.Text = "..";
            btnFromAup.UseVisualStyleBackColor = true;
            btnFromAup.Click += btnFromAup_Click;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(474, 92);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 43);
            btnRun.TabIndex = 3;
            btnRun.Text = "実行";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += button3_Click;
            // 
            // txtLog
            // 
            txtLog.Font = new Font("Yu Gothic UI", 8F);
            txtLog.Location = new Point(13, 90);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(455, 45);
            txtLog.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(561, 147);
            Controls.Add(txtLog);
            Controls.Add(btnRun);
            Controls.Add(btnFromAup);
            Controls.Add(btnToAup);
            Controls.Add(txtFromAup);
            Controls.Add(lblFromAup);
            Controls.Add(txtToAup);
            Controls.Add(lblToAup);
            Name = "Form1";
            Text = "Curve Editor カーブコピー";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Label lblToAup;
        private TextBox txtToAup;
        private Label lblFromAup;
        private TextBox txtFromAup;
        private Button btnToAup;
        private Button btnFromAup;
        private Button btnRun;
        private TextBox txtLog;
    }
}
