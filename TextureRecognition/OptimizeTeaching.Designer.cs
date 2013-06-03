namespace TextureRecognition
{
    partial class OptimizeTeaching
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
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectSample = new System.Windows.Forms.Button();
            this.cbTextureClass = new System.Windows.Forms.ComboBox();
            this.btnSelectSamples = new System.Windows.Forms.Button();
            this.btnFull = new System.Windows.Forms.Button();
            this.btnDischarged = new System.Windows.Forms.Button();
            this.btnCompact = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pbSample = new System.Windows.Forms.PictureBox();
            this.pbWork = new System.Windows.Forms.PictureBox();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.openSample = new System.Windows.Forms.OpenFileDialog();
            this.openImages = new System.Windows.Forms.OpenFileDialog();
            this.btnAuto = new System.Windows.Forms.Button();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWork)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBack,
            this.tsmiClose,
            this.tsmiOptions});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(726, 40);
            this.msMenu.TabIndex = 0;
            this.msMenu.Text = "menuStrip1";
            // 
            // tsmiBack
            // 
            this.tsmiBack.Image = global::TextureRecognition.resources.back;
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(97, 36);
            this.tsmiBack.Text = "Назад";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tsmiClose.Image = global::TextureRecognition.resources.cancel;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(114, 36);
            this.tsmiClose.Text = "Закрыть";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Image = global::TextureRecognition.resources.settings;
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(136, 36);
            this.tsmiOptions.Text = "Параметры";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // btnSelectSample
            // 
            this.btnSelectSample.AutoSize = true;
            this.btnSelectSample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectSample.Image = global::TextureRecognition.resources.camera;
            this.btnSelectSample.Location = new System.Drawing.Point(12, 44);
            this.btnSelectSample.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectSample.Name = "btnSelectSample";
            this.btnSelectSample.Size = new System.Drawing.Size(137, 54);
            this.btnSelectSample.TabIndex = 3;
            this.btnSelectSample.Text = "Выбрать \r\nпримеры";
            this.btnSelectSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectSample.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectSample.UseVisualStyleBackColor = true;
            this.btnSelectSample.Click += new System.EventHandler(this.btnSelectSample_Click);
            // 
            // cbTextureClass
            // 
            this.cbTextureClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextureClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbTextureClass.FormattingEnabled = true;
            this.cbTextureClass.Location = new System.Drawing.Point(12, 105);
            this.cbTextureClass.Name = "cbTextureClass";
            this.cbTextureClass.Size = new System.Drawing.Size(137, 21);
            this.cbTextureClass.TabIndex = 4;
            // 
            // btnSelectSamples
            // 
            this.btnSelectSamples.AutoSize = true;
            this.btnSelectSamples.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectSamples.Image = global::TextureRecognition.resources.folder;
            this.btnSelectSamples.Location = new System.Drawing.Point(12, 133);
            this.btnSelectSamples.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectSamples.Name = "btnSelectSamples";
            this.btnSelectSamples.Size = new System.Drawing.Size(137, 54);
            this.btnSelectSamples.TabIndex = 5;
            this.btnSelectSamples.Text = "Выбрать \r\nпримеры\r\nдля обучения";
            this.btnSelectSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectSamples.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectSamples.UseVisualStyleBackColor = true;
            this.btnSelectSamples.Click += new System.EventHandler(this.btnSelectSamples_Click);
            // 
            // btnFull
            // 
            this.btnFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFull.AutoSize = true;
            this.btnFull.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFull.Image = global::TextureRecognition.resources.search;
            this.btnFull.Location = new System.Drawing.Point(12, 353);
            this.btnFull.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFull.Name = "btnFull";
            this.btnFull.Size = new System.Drawing.Size(137, 54);
            this.btnFull.TabIndex = 6;
            this.btnFull.Text = "Полная \r\nвыборка";
            this.btnFull.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFull.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFull.UseVisualStyleBackColor = true;
            this.btnFull.Click += new System.EventHandler(this.btnFull_Click);
            // 
            // btnDischarged
            // 
            this.btnDischarged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDischarged.AutoSize = true;
            this.btnDischarged.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDischarged.Image = global::TextureRecognition.resources.search;
            this.btnDischarged.Location = new System.Drawing.Point(12, 291);
            this.btnDischarged.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDischarged.Name = "btnDischarged";
            this.btnDischarged.Size = new System.Drawing.Size(137, 54);
            this.btnDischarged.TabIndex = 7;
            this.btnDischarged.Text = "Разряженная \r\nвыборка";
            this.btnDischarged.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDischarged.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDischarged.UseVisualStyleBackColor = true;
            this.btnDischarged.Click += new System.EventHandler(this.btnDischarged_Click);
            // 
            // btnCompact
            // 
            this.btnCompact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompact.AutoSize = true;
            this.btnCompact.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCompact.Image = global::TextureRecognition.resources.search;
            this.btnCompact.Location = new System.Drawing.Point(12, 229);
            this.btnCompact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCompact.Name = "btnCompact";
            this.btnCompact.Size = new System.Drawing.Size(137, 54);
            this.btnCompact.TabIndex = 8;
            this.btnCompact.Text = "Компактная \r\nвыборка";
            this.btnCompact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCompact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCompact.UseVisualStyleBackColor = true;
            this.btnCompact.Click += new System.EventHandler(this.btnCompact_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(155, 44);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbOutput);
            this.splitContainer1.Size = new System.Drawing.Size(559, 364);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pbSample);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pbWork);
            this.splitContainer2.Size = new System.Drawing.Size(559, 226);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.TabIndex = 0;
            // 
            // pbSample
            // 
            this.pbSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSample.Location = new System.Drawing.Point(0, 0);
            this.pbSample.Name = "pbSample";
            this.pbSample.Size = new System.Drawing.Size(279, 226);
            this.pbSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSample.TabIndex = 0;
            this.pbSample.TabStop = false;
            // 
            // pbWork
            // 
            this.pbWork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWork.Location = new System.Drawing.Point(0, 0);
            this.pbWork.Name = "pbWork";
            this.pbWork.Size = new System.Drawing.Size(276, 226);
            this.pbWork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWork.TabIndex = 0;
            this.pbWork.TabStop = false;
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.Location = new System.Drawing.Point(0, 0);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(559, 134);
            this.lbOutput.TabIndex = 0;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(12, 414);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(702, 17);
            this.pbProgress.TabIndex = 10;
            // 
            // openSample
            // 
            this.openSample.DefaultExt = "*.xml";
            this.openSample.Filter = "Data|*.xml";
            this.openSample.Multiselect = true;
            // 
            // openImages
            // 
            this.openImages.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            this.openImages.Multiselect = true;
            // 
            // btnAuto
            // 
            this.btnAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAuto.AutoSize = true;
            this.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAuto.Location = new System.Drawing.Point(12, 195);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(137, 26);
            this.btnAuto.TabIndex = 11;
            this.btnAuto.Text = "Прогнать автоматич.";
            this.btnAuto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // OptimizeTeaching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 444);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCompact);
            this.Controls.Add(this.btnDischarged);
            this.Controls.Add(this.btnFull);
            this.Controls.Add(this.btnSelectSamples);
            this.Controls.Add(this.cbTextureClass);
            this.Controls.Add(this.btnSelectSample);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.MinimumSize = new System.Drawing.Size(600, 460);
            this.Name = "OptimizeTeaching";
            this.Text = "Оптимизация обучающей выборки";
            this.VisibleChanged += new System.EventHandler(this.OptimizeTeaching_VisibleChanged);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.Button btnSelectSample;
        private System.Windows.Forms.ComboBox cbTextureClass;
        private System.Windows.Forms.Button btnSelectSamples;
        private System.Windows.Forms.Button btnFull;
        private System.Windows.Forms.Button btnDischarged;
        private System.Windows.Forms.Button btnCompact;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbOutput;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.OpenFileDialog openSample;
        private System.Windows.Forms.OpenFileDialog openImages;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pbSample;
        private System.Windows.Forms.PictureBox pbWork;
        private System.Windows.Forms.Button btnAuto;
    }
}