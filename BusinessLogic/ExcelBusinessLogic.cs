using System;
using System.Data;
using System.Windows.Controls;
using ExcelSimilarity.Interfaces;
using ExcelSimilarity.Extensions;
using System.Threading.Tasks;

namespace ExcelSimilarity.BusinessLogic
{
    public class ExcelBusinessLogic : IExcelBusinessLogic
    {
        private DataTable ExcelTable { get; set; }

        public bool CheckStringFormat(string analyzeString)
        {
            //format of record А1+В2*С4
            if (analyzeString.Length < 5 || analyzeString.Length % 3 != 2)
                return false;

            //need regex to check А1+В2*С4 format and need to check notnull value of cells

            return true;
        }

        public Task<string> GetCellValue(string analyzeString)
        {
            return Task.Run(() => 
            {
                float value = 0;
                try
                {
                    var rowIndexX = char.ToUpper(analyzeString[0]) - 65;
                    var colIndexX = int.Parse(analyzeString[1].ToString());

                    value = float.Parse(ExcelTable.Rows[rowIndexX].ItemArray[colIndexX].ToString());

                    for (var i = 2; i < analyzeString.Length;)
                    {
                        var rowIndex = char.ToUpper(analyzeString[i + 1]) - 65;
                        var colIndex = int.Parse(analyzeString[i + 2].ToString());
                        var y = ExcelTable.Rows[rowIndex].ItemArray[colIndex].ToString();

                        value = analyzeString[i].ToString().Calculate(value, float.Parse(y.ToString()));

                        i += 3;
                    }
                }
                catch (Exception sx)
                {
                    return string.Empty;
                }

                return value.ToString();
            });
        }

        public void GridCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var textBox = e.EditingElement as TextBox;
            textBox.Text = StringAnalyze(textBox.Text);
        }

        public string StringAnalyze(string analyzeString)
        {
            if (analyzeString.TrimStart().StartsWith("'"))
            {
                return analyzeString.Remove(0, 1);
            }
            if (analyzeString.TrimStart().StartsWith("="))
            {
                analyzeString = analyzeString.Remove(0, 1);

                if (!CheckStringFormat(analyzeString))
                    return string.Empty;

                return GetCellValue(analyzeString).Result;
            }

            return analyzeString;
        }

        public void SetDataTable(DataTable dataTable)
        {
            ExcelTable = dataTable;
        }
    }
}
