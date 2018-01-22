using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RazvSF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime myDate = DateTime.Parse("01.04.2018");
            if (DateTime.Now > myDate)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Инициализация экземпляров основных классов
            MainForm mainForm = new MainForm();
            SimpleUPDCreator suc = new SimpleUPDCreator();
            Presenter presenter = new Presenter(mainForm, suc);

            Application.Run(mainForm);
        }
    }
}
