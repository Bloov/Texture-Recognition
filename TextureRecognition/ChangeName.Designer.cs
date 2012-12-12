namespace TextureRecognition
{
    partial class ChangeName
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbName.Location = new System.Drawing.Point(12, 47);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbName.MaxLength = 16;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(286, 23);
            this.tbName.TabIndex = 0;
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Location = new System.Drawing.Point(8, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(8, 3, 3, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(106, 24);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Новое имя";
            // 
            // btnDecline
            // 
            this.btnDecline.AutoSize = true;
            this.btnDecline.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDecline.Image = global::TextureRecognition.resources.cancel;
            this.btnDecline.Location = new System.Drawing.Point(12, 81);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(140, 54);
            this.btnDecline.TabIndex = 2;
            this.btnDecline.Text = "Отмена";
            this.btnDecline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // btnApply
            // 
            this.btnApply.AutoSize = true;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Image = global::TextureRecognition.resources.check;
            this.btnApply.Location = new System.Drawing.Point(158, 81);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(140, 54);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Применить";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ChangeName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 146);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tbName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeName";
            this.Text = "Изменение имени текстурного класса";
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.ChangeName_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnDecline;
    }
}