using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;
using RazvSF.Properties;

namespace RazvSF
{
    class Presenter
    {
        IMainForm mainForm;
        ISimpleUPDCreator simpleUpdCreator;
        Configuration config;

        /// <summary>
        /// Инициализация класса, в качества параметров выступает интерфейс Windows формы и интерфейс обработчика сф
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="simpleUpdCreator"></param>
        public Presenter(IMainForm mainForm, ISimpleUPDCreator simpleUpdCreator)
        {
            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.simpleUpdCreator = simpleUpdCreator ?? throw new ArgumentNullException(nameof(simpleUpdCreator));

            // подписка на события формы
            mainForm.TransformButtonClick += Transform;
            mainForm.TransformAndSaveButtonClick += TransformAndSave;
            mainForm.BezBTransformButtonClick += TransformBezB;
            mainForm.BezBTransformAndSaveButtonClick += TransformAndSaveBezB;
            mainForm.FixButtonClick += MakeFix;
            mainForm.MassTransformButtonClick += MassTransform;
            mainForm.BeforeClosing += BeforeProgramClosed;

            // папка, выбранная пользователем в прошлый раз, сохраненная в файле конфига. Достаем ее и отображаем на форме
            config = ConfigurationManager.OpenExeConfiguration(AppDomain.CurrentDomain.FriendlyName);

            mainForm.WorkingFolderText = config.AppSettings.Settings["WorkingFolder"].Value;
            mainForm.WorkingFolderPathChanged += ChangePath;

            mainForm.FileToCopy = Settings.Default.ExcelFile;
            mainForm.SheetToCopy = Settings.Default.ExcelSheet;

            // подписка на события обработчика СФ - изменение описания работы обработчика (т.е. добавление новых строчек)
            simpleUpdCreator.WorkDescriptionChanged += WorkDescriptionChange;
        }

        private void BeforeProgramClosed(object sender, EventArgs e)
        {
            Settings.Default.ExcelFile = mainForm.FileToCopy;
            Settings.Default.ExcelSheet = mainForm.SheetToCopy;
            Settings.Default.Save();
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

        /// <summary>
        /// Обработчик события изменения описания работы обработчика СФ. При добавлении в поле описания новых строчек,
        /// они добавляются в соответствующий текстбокс на форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void WorkDescriptionChange(object sender, WorkDescriptionEventArgs args)
        {
            mainForm.RichBoxText = args.WorkDescription;
        }

        /// <summary>
        /// Обработчик нажатия экстренной кнопки. Если произошло аварийное завершения процесса посреди выполнения основного метода
        /// обработки и Excel завис в состоянии, где он НЕ обновляет визуальные изменения, то данная кнопка включает обновление заново.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void MakeFix(object sender, EventArgs args)
        {
            simpleUpdCreator.FixExcel();
        }

        // При изменении пользователем текущей рабочей папки, данный обработчик события записывает новое значение пути в конфиг файл
        void ChangePath(object sender, EventArgs args)
        {
            config.AppSettings.Settings["WorkingFolder"].Value = mainForm.WorkingFolderText;
            config.Save();
        }

        private void DefineFileToCopy()
        {
            simpleUpdCreator.FileToCopy = mainForm.FileToCopy;
            simpleUpdCreator.SheetToCopy = mainForm.SheetToCopy;
        }

        /// <summary>
        /// Метод обработчик нажатия кнопки стандартной обработки одной СФ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void Transform(object sender, EventArgs args)
        {
            DefineFileToCopy();

            mainForm.StatusText = "ВЫПОЛНЕНИЕ!";
            try
            {
                simpleUpdCreator.TransformUPD(bezB: false, NeedCopy: mainForm.IsNeedCopyChecked, NeedSave: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex?.InnerException?.Message);
            }
            mainForm.StatusText = "Ожидание";
        }

        /// <summary>
        /// Метод обработчик нажатия кнопки обработки И СОХРАНЕНИЯ одной СФ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void TransformAndSave(object sender, EventArgs args)
        {
            DefineFileToCopy();

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

        /// <summary>
        /// Метот обработчик нажатия кнопки нестандартной обработки СФ, где необходимо игнорировать колонку с названием "Б"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void TransformBezB(object sender, EventArgs args)
        {
            DefineFileToCopy();

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

        /// <summary>
        /// Метот обработчик нажатия кнопки нестандартной обработки СФ И ЕЕ СОХРАНЕНИЯ, 
        /// где необходимо игнорировать колонку с названием "Б"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void TransformAndSaveBezB(object sender, EventArgs args)
        {
            DefineFileToCopy();

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
