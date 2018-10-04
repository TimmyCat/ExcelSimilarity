using System.Windows;
using ExcelSimilarity.BusinessLogic;

namespace ExcelSimilarity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ExcelSimilarityViewModel(this.maDataGrid);
        }
    }
}
