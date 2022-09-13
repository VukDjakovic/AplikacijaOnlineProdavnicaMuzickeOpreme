using ProdavnicaMuzickeOpreme;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace ProdavnicaMuzickeOpreme
{
    public partial class Prodavac : Window
    {
        public Prodavac()
        {
            InitializeComponent();
        }

        private void binDataGrid()
        {

            SqlConnection connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                CommandText = "SELECT * FROM [Prodavac]",
                Connection = connection
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Prodavac");
            dataAdapter.Fill(dataTable);

            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void ponistiUnosTxt()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";

        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                CommandText = "INSERT INTO [Prodavac](Ime, Prezime) VALUES(@Ime, @Prezime)"
            };
            command.Parameters.AddWithValue("@Ime", txtIme.Text);
            command.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                binDataGrid();
            }
            ponistiUnosTxt();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIDProdavca.Text = dr["IDProdavca"].ToString();
                txtIme.Text = dr["Ime"].ToString();
                txtPrezime.Text = dr["Prezime"].ToString();

            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            {
                SqlConnection connection = new SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
                };
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "DELETE FROM [Prodavac] WHERE IDProdavca = @IDProdavca"
                };
                command.Parameters.AddWithValue("@IDProdavca", txtIDProdavca.Text);
                command.Connection = connection;
                int provera = command.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci su uspešno obrisani");
                    binDataGrid();
                }
                ponistiUnosTxt();
            }
        }
        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                CommandText = "UPDATE [Prodavac] SET Ime = @Ime, Prezime = @Prezime WHERE IDProdavca = @IDProdavca"
            };
            command.Parameters.AddWithValue("@IDProdavca", txtIDProdavca.Text);
            command.Parameters.AddWithValue("@Ime", txtIme.Text);
            command.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");
                binDataGrid();
            }
            ponistiUnosTxt();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}