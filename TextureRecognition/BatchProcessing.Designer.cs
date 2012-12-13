namespace TextureRecognition
{
    partial class BatchProcessing
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Известные образцы", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Неизвестные образцы", System.Windows.Forms.HorizontalAlignment.Left);
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnRecognitionControl = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lwImages = new System.Windows.Forms.ListView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lbResult = new System.Windows.Forms.ListBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBack,
            this.tsmiClose});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(604, 40);
            this.msMenu.TabIndex = 0;
            // 
            // tsmiBack
            // 
            this.tsmiBack.Image = global::TextureRecognition.resources.back;
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(107, 36);
            this.tsmiBack.Text = "Назад";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiClose.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiClose.Image = global::TextureRecognition.resources.cancel;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsmiClose.Size = new System.Drawing.Size(127, 36);
            this.tsmiClose.Text = "Закрыть";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            this.openImageDialog.Multiselect = true;
            // 
            // btnRecognitionControl
            // 
            this.btnRecognitionControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecognitionControl.AutoSize = true;
            this.btnRecognitionControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecognitionControl.Image = global::TextureRecognition.resources.refresh;
            this.btnRecognitionControl.Location = new System.Drawing.Point(467, 43);
            this.btnRecognitionControl.Name = "btnRecognitionControl";
            this.btnRecognitionControl.Size = new System.Drawing.Size(125, 54);
            this.btnRecognitionControl.TabIndex = 10;
            this.btnRecognitionControl.Text = "Распознать";
            this.btnRecognitionControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRecognitionControl.UseVisualStyleBackColor = true;
            this.btnRecognitionControl.Click += new System.EventHandler(this.btnRecognitionControl_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.AutoSize = true;
            this.btnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteSelected.Image = global::TextureRecognition.resources.minus;
            this.btnDeleteSelected.Location = new System.Drawing.Point(133, 43);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(123, 54);
            this.btnDeleteSelected.TabIndex = 9;
            this.btnDeleteSelected.Text = "Удалить \r\nвыбранные";
            this.btnDeleteSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.AutoSize = true;
            this.btnSelectFiles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectFiles.Image = global::TextureRecognition.resources.add;
            this.btnSelectFiles.Location = new System.Drawing.Point(12, 43);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(115, 54);
            this.btnSelectFiles.TabIndex = 8;
            this.btnSelectFiles.Text = "Добавить";
            this.btnSelectFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.AutoSize = true;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteAll.Image = global::TextureRecognition.resources.delete;
            this.btnDeleteAll.Location = new System.Drawing.Point(262, 43);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(129, 54);
            this.btnDeleteAll.TabIndex = 11;
            this.btnDeleteAll.Text = "Удалить все";
            this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Location = new System.Drawing.Point(12, 103);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer.Panel1MinSize = 100;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lbResult);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(580, 321);
            this.splitContainer.SplitterDistance = 366;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lwImages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pbImage);
            this.splitContainer1.Size = new System.Drawing.Size(366, 321);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // lwImages
            // 
            this.lwImages.BackColor = System.Drawing.SystemColors.Control;
            this.lwImages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lwImages.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Известные образцы";
            listViewGroup1.Name = "lvgKnownSamples";
            listViewGroup2.Header = "Неизвестные образцы";
            listViewGroup2.Name = "lvgUnknownSamples";
            this.lwImages.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lwImages.Location = new System.Drawing.Point(0, 0);
            this.lwImages.Name = "lwImages";
            this.lwImages.Size = new System.Drawing.Size(364, 215);
            this.lwImages.TabIndex = 4;
            this.lwImages.TileSize = new System.Drawing.Size(168, 60);
            this.lwImages.UseCompatibleStateImageBehavior = false;
            this.lwImages.SelectedIndexChanged += new System.EventHandler(this.lwImages_SelectedIndexChanged);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(364, 96);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // lbResult
            // 
            this.lbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResult.FormattingEnabled = true;
            this.lbResult.Location = new System.Drawing.Point(0, 0);
            this.lbResult.Name = "lbResult";
            this.lbResult.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbResult.Size = new System.Drawing.Size(206, 319);
            this.lbResult.TabIndex = 0;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(12, 430);
            this.pbProgress.Maximum = 1000;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(580, 15);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 13;
            // 
            // BatchProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 457);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnRecognitionControl);
            this.Controls.Add(this.btnDeleteSelected);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.MinimumSize = new System.Drawing.Size(620, 495);
            this.Name = "BatchProcessing";
            this.Text = "Пакетная обработка изображений";
            this.VisibleChanged += new System.EventHandler(this.BatchProcessing_VisibleChanged);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Button btnRecognitionControl;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lwImages;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ListBox lbResult;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}