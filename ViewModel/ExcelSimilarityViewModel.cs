using ExcelSimilarity.Extensions;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data;
using System;
using ExcelSimilarity.Interfaces;
using ExcelSimilarity.BusinessLogic;

namespace ExcelSimilarity.ViewModel
{
    public class ExcelSimilarityViewModel : ViewModelBase
    {
        //chars A,B,C - this is rows
        //numbers 1,2,3 - this is columns
        // sry, my mistake

        #region Fields
        private string _rowCount,
            _columnCount;

        private DataGrid _dataGrid;
        private DataTable _dataTable;

        private ICommand _generateCommand;

        private IExcelBusinessLogic _businessLogic;
        #endregion

        #region Properties
        public string RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                OnPropertyChanged("RowCount");
            }
        }

        public string ColumnCount
        {
            get { return _columnCount; }
            set
            {
                _columnCount = value;
                OnPropertyChanged("ColumnCount");
            }
        }

        public DataGrid ExcelGrid
        {
            get { return _dataGrid; }
            set
            {
                _dataGrid = value;
                OnPropertyChanged("DataGrid");
            }
        }

        public DataTable ExcelTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OnPropertyChanged("ExcelTable");
            }
        }
        #endregion

        #region Constructor
        public ExcelSimilarityViewModel(DataGrid dataGrid)
        {
            _businessLogic = new ExcelBusinessLogic();
            ExcelGrid = dataGrid;
            ExcelGrid.CellEditEnding += _businessLogic.GridCellEditEnding;
        }
        #endregion

        #region Commands
        public ICommand GenerateCommand
        {
            get
            {
                return _generateCommand ??
                  (_generateCommand = new MyCommand(_ =>
                {
                    try
                    {
                        ExcelTable = new DataTable();
                        _businessLogic.SetDataTable(ExcelTable);

                        var colCount = int.Parse(ColumnCount);
                        var rowCount = int.Parse(RowCount);

                        if (colCount > 26 || rowCount > 26)
                            return;

                        for (var i = 0; i < colCount; i++)
                        {
                            ExcelTable.Columns.Add(i.ToString(), typeof(string));
                        }

                        for (var i = 0; i < rowCount - 1; i++)
                        {
                            ExcelTable.Rows.Add(ExcelTable.NewRow());
                        }
                        ExcelGrid.ItemsSource = ExcelTable.DefaultView;
                    }
                    catch (Exception sx)
                    {
                        return;
                    }
                }));
            }
        }

        #endregion
    }
}
