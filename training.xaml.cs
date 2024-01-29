using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
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

namespace Words
{
    /// <summary>
    /// Logika interakcji dla klasy training.xaml
    /// </summary>
    public partial class training : Page
    {
        string path = "C:/Users/Uczeń-13/Desktop/4pTP/Words/qwe.db";
        bool plToEng = true;
        int row;

        public training()
        {
            InitializeComponent();
            Change(new object(), new RoutedEventArgs());
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));

            try
            {
                conn.Open();
                var listOfWords = conn.Query<DataRowOfWords>("select * from slowka;").ToList();
                if (plToEng)
                {
                    if (UserInput.Text.ToLower() == listOfWords[row].pl.ToLower())
                    {
                        Change(new object(), new RoutedEventArgs());
                    }
                }
                else
                {
                    if (UserInput.Text.ToLower() == listOfWords[row].eng.ToLower())
                    {
                        Change(new object(), new RoutedEventArgs());
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
            }
            finally
            { conn.Close(); }

        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            plToEng = !plToEng;
            if (plToEng)
                plToEngButton.Content = "Pl To Eng";
            else
                plToEngButton.Content = "Eng To Pl";

            Change(new object(), new RoutedEventArgs());

        }


        private void Change(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));

            try
            {
                conn.Open();
                var listOfWords = conn.Query<DataRowOfWords>("select * from slowka;").ToList();
                
                row = rnd.Next(0, listOfWords.Count);
                if (plToEng)
                    QuestionBox.Text = listOfWords[row].eng;
                else
                    QuestionBox.Text = listOfWords[row].pl;
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
            }
            finally { conn.Close(); }
        }

        private void Hint(object sender, RoutedEventArgs e)
        {
            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));
            try
            {
                conn.Open();
                var listOfWords = conn.Query<DataRowOfWords>("select * from slowka;").ToList();
                if (plToEng)
                {
                    if (listOfWords[row].pl.Length > 4)
                        MessageBox.Show(string.Format("Pierwsze trzy litery słówka to {0}", listOfWords[row].pl[0] + listOfWords[row].pl[1] + listOfWords[row].pl[2]));
                    else
                        MessageBox.Show(string.Format("Pierwsza litera słówka to {0}", listOfWords[row].pl[0]));
                }
                else
                {
                    if (listOfWords[row].eng.Length > 4)
                        MessageBox.Show(string.Format("Pierwsze trzy litery słówka to {0}", listOfWords[row].eng[0] + listOfWords[row].eng[1] + listOfWords[row].eng[2]));
                    else
                        MessageBox.Show(string.Format("Pierwsza litera słówka to {0}", listOfWords[row].eng[0]));
                }


            }
            catch (Exception ex) 
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
            }
            finally
            { conn.Close(); }
        }
    }
}
