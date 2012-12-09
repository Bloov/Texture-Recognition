namespace TextureRecognition
{
    partial class ManageTextureClasses
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbNewTexture = new System.Windows.Forms.GroupBox();
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.btnSelectColor = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.regionColor = new System.Windows.Forms.ColorDialog();
            this.gbTextures = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnTeach = new System.Windows.Forms.Button();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pbColor2 = new System.Windows.Forms.PictureBox();
            this.gbNewTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            this.gbTextures.SuspendLayout();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.AutoSize = true;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Image = global::TextureRecognition.resources.add;
            this.btnAdd.Location = new System.Drawing.Point(208, 62);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 54);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gbNewTexture
            // 
            this.gbNewTexture.Controls.Add(this.pbColor);
            this.gbNewTexture.Controls.Add(this.btnSelectColor);
            this.gbNewTexture.Controls.Add(this.btnAdd);
            this.gbNewTexture.Controls.Add(this.tbName);
            this.gbNewTexture.Controls.Add(this.lName);
            this.gbNewTexture.Location = new System.Drawing.Point(12, 159);
            this.gbNewTexture.Name = "gbNewTexture";
            this.gbNewTexture.Size = new System.Drawing.Size(329, 122);
            this.gbNewTexture.TabIndex = 2;
            this.gbNewTexture.TabStop = false;
            this.gbNewTexture.Text = "Новая текстура";
            // 
            // pbColor
            // 
            this.pbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbColor.Location = new System.Drawing.Point(6, 62);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(54, 54);
            this.pbColor.TabIndex = 6;
            this.pbColor.TabStop = false;
            this.pbColor.Click += new System.EventHandler(this.btnSelectColor_Click);
            // 
            // btnSelectColor
            // 
            this.btnSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectColor.AutoSize = true;
            this.btnSelectColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectColor.Image = global::TextureRecognition.resources.camera;
            this.btnSelectColor.Location = new System.Drawing.Point(66, 62);
            this.btnSelectColor.Name = "btnSelectColor";
            this.btnSelectColor.Size = new System.Drawing.Size(136, 54);
            this.btnSelectColor.TabIndex = 5;
            this.btnSelectColor.Text = "Выбрать цвет\r\nрегиона";
            this.btnSelectColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectColor.UseVisualStyleBackColor = true;
            this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Location = new System.Drawing.Point(41, 33);
            this.tbName.MaxLength = 16;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(282, 20);
            this.tbName.TabIndex = 4;
            this.tbName.Text = "Текстура";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(6, 36);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(29, 13);
            this.lName.TabIndex = 2;
            this.lName.Text = "Имя";
            // 
            // regionColor
            // 
            this.regionColor.Color = System.Drawing.Color.Peru;
            // 
            // gbTextures
            // 
            this.gbTextures.Controls.Add(this.pbColor2);
            this.gbTextures.Controls.Add(this.btnDelete);
            this.gbTextures.Controls.Add(this.btnTeach);
            this.gbTextures.Controls.Add(this.cbList);
            this.gbTextures.Location = new System.Drawing.Point(12, 43);
            this.gbTextures.Name = "gbTextures";
            this.gbTextures.Size = new System.Drawing.Size(329, 110);
            this.gbTextures.TabIndex = 3;
            this.gbTextures.TabStop = false;
            this.gbTextures.Text = "Текстуры";
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Image = global::TextureRecognition.resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(6, 46);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(143, 54);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnTeach
            // 
            this.btnTeach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeach.AutoSize = true;
            this.btnTeach.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTeach.Image = global::TextureRecognition.resources.next;
            this.btnTeach.Location = new System.Drawing.Point(155, 46);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(168, 54);
            this.btnTeach.TabIndex = 3;
            this.btnTeach.Text = "Перейти к обучению";
            this.btnTeach.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnTeach.UseVisualStyleBackColor = true;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // cbList
            // 
            this.cbList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbList.FormattingEnabled = true;
            this.cbList.Location = new System.Drawing.Point(33, 19);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(290, 21);
            this.cbList.TabIndex = 1;
            this.cbList.SelectedIndexChanged += new System.EventHandler(this.cbList_SelectedIndexChanged);
            // 
            // msMenu
            // 
            this.msMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose,
            this.tsmiExit});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(354, 40);
            this.msMenu.TabIndex = 0;
            this.msMenu.TabStop = true;
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
            // pbColor2
            // 
            this.pbColor2.Location = new System.Drawing.Point(6, 19);
            this.pbColor2.Name = "pbColor2";
            this.pbColor2.Size = new System.Drawing.Size(21, 21);
            this.pbColor2.TabIndex = 7;
            this.pbColor2.TabStop = false;
            // 
            // ManageTextureClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 287);
            this.Controls.Add(this.gbTextures);
            this.Controls.Add(this.gbNewTexture);
            this.Controls.Add(this.msMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.msMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 325);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 325);
            this.Name = "ManageTextureClasses";
            this.Text = "Управление классами текстур";
            this.gbNewTexture.ResumeLayout(false);
            this.gbNewTexture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            this.gbTextures.ResumeLayout(false);
            this.gbTextures.PerformLayout();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gbNewTexture;
        private System.Windows.Forms.Button btnSelectColor;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.ColorDialog regionColor;
        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.GroupBox gbTextures;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.PictureBox pbColor2;
    }
}