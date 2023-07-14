using Microsoft.AspNetCore.Mvc;
using UsersNotebook.Persistence.Repositories;
using UsersNotebook.Persistence;
using UsersNotebook.Core.ViewModels;
using System.Text;
using UsersNotebook.Core.Models.Domains;
using UsersNotebook.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static NuGet.Packaging.PackagingConstants;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using UsersNotebook.Helpers;

namespace UsersNotebook.Controllers
{
    public class ReportController : Controller
    {
        private UserRepository _userRepository;

        public ReportController(UserDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public IActionResult UsersReport()
        {
            var vm = new UsersReportViewModel
            {
                FilterUsers = new FilterUsers(),
                Users = _userRepository.GetAll()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UsersReport(FilterUsers filter)
        {
            var users = _userRepository.Get(filter);

            return PartialView("_UsersReportTable", users);
        }
        [HttpGet]
        public IActionResult DownloadCsv(FilterUsers filter)
        {
            var users = _userRepository.Get(filter);
            var csvData = new StringBuilder();
            csvData.AppendLine("Tytuł;Imie;Nazwisko;Data urodzenia;Ilosc lat;Płeć");
            string title;
            DateTime today = DateTime.Today;
            int age;
            foreach (var user in users)
            {
                age = today.Year - user.DateOfBirth.Year;
                title = user.Gender == "Mężczyzna" ? "Pan" : "Pani";
                csvData.AppendLine($"{title};{user.Name};{user.Surname};{user.DateOfBirth};{age};{user.Gender}");
            }
            var csvBytes = Encoding.GetEncoding("iso-8859-2").GetBytes(csvData.ToString());
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}.csv";
            return File(csvBytes, "text/csv", fileName);
        }
        [HttpGet]
        public IActionResult DownloadExcel(FilterUsers filter)
        {
            var users = _userRepository.Get(filter);
            var ms = new MemoryStream();

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Users" };
                sheets.Append(sheet);

                var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                sheetData.AppendChild(
                    new DocumentFormat.OpenXml.Spreadsheet.Row(
                        new Cell() { CellValue = new CellValue("Id"), DataType = CellValues.String },
                        new Cell() { CellValue = new CellValue("Tytuł"), DataType = CellValues.String },
                        new Cell() { CellValue = new CellValue("Imię"), DataType = CellValues.String },
                        new Cell() { CellValue = new CellValue("Nazwisko"), DataType = CellValues.String },
                        new Cell() { CellValue = new CellValue("Płeć"), DataType = CellValues.String },
                        new Cell() { CellValue = new CellValue("Wiek"), DataType = CellValues.String }));
                    
                foreach (var user in users)
                {
                    string title = user.Gender == "Mężczyzna" ? "Pan" : "Pani";
                    int age = DateHelper.CalculateAge(user.DateOfBirth);

                    sheetData.AppendChild(
                        new DocumentFormat.OpenXml.Spreadsheet.Row(
                            new Cell() { CellValue = new CellValue(user.Id.ToString()), DataType = CellValues.Number },
                            new Cell() { CellValue = new CellValue(title), DataType = CellValues.String },
                            new Cell() { CellValue = new CellValue(user.Name), DataType = CellValues.String },
                            new Cell() { CellValue = new CellValue(user.Surname), DataType = CellValues.String },
                            new Cell() { CellValue = new CellValue(user.Gender), DataType = CellValues.String },
                            new Cell() { CellValue = new CellValue(age.ToString()), DataType = CellValues.Number }
                        )
                    );
                }
                Columns columns = new Columns(
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 1, Max = 1, Width = 10, CustomWidth = true },
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 2, Max = 2, Width = 10, CustomWidth = true },
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 3, Max = 3, Width = 15, CustomWidth = true },
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 4, Max = 4, Width = 15, CustomWidth = true },
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 5, Max = 5, Width = 12, CustomWidth = true },
                    new DocumentFormat.OpenXml.Spreadsheet.Column { Min = 6, Max = 6, Width = 8, CustomWidth = true });
                    
                worksheetPart.Worksheet.InsertBefore(columns, worksheetPart.Worksheet.Elements<SheetData>().First());

                worksheetPart.Worksheet.Save();
                workbookPart.Workbook.Save();
                document.Close();
            }

            ms.Position = 0;
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
