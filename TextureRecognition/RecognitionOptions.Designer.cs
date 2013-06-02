namespace TextureRecognition
{
    partial class RecognitionOptions
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
            this.lblFragmentSize = new System.Windows.Forms.Label();
            this.tbFragmentSize = new System.Windows.Forms.TextBox();
            this.lblDeviation = new System.Windows.Forms.Label();
            this.tbDeviation = new System.Windows.Forms.TextBox();
            this.lblNeigbors = new System.Windows.Forms.Label();
            this.tbNeighbors = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.tbCompactFacotor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDischargeFactor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFragmentSize
            // 
            this.lblFragmentSize.AutoSize = true;
            this.lblFragmentSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFragmentSize.Location = new System.Drawing.Point(12, 17);
            this.lblFragmentSize.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.lblFragmentSize.Name = "lblFragmentSize";
            this.lblFragmentSize.Size = new System.Drawing.Size(361, 48);
            this.lblFragmentSize.TabIndex = 0;
            this.lblFragmentSize.Text = "Размер фрагмента для распознования\r\n(не менее)";
            // 
            // tbFragmentSize
            // 
            this.tbFragmentSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFragmentSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFragmentSize.Location = new System.Drawing.Point(16, 76);
            this.tbFragmentSize.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbFragmentSize.MaxLength = 10;
            this.tbFragmentSize.Name = "tbFragmentSize";
            this.tbFragmentSize.Size = new System.Drawing.Size(104, 23);
            this.tbFragmentSize.TabIndex = 0;
            this.tbFragmentSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFragmentSize_KeyPress);
            this.tbFragmentSize.Leave += new System.EventHandler(this.tbFragmentSize_Leave);
            // 
            // lblDeviation
            // 
            this.lblDeviation.AutoSize = true;
            this.lblDeviation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDeviation.Location = new System.Drawing.Point(12, 107);
            this.lblDeviation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblDeviation.Name = "lblDeviation";
            this.lblDeviation.Size = new System.Drawing.Size(341, 48);
            this.lblDeviation.TabIndex = 2;
            this.lblDeviation.Text = "Веса компонентов дисперсии при\r\nвычеслении растояний между ПМВВ";
            // 
            // tbDeviation
            // 
            this.tbDeviation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDeviation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDeviation.Location = new System.Drawing.Point(16, 166);
            this.tbDeviation.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbDeviation.MaxLength = 10;
            this.tbDeviation.Name = "tbDeviation";
            this.tbDeviation.Size = new System.Drawing.Size(104, 23);
            this.tbDeviation.TabIndex = 1;
            this.tbDeviation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRejectsLevel_KeyPress);
            this.tbDeviation.Leave += new System.EventHandler(this.tbRejectsLevel_Leave);
            // 
            // lblNeigbors
            // 
            this.lblNeigbors.AutoSize = true;
            this.lblNeigbors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNeigbors.Location = new System.Drawing.Point(12, 197);
            this.lblNeigbors.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblNeigbors.Name = "lblNeigbors";
            this.lblNeigbors.Size = new System.Drawing.Size(343, 48);
            this.lblNeigbors.TabIndex = 4;
            this.lblNeigbors.Text = "Количество ближайших соседей при\r\nвыборе наиболее похожего класса";
            // 
            // tbNeighbors
            // 
            this.tbNeighbors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNeighbors.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNeighbors.Location = new System.Drawing.Point(16, 256);
            this.tbNeighbors.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbNeighbors.MaxLength = 10;
            this.tbNeighbors.Name = "tbNeighbors";
            this.tbNeighbors.Size = new System.Drawing.Size(104, 23);
            this.tbNeighbors.TabIndex = 2;
            this.tbNeighbors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNeighbors_KeyPress);
            this.tbNeighbors.Leave += new System.EventHandler(this.tbNeighbors_Leave);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.AutoSize = true;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Image = global::TextureRecognition.resources.check;
            this.btnApply.Location = new System.Drawing.Point(446, 379);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(140, 54);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Применить";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDecline.AutoSize = true;
            this.btnDecline.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDecline.Image = global::TextureRecognition.resources.cancel;
            this.btnDecline.Location = new System.Drawing.Point(12, 379);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(140, 54);
            this.btnDecline.TabIndex = 4;
            this.btnDecline.Text = "Отмена";
            this.btnDecline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // tbCompactFacotor
            // 
            this.tbCompactFacotor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCompactFacotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCompactFacotor.Location = new System.Drawing.Point(16, 346);
            this.tbCompactFacotor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbCompactFacotor.MaxLength = 10;
            this.tbCompactFacotor.Name = "tbCompactFacotor";
            this.tbCompactFacotor.Size = new System.Drawing.Size(104, 23);
            this.tbCompactFacotor.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 311);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Фактор компактности\r\n";
            // 
            // tbDischargeFactor
            // 
            this.tbDischargeFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDischargeFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbDischargeFactor.Location = new System.Drawing.Point(365, 346);
            this.tbDischargeFactor.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.tbDischargeFactor.MaxLength = 10;
            this.tbDischargeFactor.Name = "tbDischargeFactor";
            this.tbDischargeFactor.Size = new System.Drawing.Size(104, 23);
            this.tbDischargeFactor.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(361, 287);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 48);
            this.label2.TabIndex = 8;
            this.label2.Text = "Фактор \r\nразряженности\r\n";
            // 
            // RecognitionOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 445);
            this.Controls.Add(this.tbDischargeFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCompactFacotor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tbNeighbors);
            this.Controls.Add(this.lblNeigbors);
            this.Controls.Add(this.tbDeviation);
            this.Controls.Add(this.lblDeviation);
            this.Controls.Add(this.tbFragmentSize);
            this.Controls.Add(this.lblFragmentSize);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 394);
            this.Name = "RecognitionOptions";
            this.Text = "Настройки параметров распознавания";
            this.VisibleChanged += new System.EventHandler(this.RecognitionOptions_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFragmentSize;
        private System.Windows.Forms.TextBox tbFragmentSize;
        private System.Windows.Forms.Label lblDeviation;
        private System.Windows.Forms.TextBox tbDeviation;
        private System.Windows.Forms.Label lblNeigbors;
        private System.Windows.Forms.TextBox tbNeighbors;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.TextBox tbCompactFacotor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDischargeFactor;
        private System.Windows.Forms.Label label2;

    }
}