namespace GlacierTEXEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cbZipFiles = new System.Windows.Forms.ComboBox();
            this.btnChangeDirectory = new System.Windows.Forms.Button();
            this.lvTexDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsOpenTEX = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSaveTEX = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsImportFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsExportFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsExtractAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.pbTexture = new System.Windows.Forms.PictureBox();
            this.chkDXT1 = new System.Windows.Forms.CheckBox();
            this.chkDXT3 = new System.Windows.Forms.CheckBox();
            this.chkRGBA = new System.Windows.Forms.CheckBox();
            this.chkPALN = new System.Windows.Forms.CheckBox();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.chkU8V8 = new System.Windows.Forms.CheckBox();
            this.chkI8 = new System.Windows.Forms.CheckBox();
            this.lblDXT1 = new System.Windows.Forms.Label();
            this.lblDXT3 = new System.Windows.Forms.Label();
            this.lblPALN = new System.Windows.Forms.Label();
            this.lblI8 = new System.Windows.Forms.Label();
            this.lblU8V8 = new System.Windows.Forms.Label();
            this.lblRGBA = new System.Windows.Forms.Label();
            this.lblAll = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.grpTextureTypes = new System.Windows.Forms.GroupBox();
            this.grpEntriesCount = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnUpdateZipFile = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenTEX = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiImportFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExtractAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveTEX = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateTEXFile = new System.Windows.Forms.Button();
            this.lblRemainingTime = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.bgwExportAllFiles = new System.ComponentModel.BackgroundWorker();
            this.bgwCreateTEX = new System.ComponentModel.BackgroundWorker();
            this.bgwUpdateZip = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.smoothProgressBar1 = new GlacierTEXEditor.SmoothProgressBar();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTexture)).BeginInit();
            this.grpTextureTypes.SuspendLayout();
            this.grpEntriesCount.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbZipFiles
            // 
            this.cbZipFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZipFiles.FormattingEnabled = true;
            this.cbZipFiles.Location = new System.Drawing.Point(12, 34);
            this.cbZipFiles.Name = "cbZipFiles";
            this.cbZipFiles.Size = new System.Drawing.Size(666, 28);
            this.cbZipFiles.TabIndex = 0;
            this.cbZipFiles.SelectedIndexChanged += new System.EventHandler(this.CbZipFiles_SelectedIndexChanged);
            // 
            // btnChangeDirectory
            // 
            this.btnChangeDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeDirectory.Location = new System.Drawing.Point(684, 33);
            this.btnChangeDirectory.Name = "btnChangeDirectory";
            this.btnChangeDirectory.Size = new System.Drawing.Size(170, 29);
            this.btnChangeDirectory.TabIndex = 1;
            this.btnChangeDirectory.Text = "Change Directory";
            this.btnChangeDirectory.UseVisualStyleBackColor = true;
            this.btnChangeDirectory.Click += new System.EventHandler(this.BtnChangeDirectory_Click);
            // 
            // lvTexDetails
            // 
            this.lvTexDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvTexDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTexDetails.HideSelection = false;
            this.lvTexDetails.Location = new System.Drawing.Point(12, 76);
            this.lvTexDetails.Name = "lvTexDetails";
            this.lvTexDetails.Size = new System.Drawing.Size(842, 417);
            this.lvTexDetails.TabIndex = 2;
            this.lvTexDetails.UseCompatibleStateImageBehavior = false;
            this.lvTexDetails.View = System.Windows.Forms.View.Details;
            this.lvTexDetails.SelectedIndexChanged += new System.EventHandler(this.LvTexDetails_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File Name";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Offset";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "File Size";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Dimensions";
            this.columnHeader6.Width = 110;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Num Of Mip Maps";
            this.columnHeader7.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsOpenTEX,
            this.cmsSaveTEX,
            this.toolStripMenuItem1,
            this.cmsImportFile,
            this.cmsExportFile,
            this.toolStripMenuItem2,
            this.cmsExtractAllFiles});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 126);
            // 
            // cmsOpenTEX
            // 
            this.cmsOpenTEX.Name = "cmsOpenTEX";
            this.cmsOpenTEX.Size = new System.Drawing.Size(127, 22);
            this.cmsOpenTEX.Text = "Open TEX";
            this.cmsOpenTEX.Click += new System.EventHandler(this.CmsOpenTEX_Click);
            // 
            // cmsSaveTEX
            // 
            this.cmsSaveTEX.Name = "cmsSaveTEX";
            this.cmsSaveTEX.Size = new System.Drawing.Size(127, 22);
            this.cmsSaveTEX.Text = "Save TEX";
            this.cmsSaveTEX.Click += new System.EventHandler(this.CmsSaveTEX_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // cmsImportFile
            // 
            this.cmsImportFile.Name = "cmsImportFile";
            this.cmsImportFile.Size = new System.Drawing.Size(127, 22);
            this.cmsImportFile.Text = "Import";
            this.cmsImportFile.Click += new System.EventHandler(this.CmsImportFile_Click);
            // 
            // cmsExportFile
            // 
            this.cmsExportFile.Name = "cmsExportFile";
            this.cmsExportFile.Size = new System.Drawing.Size(127, 22);
            this.cmsExportFile.Text = "Export";
            this.cmsExportFile.Click += new System.EventHandler(this.CmsExportFile_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 6);
            // 
            // cmsExtractAllFiles
            // 
            this.cmsExtractAllFiles.Name = "cmsExtractAllFiles";
            this.cmsExtractAllFiles.Size = new System.Drawing.Size(127, 22);
            this.cmsExtractAllFiles.Text = "Extract All";
            this.cmsExtractAllFiles.Click += new System.EventHandler(this.CmsExtractAllFiles_Click);
            // 
            // pbTexture
            // 
            this.pbTexture.Location = new System.Drawing.Point(869, 237);
            this.pbTexture.Name = "pbTexture";
            this.pbTexture.Size = new System.Drawing.Size(512, 256);
            this.pbTexture.TabIndex = 3;
            this.pbTexture.TabStop = false;
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
            // btnRefreshList
            // 
            this.btnRefreshList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshList.Location = new System.Drawing.Point(869, 163);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(200, 28);
            this.btnRefreshList.TabIndex = 8;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.BtnRefreshList_Click);
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
            // lblDXT1
            // 
            this.lblDXT1.AutoSize = true;
            this.lblDXT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDXT1.Location = new System.Drawing.Point(12, 27);
            this.lblDXT1.Name = "lblDXT1";
            this.lblDXT1.Size = new System.Drawing.Size(54, 20);
            this.lblDXT1.TabIndex = 11;
            this.lblDXT1.Text = "DXT1:";
            // 
            // lblDXT3
            // 
            this.lblDXT3.AutoSize = true;
            this.lblDXT3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDXT3.Location = new System.Drawing.Point(105, 27);
            this.lblDXT3.Name = "lblDXT3";
            this.lblDXT3.Size = new System.Drawing.Size(54, 20);
            this.lblDXT3.TabIndex = 12;
            this.lblDXT3.Text = "DXT3:";
            // 
            // lblPALN
            // 
            this.lblPALN.AutoSize = true;
            this.lblPALN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPALN.Location = new System.Drawing.Point(105, 56);
            this.lblPALN.Name = "lblPALN";
            this.lblPALN.Size = new System.Drawing.Size(54, 20);
            this.lblPALN.TabIndex = 13;
            this.lblPALN.Text = "PALN:";
            // 
            // lblI8
            // 
            this.lblI8.AutoSize = true;
            this.lblI8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblI8.Location = new System.Drawing.Point(39, 86);
            this.lblI8.Name = "lblI8";
            this.lblI8.Size = new System.Drawing.Size(27, 20);
            this.lblI8.TabIndex = 14;
            this.lblI8.Text = "I8:";
            // 
            // lblU8V8
            // 
            this.lblU8V8.AutoSize = true;
            this.lblU8V8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblU8V8.Location = new System.Drawing.Point(105, 86);
            this.lblU8V8.Name = "lblU8V8";
            this.lblU8V8.Size = new System.Drawing.Size(54, 20);
            this.lblU8V8.TabIndex = 15;
            this.lblU8V8.Text = "U8V8:";
            // 
            // lblRGBA
            // 
            this.lblRGBA.AutoSize = true;
            this.lblRGBA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRGBA.Location = new System.Drawing.Point(6, 56);
            this.lblRGBA.Name = "lblRGBA";
            this.lblRGBA.Size = new System.Drawing.Size(60, 20);
            this.lblRGBA.TabIndex = 16;
            this.lblRGBA.Text = "RGBA:";
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAll.Location = new System.Drawing.Point(189, 27);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(30, 20);
            this.lblAll.TabIndex = 17;
            this.lblAll.Text = "All:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(865, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Search Name:";
            // 
            // txtSearchName
            // 
            this.txtSearchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchName.Location = new System.Drawing.Point(981, 205);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(401, 26);
            this.txtSearchName.TabIndex = 19;
            this.txtSearchName.TextChanged += new System.EventHandler(this.TxtSearchName_TextChanged);
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
            this.grpTextureTypes.Location = new System.Drawing.Point(869, 33);
            this.grpTextureTypes.Name = "grpTextureTypes";
            this.grpTextureTypes.Size = new System.Drawing.Size(200, 124);
            this.grpTextureTypes.TabIndex = 20;
            this.grpTextureTypes.TabStop = false;
            this.grpTextureTypes.Text = "Texture Types";
            // 
            // grpEntriesCount
            // 
            this.grpEntriesCount.Controls.Add(this.lblDXT1);
            this.grpEntriesCount.Controls.Add(this.lblDXT3);
            this.grpEntriesCount.Controls.Add(this.lblRGBA);
            this.grpEntriesCount.Controls.Add(this.lblPALN);
            this.grpEntriesCount.Controls.Add(this.lblAll);
            this.grpEntriesCount.Controls.Add(this.lblI8);
            this.grpEntriesCount.Controls.Add(this.lblU8V8);
            this.grpEntriesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEntriesCount.Location = new System.Drawing.Point(1087, 33);
            this.grpEntriesCount.Name = "grpEntriesCount";
            this.grpEntriesCount.Size = new System.Drawing.Size(294, 124);
            this.grpEntriesCount.TabIndex = 21;
            this.grpEntriesCount.TabStop = false;
            this.grpEntriesCount.Text = "Entries Count";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1394, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnUpdateZipFile
            // 
            this.btnUpdateZipFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateZipFile.Location = new System.Drawing.Point(1239, 163);
            this.btnUpdateZipFile.Name = "btnUpdateZipFile";
            this.btnUpdateZipFile.Size = new System.Drawing.Size(142, 28);
            this.btnUpdateZipFile.TabIndex = 23;
            this.btnUpdateZipFile.Text = "Update ZIP File";
            this.btnUpdateZipFile.UseVisualStyleBackColor = true;
            this.btnUpdateZipFile.Click += new System.EventHandler(this.BtnUpdateZipFile_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.tsmiOptions,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1394, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenTEX,
            this.tsmiOpenImage,
            this.tsmiOpenRecent,
            this.toolStripMenuItem3,
            this.tsmiImportFile,
            this.tsmiExportFile,
            this.tsmiExtractAllFiles,
            this.tsmiSaveTEX,
            this.toolStripMenuItem4,
            this.tsmiExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsmiOpenTEX
            // 
            this.tsmiOpenTEX.Name = "tsmiOpenTEX";
            this.tsmiOpenTEX.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpenTEX.Text = "Open TEX";
            this.tsmiOpenTEX.Click += new System.EventHandler(this.TsmiOpenTEX_Click);
            // 
            // tsmiOpenImage
            // 
            this.tsmiOpenImage.Name = "tsmiOpenImage";
            this.tsmiOpenImage.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpenImage.Text = "Open Image";
            this.tsmiOpenImage.Click += new System.EventHandler(this.TsmiOpenImage_Click);
            // 
            // tsmiOpenRecent
            // 
            this.tsmiOpenRecent.Name = "tsmiOpenRecent";
            this.tsmiOpenRecent.Size = new System.Drawing.Size(153, 22);
            this.tsmiOpenRecent.Text = "Open Recent";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiImportFile
            // 
            this.tsmiImportFile.Name = "tsmiImportFile";
            this.tsmiImportFile.Size = new System.Drawing.Size(153, 22);
            this.tsmiImportFile.Text = "Import";
            this.tsmiImportFile.Click += new System.EventHandler(this.TsmiImportFile_Click);
            // 
            // tsmiExportFile
            // 
            this.tsmiExportFile.Name = "tsmiExportFile";
            this.tsmiExportFile.Size = new System.Drawing.Size(153, 22);
            this.tsmiExportFile.Text = "Export";
            this.tsmiExportFile.Click += new System.EventHandler(this.TsmiExportFile_Click);
            // 
            // tsmiExtractAllFiles
            // 
            this.tsmiExtractAllFiles.Name = "tsmiExtractAllFiles";
            this.tsmiExtractAllFiles.Size = new System.Drawing.Size(153, 22);
            this.tsmiExtractAllFiles.Text = "Extract All Files";
            this.tsmiExtractAllFiles.Click += new System.EventHandler(this.TsmiExtractAllFiles_Click);
            // 
            // tsmiSaveTEX
            // 
            this.tsmiSaveTEX.Name = "tsmiSaveTEX";
            this.tsmiSaveTEX.Size = new System.Drawing.Size(153, 22);
            this.tsmiSaveTEX.Text = "Save TEX";
            this.tsmiSaveTEX.Click += new System.EventHandler(this.TsmiSaveTEX_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(153, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.TsmiExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUndo,
            this.tsmiRedo});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // tsmiUndo
            // 
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.Size = new System.Drawing.Size(142, 22);
            this.tsmiUndo.Text = "Undo Import";
            this.tsmiUndo.Click += new System.EventHandler(this.TsmiUndo_Click);
            // 
            // tsmiRedo
            // 
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.Size = new System.Drawing.Size(142, 22);
            this.tsmiRedo.Text = "Redo Import";
            this.tsmiRedo.Click += new System.EventHandler(this.TsmiRedo_Click);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings});
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(61, 20);
            this.tsmiOptions.Text = "Options";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(125, 22);
            this.tsmiSettings.Text = "Settings...";
            this.tsmiSettings.Click += new System.EventHandler(this.TsmiSettings_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(116, 22);
            this.tsmiAbout.Text = "About...";
            this.tsmiAbout.Click += new System.EventHandler(this.TsmiAbout_Click);
            // 
            // btnCreateTEXFile
            // 
            this.btnCreateTEXFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTEXFile.Location = new System.Drawing.Point(1087, 163);
            this.btnCreateTEXFile.Name = "btnCreateTEXFile";
            this.btnCreateTEXFile.Size = new System.Drawing.Size(142, 28);
            this.btnCreateTEXFile.TabIndex = 25;
            this.btnCreateTEXFile.Text = "Create TEX File";
            this.btnCreateTEXFile.UseVisualStyleBackColor = true;
            this.btnCreateTEXFile.Click += new System.EventHandler(this.BtnCreateTEXFile_Click);
            // 
            // lblRemainingTime
            // 
            this.lblRemainingTime.AutoSize = true;
            this.lblRemainingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemainingTime.Location = new System.Drawing.Point(8, 506);
            this.lblRemainingTime.Name = "lblRemainingTime";
            this.lblRemainingTime.Size = new System.Drawing.Size(127, 20);
            this.lblRemainingTime.TabIndex = 28;
            this.lblRemainingTime.Text = "Remaining Time:";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(559, 532);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 24);
            this.lblProgress.TabIndex = 29;
            this.lblProgress.Text = "label2";
            // 
            // bgwExportAllFiles
            // 
            this.bgwExportAllFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwExportAllFiles_DoWork);
            this.bgwExportAllFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgwExportAllFiles_RunWorkerCompleted);
            // 
            // bgwCreateTEX
            // 
            this.bgwCreateTEX.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwCreateTEX_DoWork);
            this.bgwCreateTEX.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgwCreateTEX_RunWorkerCompleted);
            // 
            // bgwUpdateZip
            // 
            this.bgwUpdateZip.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwUpdateZip_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(651, 529);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(541, 32);
            this.progressBar1.TabIndex = 0;
            // 
            // smoothProgressBar1
            // 
            this.smoothProgressBar1.Location = new System.Drawing.Point(12, 529);
            this.smoothProgressBar1.Maximum = 100F;
            this.smoothProgressBar1.Minimum = 0F;
            this.smoothProgressBar1.Name = "smoothProgressBar1";
            this.smoothProgressBar1.ProgressBarBackColor = System.Drawing.Color.Blue;
            this.smoothProgressBar1.Size = new System.Drawing.Size(541, 32);
            this.smoothProgressBar1.TabIndex = 27;
            this.smoothProgressBar1.TextColor = System.Drawing.Color.Black;
            this.smoothProgressBar1.Value = 0F;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 586);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblRemainingTime);
            this.Controls.Add(this.smoothProgressBar1);
            this.Controls.Add(this.btnCreateTEXFile);
            this.Controls.Add(this.btnUpdateZipFile);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.grpEntriesCount);
            this.Controls.Add(this.grpTextureTypes);
            this.Controls.Add(this.txtSearchName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.pbTexture);
            this.Controls.Add(this.lvTexDetails);
            this.Controls.Add(this.btnChangeDirectory);
            this.Controls.Add(this.cbZipFiles);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Glacier TEX Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTexture)).EndInit();
            this.grpTextureTypes.ResumeLayout(false);
            this.grpTextureTypes.PerformLayout();
            this.grpEntriesCount.ResumeLayout(false);
            this.grpEntriesCount.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbZipFiles;
        private System.Windows.Forms.Button btnChangeDirectory;
        private System.Windows.Forms.ListView lvTexDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsOpenTEX;
        private System.Windows.Forms.ToolStripMenuItem cmsSaveTEX;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmsImportFile;
        private System.Windows.Forms.ToolStripMenuItem cmsExportFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cmsExtractAllFiles;
        private System.Windows.Forms.PictureBox pbTexture;
        private System.Windows.Forms.CheckBox chkDXT1;
        private System.Windows.Forms.CheckBox chkDXT3;
        private System.Windows.Forms.CheckBox chkRGBA;
        private System.Windows.Forms.CheckBox chkPALN;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.CheckBox chkU8V8;
        private System.Windows.Forms.CheckBox chkI8;
        private System.Windows.Forms.Label lblDXT1;
        private System.Windows.Forms.Label lblDXT3;
        private System.Windows.Forms.Label lblPALN;
        private System.Windows.Forms.Label lblI8;
        private System.Windows.Forms.Label lblU8V8;
        private System.Windows.Forms.Label lblRGBA;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.GroupBox grpTextureTypes;
        private System.Windows.Forms.GroupBox grpEntriesCount;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnUpdateZipFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenTEX;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiExtractAllFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.Button btnCreateTEXFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveTEX;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private SmoothProgressBar smoothProgressBar1;
        private System.Windows.Forms.Label lblRemainingTime;
        private System.Windows.Forms.Label lblProgress;
        private System.ComponentModel.BackgroundWorker bgwExportAllFiles;
        private System.ComponentModel.BackgroundWorker bgwCreateTEX;
        private System.ComponentModel.BackgroundWorker bgwUpdateZip;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

