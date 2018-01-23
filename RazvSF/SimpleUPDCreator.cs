using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace RazvSF
{
    /// <summary>
    /// Перечисление представляет собой specialCells в Excel. Пустые, текст и числа соответственно
    /// </summary>
    enum CellTypes
    {
        Blanks,
        Text,
        Numbers
    }


    internal interface ISimpleUPDCreator
    {
        /// <summary>
        /// Обработка активного листа Excel, приводящая счет-фактуру к шести необходимым столбцам и (при необходимости) сохраняющая результат в txt файл
        /// </summary>
        /// <param name="bezB">Нужно ли игнорировать колонку с индикатором "Б", в которой обычно располагаются артикулы
        /// Это бывает необходимо в редких случаях, когда в данной колонке неверная информация или ее вообще нет</param>
        /// <param name="NeedCopy">Нужно ли предварительно скопировать исходную счет-фактуру на лист "СЮДА" в книге макросов</param>
        /// <param name="NeedSave">Нужно ли сохранить результат сразу в txt файл, готовый для загрузки</param>
        void TransformUPD(bool bezB, bool NeedCopy, bool NeedSave);

        /// <summary>
        /// Массовая обработка счетов-файтур в фиде Excel файлов, находящихся в указанной папке
        /// </summary>
        /// <param name="logFile">Папка, в которой находятся Excel файлы. В ней же будет создал лог файл программы</param>
        void MassTransformUPD(string workingFolder);

        /// <summary>
        /// Экстренный метод для включения опции обновления экрана в Excel, на случай, если программа будет аварийно завершена в момент кокда опция отключена
        /// </summary>
        void FixExcel();

        /// <summary>
        /// Событие возникает при массовой обработке счетов-фактур, когда обновляется строковое поле WorkDescription, накапливающее в себе информацию о процессе работы
        /// </summary>
        event EventHandler<WorkDescriptionEventArgs> WorkDescriptionChanged;
    }


    class SimpleUPDCreator : ISimpleUPDCreator
    {
        Excel.Application excel;
        int updTopRow; // надо будет пересмотреть и убрать
        Worksheet sheet;
        Workbook tempBook;
        string workDescription;
        string WorkDescription
        {
            get => workDescription;
            set { workDescription = value; OnWorkDescriptionChange(); }
        }

        public event EventHandler<WorkDescriptionEventArgs> WorkDescriptionChanged;

        void OnWorkDescriptionChange()
        {
            WorkDescriptionChanged?.Invoke(this, new WorkDescriptionEventArgs(workDescription));
        }


        public void TransformUPD(bool bezB, bool NeedCopy, bool NeedSave)
        {
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application")
                as Excel.Application;
            _ = excel ?? throw new NullReferenceException(nameof(excel) + ": имеет значение null. Процесс Excel не найден");


            Workbook activeWorkBook = excel.ActiveWorkbook;
            sheet = activeWorkBook.ActiveSheet;

            RemoveImages();

            if (NeedSave)
            {
                tempBook = excel.Workbooks.Add(); // создаем временную книгу, информацию из которой в последствии сохраним в txt файл
                Worksheet tempSheet = tempBook.ActiveSheet; // временная переменная, чтобы скопировать содержимое в новую книгу
                sheet.Cells.Copy(tempSheet.Range["A1"]);
                sheet = tempSheet;
            }

            if (NeedCopy)
            {
                CopySuda();
            }

#if !DEBUG
            excel.ScreenUpdating = false;
#endif

            try
            {
                RunTransformation(false);
            }
            catch (Exception ex)
            {
                excel.ScreenUpdating = true;
                MessageBox.Show("Ошибка! :(\n" + ex.Message);
                return;
            }
            finally
            {
                excel.ScreenUpdating = true;
            }

            if (NeedSave)
            {
                try
                {
                    SaveBook(activeWorkBook, tempBook);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void MassTransformUPD(string workingFolder)
        {
            LogWriter logWriter = new LogWriter(workingFolder + "\\UPDLogs.txt");

            DirectoryInfo folder = new DirectoryInfo(Path.GetFullPath(workingFolder));     // папка с файлами - задается пользователем через FolderBrowser
            int kol = folder.GetFiles().Count();

            if (kol == 0)
            {
                MessageBox.Show("В папке отсутствуют файлы!");
                return;
            }

            excel = new Excel.Application();
            int counter = 0; // счетчик итераций
            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.Attributes.HasFlag(FileAttributes.Hidden))        // если файл скрытый - пропускаем
                {
                    continue;
                }

                // заменить на file.Extention
                if (file.Extension != ".xls" && file.Extension != ".xlsx" && file.Extension != ".csv")
                {
                    continue;       // обрабатываем только файлы с определенным расширением
                }
                counter++;

                WorkDescription += file.Name;  // записываем в ричбокс имя текущего файла

                excel.Workbooks.Open(folder + "\\" + file.Name);
                Workbook activeWorkBook = excel.ActiveWorkbook;
                sheet = activeWorkBook.ActiveSheet;

                RemoveImages();   // перед дальнейшей обработкой убираем мусор из файла

                excel.ScreenUpdating = false;
                logWriter.WriteLine(file.Name);

                try
                {
                    try
                    {
                        RunTransformation(false);
                    }
                    catch (Exception)
                    {
                        WorkDescription += "  - что-то пошло не так :(\n";
                        activeWorkBook.Close(SaveChanges: false);
                        continue;    // пропускаем файл, который не получилось обработать
                    }

                    SaveBook(activeWorkBook);   // метод сохранения файла в формате txt
                    WorkDescription += "  - обработано\n";
                    logWriter.WriteLine($" -- обработано");
                }
                catch (Exception ex)          // в случае исключений ставим пометку в ричбоксе и в логах
                {
                    excel.ScreenUpdating = true;
                    WorkDescription += "  - что-то пошло не так :(\n";
                    logWriter.WriteLine($" -- ошибка: {ex.Message}");
                    CollectGarbage();
                }
            }
            logWriter?.Dispose();
            excel?.Quit();
        }

        private void CollectGarbage()
        {
            excel?.Quit();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public Range FindCell(string poisk, Range range)
        {
            Range itog = range.Find(What: poisk, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (poisk == "10а" && itog == null)
            {
                itog = sheet.Cells.Find(What: "краткое", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
                if (itog != null)
                {
                    itog = itog.Offset[1, 0];
                }
            }
            if (poisk == "10а" && itog == null)
            {
                itog = sheet.Cells.Find(What: "10a", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            }

            if (itog != null)
            {
                return itog;
            }
            else
            {
                throw new InvalidDataException($"Не найден столбец {poisk}");
            }
        }

        public void DeleteSpecialCells(int column, CellTypes cellTypes)
        {
            Range range = sheet.Cells[1, column];
            try
            {
                switch (cellTypes)
                {
                    case CellTypes.Blanks:
                        range.EntireColumn.SpecialCells(XlCellType.xlCellTypeBlanks).EntireRow.Delete();
                        break;
                    case CellTypes.Text:
                        range.EntireColumn.SpecialCells(XlCellType.xlCellTypeConstants, 2).EntireRow.Delete();
                        break;
                    case CellTypes.Numbers:
                        range.EntireColumn.SpecialCells(XlCellType.xlCellTypeConstants, 1).EntireRow.Delete();
                        break;
                    default:
                        throw new ArgumentException("Неизвестное значение перечисления CellTypes");
                }
            }
            catch (Exception) // обрабатывать исключение нет смысла. Если нечего удалять - хорошо
            {
            }
        }

        /// <summary>
        /// Замена точек на запятые в выбранном столбце
        /// </summary>
        /// <param name="column">Номер колонки</param>
        public void ReplacePoint(int column)
        {
            excel.DisplayAlerts = false;
            Range range = sheet.Cells[1, column];
            range.EntireColumn.Replace(What: ".", Replacement: ",", LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, MatchCase: false, SearchFormat: false, ReplaceFormat: false);
            excel.DisplayAlerts = true;
        }

        /// <summary>
        /// Применить операцию "текст по столбцам" в выбранном диапазоне ячеек
        /// </summary>
        /// <param name="col">Номер колонки</param>
        /// <param name="rowStart">Начиная с указанной строки</param>
        /// <param name="rowEnd">Заканчивая указанной строкой</param>
        public void DoTextToColumns(int col, int rowStart, int rowEnd)
        {
            Array fieldINFO = new int[,] { { 0, 1 } };
            Range startToEnd = sheet.Range[sheet.Cells[rowStart, col], sheet.Cells[rowEnd, col]];
            startToEnd.TextToColumns(Destination: sheet.Cells[rowStart, col], DataType: XlTextParsingType.xlFixedWidth,
                TrailingMinusNumbers: true, FieldInfo: (object)fieldINFO);
        }

        /// <summary>
        /// "Умная" проверка, в какой колонке содержатся артикулы деталей. Однако в случае, если в колонке с идентификатором "Б" данные некорректны, 
        /// метод сработает неверно.
        /// </summary>
        /// <param name="TopRowRangeOfUPD"></param>
        /// <returns></returns>
        public Range ArticleCheck(Range TopRowRangeOfUPD) // нужно изменить аргумент. передаем параметр kol.Row - то что в начале находим
        {
            Range allCells = sheet.Cells;
            string article = "артикул";

            Range itog = allCells.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (itog != null)
            {
                itog = sheet.Cells[TopRowRangeOfUPD.Row, itog.Column];
                itog.Value = "Б";
                return itog;
            }
            article = "Б";
            itog = TopRowRangeOfUPD.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (itog != null)
            {
                if (itog.Offset[1, 0].Value != null && itog.Offset[1, 0].Value.ToString() != "")
                {
                    return itog;
                }
                else
                {
                    goto gogoOne;
                }
            }
            gogoOne:
            article = "1";
            itog = TopRowRangeOfUPD.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (itog != null)
            {
                return itog;
            }
            else
            {
                throw new InvalidDataException("Не получилось найти столбец с артикулом");
            }
        }

        /// <summary>
        /// Копирование активного листа на лист "СЮДА" в файле макросы.
        /// </summary>
        /// <param name="from"></param>
        public void CopySuda()
        {
            try
            {
                Workbook macroBook = excel.Workbooks["Макросы.xlsm"]; // возможно стоит передавать лист КУДА вставлять также параметром
                Worksheet macroSheet = macroBook.Sheets["СЮДА"];
                sheet.Cells.Copy(macroSheet.Range["A1"]);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Файл Макросы не найден, " + ex.Message);
            }
        }

        /// <summary>
        /// Удаление всех изображений с листа
        /// </summary>
        public void RemoveImages()
        {
            for (int i = 1; i <= sheet.Shapes.Count; i++)
            {
                sheet.Shapes.Item(i).Delete();
            }
        }

        /// <summary>
        /// Возвращает сумму по накладной
        /// </summary>
        /// <returns></returns>
        public double MakeSum()
        {
            double sum = 0;
            Range endRange = sheet.Range["C50000"].End[XlDirection.xlUp];
            if (endRange.Offset[0, -1].Value == null || endRange.Offset[0, -1].Value.ToString() == "")
            {
                endRange.EntireRow.Delete();
                endRange = sheet.Range["C50000"].End[XlDirection.xlUp];
            }

            endRange.Offset[1, 0].NumberFormat = "Общий";
            endRange.Offset[1, 0].FormulaR1C1 = $"=SUM(R[-{endRange.Row}]C:R[-1]C)";

            sum = Convert.ToDouble(endRange.Offset[1, 0].Value);
            sum = Math.Round(sum, 2);
            endRange.Offset[1, 0].Clear();
            return sum;
        }

        /// <summary>
        /// Сохранить книгу в txt формате с разделителем табуляция
        /// </summary>
        /// <param name="wb"></param>
        public void SaveBook(Workbook workbook, Workbook tempBook = null)
        {
            string folder = workbook.Path;
            if (string.IsNullOrEmpty(folder))
            {
                throw new FileNotFoundException("Активная книга должна быть сохранена на диске");
            }
            int indexPoint = workbook.Name.IndexOf('.');
            string textName = workbook.Name.Remove(indexPoint);
            double summa = MakeSum();

            Range rangeArray = sheet.Range["F1000"].End[XlDirection.xlUp];
            int kol = rangeArray.Row;
            rangeArray = sheet.Range[sheet.Cells[1, 1], rangeArray];
            var array = rangeArray.Value;

            StreamWriter csvFile = new StreamWriter(path: folder + "\\" + textName + " на " + summa + ".txt", append: false, encoding: Encoding.GetEncoding("Windows-1251"));

            for (int i = 1; i <= kol; i++)
            {
                csvFile.WriteLine(array[i, 1] + "\t" + array[i, 2] + "\t" + array[i, 3] + "\t" + array[i, 4] + "\t" + array[i, 5] + "\t" + array[i, 6]);
            }
            csvFile.Close();

            if (tempBook != null)
            {
                tempBook.Close(SaveChanges: false);
            }
            else
            {
                workbook.Close(SaveChanges: false);
            }
        }

        /// <summary>
        /// Устанавливает ScreenUpdates = true, на случай, если произойдет непредвиденное завершение программы при отключенном обновлении экрана в Excel
        /// </summary>
        public void FixExcel()
        {
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
            excel.ScreenUpdating = true;
        }

        /// <summary>
        /// Главный метод обработки листа Excel с информацией о счет-фактуре
        /// </summary>
        /// <param name="bezB">Нужно ли игнорировать колонку с обозначением "Б"</param>
        public void RunTransformation(bool bezB)
        {
            //расширяем колонки, для того, чтоб поиск смог найти значения, ищем цифру 3 в ячейке (колонка с количеством, универсальная)
            sheet.Cells.UnMerge();
            sheet.Cells.ColumnWidth = 4;

            string KolCellValue = "3";
            string poleArt = "Б"; // это можно удалить, сейчас поле с артикулом находится через ArticleCheck, не принимающего поисковый аргумент
            string poleSum = "9";
            string poleKod = "10";
            string poleStrana = "10а";
            string poleGtd = "11";

            Range countTopCell;
            Range articleTopCell;
            Range sumTopCell;
            Range codeTopCell;
            Range countryTopCell;
            Range gtdTopCell;

            countTopCell = sheet.Cells.Find(What: KolCellValue, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);

            if (countTopCell == null)
            {
                throw new InvalidDataException("Не найден столбец 3");
            }

            updTopRow = countTopCell.Row;

            //проверка есть ли Б. Если есть то считаем, что это УПД.

            //находим все поля
            if (bezB)
            {
                poleArt = "1";
                articleTopCell = FindCell(poleArt, countTopCell.EntireRow);
            }
            else
            {
                articleTopCell = ArticleCheck(countTopCell.EntireRow);
            }

            try
            {
                countTopCell.EntireRow.SpecialCells(XlCellType.xlCellTypeBlanks).EntireColumn.Delete();
            }
            catch (Exception)
            {
            }

            countTopCell = FindCell(KolCellValue, countTopCell.EntireRow);
            sumTopCell = FindCell(poleSum, countTopCell.EntireRow);
            codeTopCell = FindCell(poleKod, countTopCell.EntireRow);
            countryTopCell = FindCell(poleStrana, countTopCell.EntireRow);
            gtdTopCell = FindCell(poleGtd, countTopCell.EntireRow);

            Range lastCell = sheet.Cells[10000, articleTopCell.Column];
            int strok = lastCell.End[XlDirection.xlUp].Row;

            Range firstLine = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 100]];

            //более быстрая пометка необходимых столбцов, без полного перебора
            firstLine.Clear();
            sheet.Cells[1, articleTopCell.Column].Value = "z";
            sheet.Cells[1, countTopCell.Column].Value = "z";
            sheet.Cells[1, sumTopCell.Column].Value = "z";
            sheet.Cells[1, codeTopCell.Column].Value = "z";
            sheet.Cells[1, countryTopCell.Column].Value = "z";
            sheet.Cells[1, gtdTopCell.Column].Value = "z";

            firstLine.SpecialCells(XlCellType.xlCellTypeBlanks).EntireColumn.Delete();
            DoTextToColumns(2, articleTopCell.Row + 1, strok);
            DoTextToColumns(3, articleTopCell.Row + 1, strok);

            ReplacePoint(2);
            ReplacePoint(3);

            // Чтоб убрать разрывы страниц в УПД, удаляем в различных столбцах пустые ячейки или константы (текст/числа)
            DeleteSpecialCells(2, CellTypes.Blanks);
            DeleteSpecialCells(3, CellTypes.Blanks);
            DeleteSpecialCells(6, CellTypes.Numbers);
            DeleteSpecialCells(2, CellTypes.Text);
            DeleteSpecialCells(3, CellTypes.Text);

            sheet.Cells.WrapText = false;
            sheet.Cells.RowHeight = 14;
            sheet.Cells.Columns.AutoFit();

            gogogo:
            Range lastCheck = sheet.Range["A1"].EntireColumn.Find(What: "Б", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (lastCheck != null)
            {
                lastCheck.EntireRow.Delete();
                goto gogogo;
            }

            //сбрасываем поиск до стандартного, xlpart
            lastCheck = sheet.Range["A1"].EntireColumn.Find(What: "Б", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);

            excel.DisplayAlerts = false;
            sheet.Cells.Replace(What: "\"", Replacement: "", MatchCase: false, SearchFormat: false);
            sheet.Cells.Replace(What: ";", Replacement: "", MatchCase: false, SearchFormat: false);
            sheet.Cells.Replace(What: "\n", Replacement: "", MatchCase: false, SearchFormat: false);
            excel.DisplayAlerts = true;

            sheet.Range["D1"].EntireColumn.NumberFormat = "@";
            sheet.Range["B1"].EntireColumn.NumberFormat = "@";
            sheet.Range["C1"].EntireColumn.NumberFormat = "@";
            //someSheet.Range["E1"].EntireColumn.NumberFormat = "@";
            int rowEnd = sheet.Range["E3000"].End[XlDirection.xlUp].Row;
        }
    }
}
