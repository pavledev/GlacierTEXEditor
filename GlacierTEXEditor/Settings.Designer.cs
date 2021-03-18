namespace GlacierTEXEditor
{
    partial class Settings
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
            this.rbOptimal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbFastest = new System.Windows.Forms.RadioButton();
            this.chkUpdateZipAutomatically = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPS4 = new System.Windows.Forms.RadioButton();
            this.rbPC = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbOptimal
            // 
            this.rbOptimal.AutoSize = true;
            this.rbOptimal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOptimal.Location = new System.Drawing.Point(32, 68);
            this.rbOptimal.Name = "rbOptimal";
            this.rbOptimal.Size = new System.Drawing.Size(81, 24);
            this.rbOptimal.TabIndex = 0;
            this.rbOptimal.TabStop = true;
            this.rbOptimal.Text = "Optimal";
            this.rbOptimal.UseVisualStyleBackColor = true;
            this.rbOptimal.CheckedChanged += new System.EventHandler(this.RbOptimal_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose compression level:";
            // 
            // rbFastest
            // 
            this.rbFastest.AutoSize = true;
            this.rbFastest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFastest.Location = new System.Drawing.Point(119, 68);
            this.rbFastest.Name = "rbFastest";
            this.rbFastest.Size = new System.Drawing.Size(81, 24);
            this.rbFastest.TabIndex = 2;
            this.rbFastest.TabStop = true;
            this.rbFastest.Text = "Fastest";
            this.rbFastest.UseVisualStyleBackColor = true;
            this.rbFastest.CheckedChanged += new System.EventHandler(this.RbFastest_CheckedChanged);
            // 
            // chkUpdateZipAutomatically
            // 
            this.chkUpdateZipAutomatically.AutoSize = true;
            this.chkUpdateZipAutomatically.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUpdateZipAutomatically.Location = new System.Drawing.Point(32, 126);
            this.chkUpdateZipAutomatically.Name = "chkUpdateZipAutomatically";
            this.chkUpdateZipAutomatically.Size = new System.Drawing.Size(376, 24);
            this.chkUpdateZipAutomatically.TabIndex = 0;
            this.chkUpdateZipAutomatically.Text = "Automatically update zip file after generating TEX";
            this.chkUpdateZipAutomatically.UseVisualStyleBackColor = true;
            this.chkUpdateZipAutomatically.CheckedChanged += new System.EventHandler(this.ChkUpdateZipAutomatically_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(169, 219);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(137, 38);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPS4);
            this.groupBox1.Controls.Add(this.rbPC);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(241, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 78);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game version";
            // 
            // rbPS4
            // 
            this.rbPS4.AutoSize = true;
            this.rbPS4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPS4.Location = new System.Drawing.Point(103, 27);
            this.rbPS4.Name = "rbPS4";
            this.rbPS4.Size = new System.Drawing.Size(57, 24);
            this.rbPS4.TabIndex = 4;
            this.rbPS4.TabStop = true;
            this.rbPS4.Text = "PS4";
            this.rbPS4.UseVisualStyleBackColor = true;
            this.rbPS4.CheckedChanged += new System.EventHandler(this.RbPS4_CheckedChanged);
            // 
            // rbPC
            // 
            this.rbPC.AutoSize = true;
            this.rbPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPC.Location = new System.Drawing.Point(16, 27);
            this.rbPC.Name = "rbPC";
            this.rbPC.Size = new System.Drawing.Size(48, 24);
            this.rbPC.TabIndex = 3;
            this.rbPC.TabStop = true;
            this.rbPC.Text = "PC";
            this.rbPC.UseVisualStyleBackColor = true;
            this.rbPC.CheckedChanged += new System.EventHandler(this.RbPC_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 269);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkUpdateZipAutomatically);
            this.Controls.Add(this.rbFastest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbOptimal);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbOptimal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbFastest;
        private System.Windows.Forms.CheckBox chkUpdateZipAutomatically;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPS4;
        private System.Windows.Forms.RadioButton rbPC;
    }
}