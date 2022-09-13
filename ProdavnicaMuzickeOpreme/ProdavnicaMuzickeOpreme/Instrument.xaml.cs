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
    public partial class Instrument : Window
    {
        public Instrument()
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
                CommandText = "SELECT * FROM [Instrument]",
                Connection = connection
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Instrument");
            dataAdapter.Fill(dataTable);

            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void ponistiUnosTxt()
        {
            txtVrsta.Text = "";
            txtModel.Text = "";
            txtCena.Text = "";

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIDInstrumenta.Text = dr["IDInstrumenta"].ToString();
                txtVrsta.Text = dr["Vrsta"].ToString();
                txtModel.Text = dr["Model"].ToString();
                txtCena.Text = dr["Cena"].ToString();
            }
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
                CommandText = "INSERT INTO [Instrument](Vrsta, Model, Cena) VALUES(@Vrsta, @Model, @Cena)"
            };
            command.Parameters.AddWithValue("@Vrsta", txtVrsta.Text);
            command.Parameters.AddWithValue("@Model", txtModel.Text);
            command.Parameters.AddWithValue("@Cena", txtCena.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno upisani");
                binDataGrid();
            }
            ponistiUnosTxt();
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
                    CommandText = "DELETE FROM [Instrument] WHERE IDInstrumenta = @IDInstrumenta"
                };
                command.Parameters.AddWithValue("@IDInstrumenta", txtIDInstrumenta.Text);
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
                CommandText = "UPDATE [Instrument] SET Vrsta = @Vrsta, Model = @Model, Cena = @Cena WHERE IDInstrumenta = @IDInstrumenta"
            };
            command.Parameters.AddWithValue("@IDInstrumenta", txtIDInstrumenta.Text);
            command.Parameters.AddWithValue("@Vrsta", txtVrsta.Text);
            command.Parameters.AddWithValue("@Model", txtModel.Text);
            command.Parameters.AddWithValue("@Cena", txtCena.Text);
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