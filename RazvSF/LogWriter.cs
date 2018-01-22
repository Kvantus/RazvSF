using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RazvSF
{
    /// <summary>
    /// Класс предназначен для записи логов в txt файл
    /// </summary>
    class LogWriter : IDisposable
    {
        StreamWriter writer;

        /// <summary>
        /// Инициализация поля типа StreamWriter на основании полного имени файла, переданного в конструктор
        /// </summary>
        /// <param name="logFilePath">Полное имя txt файла</param>
        public LogWriter(string logFilePath)
        {
            this.writer = new StreamWriter(path: logFilePath, append: true, encoding: Encoding.GetEncoding(1251));
            writer.AutoFlush = true;
        }

        public void Dispose()
        {
            writer?.Dispose();
        }

        /// <summary>
        /// Запись переданной строки в лог файл
        /// </summary>
        /// <param name="line"></param>
        public void WriteLine(string line)
        {
            writer.WriteLine($"{DateTime.Now}: {line}");
        }
    }
}
