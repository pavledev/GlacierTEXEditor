namespace GlacierTEXEditor
{
    partial class ExportAllTextures
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
            this.btnChangeExportPath = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.btnSelectAllExtensionTypes = new System.Windows.Forms.Button();
            this.lvTextures = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpTextureTypes = new System.Windows.Forms.GroupBox();
            this.chkDXT1 = new System.Windows.Forms.CheckBox();
            this.chkU8V8 = new System.Windows.Forms.CheckBox();
            this.chkDXT3 = new System.Windows.Forms.CheckBox();
            this.chkI8 = new System.Windows.Forms.CheckBox();
            this.chkPALN = new System.Windows.Forms.CheckBox();
            this.chkRGBA = new System.Windows.Forms.CheckBox();
            this.btnClearAllDimensions = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.btnExportAllTextures = new System.Windows.Forms.Button();
            this.grpExportAllAs = new System.Windows.Forms.GroupBox();
            this.btnSelectAllDimensions = new System.Windows.Forms.Button();
            this.cklDimensions = new System.Windows.Forms.CheckedListBox();
            this.chkExportAsSingleFile = new System.Windows.Forms.CheckBox();
            this.lbExtensions = new System.Windows.Forms.ListBox();
            this.chkDDS = new System.Windows.Forms.CheckBox();
            this.chkJPG = new System.Windows.Forms.CheckBox();
            this.chkBMP = new System.Windows.Forms.CheckBox();
            this.chkTGA = new System.Windows.Forms.CheckBox();
            this.chkPNG = new System.Windows.Forms.CheckBox();
            this.grpTextureTypes.SuspendLayout();
            this.grpExportAllAs.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChangeExportPath
            // 
            this.btnChangeExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeExportPath.Location = new System.Drawing.Point(764, 11);
            this.btnChangeExportPath.Name = "btnChangeExportPath";
            this.btnChangeExportPath.Size = new System.Drawing.Size(170, 29);
            this.btnChangeExportPath.TabIndex = 2;
            this.btnChangeExportPath.Text = "Change Directory";
            this.btnChangeExportPath.UseVisualStyleBackColor = true;
            this.btnChangeExportPath.Click += new System.EventHandler(this.BtnChangeExportPath_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExportPath.Location = new System.Drawing.Point(12, 12);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.Size = new System.Drawing.Size(746, 26);
            this.txtExportPath.TabIndex = 1;
            // 
            // btnSelectAllExtensionTypes
            // 
            this.btnSelectAllExtensionTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAllExtensionTypes.Location = new System.Drawing.Point(973, 182);
            this.btnSelectAllExtensionTypes.Name = "btnSelectAllExtensionTypes";
            this.btnSelectAllExtensionTypes.Size = new System.Drawing.Size(200, 28);
            this.btnSelectAllExtensionTypes.TabIndex = 52;
            this.btnSelectAllExtensionTypes.Text = "Select All";
            this.btnSelectAllExtensionTypes.UseVisualStyleBackColor = true;
            this.btnSelectAllExtensionTypes.Click += new System.EventHandler(this.BtnSelectAllExtensionTypes_Click);
            // 
            // lvTextures
            // 
            this.lvTextures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTextures.HideSelection = false;
            this.lvTextures.Location = new System.Drawing.Point(12, 49);
            this.lvTextures.Name = "lvTextures";
            this.lvTextures.Size = new System.Drawing.Size(746, 560);
            this.lvTextures.TabIndex = 50;
            this.lvTextures.UseCompatibleStateImageBehavior = false;
            this.lvTextures.View = System.Windows.Forms.View.Details;
            this.lvTextures.SelectedIndexChanged += new System.EventHandler(this.LvTextures_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Name";
            this.columnHeader2.Width = 600;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            // 
            // grpTextureTypes
            // 
            this.grpTextureTypes.Controls.Add(this.chkDXT1);
            this.grpTextureTypes.Controls.Add(this.chkU8V8);
            this.grpTextureTypes.Controls.Add(this.chkDXT3);
            this.grpTextureTypes.Controls.Add(this.chkI8);
            this.grpTextureTypes.Controls.Add(this.chkPALN);
            this.grpTextureTypes.Controls.Add(this.chkRGBA);
            this.grpTextureTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextureTypes.Location = new System.Drawing.Point(767, 52);
            this.grpTextureTypes.Name = "grpTextureTypes";
            this.grpTextureTypes.Size = new System.Drawing.Size(200, 124);
            this.grpTextureTypes.TabIndex = 41;
            this.grpTextureTypes.TabStop = false;
            this.grpTextureTypes.Text = "Texture Types";
            // 
            // chkDXT1
            // 
            this.chkDXT1.AutoSize = true;
            this.chkDXT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDXT1.Location = new System.Drawing.Point(19, 26);
            this.chkDXT1.Name = "chkDXT1";
            this.chkDXT1.Size = new System.Drawing.Size(69, 24);
            this.chkDXT1.TabIndex = 4;
            this.chkDXT1.Text = "DXT1";
            this.chkDXT1.UseVisualStyleBackColor = true;
            // 
            // chkU8V8
            // 
            this.chkU8V8.AutoSize = true;
            this.chkU8V8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkU8V8.Location = new System.Drawing.Point(101, 86);
            this.chkU8V8.Name = "chkU8V8";
            this.chkU8V8.Size = new System.Drawing.Size(69, 24);
            this.chkU8V8.TabIndex = 10;
            this.chkU8V8.Text = "U8V8";
            this.chkU8V8.UseVisualStyleBackColor = true;
            // 
            // chkDXT3
            // 
            this.chkDXT3.AutoSize = true;
            this.chkDXT3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDXT3.Location = new System.Drawing.Point(101, 26);
            this.chkDXT3.Name = "chkDXT3";
            this.chkDXT3.Size = new System.Drawing.Size(69, 24);
            this.chkDXT3.TabIndex = 5;
            this.chkDXT3.Text = "DXT3";
            this.chkDXT3.UseVisualStyleBackColor = true;
            // 
            // chkI8
            // 
            this.chkI8.AutoSize = true;
            this.chkI8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkI8.Location = new System.Drawing.Point(19, 86);
            this.chkI8.Name = "chkI8";
            this.chkI8.Size = new System.Drawing.Size(50, 24);
            this.chkI8.TabIndex = 9;
            this.chkI8.Text = "I8  ";
            this.chkI8.UseVisualStyleBackColor = true;
            // 
            // chkPALN
            // 
            this.chkPALN.AutoSize = true;
            this.chkPALN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPALN.Location = new System.Drawing.Point(101, 56);
            this.chkPALN.Name = "chkPALN";
            this.chkPALN.Size = new System.Drawing.Size(69, 24);
            this.chkPALN.TabIndex = 7;
            this.chkPALN.Text = "PALN";
            this.chkPALN.UseVisualStyleBackColor = true;
            // 
            // chkRGBA
            // 
            this.chkRGBA.AutoSize = true;
            this.chkRGBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRGBA.Location = new System.Drawing.Point(19, 56);
            this.chkRGBA.Name = "chkRGBA";
            this.chkRGBA.Size = new System.Drawing.Size(75, 24);
            this.chkRGBA.TabIndex = 6;
            this.chkRGBA.Text = "RGBA";
            this.chkRGBA.UseVisualStyleBackColor = true;
            // 
            // btnClearAllDimensions
            // 
            this.btnClearAllDimensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllDimensions.Location = new System.Drawing.Point(972, 510);
            this.btnClearAllDimensions.Name = "btnClearAllDimensions";
            this.btnClearAllDimensions.Size = new System.Drawing.Size(200, 28);
            this.btnClearAllDimensions.TabIndex = 48;
            this.btnClearAllDimensions.Text = "Clear All";
            this.btnClearAllDimensions.UseVisualStyleBackColor = true;
            this.btnClearAllDimensions.Click += new System.EventHandler(this.BtnClearAllDimensions_Click);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshList.Location = new System.Drawing.Point(767, 182);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(200, 28);
            this.btnRefreshList.TabIndex = 40;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.BtnRefreshList_Click);
            // 
            // btnExportAllTextures
            // 
            this.btnExportAllTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllTextures.Location = new System.Drawing.Point(868, 577);
            this.btnExportAllTextures.Name = "btnExportAllTextures";
            this.btnExportAllTextures.Size = new System.Drawing.Size(200, 28);
            this.btnExportAllTextures.TabIndex = 47;
            this.btnExportAllTextures.Text = "Export All Textures";
            this.btnExportAllTextures.UseVisualStyleBackColor = true;
            this.btnExportAllTextures.Click += new System.EventHandler(this.BtnExportAllTextures_Click);
            // 
            // grpExportAllAs
            // 
            this.grpExportAllAs.Controls.Add(this.chkDDS);
            this.grpExportAllAs.Controls.Add(this.chkJPG);
            this.grpExportAllAs.Controls.Add(this.chkBMP);
            this.grpExportAllAs.Controls.Add(this.chkTGA);
            this.grpExportAllAs.Controls.Add(this.chkPNG);
            this.grpExportAllAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportAllAs.Location = new System.Drawing.Point(973, 52);
            this.grpExportAllAs.Name = "grpExportAllAs";
            this.grpExportAllAs.Size = new System.Drawing.Size(178, 124);
            this.grpExportAllAs.TabIndex = 42;
            this.grpExportAllAs.TabStop = false;
            this.grpExportAllAs.Text = "Export All As:";
            // 
            // btnSelectAllDimensions
            // 
            this.btnSelectAllDimensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAllDimensions.Location = new System.Drawing.Point(972, 476);
            this.btnSelectAllDimensions.Name = "btnSelectAllDimensions";
            this.btnSelectAllDimensions.Size = new System.Drawing.Size(200, 28);
            this.btnSelectAllDimensions.TabIndex = 43;
            this.btnSelectAllDimensions.Text = "Select All";
            this.btnSelectAllDimensions.UseVisualStyleBackColor = true;
            this.btnSelectAllDimensions.Click += new System.EventHandler(this.BtnSelectAllDimensions_Click);
            // 
            // cklDimensions
            // 
            this.cklDimensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cklDimensions.FormattingEnabled = true;
            this.cklDimensions.Location = new System.Drawing.Point(972, 271);
            this.cklDimensions.Name = "cklDimensions";
            this.cklDimensions.Size = new System.Drawing.Size(200, 193);
            this.cklDimensions.TabIndex = 44;
            this.cklDimensions.SelectedIndexChanged += new System.EventHandler(this.CklDimensions_SelectedIndexChanged);
            // 
            // chkExportAsSingleFile
            // 
            this.chkExportAsSingleFile.AutoSize = true;
            this.chkExportAsSingleFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExportAsSingleFile.Location = new System.Drawing.Point(767, 241);
            this.chkExportAsSingleFile.Name = "chkExportAsSingleFile";
            this.chkExportAsSingleFile.Size = new System.Drawing.Size(174, 24);
            this.chkExportAsSingleFile.TabIndex = 53;
            this.chkExportAsSingleFile.Text = "Export As Single File";
            this.chkExportAsSingleFile.UseVisualStyleBackColor = true;
            this.chkExportAsSingleFile.CheckedChanged += new System.EventHandler(this.ChkExportAsSingleFile_CheckedChanged);
            // 
            // lbExtensions
            // 
            this.lbExtensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExtensions.FormattingEnabled = true;
            this.lbExtensions.ItemHeight = 20;
            this.lbExtensions.Location = new System.Drawing.Point(767, 271);
            this.lbExtensions.Name = "lbExtensions";
            this.lbExtensions.Size = new System.Drawing.Size(200, 184);
            this.lbExtensions.TabIndex = 54;
            this.lbExtensions.SelectedIndexChanged += new System.EventHandler(this.LbExtensions_SelectedIndexChanged);
            // 
            // chkDDS
            // 
            this.chkDDS.AutoSize = true;
            this.chkDDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDDS.Location = new System.Drawing.Point(15, 26);
            this.chkDDS.Name = "chkDDS";
            this.chkDDS.Size = new System.Drawing.Size(63, 24);
            this.chkDDS.TabIndex = 29;
            this.chkDDS.Text = "DDS";
            this.chkDDS.UseVisualStyleBackColor = true;
            // 
            // chkJPG
            // 
            this.chkJPG.AutoSize = true;
            this.chkJPG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkJPG.Location = new System.Drawing.Point(84, 56);
            this.chkJPG.Name = "chkJPG";
            this.chkJPG.Size = new System.Drawing.Size(59, 24);
            this.chkJPG.TabIndex = 33;
            this.chkJPG.Text = "JPG";
            this.chkJPG.UseVisualStyleBackColor = true;
            // 
            // chkBMP
            // 
            this.chkBMP.AutoSize = true;
            this.chkBMP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBMP.Location = new System.Drawing.Point(15, 86);
            this.chkBMP.Name = "chkBMP";
            this.chkBMP.Size = new System.Drawing.Size(62, 24);
            this.chkBMP.TabIndex = 31;
            this.chkBMP.Text = "BMP";
            this.chkBMP.UseVisualStyleBackColor = true;
            // 
            // chkTGA
            // 
            this.chkTGA.AutoSize = true;
            this.chkTGA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTGA.Location = new System.Drawing.Point(15, 56);
            this.chkTGA.Name = "chkTGA";
            this.chkTGA.Size = new System.Drawing.Size(61, 24);
            this.chkTGA.TabIndex = 30;
            this.chkTGA.Text = "TGA";
            this.chkTGA.UseVisualStyleBackColor = true;
            // 
            // chkPNG
            // 
            this.chkPNG.AutoSize = true;
            this.chkPNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPNG.Location = new System.Drawing.Point(84, 26);
            this.chkPNG.Name = "chkPNG";
            this.chkPNG.Size = new System.Drawing.Size(62, 24);
            this.chkPNG.TabIndex = 32;
            this.chkPNG.Text = "PNG";
            this.chkPNG.UseVisualStyleBackColor = true;
            // 
            // ExportAllTextures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 620);
            this.Controls.Add(this.lbExtensions);
            this.Controls.Add(this.chkExportAsSingleFile);
            this.Controls.Add(this.btnSelectAllExtensionTypes);
            this.Controls.Add(this.lvTextures);
            this.Controls.Add(this.grpTextureTypes);
            this.Controls.Add(this.btnClearAllDimensions);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.btnExportAllTextures);
            this.Controls.Add(this.grpExportAllAs);
            this.Controls.Add(this.btnSelectAllDimensions);
            this.Controls.Add(this.cklDimensions);
            this.Controls.Add(this.btnChangeExportPath);
            this.Controls.Add(this.txtExportPath);
            this.Name = "ExportAllTextures";
            this.Text = "ExportAllTextures";
            this.Load += new System.EventHandler(this.ExportAllTextures_Load);
            this.grpTextureTypes.ResumeLayout(false);
            this.grpTextureTypes.PerformLayout();
            this.grpExportAllAs.ResumeLayout(false);
            this.grpExportAllAs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtExportPath;
        private System.Windows.Forms.Button btnChangeExportPath;
        private System.Windows.Forms.Button btnSelectAllExtensionTypes;
        private System.Windows.Forms.ListView lvTextures;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox grpTextureTypes;
        private System.Windows.Forms.CheckBox chkDXT1;
        private System.Windows.Forms.CheckBox chkU8V8;
        private System.Windows.Forms.CheckBox chkDXT3;
        private System.Windows.Forms.CheckBox chkI8;
        private System.Windows.Forms.CheckBox chkPALN;
        private System.Windows.Forms.CheckBox chkRGBA;
        private System.Windows.Forms.Button btnClearAllDimensions;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Button btnExportAllTextures;
        private System.Windows.Forms.GroupBox grpExportAllAs;
        private System.Windows.Forms.Button btnSelectAllDimensions;
        private System.Windows.Forms.CheckedListBox cklDimensions;
        private System.Windows.Forms.CheckBox chkExportAsSingleFile;
        private System.Windows.Forms.ListBox lbExtensions;
        private System.Windows.Forms.CheckBox chkDDS;
        private System.Windows.Forms.CheckBox chkJPG;
        private System.Windows.Forms.CheckBox chkBMP;
        private System.Windows.Forms.CheckBox chkTGA;
        private System.Windows.Forms.CheckBox chkPNG;
    }
}