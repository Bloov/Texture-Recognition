namespace TextureRecognition
{
    partial class TeachTextureClass
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
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChandeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNext = new System.Windows.Forms.ToolStripMenuItem();
            this.openImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.pbTeach = new System.Windows.Forms.ProgressBar();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lwImages = new System.Windows.Forms.ListView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnTeachingControl = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExit,
            this.tsmiClose,
            this.tsmiPrev,
            this.tsmiClass,
            this.tsmiNext});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(760, 40);
            this.msMenu.TabIndex = 0;
            // 
            // tsmiExit
            // 
            this.tsmiExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiExit.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiExit.Image = global::TextureRecognition.resources.cancel;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsmiExit.Size = new System.Drawing.Size(127, 36);
            this.tsmiExit.Text = "Закрыть";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiClose.Image = global::TextureRecognition.resources.back;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(107, 36);
            this.tsmiClose.Text = "Назад";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsmiPrev
            // 
            this.tsmiPrev.AutoToolTip = true;
            this.tsmiPrev.Image = global::TextureRecognition.resources.back;
            this.tsmiPrev.Margin = new System.Windows.Forms.Padding(80, 0, 0, 0);
            this.tsmiPrev.Name = "tsmiPrev";
            this.tsmiPrev.Size = new System.Drawing.Size(44, 36);
            this.tsmiPrev.ToolTipText = "Перейти к предыдущему классу";
            this.tsmiPrev.Click += new System.EventHandler(this.tsmiPrev_Click);
            // 
            // tsmiClass
            // 
            this.tsmiClass.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangeName,
            this.tsmiChandeColor});
            this.tsmiClass.Name = "tsmiClass";
            this.tsmiClass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsmiClass.Size = new System.Drawing.Size(51, 36);
            this.tsmiClass.Text = "Класс";
            // 
            // tsmiChangeName
            // 
            this.tsmiChangeName.Name = "tsmiChangeName";
            this.tsmiChangeName.Size = new System.Drawing.Size(155, 22);
            this.tsmiChangeName.Text = "Изменить имя";
            this.tsmiChangeName.Click += new System.EventHandler(this.tsmiChangeName_Click);
            // 
            // tsmiChandeColor
            // 
            this.tsmiChandeColor.Name = "tsmiChandeColor";
            this.tsmiChandeColor.Size = new System.Drawing.Size(155, 22);
            this.tsmiChandeColor.Text = "Изменить цвет";
            this.tsmiChandeColor.Click += new System.EventHandler(this.tsmiChandeColor_Click);
            // 
            // tsmiNext
            // 
            this.tsmiNext.AutoToolTip = true;
            this.tsmiNext.Image = global::TextureRecognition.resources.next;
            this.tsmiNext.Name = "tsmiNext";
            this.tsmiNext.Size = new System.Drawing.Size(44, 36);
            this.tsmiNext.ToolTipText = "Перейти к следующему классу";
            this.tsmiNext.Click += new System.EventHandler(this.tsmiNext_Click);
            // 
            // openImagesDialog
            // 
            this.openImagesDialog.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            this.openImagesDialog.Multiselect = true;
            // 
            // pbTeach
            // 
            this.pbTeach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTeach.Location = new System.Drawing.Point(12, 421);
            this.pbTeach.Maximum = 1000;
            this.pbTeach.Name = "pbTeach";
            this.pbTeach.Size = new System.Drawing.Size(736, 15);
            this.pbTeach.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbTeach.TabIndex = 6;
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
            this.splitContainer.Panel1.Controls.Add(this.lwImages);
            this.splitContainer.Panel1MinSize = 100;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pbImage);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(736, 312);
            this.splitContainer.SplitterDistance = 399;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 8;
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
            this.lwImages.Size = new System.Drawing.Size(397, 310);
            this.lwImages.TabIndex = 3;
            this.lwImages.TileSize = new System.Drawing.Size(168, 60);
            this.lwImages.UseCompatibleStateImageBehavior = false;
            this.lwImages.SelectedIndexChanged += new System.EventHandler(this.lwImages_SelectedIndexChanged);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(329, 310);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            // 
            // btnTeachingControl
            // 
            this.btnTeachingControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeachingControl.AutoSize = true;
            this.btnTeachingControl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTeachingControl.Image = global::TextureRecognition.resources.refresh;
            this.btnTeachingControl.Location = new System.Drawing.Point(598, 43);
            this.btnTeachingControl.Name = "btnTeachingControl";
            this.btnTeachingControl.Size = new System.Drawing.Size(150, 54);
            this.btnTeachingControl.TabIndex = 7;
            this.btnTeachingControl.Text = "Начать обучение";
            this.btnTeachingControl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTeachingControl.UseVisualStyleBackColor = true;
            this.btnTeachingControl.Click += new System.EventHandler(this.btnTeachingControl_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.AutoSize = true;
            this.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteAll.Image = global::TextureRecognition.resources.delete;
            this.btnDeleteAll.Location = new System.Drawing.Point(262, 43);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(129, 54);
            this.btnDeleteAll.TabIndex = 5;
            this.btnDeleteAll.Text = "Удалить все";
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.AutoSize = true;
            this.btnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteSelected.Image = global::TextureRecognition.resources.minus;
            this.btnDeleteSelected.Location = new System.Drawing.Point(133, 43);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(123, 54);
            this.btnDeleteSelected.TabIndex = 4;
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
            this.btnSelectFiles.TabIndex = 1;
            this.btnSelectFiles.Text = "Добавить";
            this.btnSelectFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // TeachTextureClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 448);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.btnTeachingControl);
            this.Controls.Add(this.pbTeach);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDeleteSelected);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.msMenu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.msMenu;
            this.MinimumSize = new System.Drawing.Size(570, 350);
            this.Name = "TeachTextureClass";
            this.Text = "Обучение класса текстур";
            this.VisibleChanged += new System.EventHandler(this.TeachTextureClass_VisibleChanged);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.OpenFileDialog openImagesDialog;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.ProgressBar pbTeach;
        private System.Windows.Forms.Button btnTeachingControl;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiClass;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrev;
        private System.Windows.Forms.ToolStripMenuItem tsmiNext;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView lwImages;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeName;
        private System.Windows.Forms.ToolStripMenuItem tsmiChandeColor;
    }
}