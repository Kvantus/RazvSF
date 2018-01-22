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
            this.BSF2 = new System.Windows.Forms.Button();
            this.BTestik = new System.Windows.Forms.Button();
            this.BUPD1 = new System.Windows.Forms.Button();
            this.ChCopySud = new System.Windows.Forms.CheckBox();
            this.BUPDSave = new System.Windows.Forms.Button();
            this.BBezB = new System.Windows.Forms.Button();
            this.BFix = new System.Windows.Forms.Button();
            this.LStatus = new System.Windows.Forms.Label();
            this.RTfiles = new System.Windows.Forms.RichTextBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.BChoseFolder = new System.Windows.Forms.Button();
            this.LFolder = new System.Windows.Forms.Label();
            this.BMassBoom = new System.Windows.Forms.Button();
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
            // BSF2
            // 
            this.BSF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BSF2.Location = new System.Drawing.Point(30, 179);
            this.BSF2.Name = "BSF2";
            this.BSF2.Size = new System.Drawing.Size(156, 55);
            this.BSF2.TabIndex = 2;
            this.BSF2.Text = "УПД (без \"Б\")";
            this.BSF2.UseVisualStyleBackColor = true;
            this.BSF2.Click += new System.EventHandler(this.BSF2_Click);
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
            this.BUPD1.Click += new System.EventHandler(this.BUPD1_Click);
            // 
            // ChCopySud
            // 
            this.ChCopySud.AutoSize = true;
            this.ChCopySud.Checked = true;
            this.ChCopySud.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChCopySud.Location = new System.Drawing.Point(197, 13);
            this.ChCopySud.Name = "ChCopySud";
            this.ChCopySud.Size = new System.Drawing.Size(172, 17);
            this.ChCopySud.TabIndex = 5;
            this.ChCopySud.Text = "Копировать на лист \"СЮДА\"";
            this.ChCopySud.UseVisualStyleBackColor = true;
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
            this.BUPDSave.Click += new System.EventHandler(this.BUPDSave_Click);
            // 
            // BBezB
            // 
            this.BBezB.BackColor = System.Drawing.SystemColors.Control;
            this.BBezB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BBezB.Location = new System.Drawing.Point(245, 191);
            this.BBezB.Name = "BBezB";
            this.BBezB.Size = new System.Drawing.Size(123, 31);
            this.BBezB.TabIndex = 7;
            this.BBezB.Text = "Save2";
            this.BBezB.UseVisualStyleBackColor = false;
            this.BBezB.Click += new System.EventHandler(this.BBezB_Click);
            // 
            // BFix
            // 
            this.BFix.BackColor = System.Drawing.Color.Yellow;
            this.BFix.Location = new System.Drawing.Point(245, 254);
            this.BFix.Name = "BFix";
            this.BFix.Size = new System.Drawing.Size(123, 32);
            this.BFix.TabIndex = 8;
            this.BFix.Text = "Пофиксить";
            this.BFix.UseVisualStyleBackColor = false;
            this.BFix.Click += new System.EventHandler(this.BFix_Click);
            // 
            // LStatus
            // 
            this.LStatus.AutoSize = true;
            this.LStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LStatus.Location = new System.Drawing.Point(202, 33);
            this.LStatus.Name = "LStatus";
            this.LStatus.Size = new System.Drawing.Size(0, 24);
            this.LStatus.TabIndex = 9;
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
            this.LFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LFolder.Location = new System.Drawing.Point(193, 71);
            this.LFolder.Name = "LFolder";
            this.LFolder.Size = new System.Drawing.Size(147, 20);
            this.LFolder.TabIndex = 12;
            this.LFolder.Text = "Выбрана папка: ";
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
            this.BMassBoom.Click += new System.EventHandler(this.BMassBoom_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 485);
            this.Controls.Add(this.BMassBoom);
            this.Controls.Add(this.LFolder);
            this.Controls.Add(this.BChoseFolder);
            this.Controls.Add(this.RTfiles);
            this.Controls.Add(this.LStatus);
            this.Controls.Add(this.BFix);
            this.Controls.Add(this.BBezB);
            this.Controls.Add(this.BUPDSave);
            this.Controls.Add(this.ChCopySud);
            this.Controls.Add(this.BUPD1);
            this.Controls.Add(this.BTestik);
            this.Controls.Add(this.BSF2);
            this.Controls.Add(this.Bend);
            this.Name = "MainForm";
            this.Text = "Обработчик СФ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Bend;
        private System.Windows.Forms.Button BSF2;
        private System.Windows.Forms.Button BTestik;
        private System.Windows.Forms.Button BUPD1;
        private System.Windows.Forms.CheckBox ChCopySud;
        private System.Windows.Forms.Button BUPDSave;
        private System.Windows.Forms.Button BBezB;
        private System.Windows.Forms.Button BFix;
        private System.Windows.Forms.Label LStatus;
        private System.Windows.Forms.RichTextBox RTfiles;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Button BChoseFolder;
        private System.Windows.Forms.Label LFolder;
        private System.Windows.Forms.Button BMassBoom;
    }
}

