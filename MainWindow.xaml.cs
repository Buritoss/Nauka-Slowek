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
using System.Data.SQLite;
using Dapper;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Words
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            ChangeToTraining(new object(), new RoutedEventArgs());
        }

        private void ChangeToTraining(object sender, RoutedEventArgs e)
        {
            Main.Content = new training();
        }
        private void ChangeToModifying(object sender, RoutedEventArgs e)
        {
            Main.Content = new Modify();
        }
    }
}
