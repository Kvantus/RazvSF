namespace RazvSF
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Bend = new System.Windows.Forms.Button();
            this.BBezB = new System.Windows.Forms.Button();
            this.BTestik = new System.Windows.Forms.Button();
            this.BUPD1 = new System.Windows.Forms.Button();
            this.ChBxNeedCopy = new System.Windows.Forms.CheckBox();
            this.BUPDSave = new System.Windows.Forms.Button();
            this.BBezBSave = new System.Windows.Forms.Button();
            this.FixButton = new System.Windows.Forms.Button();
            this.LStatus = new System.Windows.Forms.Label();
            this.RTfiles = new System.Windows.Forms.RichTextBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.BChoseFolder = new System.Windows.Forms.Button();
            this.LFolder = new System.Windows.Forms.Label();
            this.BMassBoom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Bend
            // 
            this.Bend.BackColor = System.Drawing.Color.Red;
            this.Bend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Bend.Location = new System.Drawing.Point(30, 13);
            this.Bend.Name = "Bend";
            this.Bend.Size = new System.Drawing.Size(156, 55);
            this.Bend.TabIndex = 1;
            this.Bend.Text = "Закрыть";
            this.Bend.UseVisualStyleBackColor = false;
            this.Bend.Click += new System.EventHandler(this.Bend_Click);
            // 
            // BBezB
            // 
            this.BBezB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BBezB.Location = new System.Drawing.Point(30, 179);
            this.BBezB.Name = "BBezB";
            this.BBezB.Size = new System.Drawing.Size(156, 55);
            this.BBezB.TabIndex = 2;
            this.BBezB.Text = "УПД (без \"Б\")";
            this.BBezB.UseVisualStyleBackColor = true;
            // 
            // BTestik
            // 
            this.BTestik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTestik.Location = new System.Drawing.Point(30, 254);
            this.BTestik.Name = "BTestik";
            this.BTestik.Size = new System.Drawing.Size(156, 55);
            this.BTestik.TabIndex = 3;
            this.BTestik.Text = "TEST";
            this.BTestik.UseVisualStyleBackColor = true;
            this.BTestik.Click += new System.EventHandler(this.BTestik_Click);
            // 
            // BUPD1
            // 
            this.BUPD1.BackColor = System.Drawing.Color.LawnGreen;
            this.BUPD1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BUPD1.Location = new System.Drawing.Point(30, 108);
            this.BUPD1.Name = "BUPD1";
            this.BUPD1.Size = new System.Drawing.Size(156, 55);
            this.BUPD1.TabIndex = 4;
            this.BUPD1.Text = "СФ и УПД";
            this.BUPD1.UseVisualStyleBackColor = false;
            // 
            // ChBxNeedCopy
            // 
            this.ChBxNeedCopy.AutoSize = true;
            this.ChBxNeedCopy.Checked = true;
            this.ChBxNeedCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBxNeedCopy.Location = new System.Drawing.Point(197, 13);
            this.ChBxNeedCopy.Name = "ChBxNeedCopy";
            this.ChBxNeedCopy.Size = new System.Drawing.Size(172, 17);
            this.ChBxNeedCopy.TabIndex = 5;
            this.ChBxNeedCopy.Text = "Копировать на лист \"СЮДА\"";
            this.ChBxNeedCopy.UseVisualStyleBackColor = true;
            // 
            // BUPDSave
            // 
            this.BUPDSave.BackColor = System.Drawing.Color.LawnGreen;
            this.BUPDSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BUPDSave.Location = new System.Drawing.Point(245, 120);
            this.BUPDSave.Name = "BUPDSave";
            this.BUPDSave.Size = new System.Drawing.Size(123, 31);
            this.BUPDSave.TabIndex = 6;
            this.BUPDSave.Text = "Save1";
            this.BUPDSave.UseVisualStyleBackColor = false;
            // 
            // BBezBSave
            // 
            this.BBezBSave.BackColor = System.Drawing.SystemColors.Control;
            this.BBezBSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BBezBSave.Location = new System.Drawing.Point(245, 191);
            this.BBezBSave.Name = "BBezBSave";
            this.BBezBSave.Size = new System.Drawing.Size(123, 31);
            this.BBezBSave.TabIndex = 7;
            this.BBezBSave.Text = "Save2";
            this.BBezBSave.UseVisualStyleBackColor = false;
            // 
            // FixButton
            // 
            this.FixButton.BackColor = System.Drawing.Color.Yellow;
            this.FixButton.Location = new System.Drawing.Point(245, 254);
            this.FixButton.Name = "FixButton";
            this.FixButton.Size = new System.Drawing.Size(123, 32);
            this.FixButton.TabIndex = 8;
            this.FixButton.Text = "Пофиксить";
            this.FixButton.UseVisualStyleBackColor = false;
            // 
            // LStatus
            // 
            this.LStatus.AutoSize = true;
            this.LStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LStatus.Location = new System.Drawing.Point(310, 44);
            this.LStatus.Name = "LStatus";
            this.LStatus.Size = new System.Drawing.Size(113, 24);
            this.LStatus.TabIndex = 9;
            this.LStatus.Text = "Ожидание";
            // 
            // RTfiles
            // 
            this.RTfiles.Location = new System.Drawing.Point(397, 108);
            this.RTfiles.Name = "RTfiles";
            this.RTfiles.Size = new System.Drawing.Size(677, 365);
            this.RTfiles.TabIndex = 10;
            this.RTfiles.Text = "";
            // 
            // BChoseFolder
            // 
            this.BChoseFolder.BackColor = System.Drawing.Color.GreenYellow;
            this.BChoseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BChoseFolder.Location = new System.Drawing.Point(30, 418);
            this.BChoseFolder.Name = "BChoseFolder";
            this.BChoseFolder.Size = new System.Drawing.Size(156, 55);
            this.BChoseFolder.TabIndex = 11;
            this.BChoseFolder.Text = "Выбрать папку";
            this.BChoseFolder.UseVisualStyleBackColor = false;
            this.BChoseFolder.Click += new System.EventHandler(this.BChoseFolder_Click);
            // 
            // LFolder
            // 
            this.LFolder.AutoSize = true;
            this.LFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LFolder.Location = new System.Drawing.Point(310, 74);
            this.LFolder.Name = "LFolder";
            this.LFolder.Size = new System.Drawing.Size(124, 24);
            this.LFolder.TabIndex = 12;
            this.LFolder.Text = "не выбрана";
            // 
            // BMassBoom
            // 
            this.BMassBoom.BackColor = System.Drawing.Color.GreenYellow;
            this.BMassBoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BMassBoom.Location = new System.Drawing.Point(212, 417);
            this.BMassBoom.Name = "BMassBoom";
            this.BMassBoom.Size = new System.Drawing.Size(156, 55);
            this.BMassBoom.TabIndex = 13;
            this.BMassBoom.Text = "Массовая обработка";
            this.BMassBoom.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(220, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Статус:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(230, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "Папка:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 485);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BMassBoom);
            this.Controls.Add(this.LFolder);
            this.Controls.Add(this.BChoseFolder);
            this.Controls.Add(this.RTfiles);
            this.Controls.Add(this.LStatus);
            this.Controls.Add(this.FixButton);
            this.Controls.Add(this.BBezBSave);
            this.Controls.Add(this.BUPDSave);
            this.Controls.Add(this.ChBxNeedCopy);
            this.Controls.Add(this.BUPD1);
            this.Controls.Add(this.BTestik);
            this.Controls.Add(this.BBezB);
            this.Controls.Add(this.Bend);
            this.Name = "MainForm";
            this.Text = "Обработчик СФ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Bend;
        private System.Windows.Forms.Button BBezB;
        private System.Windows.Forms.Button BTestik;
        private System.Windows.Forms.Button BUPD1;
        private System.Windows.Forms.CheckBox ChBxNeedCopy;
        private System.Windows.Forms.Button BUPDSave;
        private System.Windows.Forms.Button BBezBSave;
        private System.Windows.Forms.Button FixButton;
        private System.Windows.Forms.Label LStatus;
        private System.Windows.Forms.RichTextBox RTfiles;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Button BChoseFolder;
        private System.Windows.Forms.Label LFolder;
        private System.Windows.Forms.Button BMassBoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

