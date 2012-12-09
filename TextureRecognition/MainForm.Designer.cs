namespace TextureRecognition
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiResetKnowledges = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadKnowledges = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveKnoledges = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTeaching = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecognition = new System.Windows.Forms.ToolStripMenuItem();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.msMainMenu.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystem,
            this.tsmiTeaching,
            this.tsmiRecognition});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(444, 24);
            this.msMainMenu.TabIndex = 3;
            // 
            // tsmiSystem
            // 
            this.tsmiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetKnowledges,
            this.tsmiLoadKnowledges,
            this.tsmiSaveKnoledges,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiSystem.Name = "tsmiSystem";
            this.tsmiSystem.Size = new System.Drawing.Size(66, 20);
            this.tsmiSystem.Text = "Система";
            // 
            // tsmiResetKnowledges
            // 
            this.tsmiResetKnowledges.Name = "tsmiResetKnowledges";
            this.tsmiResetKnowledges.Size = new System.Drawing.Size(173, 22);
            this.tsmiResetKnowledges.Text = "Сбросить знания";
            // 
            // tsmiLoadKnowledges
            // 
            this.tsmiLoadKnowledges.Name = "tsmiLoadKnowledges";
            this.tsmiLoadKnowledges.Size = new System.Drawing.Size(173, 22);
            this.tsmiLoadKnowledges.Text = "Загрузить знания";
            // 
            // tsmiSaveKnoledges
            // 
            this.tsmiSaveKnoledges.Name = "tsmiSaveKnoledges";
            this.tsmiSaveKnoledges.Size = new System.Drawing.Size(173, 22);
            this.tsmiSaveKnoledges.Text = "Сохранить знания";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(173, 22);
            this.tsmiExit.Text = "Выход";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiTeaching
            // 
            this.tsmiTeaching.Name = "tsmiTeaching";
            this.tsmiTeaching.Size = new System.Drawing.Size(74, 20);
            this.tsmiTeaching.Text = "Обучение";
            this.tsmiTeaching.Click += new System.EventHandler(this.tsmiTeaching_Click);
            // 
            // tsmiRecognition
            // 
            this.tsmiRecognition.Name = "tsmiRecognition";
            this.tsmiRecognition.Size = new System.Drawing.Size(103, 20);
            this.tsmiRecognition.Text = "Распознование";
            this.tsmiRecognition.Click += new System.EventHandler(this.tsmiRecognition_Click);
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Location = new System.Drawing.Point(12, 27);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(420, 113);
            this.gbInfo.TabIndex = 4;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Информация";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 152);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.msMainMenu);
            this.MainMenuStrip = this.msMainMenu;
            this.MinimumSize = new System.Drawing.Size(460, 185);
            this.Name = "MainForm";
            this.Text = "Курсовая работа по МРО. Петровский А.В. гр. 107529";
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.gbInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetKnowledges;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadKnowledges;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveKnoledges;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiTeaching;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecognition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label label1;
    }
}

