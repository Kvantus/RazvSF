using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace RazvSF
{
    class Presenter
    {
        IMainForm mainForm;
        ISimpleUPDCreator simpleUpdCreator;
        Configuration config;


        public Presenter(IMainForm mainForm, ISimpleUPDCreator simpleUpdCreator)
        {
            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.simpleUpdCreator = simpleUpdCreator ?? throw new ArgumentNullException(nameof(simpleUpdCreator));

            mainForm.TransformButtonClick += Transform;
            mainForm.TransformAndSaveButtonClick += TransformAndSave;
            mainForm.BezBTransformButtonClick += TransformBezB;
            mainForm.BezBTransformAndSaveButtonClick += TransformAndSaveBezB;
            mainForm.FixButtonClick += MakeFix;
            mainForm.MassTransformButtonClick += MassTransform;

            config = ConfigurationManager.OpenExeConfiguration(AppDomain.CurrentDomain.FriendlyName);

            mainForm.WorkingFolderText = config.AppSettings.Settings["WorkingFolder"].Value;
            mainForm.WorkingFolderPathChanged += ChangePath;

            simpleUpdCreator.WorkDescriptionChanged += WorkDescriptionChange; 
        }

        void MassTransform(object sender, EventArgs args)
        {
            // проверка, выбрана ли папка пользователем
            if (mainForm.WorkingFolderText == "" || mainForm.WorkingFolderText == "не выбрана")
            {
                MessageBox.Show("Не выбрана папка с файлами");
                return;
            }

            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.MassTransformUPD(mainForm.WorkingFolderText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainForm.RichBoxText += "\n\n" + new string('-', 20) + "\nОБРАБОТКА ЗАКОНЧЕНА";
            mainForm.StatusText = "Ожидание";
        }

        void WorkDescriptionChange(object sender, WorkDescriptionEventArgs args)
        {
            mainForm.RichBoxText = args.WorkDescription;
        }

        void MakeFix(object sender, EventArgs args)
        {
            simpleUpdCreator.FixExcel();
        }

        void ChangePath(object sender, EventArgs args)
        {
            config.AppSettings.Settings["WorkingFolder"].Value = mainForm.WorkingFolderText;
            config.Save();
        }

        void Transform(object sender, EventArgs args)
        {
            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.TransformUPD(bezB: false, NeedCopy: mainForm.IsNeedCopyChecked, NeedSave: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainForm.StatusText = "Ожидание";
        }

        void TransformAndSave(object sender, EventArgs args)
        {
            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.TransformUPD(bezB: false, NeedCopy: mainForm.IsNeedCopyChecked, NeedSave: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainForm.StatusText = "Ожидание";
        }

        void TransformBezB(object sender, EventArgs args)
        {
            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.TransformUPD(bezB: true, NeedCopy: mainForm.IsNeedCopyChecked, NeedSave: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainForm.StatusText = "Ожидание";
        }

        void TransformAndSaveBezB(object sender, EventArgs args)
        {
            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.TransformUPD(bezB: true, NeedCopy: mainForm.IsNeedCopyChecked, NeedSave: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainForm.StatusText = "Ожидание";
        }
    }
}
