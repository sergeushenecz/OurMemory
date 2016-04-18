using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using LinqToExcel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OurMemory.Common;
using OurMemory.Resource;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;

namespace OurMemory.Service.Parsers
{
    public class ExcellParser : IExcellParser
    {
        private readonly ExcelQueryFactory excel;

        public ExcellParser(string filePath)
        {
            excel = new ExcelQueryFactory(filePath);

        }

        public List<VeteranMapping> GetVeterans()
        {
            var rows = (from c in excel.Worksheet<VeteranMapping>()
                        select c).ToList();

            return rows;
        }


        public static string GenerateReport(List<VeteranMapping> veteranMapping)
        {
            // Set the file name and get the output directory
            var fileName = "Report-" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            var outputDir = HttpContext.Current.Server.MapPath(ConfigurationSettingsModule.GetItem("Temp"));
            var virtualPath = ConfigurationSettingsModule.GetItem("Temp");

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Create the file using the FileInfo object
            var file = new FileInfo(outputDir + fileName);


            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1 - " + DateTime.Now.ToShortDateString());

                AddFormating(worksheet);
                AddHeader(worksheet);
                AddData(worksheet, veteranMapping);


                // save our new workbook and we are done!
                package.Save();
            }


            return virtualPath + fileName;
        }


        private static void AddFormating(ExcelWorksheet excelWorksheet)
        {
            // Add some formatting to the worksheet
            excelWorksheet.TabColor = Color.Blue;
            excelWorksheet.DefaultRowHeight = 20;
            excelWorksheet.DefaultColWidth = 20;
            excelWorksheet.Cells["A1:K1"].Style.Font.Bold = true;
            excelWorksheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelWorksheet.Column(11).Width = 150;


        }

        private static void AddHeader(ExcelWorksheet excelWorksheet)
        {
            excelWorksheet.Cells[1, 1].Value = OurMemoryResource.Excell_Header_FirstName;
            excelWorksheet.Cells[1, 2].Value = OurMemoryResource.Excell_Header_LastName;
            excelWorksheet.Cells[1, 3].Value = OurMemoryResource.Excell_Header_Url_MiddleName;
            excelWorksheet.Cells[1, 4].Value = OurMemoryResource.Excell_Header_DateBirth;
            excelWorksheet.Cells[1, 5].Value = OurMemoryResource.Excell_Header_BirthPlace;
            excelWorksheet.Cells[1, 6].Value = OurMemoryResource.Excell_Header_DateDeath;
            excelWorksheet.Cells[1, 7].Value = OurMemoryResource.Excell_Header_Called;
            excelWorksheet.Cells[1, 8].Value = OurMemoryResource.Excell_Header_Awards;
            excelWorksheet.Cells[1, 9].Value = OurMemoryResource.Excell_Header_Troops;
            excelWorksheet.Cells[1, 10].Value = OurMemoryResource.Excell_Header_Description;
            excelWorksheet.Cells[1, 11].Value = OurMemoryResource.Excell_Header_Url_Images;
        }

        private static void AddData(ExcelWorksheet excelWorksheet, List<VeteranMapping> veteranMapping)
        {
            for (int i = 0; i < veteranMapping.Count; i++)
            {
                excelWorksheet.Cells[i + 2, 1].Value = veteranMapping[i].FirstName;
                excelWorksheet.Cells[i + 2, 2].Value = veteranMapping[i].LastName;
                excelWorksheet.Cells[i + 2, 3].Value = veteranMapping[i].MiddleName;
                excelWorksheet.Cells[i + 2, 4].Value = veteranMapping[i].DateBirth;
                excelWorksheet.Cells[i + 2, 5].Value = veteranMapping[i].BirthPlace;
                excelWorksheet.Cells[i + 2, 6].Value = veteranMapping[i].DateDeath;
                excelWorksheet.Cells[i + 2, 7].Value = veteranMapping[i].Called;
                excelWorksheet.Cells[i + 2, 8].Value = veteranMapping[i].Awards;
                excelWorksheet.Cells[i + 2, 9].Value = veteranMapping[i].Troops;
                excelWorksheet.Cells[i + 2, 10].Value = veteranMapping[i].Description;
                excelWorksheet.Cells[i + 2, 11].Value = veteranMapping[i].UrlImages;
            }
        }
    }
}
