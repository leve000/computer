using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace ComputerWpf
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OPRendszer_Lekérdezés()
        {
            string connectionString = "Server=localhost;Database=computer;Uid=root;Password=;SslMode=None";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $"SELECT * FROM `osystem`;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        sqlListBox.Items.Add(dr["id"].ToString() + ".: " + dr["name"].ToString() + ", " + dr["CreatedTime"].ToString());
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt: {ex.Message}");
                }
            }
        }

        private void Comp_Lekérdezés()
        {
            string connectionString = "Server=localhost;Database=computer;Uid=root;Password=;SslMode=None";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $"SELECT * FROM `comp`;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        sqlListBox.Items.Add(dr["id"].ToString() + ".: " + dr["Brand"].ToString() + ", " + dr["Type"].ToString() + ", " + dr["Display"].ToString() + ", " + dr["Memory"].ToString() + ", " + dr["CreatedTime"].ToString() + ", " + dr["OsId"].ToString());
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt: {ex.Message}");
                }
            }
        }

        private void Comp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Számítógépek");
            Comp_Lekérdezés();

        }

        private void OPS_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OPrendszerek");
            OPRendszer_Lekérdezés();
        }

        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}