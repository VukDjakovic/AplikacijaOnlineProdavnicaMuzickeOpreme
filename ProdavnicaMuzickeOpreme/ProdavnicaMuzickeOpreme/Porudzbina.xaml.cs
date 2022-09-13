using ProdavnicaMuzickeOpreme;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    public partial class Porudzbina : Window
    {


        public Porudzbina()
        {
            InitializeComponent();
            binDataGrid();
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
                CommandText = "SELECT * FROM [Porudzbina] INNER JOIN [Instrument] ON [Porudzbina].IDInstrumenta = [Instrument].IDInstrumenta",
            Connection = connection
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Porudzbina");
            dataAdapter.Fill(dataTable);

            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void ponistiUnosTxt()
        {
            txtIDPorudzbine.Text = "";
            txtRacun.Text = "";
            dtDatum.Text = "";
            cbxIDInstrumenta.Text = "";


        }
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
            };
            connection.Open();
            DateTime Datum = Convert.ToDateTime(dtDatum.Text);
            string myDate = "01-01-2022 07:50:00:AM";
            DateTime dt1 = DateTime.ParseExact(myDate, "dd-MM-yyyy hh:mm:ss:tt",
                                                       CultureInfo.InvariantCulture);
            SqlCommand command = new SqlCommand
            {
                CommandText = "INSERT INTO [Porudzbina](Racun, Datum, IDInstrumenta) VALUES(@Racun, @Datum, @IDInstrumenta)"
            };
            command.Parameters.AddWithValue("@Racun", txtRacun.Text);
            command.Parameters.AddWithValue("@Datum", dtDatum.SelectedDate);
            command.Parameters.AddWithValue("@IDInstrumenta", cbxIDInstrumenta.SelectedValue);

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
                txtIDPorudzbine.Text = dr["IDPorudzbine"].ToString();
                txtRacun.Text = dr["Racun"].ToString();
                dtDatum.Text = dr["Datum"].ToString();
                cbxIDInstrumenta.Text = dr["IDInstrumenta"].ToString();


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
                    CommandText = "DELETE FROM [Porudzbina] WHERE IDPorudzbine = @IDPorudzbine"
                };
                command.Parameters.AddWithValue("@IDPorudzbine", txtIDPorudzbine.Text);
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
                CommandText = "UPDATE [Porudzbina] SET Racun = @Racun, Datum = @Datum, IDInstrumenta = @IDInstrumenta WHERE IDPorudzbine = @IDPorudzbine"
            };
            command.Parameters.AddWithValue("@IDPorudzbine", txtIDPorudzbine.Text);
            command.Parameters.AddWithValue("@Racun", txtRacun.Text);
            command.Parameters.AddWithValue("@Datum", dtDatum.SelectedDate);
            command.Parameters.AddWithValue("@IDInstrumenta", cbxIDInstrumenta.Text);

            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno promenjeni");
                binDataGrid();
            }
            ponistiUnosTxt();

        }

        private void cbxIDInstrumenta_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["opmo"].ConnectionString
            };
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Instrument] ORDER BY IDInstrumenta";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Porudzbina");
            dataAdapterCbx.Fill(dataTableCbx);
            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                cbxIDInstrumenta.Items.Add(dataTableCbx.Rows[i]["IDInstrumenta"]);
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}