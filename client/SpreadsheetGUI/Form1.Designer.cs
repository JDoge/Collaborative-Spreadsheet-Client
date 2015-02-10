namespace SpreadsheetGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoOnServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllNegativesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TextContents = new System.Windows.Forms.TextBox();
            this.LabelValue1 = new System.Windows.Forms.Label();
            this.LabelContent = new System.Windows.Forms.Label();
            this.LabelName1 = new System.Windows.Forms.Label();
            this.LabelName2 = new System.Windows.Forms.Label();
            this.LabelValue2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.submitButton = new System.Windows.Forms.Button();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.filelistPanel = new System.Windows.Forms.Panel();
            this.createFileButton = new System.Windows.Forms.Button();
            this.openFileButton = new System.Windows.Forms.Button();
            this.newFileLabel = new System.Windows.Forms.Label();
            this.newFileBox = new System.Windows.Forms.TextBox();
            this.filelistLabel = new System.Windows.Forms.Label();
            this.filelistBox = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.hostConnectPanel = new System.Windows.Forms.Panel();
            this.hostSubmitButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextbox = new System.Windows.Forms.TextBox();
            this.hostnameLabel = new System.Windows.Forms.Label();
            this.hostnameTextbox = new System.Windows.Forms.TextBox();
            this.skyntaxServerName = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.filelistPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.hostConnectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skyntaxServerName)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(765, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.connectToServerToolStripMenuItem,
            this.undoOnServerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // connectToServerToolStripMenuItem
            // 
            this.connectToServerToolStripMenuItem.Name = "connectToServerToolStripMenuItem";
            this.connectToServerToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.connectToServerToolStripMenuItem.Text = "Connect to Server";
            this.connectToServerToolStripMenuItem.Click += new System.EventHandler(this.connectToServerToolStripMenuItem_Click);
            // 
            // undoOnServerToolStripMenuItem
            // 
            this.undoOnServerToolStripMenuItem.Name = "undoOnServerToolStripMenuItem";
            this.undoOnServerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoOnServerToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.undoOnServerToolStripMenuItem.Text = "&Undo On Server";
            this.undoOnServerToolStripMenuItem.Visible = false;
            this.undoOnServerToolStripMenuItem.Click += new System.EventHandler(this.undoOnServerToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayDependentsToolStripMenuItem,
            this.showAllNegativesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // displayDependentsToolStripMenuItem
            // 
            this.displayDependentsToolStripMenuItem.Name = "displayDependentsToolStripMenuItem";
            this.displayDependentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.displayDependentsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.displayDependentsToolStripMenuItem.Text = "Display &Dependents";
            this.displayDependentsToolStripMenuItem.Click += new System.EventHandler(this.displayDependentsToolStripMenuItem_Click);
            // 
            // showAllNegativesToolStripMenuItem
            // 
            this.showAllNegativesToolStripMenuItem.Name = "showAllNegativesToolStripMenuItem";
            this.showAllNegativesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.showAllNegativesToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showAllNegativesToolStripMenuItem.Text = "Show All &Negatives";
            this.showAllNegativesToolStripMenuItem.Click += new System.EventHandler(this.showAllNegativesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 81);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(765, 524);
            this.spreadsheetPanel1.TabIndex = 1;
            this.spreadsheetPanel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheetPanel1_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TextContents, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelValue1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelContent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelName1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelName2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LabelValue2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 57);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TextContents
            // 
            this.TextContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextContents.Location = new System.Drawing.Point(126, 32);
            this.TextContents.Name = "TextContents";
            this.TextContents.Size = new System.Drawing.Size(635, 20);
            this.TextContents.TabIndex = 1;
            this.TextContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextContents_KeyDown);
            // 
            // LabelValue1
            // 
            this.LabelValue1.AutoSize = true;
            this.LabelValue1.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelValue1.Location = new System.Drawing.Point(82, 1);
            this.LabelValue1.Name = "LabelValue1";
            this.LabelValue1.Size = new System.Drawing.Size(37, 27);
            this.LabelValue1.TabIndex = 2;
            this.LabelValue1.Text = "Value:";
            this.LabelValue1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelContent
            // 
            this.LabelContent.AutoSize = true;
            this.LabelContent.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelContent.Location = new System.Drawing.Point(67, 29);
            this.LabelContent.Name = "LabelContent";
            this.LabelContent.Size = new System.Drawing.Size(52, 27);
            this.LabelContent.TabIndex = 3;
            this.LabelContent.Text = "Contents:";
            this.LabelContent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelName1
            // 
            this.LabelName1.AutoSize = true;
            this.LabelName1.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelName1.Location = new System.Drawing.Point(38, 1);
            this.LabelName1.Name = "LabelName1";
            this.LabelName1.Size = new System.Drawing.Size(20, 27);
            this.LabelName1.TabIndex = 4;
            this.LabelName1.Text = "C4";
            this.LabelName1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelName2
            // 
            this.LabelName2.AutoSize = true;
            this.LabelName2.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelName2.Location = new System.Drawing.Point(38, 29);
            this.LabelName2.Name = "LabelName2";
            this.LabelName2.Size = new System.Drawing.Size(20, 27);
            this.LabelName2.TabIndex = 5;
            this.LabelName2.Text = "C4";
            this.LabelName2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelValue2
            // 
            this.LabelValue2.AutoSize = true;
            this.LabelValue2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelValue2.Location = new System.Drawing.Point(126, 1);
            this.LabelValue2.Name = "LabelValue2";
            this.LabelValue2.Size = new System.Drawing.Size(635, 27);
            this.LabelValue2.TabIndex = 6;
            this.LabelValue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 605);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(765, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "untitled";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(340, 106);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Visible = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(237, 47);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(281, 20);
            this.passwordTextbox.TabIndex = 1;
            this.passwordTextbox.Visible = false;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(150, 50);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(81, 13);
            this.passwordLabel.TabIndex = 0;
            this.passwordLabel.Text = "Enter Password";
            this.passwordLabel.Visible = false;
            // 
            // filelistPanel
            // 
            this.filelistPanel.Controls.Add(this.createFileButton);
            this.filelistPanel.Controls.Add(this.openFileButton);
            this.filelistPanel.Controls.Add(this.newFileLabel);
            this.filelistPanel.Controls.Add(this.newFileBox);
            this.filelistPanel.Controls.Add(this.filelistLabel);
            this.filelistPanel.Controls.Add(this.filelistBox);
            this.filelistPanel.Location = new System.Drawing.Point(3, 3);
            this.filelistPanel.Name = "filelistPanel";
            this.filelistPanel.Size = new System.Drawing.Size(748, 609);
            this.filelistPanel.TabIndex = 5;
            this.filelistPanel.Visible = false;
            // 
            // createFileButton
            // 
            this.createFileButton.Location = new System.Drawing.Point(337, 336);
            this.createFileButton.Name = "createFileButton";
            this.createFileButton.Size = new System.Drawing.Size(75, 23);
            this.createFileButton.TabIndex = 5;
            this.createFileButton.Text = "Create";
            this.createFileButton.UseVisualStyleBackColor = true;
            this.createFileButton.Click += new System.EventHandler(this.createFileButton_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(337, 220);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 4;
            this.openFileButton.Text = "Open";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // newFileLabel
            // 
            this.newFileLabel.AutoSize = true;
            this.newFileLabel.Location = new System.Drawing.Point(322, 284);
            this.newFileLabel.Name = "newFileLabel";
            this.newFileLabel.Size = new System.Drawing.Size(105, 13);
            this.newFileLabel.TabIndex = 3;
            this.newFileLabel.Text = "Or Create a New File";
            // 
            // newFileBox
            // 
            this.newFileBox.Location = new System.Drawing.Point(185, 310);
            this.newFileBox.Name = "newFileBox";
            this.newFileBox.Size = new System.Drawing.Size(378, 20);
            this.newFileBox.TabIndex = 2;
            // 
            // filelistLabel
            // 
            this.filelistLabel.AutoSize = true;
            this.filelistLabel.Location = new System.Drawing.Point(342, 12);
            this.filelistLabel.Name = "filelistLabel";
            this.filelistLabel.Size = new System.Drawing.Size(65, 13);
            this.filelistLabel.TabIndex = 1;
            this.filelistLabel.Text = "Select a File";
            // 
            // filelistBox
            // 
            this.filelistBox.FormattingEnabled = true;
            this.filelistBox.Location = new System.Drawing.Point(185, 28);
            this.filelistBox.Name = "filelistBox";
            this.filelistBox.Size = new System.Drawing.Size(378, 186);
            this.filelistBox.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.hostConnectPanel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(765, 627);
            this.flowLayoutPanel1.TabIndex = 6;
            this.flowLayoutPanel1.Visible = false;
            // 
            // hostConnectPanel
            // 
            this.hostConnectPanel.Controls.Add(this.filelistPanel);
            this.hostConnectPanel.Controls.Add(this.hostSubmitButton);
            this.hostConnectPanel.Controls.Add(this.submitButton);
            this.hostConnectPanel.Controls.Add(this.portLabel);
            this.hostConnectPanel.Controls.Add(this.passwordTextbox);
            this.hostConnectPanel.Controls.Add(this.portTextbox);
            this.hostConnectPanel.Controls.Add(this.passwordLabel);
            this.hostConnectPanel.Controls.Add(this.hostnameLabel);
            this.hostConnectPanel.Controls.Add(this.hostnameTextbox);
            this.hostConnectPanel.Location = new System.Drawing.Point(3, 3);
            this.hostConnectPanel.Name = "hostConnectPanel";
            this.hostConnectPanel.Size = new System.Drawing.Size(754, 615);
            this.hostConnectPanel.TabIndex = 4;
            this.hostConnectPanel.Visible = false;
            // 
            // hostSubmitButton
            // 
            this.hostSubmitButton.Location = new System.Drawing.Point(340, 106);
            this.hostSubmitButton.Name = "hostSubmitButton";
            this.hostSubmitButton.Size = new System.Drawing.Size(75, 23);
            this.hostSubmitButton.TabIndex = 4;
            this.hostSubmitButton.Text = "Connect";
            this.hostSubmitButton.UseVisualStyleBackColor = true;
            this.hostSubmitButton.Click += new System.EventHandler(this.hostSubmitButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(205, 71);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 3;
            this.portLabel.Text = "Port";
            // 
            // portTextbox
            // 
            this.portTextbox.Location = new System.Drawing.Point(237, 68);
            this.portTextbox.Name = "portTextbox";
            this.portTextbox.Size = new System.Drawing.Size(281, 20);
            this.portTextbox.TabIndex = 2;
            // 
            // hostnameLabel
            // 
            this.hostnameLabel.AutoSize = true;
            this.hostnameLabel.Location = new System.Drawing.Point(176, 29);
            this.hostnameLabel.Name = "hostnameLabel";
            this.hostnameLabel.Size = new System.Drawing.Size(55, 13);
            this.hostnameLabel.TabIndex = 1;
            this.hostnameLabel.Text = "Hostname";
            // 
            // hostnameTextbox
            // 
            this.hostnameTextbox.Location = new System.Drawing.Point(237, 24);
            this.hostnameTextbox.Name = "hostnameTextbox";
            this.hostnameTextbox.Size = new System.Drawing.Size(281, 20);
            this.hostnameTextbox.TabIndex = 0;
            // 
            // skyntaxServerName
            // 
            this.skyntaxServerName.Image = ((System.Drawing.Image)(resources.GetObject("skyntaxServerName.Image")));
            this.skyntaxServerName.Location = new System.Drawing.Point(188, 0);
            this.skyntaxServerName.Name = "skyntaxServerName";
            this.skyntaxServerName.Size = new System.Drawing.Size(384, 24);
            this.skyntaxServerName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.skyntaxServerName.TabIndex = 7;
            this.skyntaxServerName.TabStop = false;
            this.skyntaxServerName.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 627);
            this.Controls.Add(this.skyntaxServerName);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Spreadsheet - untitled";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.filelistPanel.ResumeLayout(false);
            this.filelistPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.hostConnectPanel.ResumeLayout(false);
            this.hostConnectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skyntaxServerName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox TextContents;
        private System.Windows.Forms.Label LabelValue1;
        private System.Windows.Forms.Label LabelContent;
        private System.Windows.Forms.Label LabelName1;
        private System.Windows.Forms.Label LabelName2;
        private System.Windows.Forms.Label LabelValue2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayDependentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllNegativesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem connectToServerToolStripMenuItem;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Panel filelistPanel;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label newFileLabel;
        private System.Windows.Forms.TextBox newFileBox;
        private System.Windows.Forms.Label filelistLabel;
        private System.Windows.Forms.ListBox filelistBox;
        private System.Windows.Forms.Button createFileButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem undoOnServerToolStripMenuItem;
        private System.Windows.Forms.Panel hostConnectPanel;
        private System.Windows.Forms.Button hostSubmitButton;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextbox;
        private System.Windows.Forms.Label hostnameLabel;
        private System.Windows.Forms.TextBox hostnameTextbox;
        private System.Windows.Forms.PictureBox skyntaxServerName;

    }
}

