using System.Windows;

namespace AssemlyBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ModelView();
        }

    }
}
