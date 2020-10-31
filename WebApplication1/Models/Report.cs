using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Report : Controller
    {
        public FileResult GetReportBy(List<PS_Log> lis)
        {
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Users");
                List<string[]> header = new List<string[]>()
                {
                    new string[] { "Клиент", "ИНН", "Дата рождения" }
                };
                worksheet.Cells("A1").Value = "System_Req";
                worksheet.Cells("B1").Value = "Request";
                worksheet.Cells("C1").Value = "Response";
                worksheet.Cells("D1").Value = "Target_Path";
                worksheet.Cells("E1").Value = "INS_Date";
                worksheet.Cells("F1").Value = "AMNT";
                worksheet.Cells("G1").Value = "ACC";
                worksheet.Cells("H1").Value = "CUR";

                int currentRow = 1;

                foreach (var user in lis)
                {
                    currentRow++;
                    worksheet.Cells("A" + currentRow).Value = user.System_Req;
                    worksheet.Cells("B" + currentRow).Value = user.Request;
                    worksheet.Cells("C" + currentRow).Value = user.Response;
                    worksheet.Cells("D" + currentRow).Value = user.Target_Path;
                    worksheet.Cells("E" + currentRow).Value = user.INS_Date;
                    worksheet.Cells("F" + currentRow).Value = user.AMNT;
                    worksheet.Cells("G" + currentRow).Value = user.ACC;
                    worksheet.Cells("H" + currentRow).Value = user.Cur;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reports on Log on: " + DateTime.Now.Date +
                        ".xlsx");
                }

                //worksheet.GetAsByteArray(), "application/excel", "Отчет за " + DateTime.Now.Date + ".xlsx"
                //);
            }

        }
    }
}
