namespace TextureRecognition
{
    partial class CreateSample
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
            this.pbSample = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbTextureClasses = new System.Windows.Forms.ListBox();
            this.pbTextureClass = new System.Windows.Forms.PictureBox();
            this.openImage = new System.Windows.Forms.OpenFileDialog();
            this.saveSample = new System.Windows.Forms.SaveFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.openSample = new System.Windows.Forms.OpenFileDialog();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextureClass)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBack,
            this.tsmiClose});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(844, 40);
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
            this.tsmiClose.Image = global::TextureRecognition.resources.cancel;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsmiClose.Size = new System.Drawing.Size(114, 36);
            this.tsmiClose.Text = "Закрыть";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // pbSample
            // 
            this.pbSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSample.Location = new System.Drawing.Point(197, 43);
            this.pbSample.Name = "pbSample";
            this.pbSample.Size = new System.Drawing.Size(635, 416);
            this.pbSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSample.TabIndex = 1;
            this.pbSample.TabStop = false;
            this.pbSample.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbSample_MouseDown);
            this.pbSample.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSample_MouseMove);
            this.pbSample.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbSample_MouseUp);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.AutoSize = true;
            this.btnSelectImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectImage.Image = global::TextureRecognition.resources.camera;
            this.btnSelectImage.Location = new System.Drawing.Point(12, 44);
            this.btnSelectImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(179, 54);
            this.btnSelectImage.TabIndex = 2;
            this.btnSelectImage.Text = "Выбрать\r\nизображение";
            this.btnSelectImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Image = global::TextureRecognition.resources.save;
            this.btnSave.Location = new System.Drawing.Point(12, 168);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(179, 54);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить \r\nпример";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbTextureClasses
            // 
            this.lbTextureClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTextureClasses.FormattingEnabled = true;
            this.lbTextureClasses.Location = new System.Drawing.Point(12, 268);
            this.lbTextureClasses.Name = "lbTextureClasses";
            this.lbTextureClasses.Size = new System.Drawing.Size(179, 186);
            this.lbTextureClasses.TabIndex = 5;
            this.lbTextureClasses.SelectedIndexChanged += new System.EventHandler(this.lbTextureClasses_SelectedIndexChanged);
            // 
            // pbTextureClass
            // 
            this.pbTextureClass.Location = new System.Drawing.Point(12, 229);
            this.pbTextureClass.Name = "pbTextureClass";
            this.pbTextureClass.Size = new System.Drawing.Size(179, 30);
            this.pbTextureClass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTextureClass.TabIndex = 6;
            this.pbTextureClass.TabStop = false;
            // 
            // openImage
            // 
            this.openImage.Filter = "All Images|*.jpg;*.jpeg;*.png;*.bmp|JPG Image|*.jpg;*.jpeg|PNG Image|*.png|BMP Im" +
    "age|*.bmp";
            // 
            // saveSample
            // 
            this.saveSample.DefaultExt = "xml";
            this.saveSample.Filter = "Data|*.xml";
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = true;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpen.Image = global::TextureRecognition.resources.folder;
            this.btnOpen.Location = new System.Drawing.Point(12, 106);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(179, 54);
            this.btnOpen.TabIndex = 7;
            this.btnOpen.Text = "Открыть \r\nпример";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // openSample
            // 
            this.openSample.Filter = "Data|*.xml";
            // 
            // CreateSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 471);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pbTextureClass);
            this.Controls.Add(this.lbTextureClasses);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.pbSample);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.Name = "CreateSample";
            this.Text = "Создание примера распознаного изображения";
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextureClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.PictureBox pbSample;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lbTextureClasses;
        private System.Windows.Forms.PictureBox pbTextureClass;
        private System.Windows.Forms.OpenFileDialog openImage;
        private System.Windows.Forms.SaveFileDialog saveSample;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog openSample;
    }
}