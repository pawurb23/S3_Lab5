using S3_Lab5.Abstractions;
using System.Windows;

namespace S3_Lab5
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(IPlywacyRepository repository)
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(repository);
            this.DataContext = _viewModel;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e) => _viewModel.DodajNowego();
        private void BtnUsun_Click(object sender, RoutedEventArgs e) => _viewModel.UsunWybranego();
        private void BtnZapisz_Click(object sender, RoutedEventArgs e) => _viewModel.ZapiszZmiany();
        private void BtnWyczysc_Click(object sender, RoutedEventArgs e) => _viewModel.WyczyscFormularz();
    }
}