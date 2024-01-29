using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
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
using Dapper;

namespace Words
{
    /// <summary>
    /// Logika interakcji dla klasy Modify.xaml
    /// </summary>
    public partial class Modify : Page
    {
        string path = "C:/Users/Uczeń-13/Desktop/4pTP/Words/qwe.db";
        public Modify()
        {
            InitializeComponent();
            CreateTableOfData();
        }

        private void CreateTableOfData()
        {
            AddRow("Id","Polskie słowa","Angielskie słowa","");

            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));
            try
            {
                conn.Open();
                var listOfWords = conn.Query<DataRowOfWords>("select * from slowka;").ToList();

                foreach (var element in listOfWords)
                {
                    AddRow(element.id.ToString(), element.pl, element.eng,element.difficulty.ToString());
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

        private void AddRow(string id, string pl, string eng, string difficulty)
        {
            var rowGroup = dataTable.RowGroups.FirstOrDefault();

            if (rowGroup != null)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();




                cell.Blocks.Add(new Paragraph(new Run(id)));
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Blocks.Add(new Paragraph(new Run(pl)));
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Blocks.Add(new Paragraph(new Run(eng)));
                row.Cells.Add(cell);

                rowGroup.Rows.Add(row);
            }
        }

        private void RemoveRows()
        {
            var rowGroup = dataTable.RowGroups.FirstOrDefault();

            if (rowGroup != null)
            {
                rowGroup.Rows.Clear();
            }
        }

        private void AddRowToDatabase(object sender, RoutedEventArgs e)
        {
            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));

            try
            {
                conn.Open();
                if(polishWordBox.Text != null && englishWordBox.Text != null)
                    conn.Query<DataRowOfWords>(String.Format("insert into slowka values(null,'{0}','{1}',1);", 
                        polishWordBox.Text, 
                        englishWordBox.Text));
            }
            catch (Exception ex) 
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
            }
            finally
            { conn.Close(); }

            RemoveRows();
            CreateTableOfData();
        }

        private void RemoveRowFromDatabase(object sender, RoutedEventArgs e)
        {
            var conn = new SQLiteConnection(string.Format("Data Source={0}", path));

            try
            {
                conn.Open();
                if (rowToRemove.Text != null && int.TryParse(rowToRemove.Text, out int value))
                    conn.Query<DataRowOfWords>(String.Format("delete from slowka where id = {0}",
                        rowToRemove.Text));
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------");
            }
            finally
            { conn.Close(); }

            RemoveRows();
            CreateTableOfData();
        }
    }
}
