namespace CCSFileExplorerWV
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pb1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCCSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCCSFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.unpackBINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repackBINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedBlobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAsBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInImageImporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRotationOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hb1 = new Be.Windows.Forms.HexBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pic2 = new System.Windows.Forms.PictureBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(571, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pb1
            // 
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.selectedBlobToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(571, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCCSFileToolStripMenuItem,
            this.saveCCSFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.unpackBINToolStripMenuItem,
            this.repackBINToolStripMenuItem,
            this.toolStripMenuItem3,
            this.recentToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCCSFileToolStripMenuItem
            // 
            this.openCCSFileToolStripMenuItem.Name = "openCCSFileToolStripMenuItem";
            this.openCCSFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openCCSFileToolStripMenuItem.Text = "Open CCSFile...";
            this.openCCSFileToolStripMenuItem.Click += new System.EventHandler(this.openCCSFileToolStripMenuItem_Click);
            // 
            // saveCCSFileToolStripMenuItem
            // 
            this.saveCCSFileToolStripMenuItem.Name = "saveCCSFileToolStripMenuItem";
            this.saveCCSFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveCCSFileToolStripMenuItem.Text = "Save CCSFile...";
            this.saveCCSFileToolStripMenuItem.Click += new System.EventHandler(this.saveCCSFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // unpackBINToolStripMenuItem
            // 
            this.unpackBINToolStripMenuItem.Name = "unpackBINToolStripMenuItem";
            this.unpackBINToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.unpackBINToolStripMenuItem.Text = "Unpack BIN...";
            this.unpackBINToolStripMenuItem.Click += new System.EventHandler(this.unpackBINToolStripMenuItem_Click);
            // 
            // repackBINToolStripMenuItem
            // 
            this.repackBINToolStripMenuItem.Name = "repackBINToolStripMenuItem";
            this.repackBINToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.repackBINToolStripMenuItem.Text = "Repack BIN...";
            this.repackBINToolStripMenuItem.Click += new System.EventHandler(this.repackBINToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 6);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Enabled = false;
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.recentToolStripMenuItem.Text = "Recent";
            // 
            // selectedBlobToolStripMenuItem
            // 
            this.selectedBlobToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportRawToolStripMenuItem,
            this.importRawToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exportAsBitmapToolStripMenuItem,
            this.openInImageImporterToolStripMenuItem,
            this.exportToObjToolStripMenuItem});
            this.selectedBlobToolStripMenuItem.Name = "selectedBlobToolStripMenuItem";
            this.selectedBlobToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.selectedBlobToolStripMenuItem.Text = "Selected Node";
            // 
            // exportRawToolStripMenuItem
            // 
            this.exportRawToolStripMenuItem.Name = "exportRawToolStripMenuItem";
            this.exportRawToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exportRawToolStripMenuItem.Text = "Export raw...";
            this.exportRawToolStripMenuItem.Click += new System.EventHandler(this.exportRawToolStripMenuItem_Click);
            // 
            // importRawToolStripMenuItem
            // 
            this.importRawToolStripMenuItem.Name = "importRawToolStripMenuItem";
            this.importRawToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.importRawToolStripMenuItem.Text = "Import raw...";
            this.importRawToolStripMenuItem.Click += new System.EventHandler(this.importRawToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(186, 6);
            // 
            // exportAsBitmapToolStripMenuItem
            // 
            this.exportAsBitmapToolStripMenuItem.Name = "exportAsBitmapToolStripMenuItem";
            this.exportAsBitmapToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exportAsBitmapToolStripMenuItem.Text = "Export as bitmap...";
            this.exportAsBitmapToolStripMenuItem.Click += new System.EventHandler(this.exportAsBitmapToolStripMenuItem_Click);
            // 
            // openInImageImporterToolStripMenuItem
            // 
            this.openInImageImporterToolStripMenuItem.Name = "openInImageImporterToolStripMenuItem";
            this.openInImageImporterToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openInImageImporterToolStripMenuItem.Text = "Open in Image Importer";
            this.openInImageImporterToolStripMenuItem.Click += new System.EventHandler(this.openInImageImporterToolStripMenuItem_Click);
            // 
            // exportToObjToolStripMenuItem
            // 
            this.exportToObjToolStripMenuItem.Name = "exportToObjToolStripMenuItem";
            this.exportToObjToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exportToObjToolStripMenuItem.Text = "Export to obj...";
            this.exportToObjToolStripMenuItem.Click += new System.EventHandler(this.exportToObjToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoRotationOnToolStripMenuItem,
            this.wireframeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // autoRotationOnToolStripMenuItem
            // 
            this.autoRotationOnToolStripMenuItem.Checked = true;
            this.autoRotationOnToolStripMenuItem.CheckOnClick = true;
            this.autoRotationOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoRotationOnToolStripMenuItem.Name = "autoRotationOnToolStripMenuItem";
            this.autoRotationOnToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.autoRotationOnToolStripMenuItem.Text = "Auto Rotation On";
            this.autoRotationOnToolStripMenuItem.Click += new System.EventHandler(this.autoRotationOnToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtb1);
            this.splitContainer1.Size = new System.Drawing.Size(571, 460);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(571, 378);
            this.splitContainer2.SplitterDistance = 209;
            this.splitContainer2.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(209, 378);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(358, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hb1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(350, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Raw View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hb1
            // 
            this.hb1.BoldFont = null;
            this.hb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hb1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hb1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hb1.LineInfoVisible = true;
            this.hb1.Location = new System.Drawing.Point(3, 3);
            this.hb1.Name = "hb1";
            this.hb1.ReadOnly = true;
            this.hb1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hb1.Size = new System.Drawing.Size(344, 346);
            this.hb1.StringViewVisible = true;
            this.hb1.TabIndex = 0;
            this.hb1.UseFixedBytesPerLine = true;
            this.hb1.VScrollBarVisible = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pic1);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(350, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Texture View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pic1
            // 
            this.pic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic1.Location = new System.Drawing.Point(3, 24);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(344, 325);
            this.pic1.TabIndex = 0;
            this.pic1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(344, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pic2);
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(350, 352);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Model View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pic2
            // 
            this.pic2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic2.Location = new System.Drawing.Point(3, 24);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(344, 325);
            this.pic2.TabIndex = 2;
            this.pic2.TabStop = false;
            this.pic2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic2_MouseMove);
            this.pic2.Resize += new System.EventHandler(this.pic2_Resize);
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(344, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // rtb1
            // 
            this.rtb1.DetectUrls = false;
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rtb1.Location = new System.Drawing.Point(0, 0);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(571, 78);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.CheckOnClick = true;
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.wireframeToolStripMenuItem.Text = "Wireframe";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 506);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CCSFile Explorer by Warranty Voider";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pb1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCCSFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCCSFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem unpackBINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repackBINToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Be.Windows.Forms.HexBox hb1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtb1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.ToolStripMenuItem selectedBlobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exportAsBitmapToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem importRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInImageImporterToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem exportToObjToolStripMenuItem;
        private System.Windows.Forms.PictureBox pic2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoRotationOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
    }
}

