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
    public partial class MainForm : Form
    {
        public static Excel.Application excel;
        Workbook myWB;
        Worksheet mySheet;
        Range arti;
        Range kol;
        Range sum;
        Range kod;
        Range strana;
        Range gtd;
        string poleArt;
        string poleKol;
        string poleSum;
        string poleKod;
        string poleStrana;
        string poleGtd;
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        string logFile = "SFandUPDlogs.txt";
        string optionsFile = "OptionsForRazvSF.txt";
        string mySFPath;
        StreamReader optionsRead;
        StreamWriter optionsWrite;
        MySFMethods myMethods = new MySFMethods();
        public static StreamWriter logWrite;











        //interface IMainForm
        //{
        //    bool IsCopyOnOtherListChecked { get; }
        //    event EventHandler CloseButtonClick;
        //    event EventHandler TransformButtonClick;
        //    event EventHandler MassTransformButtonClick;
        //    event EventHandler BezBTransformButtonClick;
        //    event EventHandler BezBMassTransformButtonClick;
        //    event EventHandler FixButtonClick;
        //}


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

            // все это отсюда убрать
            if (File.Exists(desktopPath + optionsFile))
            {
                optionsRead = new StreamReader(desktopPath  + optionsFile);
                mySFPath = optionsRead.ReadLine();
                LFolder.Text += mySFPath;
                FolderBrowser.SelectedPath = LFolder.Text;
                
                optionsRead.Close();
            }

        }
























        public Range FindCell(string poisk, Range range, Worksheet sheet)
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

            //if (poisk == "Б" && itog == null)
            //{
            //    poisk = "1";
            //    itog = range.Find(What: poisk, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
            //    SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            //}


            if (itog != null)
            {
                return itog;
            }
            else
            {
                MessageBox.Show($"Не найден столбец {poisk}");
                Environment.Exit(0);
                return null;
            }
        }

        [Obsolete("Не используется", true)]
        public Range FindCellWhole(string poisk, Range range, Worksheet sheet)
        {


            Range itog = range.Find(What: poisk, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
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
                MessageBox.Show($"Не найден столбец {poisk}");
                Environment.Exit(0);
                return null;
            }
        }

        public void DeleteEmpty(int column, Worksheet sheet)
        {
            try
            {
                Range range = sheet.Cells[1, column];
                range.EntireColumn.SpecialCells(XlCellType.xlCellTypeBlanks).EntireRow.Delete();
            }
            catch (Exception)
            {
            }
        }

        public void DeleteText(int column, Worksheet sheet)
        {
            try
            {
                Range range = sheet.Cells[1, column];
                range.EntireColumn.SpecialCells(XlCellType.xlCellTypeConstants, 2).EntireRow.Delete();
            }
            catch (Exception)
            {
            }
        }

        public void DeleteNumbers(int column, Worksheet sheet)
        {
            try
            {
                Range range = sheet.Cells[1, column];
                range.EntireColumn.SpecialCells(XlCellType.xlCellTypeConstants, 1).EntireRow.Delete();
            }
            catch (Exception)
            {
            }
        }

        public void ReplacePoint(int column, Worksheet sheet)
        {
            excel.DisplayAlerts = false;
            Range range = sheet.Cells[1, column];
            range.EntireColumn.Replace(What: ".", Replacement: ",", LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, MatchCase: false, SearchFormat: false, ReplaceFormat: false);
            excel.DisplayAlerts = true;
        }

        public void DoTextToColumns(int col, int rowStart, int rowEnd, Worksheet sheet)
        {
            Array fieldINFO = new int[,] { { 0, 1 } };
            Range startToEnd = sheet.Range[sheet.Cells[rowStart, col], sheet.Cells[rowEnd, col]];
            startToEnd.TextToColumns(Destination: sheet.Cells[rowStart, col], DataType: XlTextParsingType.xlFixedWidth,
                TrailingMinusNumbers: true, FieldInfo: (object)fieldINFO);
        }

        public Range ArticleCheck(Range range, Worksheet sheet)
        {
            Range mayBeArt = sheet.Cells;
            string article = "артикул";

            Range itog = mayBeArt.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (itog != null)
            {
                itog = sheet.Cells[kol.Row, itog.Column];
                itog.Value = "Б";
                return itog;
            }
            article = "Б";
            itog = range.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
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
            itog = range.Find(What: article, LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (itog != null)
            {
                return itog;
            }
            else
            {
                MessageBox.Show("Не получилось найти столбец с артикулом");
                Environment.Exit(0);
                return null;
            }

        }

        public void CopySuda(Worksheet from)
        {
            if (!ChCopySud.Checked)
            {
                return;
            }
            try
            {
                Workbook macroBook = excel.Workbooks["Макросы.xlsm"];
                Worksheet macroSheet = macroBook.Sheets["СЮДА"];
                from.Cells.Copy(macroSheet.Range["A1"]);

            }
            catch (Exception)
            {
                MessageBox.Show("Файл Макросы не найден");
                Environment.Exit(0);
            }
        }

        public void RemoveImages(Worksheet list)
        {
            for (int i = 1; i <= list.Shapes.Count; i++)
            {
                list.Shapes.Item(i).Delete();
            }
        }

        public void Obrabotka(Worksheet someSheet, bool bezB)
        {
            //расширяем колонки, для того, чтоб поиск смог найти значения, ищем цифру 3 в ячейке (колонка с количеством, универсальная)
            someSheet.Cells.UnMerge();
            someSheet.Cells.ColumnWidth = 4;
            kol = someSheet.Cells.Find(What: "3", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);

            if (kol != null)
            {
                poleKol = "3";
            }
            else
            {
                MessageBox.Show($"Не найден столбец 3");
                Environment.Exit(0);
            }



            //проверка есть ли Б. Если есть то считаем, что это УПД.

            poleArt = "Б"; // это можно удалить, сейчас поле с артикулом находится через ArticleCheck, не принимающего поисковый аргумент
            poleKol = "3";
            poleSum = "9";
            poleKod = "10";
            poleStrana = "10а";
            poleGtd = "11";


            //находим все поля
            if (bezB)
            {
                poleArt = "1";
                arti = FindCell(poleArt, kol.EntireRow, mySheet);
            }
            else
            {
                arti = ArticleCheck(kol.EntireRow, someSheet);
            }

            try
            {
                kol.EntireRow.SpecialCells(XlCellType.xlCellTypeBlanks).EntireColumn.Delete();
            }
            catch (Exception)
            {
            }

            kol = FindCell(poleKol, kol.EntireRow, someSheet);
            sum = FindCell(poleSum, kol.EntireRow, someSheet);
            kod = FindCell(poleKod, kol.EntireRow, someSheet);
            strana = FindCell(poleStrana, kol.EntireRow, someSheet);
            gtd = FindCell(poleGtd, kol.EntireRow, someSheet);

            //MessageBox.Show($"арткикул - {arti.Column} колво - {kol.Column} сумма - {sum.Column} код - {kod.Column} страна - {strana.Column} гтд - {gtd.Column}");

            Range lastCell = someSheet.Cells[10000, arti.Column];
            int strok = lastCell.End[XlDirection.xlUp].Row;


            Range firstLine = someSheet.Range[someSheet.Cells[1, 1], someSheet.Cells[1, 100]];

            //более быстрая пометка необходимых столбцов, без полного перебора
            firstLine.Clear();
            someSheet.Cells[1, arti.Column].Value = "z";
            someSheet.Cells[1, kol.Column].Value = "z";
            someSheet.Cells[1, sum.Column].Value = "z";
            someSheet.Cells[1, kod.Column].Value = "z";
            someSheet.Cells[1, strana.Column].Value = "z";
            someSheet.Cells[1, gtd.Column].Value = "z";

            //foreach (Range cell in firstLine)
            //{
            //    if (cell.Column != arti.Column && cell.Column != kol.Column && cell.Column != sum.Column &&
            //        cell.Column != kod.Column && cell.Column != strana.Column && cell.Column != gtd.Column)
            //    {
            //        cell.Clear();
            //    }
            //    else
            //    {
            //        cell.Value = "z";
            //    }
            //}

            firstLine.SpecialCells(XlCellType.xlCellTypeBlanks).EntireColumn.Delete();
            DoTextToColumns(2, arti.Row + 1, strok, someSheet);
            DoTextToColumns(3, arti.Row + 1, strok, someSheet);

            ReplacePoint(2, someSheet);
            ReplacePoint(3, someSheet);

            DeleteEmpty(2, someSheet);
            DeleteEmpty(3, someSheet);
            DeleteNumbers(6, someSheet);
            DeleteText(2, someSheet);
            DeleteText(3, someSheet);

            someSheet.Cells.WrapText = false;
            someSheet.Cells.RowHeight = 14;
            someSheet.Cells.Columns.AutoFit();

            gogogo:
            Range lastCheck = someSheet.Range["A1"].EntireColumn.Find(What: "Б", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlWhole,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);
            if (lastCheck != null)
            {
                lastCheck.EntireRow.Delete();
                goto gogogo;
            }

            //сбрасываем поиск до стандартного, xlpart
            lastCheck = someSheet.Range["A1"].EntireColumn.Find(What: "Б", LookIn: XlFindLookIn.xlValues, LookAt: XlLookAt.xlPart,
                SearchOrder: XlSearchOrder.xlByRows, SearchDirection: XlSearchDirection.xlNext, MatchCase: false, SearchFormat: false);

            excel.DisplayAlerts = false;
            someSheet.Cells.Replace(What: "\"", Replacement: "", MatchCase: false, SearchFormat: false);
            someSheet.Cells.Replace(What: ";", Replacement: "", MatchCase: false, SearchFormat: false);
            someSheet.Cells.Replace(What: "\n", Replacement: "", MatchCase: false, SearchFormat: false);
            excel.DisplayAlerts = true;

            someSheet.Range["D1"].EntireColumn.NumberFormat = "@";
            someSheet.Range["B1"].EntireColumn.NumberFormat = "@";
            someSheet.Range["C1"].EntireColumn.NumberFormat = "@";
            //someSheet.Range["E1"].EntireColumn.NumberFormat = "@";
            int rowEnd = someSheet.Range["E3000"].End[XlDirection.xlUp].Row;




        }

     

        public double MakeSum(Worksheet someSheet)
        {
            double sum = 0;
            Range endRange = someSheet.Range["C50000"].End[XlDirection.xlUp];
            if (endRange.Offset[0, -1].Value == null || endRange.Offset[0, -1].Value.ToString() == "")
            {
                endRange.EntireRow.Delete();
                endRange = someSheet.Range["C50000"].End[XlDirection.xlUp];
            }

            endRange.Offset[1, 0].NumberFormat = "Общий";
            endRange.Offset[1, 0].FormulaR1C1 = $"=SUM(R[-{endRange.Row}]C:R[-1]C)";

            sum = Convert.ToDouble(endRange.Offset[1, 0].Value);
            sum = Math.Round(sum, 2);
            endRange.Offset[1, 0].Clear();
            return sum;
        }

        public void SaveBook(Workbook wb, Worksheet ws)
        {
            string folder = myWB.Path;
            int indexPoint = myWB.Name.IndexOf('.');
            string textName = myWB.Name.Remove(indexPoint);
            double summa = MakeSum(ws);

            Range rangeArray = ws.Range["A1000"].End[XlDirection.xlUp];
            rangeArray = rangeArray.Offset[0, 5];
            int kol = rangeArray.Row;
            rangeArray = ws.Range[ws.Cells[1, 1], rangeArray];
            var array = rangeArray.Value;

            StreamWriter pishem = new StreamWriter(path: folder + "\\" + textName + " на " + summa + ".txt", append: false, encoding: Encoding.GetEncoding("Windows-1251"));
            
            for (int i = 1; i <= kol; i++)
            {
                pishem.WriteLine(array[i, 1] + "\t" + array[i, 2] + "\t" + array[i, 3] + "\t" + array[i, 4] + "\t" + array[i, 5] + "\t" + array[i, 6]);
            }
            pishem.Close();
            //if (Environment.UserName.ToLower() == "embakh_a")
            //{
            //    ws.SaveAs(Filename: folder + "\\" + textName + " на " + summa + ".txt", FileFormat: XlFileFormat.xlTextWindows, Local: false);
            //}
            //else
            //{
            //    ws.SaveAs(Filename: folder + "\\" + textName + " на " + summa + ".txt", FileFormat: XlFileFormat.xlTextWindows, Local: true);
            //}

            wb.Close(SaveChanges: false);


        }

        private void Bend_Click(object sender, EventArgs e)
        {
            excel = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Close();
        }

        private void EndikShpendik()
        {

            try
            {
                MainForm.ActiveForm.WindowState = FormWindowState.Minimized;
            }
            catch (Exception)
            {
            }
            Clipboard.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //if (Environment.UserName.ToLower() != "embakh_a")
            //{
            //    this.Close();
            //}

        }


        private void BSF2_Click(object sender, EventArgs e) // кнопка "без Б"
        {
            LStatus.Text = "ВЫПОЛНЯЮСЬ!";
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application")
                as Excel.Application;
            myWB = excel.ActiveWorkbook;
            mySheet = myWB.ActiveSheet;


            RemoveImages(mySheet);
            CopySuda(mySheet);

            excel.ScreenUpdating = false;
            try
            {
                Obrabotka(mySheet, true);

            }
            catch (Exception)
            {
                excel.ScreenUpdating = true;
                MessageBox.Show("Чет пошло не так :(");
                goto ends;
            }
            excel.ScreenUpdating = true;
            //SaveBook(myWB, mySheet);


            ends:
            EndikShpendik();
        }

        private void BChoseFolder_Click(object sender, EventArgs e)
        {
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                mySFPath = FolderBrowser.SelectedPath;
                LFolder.Text = mySFPath + "\\";
                optionsWrite = new StreamWriter(desktopPath + optionsFile);
                optionsWrite.WriteLine(mySFPath);
                optionsWrite.Close();
            }
        }

        private void BTestik_Click(object sender, EventArgs e)
        {
            // определяем лог файл
            logWrite = new StreamWriter(mySFPath + "\\" + logFile);
            logWrite.AutoFlush = true;

            LStatus.Text = "ВЫПОЛНЯЮСЬ!";
            DirectoryInfo folder = new DirectoryInfo(mySFPath);     // папка с файлами - зада пользователем через браузер папок
            excel = new Excel.Application();
            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.Attributes.HasFlag(FileAttributes.Hidden))        // если файл скрытый - пропускаем
                {
                    continue;
                }

                MySFMethods.vseOK = true;       // устанавливаем флай ВсеОК true (далее если после обработки он будет false - пропускаем файл

                if ((file.Name.IndexOf(".xls") == -1) && (file.Name.IndexOf(".xlsx") == -1) && (file.Name.IndexOf(".csv") == -1))
                {
                    continue;       // обрабатываем только файлы с определенным расширением
                }
                LStatus.Text = $"ВЫПОЛНЯЮСЬ! текущий файл: {file.Name}";
                RTfiles.Text += file.Name;  // записываем в ричбокс имя текущего файла

                excel.Workbooks.Open(folder + "\\" + file.Name);
                myWB = excel.ActiveWorkbook;
                mySheet = myWB.ActiveSheet;
                myMethods.RemoveImages(mySheet);   // перед дальнейшей обработкой убираем мусор из файла
                excel.ScreenUpdating = false;
                logWrite.Write($"{DateTime.Now}:  {file.Name}");
                try
                {
                    myMethods.Obrabotka(mySheet, false);   // основной метод обработки, оставляет только 6 столбцов
                    if (MySFMethods.vseOK == false)
                    {
                        RTfiles.Text += "  - что-то пошло не так :(\n";      // false бывает в случае неверной СФ, под которую не настроена программа
                        continue;    // пропускаем файл, который не получилось обработать
                    }

                    myMethods.SaveBook(myWB, mySheet);   // метод сохранения файла в формате txt
                    RTfiles.Text += "  - обработано\n";
                    logWrite.WriteLine($" -- обработано");
                }
                catch (Exception ex)          // в случае исключений ставим поомутку в ричбоксе и в логах
                {
                    excel.ScreenUpdating = true;
                    RTfiles.Text += "  - что-то пошло не так :(\n";
                    logWrite.WriteLine($" -- ошибка: {ex.Message}");
                }
            }

            LStatus.Text = "ГОТОВО!";
            EndikShpendik();
        }

        private void BUPD1_Click(object sender, EventArgs e) // зеленая кнопка
        {
            LStatus.Text = "ВЫПОЛНЯЮСЬ!";
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application")
                as Excel.Application;
            myWB = excel.ActiveWorkbook;
            mySheet = myWB.ActiveSheet;

            RemoveImages(mySheet);
            CopySuda(mySheet);

#if !DEBUG
            excel.ScreenUpdating = false;
#endif
            try
            {
                Obrabotka(mySheet, false);

            }
            catch (Exception)
            {
                excel.ScreenUpdating = true;
                MessageBox.Show("Чет пошло не так :(");
                goto ends;
            }
            excel.ScreenUpdating = true;
            //SaveBook(myWB, mySheet);

            
            ends:
            EndikShpendik();
        }

        private void BUPDSave_Click(object sender, EventArgs e)
        {
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
            myWB = excel.ActiveWorkbook;
            mySheet = myWB.ActiveSheet;

            RemoveImages(mySheet);

            Workbook tempBook = excel.Workbooks.Add();
            Worksheet tempSheet = tempBook.ActiveSheet;

            mySheet.Cells.Copy(tempSheet.Range["A1"]);

            //excel.ScreenUpdating = false;
            try
            {
                Obrabotka(tempSheet, false);

            }
            catch (Exception)
            {
                excel.ScreenUpdating = true;
                MessageBox.Show("Чет пошло не так :(");
                goto ends;
            }
            excel.ScreenUpdating = true;
            SaveBook(tempBook, tempSheet);

            ends:
            EndikShpendik();
        }

        private void BBezB_Click(object sender, EventArgs e)
        {
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
            myWB = excel.ActiveWorkbook;
            mySheet = myWB.ActiveSheet;

            RemoveImages(mySheet);

            Workbook tempBook = excel.Workbooks.Add();
            Worksheet tempSheet = tempBook.ActiveSheet;

            mySheet.Cells.Copy(tempSheet.Range["A1"]);


            excel.ScreenUpdating = false;
            try
            {
                Obrabotka(tempSheet, true);

            }
            catch (Exception)
            {
                excel.ScreenUpdating = true;
                MessageBox.Show("Чет пошло не так :(");
                goto ends;
            }
            excel.ScreenUpdating = true;
            SaveBook(tempBook, tempSheet);

            ends:
            EndikShpendik();
        }

        private void BFix_Click(object sender, EventArgs e)
        {
            excel = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
            excel.ScreenUpdating = true;
        }

        private void BMassBoom_Click(object sender, EventArgs e)
        {
            if (mySFPath == "" || LFolder.Text == "Выбрана папка: ")
            {
                MessageBox.Show("Не обозначена папка с файлами!");
                return;
            }

            DirectoryInfo folder = new DirectoryInfo(mySFPath);     // папка с файлами - зада пользователем через браузер папок
            int kol = folder.GetFiles().Count();

            if (kol == 0)
            {
                MessageBox.Show("В папке отсутствуют файлы!");
                return;
            }

            // определяем лог файл
            logWrite = new StreamWriter(mySFPath + "\\" + logFile);
            logWrite.AutoFlush = true;

            LStatus.Text = "ВЫПОЛНЯЮСЬ!";
            LStatus.Refresh();
            
            excel = new Excel.Application();
            int counter = 0; // счетчик утераций
            foreach (FileInfo file in folder.GetFiles())
            {
                if (file.Attributes.HasFlag(FileAttributes.Hidden))        // если файл скрытый - пропускаем
                {
                    continue;
                }

                MySFMethods.vseOK = true;       // устанавливаем флай ВсеОК true (далее если после обработки он будет false - пропускаем файл

                if ((file.Name.IndexOf(".xls") == -1) && (file.Name.IndexOf(".xlsx") == -1) && (file.Name.IndexOf(".csv") == -1))
                {
                    continue;       // обрабатываем только файлы с определенным расширением
                }
                counter++;
                LStatus.Text = $"ВЫПОЛНЯЮСЬ! текущий файл: {file.Name}";
                RTfiles.Text += file.Name;  // записываем в ричбокс имя текущего файла

                excel.Workbooks.Open(folder + "\\" + file.Name);
                myWB = excel.ActiveWorkbook;
                mySheet = myWB.ActiveSheet;
                myMethods.RemoveImages(mySheet);   // перед дальнейшей обработкой убираем мусор из файла
                excel.ScreenUpdating = false;
                logWrite.Write($"{DateTime.Now}:  {file.Name}");
                try
                {
                    myMethods.Obrabotka(mySheet, false);   // основной метод обработки, оставляет только 6 столбцов
                    if (MySFMethods.vseOK == false)
                    {
                        RTfiles.Text += "  - что-то пошло не так :(\n";      // false бывает в случае неверной СФ, под которую не настроена программа
                        RTfiles.Select(RTfiles.GetFirstCharIndexFromLine(1), 1);
                        RTfiles.SelectionBackColor = Color.LawnGreen;

                        continue;    // пропускаем файл, который не получилось обработать
                    }

                    myMethods.SaveBook(myWB, mySheet);   // метод сохранения файла в формате txt
                    RTfiles.Text += "  - обработано\n";
                    logWrite.WriteLine($" -- обработано");
                }
                catch (Exception ex)          // в случае исключений ставим пометку в ричбоксе и в логах
                {
                    excel.ScreenUpdating = true;
                    RTfiles.Text += "  - что-то пошло не так :(\n";
                    //RTfiles.Select(RTfiles.GetFirstCharIndexFromLine(counter), file.Name.Length);
                    //RTfiles.SelectionBackColor = Color.LawnGreen;
                    logWrite.WriteLine($" -- ошибка: {ex.Message}");
                }
                
            }
            logWrite.Close();

            LStatus.Text = "ГОТОВО!";
            EndikShpendik();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
