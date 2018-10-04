using System.Data;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace ExcelSimilarity.Interfaces
{
    public interface IExcelBusinessLogic
    {
        void SetDataTable(DataTable dataTable);

        bool CheckStringFormat(string analyzeString);

        string StringAnalyze(string analyzeString);

        Task<string> GetCellValue(string analyzeString);

        void GridCellEditEnding(object sender, DataGridCellEditEndingEventArgs e);
    }
}
