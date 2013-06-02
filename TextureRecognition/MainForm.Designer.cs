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
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTeaching = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecognition = new System.Windows.Forms.ToolStripMenuItem();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.saveKnowledgesDialog = new System.Windows.Forms.SaveFileDialog();
            this.openKnowledgesDialog = new System.Windows.Forms.OpenFileDialog();
            this.tsmiOptimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateSample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptimizeTeachig = new System.Windows.Forms.ToolStripMenuItem();
            this.msMainMenu.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainMenu
            // 
            this.msMainMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystem,
            this.tsmiTeaching,
            this.tsmiRecognition,
            this.tsmiOptimize});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(584, 27);
            this.msMainMenu.TabIndex = 3;
            // 
            // tsmiSystem
            // 
            this.tsmiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetKnowledges,
            this.tsmiLoadKnowledges,
            this.tsmiSaveKnoledges,
            this.tsSeparator,
            this.tsmiExit});
            this.tsmiSystem.Name = "tsmiSystem";
            this.tsmiSystem.Size = new System.Drawing.Size(74, 23);
            this.tsmiSystem.Text = "Система";
            // 
            // tsmiResetKnowledges
            // 
            this.tsmiResetKnowledges.Name = "tsmiResetKnowledges";
            this.tsmiResetKnowledges.Size = new System.Drawing.Size(193, 24);
            this.tsmiResetKnowledges.Text = "Сбросить знания";
            this.tsmiResetKnowledges.Click += new System.EventHandler(this.tsmiResetKnowledges_Click);
            // 
            // tsmiLoadKnowledges
            // 
            this.tsmiLoadKnowledges.Name = "tsmiLoadKnowledges";
            this.tsmiLoadKnowledges.Size = new System.Drawing.Size(193, 24);
            this.tsmiLoadKnowledges.Text = "Загрузить знания";
            this.tsmiLoadKnowledges.Click += new System.EventHandler(this.tsmiLoadKnowledges_Click);
            // 
            // tsmiSaveKnoledges
            // 
            this.tsmiSaveKnoledges.Name = "tsmiSaveKnoledges";
            this.tsmiSaveKnoledges.Size = new System.Drawing.Size(193, 24);
            this.tsmiSaveKnoledges.Text = "Сохранить знания";
            this.tsmiSaveKnoledges.Click += new System.EventHandler(this.tsmiSaveKnoledges_Click);
            // 
            // tsSeparator
            // 
            this.tsSeparator.Name = "tsSeparator";
            this.tsSeparator.Size = new System.Drawing.Size(190, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(193, 24);
            this.tsmiExit.Text = "Выход";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiTeaching
            // 
            this.tsmiTeaching.Name = "tsmiTeaching";
            this.tsmiTeaching.Size = new System.Drawing.Size(85, 23);
            this.tsmiTeaching.Text = "Обучение";
            this.tsmiTeaching.Click += new System.EventHandler(this.tsmiTeaching_Click);
            // 
            // tsmiRecognition
            // 
            this.tsmiRecognition.Name = "tsmiRecognition";
            this.tsmiRecognition.Size = new System.Drawing.Size(117, 23);
            this.tsmiRecognition.Text = "Распознование";
            this.tsmiRecognition.Click += new System.EventHandler(this.tsmiRecognition_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.lblInfo);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 27);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(584, 245);
            this.panelInfo.TabIndex = 4;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(8);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(584, 245);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = resources.GetString("lblInfo.Text");
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveKnowledgesDialog
            // 
            this.saveKnowledgesDialog.DefaultExt = "*.xml";
            this.saveKnowledgesDialog.Filter = "XML Files|*.xml";
            // 
            // openKnowledgesDialog
            // 
            this.openKnowledgesDialog.DefaultExt = "*.xml";
            this.openKnowledgesDialog.FileName = "openFileDialog1";
            this.openKnowledgesDialog.Filter = "XML Files|*.xml";
            // 
            // tsmiOptimize
            // 
            this.tsmiOptimize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateSample,
            this.tsmiOptimizeTeachig});
            this.tsmiOptimize.Name = "tsmiOptimize";
            this.tsmiOptimize.Size = new System.Drawing.Size(108, 23);
            this.tsmiOptimize.Text = "Оптимизация";
            // 
            // tsmiCreateSample
            // 
            this.tsmiCreateSample.Name = "tsmiCreateSample";
            this.tsmiCreateSample.Size = new System.Drawing.Size(225, 24);
            this.tsmiCreateSample.Text = "Создание примера";
            this.tsmiCreateSample.Click += new System.EventHandler(this.tsmiCreateSample_Click);
            // 
            // tsmiOptimizeTeachig
            // 
            this.tsmiOptimizeTeachig.Name = "tsmiOptimizeTeachig";
            this.tsmiOptimizeTeachig.Size = new System.Drawing.Size(225, 24);
            this.tsmiOptimizeTeachig.Text = "Оптимизация выборки";
            this.tsmiOptimizeTeachig.Click += new System.EventHandler(this.tsmiOptimizeTeachig_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 272);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.msMainMenu);
            this.MainMenuStrip = this.msMainMenu;
            this.MinimumSize = new System.Drawing.Size(600, 310);
            this.Name = "MainForm";
            this.Text = "Курсовая работа по МРО. Петровский А.В. гр. 107529";
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.panelInfo.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.SaveFileDialog saveKnowledgesDialog;
        private System.Windows.Forms.OpenFileDialog openKnowledgesDialog;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptimize;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateSample;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptimizeTeachig;
    }
}

