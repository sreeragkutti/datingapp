using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style.XmlAccess;

private byte[] ExcelGenerator(DataTable dataTable, string worksheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage data = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet worksheet = data.Workbook.Worksheets.Add(worksheetName);
                    worksheet.View.FreezePanes(2, 1);
                    worksheet.DefaultColWidth = 25;
                    worksheet.DefaultRowHeight = 15;
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                    var allCells = worksheet.Cells[worksheet.Dimension.Address];
                    allCells.AutoFilter = true;
                    allCells.Style.WrapText = true;
                    allCells.AutoFitColumns();
                    allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;                   
                    for (var row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (row % 2 != 0)
                        {
                            worksheet.Row(row).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Row(row).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);                               
                        }
                    }
                    string StyleName = "HyperStyle";
                    ExcelNamedStyleXml HyperStyle = worksheet.Workbook.Styles.CreateNamedStyle(StyleName);
                    HyperStyle.Style.Font.UnderLine = true;
                    HyperStyle.Style.Font.Size = 12;
                    HyperStyle.Style.WrapText = true;
                    HyperStyle.Style.Font.Color.SetColor(Color.Blue);
                    
                    for (int i = 0,j=1; i <= dataTable.Rows.Count -1; i++,j++)
                    {
                        using (ExcelRange Rng = worksheet.Cells[j + 1, 9])
                        {
                            if (string.IsNullOrEmpty(dataTable.Rows[i][9].ToString()))
                            {
                                Rng.Value = "";                                
                            }
                            else
                            {
                                Rng.Hyperlink = new Uri(dataTable.Rows[i][9].ToString(), UriKind.Absolute);
                                Rng.Value = dataTable.Rows[i][8].ToString();//Link name
                                Rng.StyleName = StyleName;                                
                            }
                        }

                    }
                    
                    var headerCells = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
                    headerCells.Style.Font.Bold = true;
                    worksheet.DeleteColumn(10);
                    return data.GetAsByteArray();                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }              
            }
        }