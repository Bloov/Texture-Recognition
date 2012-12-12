namespace TextureRecognition
{
    partial class RecognizeImage
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
            this.tsmiSaveResult = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.panelParameters = new System.Windows.Forms.Panel();
            this.cbShowImage = new System.Windows.Forms.CheckBox();
            this.gbRecognitionWays = new System.Windows.Forms.GroupBox();
            this.rbWay2 = new System.Windows.Forms.RadioButton();
            this.rbWay1 = new System.Windows.Forms.RadioButton();
            this.gbFeatures = new System.Windows.Forms.GroupBox();
            this.cbFeature2 = new System.Windows.Forms.CheckBox();
            this.cbFeature1 = new System.Windows.Forms.CheckBox();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnUpdateImage = new System.Windows.Forms.Button();
            this.msMenu.SuspendLayout();
            this.panelParameters.SuspendLayout();
            this.gbRecognitionWays.SuspendLayout();
            this.gbFeatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBack,
            this.tsmiClose,
            this.tsmiSaveResult,
            this.tsmiLegend,
            this.tsmiOptions});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(849, 40);
            this.msMenu.TabIndex = 0;
            // 
            // tsmiBack
            // 
            this.tsmiBack.Font = new System.Drawing.Font("Segoe UI", 14F);
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
            // tsmiSaveResult
            // 
            this.tsmiSaveResult.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiSaveResult.Image = global::TextureRecognition.resources.save;
            this.tsmiSaveResult.Name = "tsmiSaveResult";
            this.tsmiSaveResult.Size = new System.Drawing.Size(149, 36);
            this.tsmiSaveResult.Text = "Сохранить";
            this.tsmiSaveResult.Click += new System.EventHandler(this.tsmiSaveResult_Click);
            // 
            // tsmiLegend
            // 
            this.tsmiLegend.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiLegend.Image = global::TextureRecognition.resources.folder;
            this.tsmiLegend.Name = "tsmiLegend";
            this.tsmiLegend.Size = new System.Drawing.Size(127, 36);
            this.tsmiLegend.Text = "Легенда";
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tsmiOptions.Image = global::TextureRecognition.resources.settings;
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(156, 36);
            this.tsmiOptions.Text = "Параметры";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // panelParameters
            // 
            this.panelParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelParameters.Controls.Add(this.cbShowImage);
            this.panelParameters.Controls.Add(this.gbRecognitionWays);
            this.panelParameters.Controls.Add(this.gbFeatures);
            this.panelParameters.Location = new System.Drawing.Point(12, 225);
            this.panelParameters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.Size = new System.Drawing.Size(179, 219);
            this.panelParameters.TabIndex = 2;
            // 
            // cbShowImage
            // 
            this.cbShowImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowImage.AutoSize = true;
            this.cbShowImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbShowImage.Location = new System.Drawing.Point(14, 192);
            this.cbShowImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 8);
            this.cbShowImage.Name = "cbShowImage";
            this.cbShowImage.Size = new System.Drawing.Size(142, 17);
            this.cbShowImage.TabIndex = 2;
            this.cbShowImage.Text = "исходное изображение";
            this.cbShowImage.UseVisualStyleBackColor = true;
            this.cbShowImage.CheckedChanged += new System.EventHandler(this.cbShowImage_CheckedChanged);
            // 
            // gbRecognitionWays
            // 
            this.gbRecognitionWays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecognitionWays.Controls.Add(this.rbWay2);
            this.gbRecognitionWays.Controls.Add(this.rbWay1);
            this.gbRecognitionWays.Location = new System.Drawing.Point(3, 107);
            this.gbRecognitionWays.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbRecognitionWays.Name = "gbRecognitionWays";
            this.gbRecognitionWays.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbRecognitionWays.Size = new System.Drawing.Size(171, 78);
            this.gbRecognitionWays.TabIndex = 1;
            this.gbRecognitionWays.TabStop = false;
            this.gbRecognitionWays.Text = "Способ распознования";
            // 
            // rbWay2
            // 
            this.rbWay2.AutoSize = true;
            this.rbWay2.Checked = true;
            this.rbWay2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbWay2.Location = new System.Drawing.Point(11, 52);
            this.rbWay2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbWay2.Name = "rbWay2";
            this.rbWay2.Size = new System.Drawing.Size(113, 17);
            this.rbWay2.TabIndex = 1;
            this.rbWay2.TabStop = true;
            this.rbWay2.Text = "всё изображение";
            this.rbWay2.UseVisualStyleBackColor = true;
            // 
            // rbWay1
            // 
            this.rbWay1.AutoSize = true;
            this.rbWay1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbWay1.Location = new System.Drawing.Point(11, 24);
            this.rbWay1.Margin = new System.Windows.Forms.Padding(8, 8, 3, 8);
            this.rbWay1.Name = "rbWay1";
            this.rbWay1.Size = new System.Drawing.Size(130, 17);
            this.rbWay1.TabIndex = 0;
            this.rbWay1.Text = "выделенная область";
            this.rbWay1.UseVisualStyleBackColor = true;
            // 
            // gbFeatures
            // 
            this.gbFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFeatures.Controls.Add(this.cbFeature2);
            this.gbFeatures.Controls.Add(this.cbFeature1);
            this.gbFeatures.Location = new System.Drawing.Point(3, 4);
            this.gbFeatures.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbFeatures.Name = "gbFeatures";
            this.gbFeatures.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbFeatures.Size = new System.Drawing.Size(171, 95);
            this.gbFeatures.TabIndex = 0;
            this.gbFeatures.TabStop = false;
            this.gbFeatures.Text = "Текстурные признаки";
            // 
            // cbFeature2
            // 
            this.cbFeature2.AutoSize = true;
            this.cbFeature2.Checked = true;
            this.cbFeature2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFeature2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFeature2.Location = new System.Drawing.Point(11, 66);
            this.cbFeature2.Margin = new System.Windows.Forms.Padding(8, 4, 3, 4);
            this.cbFeature2.Name = "cbFeature2";
            this.cbFeature2.Size = new System.Drawing.Size(114, 17);
            this.cbFeature2.TabIndex = 1;
            this.cbFeature2.Text = "LBP гистограмма";
            this.cbFeature2.UseVisualStyleBackColor = true;
            this.cbFeature2.CheckedChanged += new System.EventHandler(this.cbFeature2_CheckedChanged);
            // 
            // cbFeature1
            // 
            this.cbFeature1.Checked = true;
            this.cbFeature1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFeature1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFeature1.Location = new System.Drawing.Point(11, 24);
            this.cbFeature1.Margin = new System.Windows.Forms.Padding(8, 8, 3, 8);
            this.cbFeature1.Name = "cbFeature1";
            this.cbFeature1.Size = new System.Drawing.Size(154, 30);
            this.cbFeature1.TabIndex = 0;
            this.cbFeature1.Text = "матрица взаимной встречаемости";
            this.cbFeature1.UseVisualStyleBackColor = true;
            this.cbFeature1.CheckedChanged += new System.EventHandler(this.cbFeature1_CheckedChanged);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(12, 450);
            this.pbProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbProgress.Maximum = 1000;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(825, 15);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 5;
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "*.jpg";
            this.saveImageDialog.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            // 
            // pbImage
            // 
            this.pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Location = new System.Drawing.Point(197, 43);
            this.pbImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(640, 401);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 4;
            this.pbImage.TabStop = false;
            this.pbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseDown);
            this.pbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseMove);
            this.pbImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseUp);
            // 
            // btnRecognize
            // 
            this.btnRecognize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecognize.Image = global::TextureRecognition.resources.search;
            this.btnRecognize.Location = new System.Drawing.Point(12, 105);
            this.btnRecognize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(179, 52);
            this.btnRecognize.TabIndex = 3;
            this.btnRecognize.Text = "Распознать\r\nтекстуры";
            this.btnRecognize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecognize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.btnRecognize_Click);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.AutoSize = true;
            this.btnSelectImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectImage.Image = global::TextureRecognition.resources.camera;
            this.btnSelectImage.Location = new System.Drawing.Point(12, 43);
            this.btnSelectImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(179, 54);
            this.btnSelectImage.TabIndex = 1;
            this.btnSelectImage.Text = "Выбрать\r\nизображение";
            this.btnSelectImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnUpdateImage
            // 
            this.btnUpdateImage.AutoSize = true;
            this.btnUpdateImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateImage.Image = global::TextureRecognition.resources.refresh;
            this.btnUpdateImage.Location = new System.Drawing.Point(12, 164);
            this.btnUpdateImage.Name = "btnUpdateImage";
            this.btnUpdateImage.Size = new System.Drawing.Size(179, 54);
            this.btnUpdateImage.TabIndex = 6;
            this.btnUpdateImage.Text = "Обновить \r\nизображение";
            this.btnUpdateImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdateImage.UseVisualStyleBackColor = true;
            // 
            // RecognizeImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 477);
            this.Controls.Add(this.btnUpdateImage);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.btnRecognize);
            this.Controls.Add(this.panelParameters);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(760, 515);
            this.Name = "RecognizeImage";
            this.Text = "Распознать изображение";
            this.VisibleChanged += new System.EventHandler(this.RecognizeImage_VisibleChanged);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.panelParameters.ResumeLayout(false);
            this.panelParameters.PerformLayout();
            this.gbRecognitionWays.ResumeLayout(false);
            this.gbRecognitionWays.PerformLayout();
            this.gbFeatures.ResumeLayout(false);
            this.gbFeatures.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.Button btnRecognize;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox gbFeatures;
        private System.Windows.Forms.CheckBox cbFeature1;
        private System.Windows.Forms.CheckBox cbShowImage;
        private System.Windows.Forms.GroupBox gbRecognitionWays;
        private System.Windows.Forms.RadioButton rbWay2;
        private System.Windows.Forms.RadioButton rbWay1;
        private System.Windows.Forms.CheckBox cbFeature2;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveResult;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.ToolStripMenuItem tsmiLegend;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.Button btnUpdateImage;
    }
}