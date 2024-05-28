using System.Windows;

namespace RemoteCheckPosForNet
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this._vm = new MainWindowViewModel();
        }

        private async void BtnTest_OnClick(object sender, RoutedEventArgs e)
        {
            await _vm.IsFingerprintPresent();
        }
    }
}
