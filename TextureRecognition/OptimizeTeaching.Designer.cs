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
            this.btnSelectSample = new System.Windows.Forms.Button();
            this.cbTextureClass = new System.Windows.Forms.ComboBox();
            this.btnSelectSamples = new System.Windows.Forms.Button();
            this.btnFull = new System.Windows.Forms.Button();
            this.btnDischarged = new System.Windows.Forms.Button();
            this.btnCompact = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbSamples = new System.Windows.Forms.ListBox();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.openSample = new System.Windows.Forms.OpenFileDialog();
            this.openImages = new System.Windows.Forms.OpenFileDialog();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.msMenu.Size = new System.Drawing.Size(829, 40);
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
            // btnSelectSample
            // 
            this.btnSelectSample.AutoSize = true;
            this.btnSelectSample.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectSample.Image = global::TextureRecognition.resources.camera;
            this.btnSelectSample.Location = new System.Drawing.Point(12, 44);
            this.btnSelectSample.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectSample.Name = "btnSelectSample";
            this.btnSelectSample.Size = new System.Drawing.Size(179, 54);
            this.btnSelectSample.TabIndex = 3;
            this.btnSelectSample.Text = "Выбрать пример\r\nдля распознавания";
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
            this.cbTextureClass.Size = new System.Drawing.Size(179, 21);
            this.cbTextureClass.TabIndex = 4;
            this.cbTextureClass.SelectedIndexChanged += new System.EventHandler(this.cbTextureClass_SelectedIndexChanged);
            // 
            // btnSelectSamples
            // 
            this.btnSelectSamples.AutoSize = true;
            this.btnSelectSamples.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectSamples.Image = global::TextureRecognition.resources.folder;
            this.btnSelectSamples.Location = new System.Drawing.Point(12, 133);
            this.btnSelectSamples.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectSamples.Name = "btnSelectSamples";
            this.btnSelectSamples.Size = new System.Drawing.Size(179, 54);
            this.btnSelectSamples.TabIndex = 5;
            this.btnSelectSamples.Text = "Выбрать примеры\r\nдля обучения";
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
            this.btnFull.Location = new System.Drawing.Point(12, 360);
            this.btnFull.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFull.Name = "btnFull";
            this.btnFull.Size = new System.Drawing.Size(179, 54);
            this.btnFull.TabIndex = 6;
            this.btnFull.Text = "Полная выборка";
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
            this.btnDischarged.Location = new System.Drawing.Point(12, 298);
            this.btnDischarged.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDischarged.Name = "btnDischarged";
            this.btnDischarged.Size = new System.Drawing.Size(179, 54);
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
            this.btnCompact.Location = new System.Drawing.Point(12, 236);
            this.btnCompact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCompact.Name = "btnCompact";
            this.btnCompact.Size = new System.Drawing.Size(179, 54);
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
            this.splitContainer1.Location = new System.Drawing.Point(197, 44);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbSamples);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbOutput);
            this.splitContainer1.Size = new System.Drawing.Size(620, 371);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 9;
            // 
            // lbSamples
            // 
            this.lbSamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSamples.FormattingEnabled = true;
            this.lbSamples.Location = new System.Drawing.Point(0, 0);
            this.lbSamples.Name = "lbSamples";
            this.lbSamples.Size = new System.Drawing.Size(620, 231);
            this.lbSamples.TabIndex = 0;
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.Location = new System.Drawing.Point(0, 0);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(620, 136);
            this.lbOutput.TabIndex = 0;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(12, 421);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(805, 17);
            this.pbProgress.TabIndex = 10;
            // 
            // openSample
            // 
            this.openSample.DefaultExt = "*.xml";
            this.openSample.Filter = "Data|*.xml";
            // 
            // openImages
            // 
            this.openImages.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            this.openImages.Multiselect = true;
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Image = global::TextureRecognition.resources.settings;
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(136, 36);
            this.tsmiOptions.Text = "Параметры";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // OptimizeTeaching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 451);
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
            this.Name = "OptimizeTeaching";
            this.Text = "Оптимизация обучающей выборки";
            this.VisibleChanged += new System.EventHandler(this.OptimizeTeaching_VisibleChanged);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox lbSamples;
        private System.Windows.Forms.ListBox lbOutput;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.OpenFileDialog openSample;
        private System.Windows.Forms.OpenFileDialog openImages;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
    }
}