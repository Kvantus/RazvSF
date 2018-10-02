using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Xml.Linq;
using Microsoft.Office.Core;



namespace RazvSF
{

    interface IMainForm
    {
        bool IsNeedCopyChecked { get; }
        event EventHandler BeforeClosing;
        event EventHandler TransformButtonClick;
        event EventHandler TransformAndSaveButtonClick;
        event EventHandler BezBTransformButtonClick;
        event EventHandler BezBTransformAndSaveButtonClick;
        event EventHandler FixButtonClick;
        event EventHandler MassTransformButtonClick;
        event EventHandler WorkingFolderPathChanged;
        string StatusText { get; set; }
        string WorkingFolderText { get; set; }
        string RichBoxText { get; set; }
        string FileToCopy { get; set; }
        string SheetToCopy { get; set; }
    }



    partial class MainForm : Form, IMainForm
    {
        public event EventHandler BeforeClosing;
        public event EventHandler TransformButtonClick;
        public event EventHandler TransformAndSaveButtonClick;
        public event EventHandler BezBTransformButtonClick;
        public event EventHandler BezBTransformAndSaveButtonClick;
        public event EventHandler FixButtonClick;
        public event EventHandler MassTransformButtonClick;
        public event EventHandler WorkingFolderPathChanged;

        public string FileToCopy
        {
            get => txtFile.Text;
            set => txtFile.Text = value;
        }

        public string SheetToCopy
        {
            get => txtSheet.Text;
            set => txtSheet.Text = value;
        }

        public string StatusText
        {
            get => LStatus.Text;
            set => LStatus.Text = value;
        }

        public string WorkingFolderText
        {
            get { return LFolder.Text; }
            set { LFolder.Text = value; }
        }

        public bool IsNeedCopyChecked
        {
            get
            { return ChBxNeedCopy.Checked; }
            protected set
            { ChBxNeedCopy.Checked = value; }
        }

        public string RichBoxText
        {
            get { return RTfiles.Text; }
            set { RTfiles.Text = value; }
        }


        public MainForm()
        {
            InitializeComponent();
            if (Environment.UserName.ToLower() == "viktor_k")
            {
                BTestik.Visible = true;
            }
            else
            {
                BTestik.Visible = false;
            }

            ChBxNeedCopy.CheckedChanged += CheckBoxNeedCopy_CheckedChanged;

            BUPD1.Click += TransformButton;
            BUPDSave.Click += TransformAndSaveButton;
            BBezB.Click += TransformBezBButton;
            BBezBSave.Click += TransformAndSaveBezBButton;
            FixButton.Click += MakeFix;
            BMassBoom.Click += MassBoomClick;
            FormClosing += OnClosing;
        }

        private void OnClosing(object sender, EventArgs e)
        {
            BeforeClosing?.Invoke(this, EventArgs.Empty);
        }

        private void MassBoomClick(object sender, EventArgs e)
        {
            MassTransformButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void MakeFix(object sender, EventArgs e)
        {
            FixButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void TransformButton(object sender, EventArgs e)
        {
            TransformButtonClick?.Invoke(this, EventArgs.Empty);
            this.WindowState = FormWindowState.Minimized;
        }

        private void TransformAndSaveButton(object sender, EventArgs e)
        {
            TransformAndSaveButtonClick?.Invoke(this, EventArgs.Empty);
            this.WindowState = FormWindowState.Minimized;
        }

        private void TransformBezBButton(object sender, EventArgs e)
        {
            BezBTransformButtonClick?.Invoke(this, EventArgs.Empty);
            this.WindowState = FormWindowState.Minimized;
        }

        private void TransformAndSaveBezBButton(object sender, EventArgs e)
        {
            BezBTransformAndSaveButtonClick?.Invoke(this, EventArgs.Empty);
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Обработчик события изменения состояния чекбокса ChBxNeedCopy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxNeedCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (ChBxNeedCopy.Checked)
            {
                IsNeedCopyChecked = true;
            }
            else
            {
                IsNeedCopyChecked = false;
            }
        }


        private void Bend_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BChoseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowser.SelectedPath = LFolder.Text;

            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPAth = FolderBrowser.SelectedPath;
                LFolder.Text = folderPAth;
                WorkingFolderPathChanged?.Invoke(this, EventArgs.Empty);
            }
        }






















        
        private void BTestik_Click(object sender, EventArgs e)
        {

        }

        
    }
}
