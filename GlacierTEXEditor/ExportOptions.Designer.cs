namespace GlacierTEXEditor
{
    partial class ExportOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportImage = new System.Windows.Forms.Button();
            this.pnlDimensions = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkExportAsSingleFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select dimensions of image you want to export(width-height):";
            // 
            // btnExportImage
            // 
            this.btnExportImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportImage.Location = new System.Drawing.Point(290, 400);
            this.btnExportImage.Name = "btnExportImage";
            this.btnExportImage.Size = new System.Drawing.Size(137, 38);
            this.btnExportImage.TabIndex = 2;
            this.btnExportImage.Text = "Export Image";
            this.btnExportImage.UseVisualStyleBackColor = true;
            this.btnExportImage.Click += new System.EventHandler(this.BtnExportImage_Click);
            // 
            // pnlDimensions
            // 
            this.pnlDimensions.Location = new System.Drawing.Point(16, 63);
            this.pnlDimensions.Name = "pnlDimensions";
            this.pnlDimensions.Size = new System.Drawing.Size(200, 331);
            this.pnlDimensions.TabIndex = 3;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(51, 400);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(137, 38);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "File name:";
            // 
            // chkExportAsSingleFile
            // 
            this.chkExportAsSingleFile.AutoSize = true;
            this.chkExportAsSingleFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExportAsSingleFile.Location = new System.Drawing.Point(263, 63);
            this.chkExportAsSingleFile.Name = "chkExportAsSingleFile";
            this.chkExportAsSingleFile.Size = new System.Drawing.Size(164, 24);
            this.chkExportAsSingleFile.TabIndex = 6;
            this.chkExportAsSingleFile.Text = "Export as single file";
            this.chkExportAsSingleFile.UseVisualStyleBackColor = true;
            this.chkExportAsSingleFile.CheckedChanged += new System.EventHandler(this.ChkExportAsSingleFile_CheckedChanged);
            // 
            // ExportOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 450);
            this.Controls.Add(this.chkExportAsSingleFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.pnlDimensions);
            this.Controls.Add(this.btnExportImage);
            this.Controls.Add(this.label1);
            this.Name = "ExportOptions";
            this.Text = "Export Options";
            this.Load += new System.EventHandler(this.ExportOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportImage;
        private System.Windows.Forms.Panel pnlDimensions;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkExportAsSingleFile;
    }
}