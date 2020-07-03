namespace GlacierTEXEditor
{
    partial class SelectTextureType
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
            this.rbDXT1 = new System.Windows.Forms.RadioButton();
            this.rbDXT3 = new System.Windows.Forms.RadioButton();
            this.rbRGBA = new System.Windows.Forms.RadioButton();
            this.rbPALN = new System.Windows.Forms.RadioButton();
            this.rbI8 = new System.Windows.Forms.RadioButton();
            this.rbU8V8 = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbDXT1
            // 
            this.rbDXT1.AutoSize = true;
            this.rbDXT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDXT1.Location = new System.Drawing.Point(74, 44);
            this.rbDXT1.Name = "rbDXT1";
            this.rbDXT1.Size = new System.Drawing.Size(68, 24);
            this.rbDXT1.TabIndex = 0;
            this.rbDXT1.TabStop = true;
            this.rbDXT1.Text = "DXT1";
            this.rbDXT1.UseVisualStyleBackColor = true;
            this.rbDXT1.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbDXT3
            // 
            this.rbDXT3.AutoSize = true;
            this.rbDXT3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDXT3.Location = new System.Drawing.Point(155, 44);
            this.rbDXT3.Name = "rbDXT3";
            this.rbDXT3.Size = new System.Drawing.Size(68, 24);
            this.rbDXT3.TabIndex = 1;
            this.rbDXT3.TabStop = true;
            this.rbDXT3.Text = "DXT3";
            this.rbDXT3.UseVisualStyleBackColor = true;
            this.rbDXT3.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbRGBA
            // 
            this.rbRGBA.AutoSize = true;
            this.rbRGBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRGBA.Location = new System.Drawing.Point(236, 44);
            this.rbRGBA.Name = "rbRGBA";
            this.rbRGBA.Size = new System.Drawing.Size(74, 24);
            this.rbRGBA.TabIndex = 2;
            this.rbRGBA.TabStop = true;
            this.rbRGBA.Text = "RGBA";
            this.rbRGBA.UseVisualStyleBackColor = true;
            this.rbRGBA.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbPALN
            // 
            this.rbPALN.AutoSize = true;
            this.rbPALN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPALN.Location = new System.Drawing.Point(74, 74);
            this.rbPALN.Name = "rbPALN";
            this.rbPALN.Size = new System.Drawing.Size(68, 24);
            this.rbPALN.TabIndex = 3;
            this.rbPALN.TabStop = true;
            this.rbPALN.Text = "PALN";
            this.rbPALN.UseVisualStyleBackColor = true;
            this.rbPALN.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbI8
            // 
            this.rbI8.AutoSize = true;
            this.rbI8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbI8.Location = new System.Drawing.Point(155, 74);
            this.rbI8.Name = "rbI8";
            this.rbI8.Size = new System.Drawing.Size(41, 24);
            this.rbI8.TabIndex = 4;
            this.rbI8.TabStop = true;
            this.rbI8.Text = "I8";
            this.rbI8.UseVisualStyleBackColor = true;
            this.rbI8.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbU8V8
            // 
            this.rbU8V8.AutoSize = true;
            this.rbU8V8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbU8V8.Location = new System.Drawing.Point(236, 74);
            this.rbU8V8.Name = "rbU8V8";
            this.rbU8V8.Size = new System.Drawing.Size(68, 24);
            this.rbU8V8.TabIndex = 5;
            this.rbU8V8.TabStop = true;
            this.rbU8V8.Text = "U8V8";
            this.rbU8V8.UseVisualStyleBackColor = true;
            this.rbU8V8.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(123, 142);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(136, 33);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Texture Type:";
            // 
            // SelectTextureType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 187);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbU8V8);
            this.Controls.Add(this.rbI8);
            this.Controls.Add(this.rbPALN);
            this.Controls.Add(this.rbRGBA);
            this.Controls.Add(this.rbDXT3);
            this.Controls.Add(this.rbDXT1);
            this.Name = "SelectTextureType";
            this.Text = "Texture Types";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbDXT1;
        private System.Windows.Forms.RadioButton rbDXT3;
        private System.Windows.Forms.RadioButton rbRGBA;
        private System.Windows.Forms.RadioButton rbPALN;
        private System.Windows.Forms.RadioButton rbI8;
        private System.Windows.Forms.RadioButton rbU8V8;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
    }
}