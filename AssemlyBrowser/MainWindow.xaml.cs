using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssemblyLibrary;

namespace AssemlyBrowser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Browser browser = new Browser();
            AssemblyInfo a = browser.GetResult(@"d:\Ангелина\5 сем\5 сем\СПП\Lab2-MPP\MPP-Faker\FakerLibrary\bin\Debug\netstandard2.0\FakerLibrary.dll");
            string s = "ddd";
        
        }
    }
}
