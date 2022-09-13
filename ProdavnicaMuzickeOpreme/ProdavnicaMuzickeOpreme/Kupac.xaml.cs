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
    public partial class Kupac : Window
    {
        public Kupac()
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
                CommandText = "SELECT * FROM [Kupac]",
                Connection = connection
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Kupac");
            dataAdapter.Fill(dataTable);

            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void ponistiUnosTxt()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtAdresa.Text = "";
            txtTelefon.Text = "";

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
                CommandText = "INSERT INTO [Kupac](Ime, Prezime, Adresa, Telefon) VALUES(@Ime, @Prezime, @Adresa, @Telefon)"
            };
            command.Parameters.AddWithValue("@Ime", txtIme.Text);
            command.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            command.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            command.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
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
                txtIDKupca.Text = dr["IDKupca"].ToString();
                txtIme.Text = dr["Ime"].ToString();
                txtPrezime.Text = dr["Prezime"].ToString();
                txtAdresa.Text = dr["Adresa"].ToString();
                txtTelefon.Text = dr["Telefon"].ToString();
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
                    CommandText = "DELETE FROM [Kupac] WHERE IDKupca = @IDKupca"
                };
                command.Parameters.AddWithValue("@IDKupca", txtIDKupca.Text);
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
                CommandText = "UPDATE [Kupac] SET Ime = @Ime, Prezime = @Prezime, Adresa = @Adresa, Telefon = @Telefon WHERE IDDostavljaca = @IDDostavljaca"
            };
            command.Parameters.AddWithValue("@IDKupca", txtIDKupca.Text);
            command.Parameters.AddWithValue("@Ime", txtIme.Text);
            command.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            command.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            command.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
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