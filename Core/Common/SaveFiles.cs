using ClosedXML.Excel;
using Domain.Common;
 
 

namespace Core.Common
{
    public class SaveFiles
    { 

        public string SaveFileBase64(ObjectFileSave objectFileSave )
        {
            if (!Directory.Exists(objectFileSave.FilePath))
            {
                Directory.CreateDirectory(objectFileSave.FilePath);
            }
            byte[] fileBytes = Convert.FromBase64String(objectFileSave.Base64String);
            string fullPath = Path.Combine(objectFileSave.FilePath, objectFileSave.FileName);
            File.WriteAllBytes(fullPath, fileBytes);
            return fullPath;
        }

        public string SaveExcel(ObjectFileSaveExcel objectFileSaveExcel)
        {
            String ruta = objectFileSaveExcel.Path+@"\"+ @"ExcelRejected" + objectFileSaveExcel.IdUser+ @"_"+DateTime.Now.ToString("yyyyMMdd")+ @".xlsx";       
            var workbook = new XLWorkbook();
        
            var worksheet = workbook.Worksheets.Add("Data");
            worksheet.Cell(1, 1).Value = "IdProceso";
            worksheet.Cell(1, 2).Value = "Idvacante";
            worksheet.Cell(1, 3).Value = "Nombre candidato";
            worksheet.Cell(1, 4).Value = "Correo";
            worksheet.Cell(2, 1).InsertData(objectFileSaveExcel.Lista);
            workbook.SaveAs(ruta);           
            return ruta;
        }
    }
}
