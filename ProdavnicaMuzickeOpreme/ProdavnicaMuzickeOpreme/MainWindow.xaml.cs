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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Prodavac objProdavac = new Prodavac();
            this.Visibility = Visibility.Hidden;
            objProdavac.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Kupac objKupac = new Kupac();
            this.Visibility = Visibility.Hidden;
            objKupac.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dostavljac objDostavljac = new Dostavljac();
            this.Visibility = Visibility.Hidden;
            objDostavljac.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Instrument objInstrument = new Instrument();
            this.Visibility = Visibility.Hidden;
            objInstrument.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Porudzbina objPorudzbina = new Porudzbina();
            this.Visibility = Visibility.Hidden;
            objPorudzbina.Show();
        }
    }
}
