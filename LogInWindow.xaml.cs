using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerWpf
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {

        public LogInWindow()
        {
            InitializeComponent();
            Reg_Felhasznalonev.Visibility = Visibility.Hidden;
            Reg_Felhasznalonev_tbox.Visibility = Visibility.Hidden;
            Reg_Jelszo_label.Visibility = Visibility.Hidden;
            Reg_Jelszo_tbox.Visibility = Visibility.Hidden;
            Jelszo_Ujra_label.Visibility = Visibility.Hidden;
            Jelszo_Ujra_tbox.Visibility = Visibility.Hidden;
            GoToLogIn.Visibility = Visibility.Hidden;
            Regisztracio.Visibility = Visibility.Hidden;
            Reg.Visibility = Visibility.Hidden;
            Title = "Bejelentkezés";
        }

        private void Belepes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Database=computer;Uid=root;Password=;SslMode=None";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var nev = Felhasznalonev_tbox.Text;
                    var jelszo = Jelszo_tbox.Password;
                    var sql = $"SELECT * FROM users WHERE UserName = '{nev}' AND Password = '{jelszo}'";
                    var lekerdezes = new MySqlCommand(sql, connection).ExecuteReader();
                    if (lekerdezes.Read())
                    {
                        MessageBox.Show("Sikeres bejelentkezés!");
                        MainWindow MainWindow = new MainWindow();
                        MainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sikertelen bejelentkezés!\nHibás felhasználónév vagy jelszó!");
                    }
                    lekerdezes.Close();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Database=computer;Uid=root;Password=;SslMode=None";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                if (Reg_Felhasznalonev_tbox.Text == "" || Reg_Jelszo_tbox.Password == "" || Jelszo_Ujra_tbox.Password == "")
                {
                    MessageBox.Show("Sikertelen regisztráció!\nA mezők nem lehetnek üresek!");
                    return;
                }

                else if (Reg_Jelszo_tbox.Password != Jelszo_Ujra_tbox.Password)
                {
                    MessageBox.Show("Sikertelen regisztráció!\nA két jelszó nem egyezik!");
                    Reg_Jelszo_tbox.Password = "";
                    Jelszo_Ujra_tbox.Password = "";
                    return;
                }

                var sql = $"SELECT * FROM users WHERE UserName = '{Reg_Felhasznalonev_tbox.Text}'";
                var lekerdezes = new MySqlCommand(sql, connection).ExecuteReader();
                if (lekerdezes.Read())
                {
                    MessageBox.Show("Sikertelen regisztráció!\nA felhasználó már létezik! Jelentkezzen be!");
                    Reg_Felhasznalonev.Visibility = Visibility.Hidden;
                    Reg_Felhasznalonev_tbox.Visibility = Visibility.Hidden;
                    Reg_Jelszo_label.Visibility = Visibility.Hidden;
                    Reg_Jelszo_tbox.Visibility = Visibility.Hidden;
                    Jelszo_Ujra_label.Visibility = Visibility.Hidden;
                    Jelszo_Ujra_tbox.Visibility = Visibility.Hidden;
                    GoToLogIn.Visibility = Visibility.Hidden;
                    Regisztracio.Visibility = Visibility.Hidden;
                    Reg.Visibility = Visibility.Hidden;

                    Felhasznalonev.Visibility = Visibility.Visible;
                    Felhasznalonev_tbox.Visibility = Visibility.Visible;
                    Jelszo_label.Visibility = Visibility.Visible;
                    Jelszo_tbox.Visibility = Visibility.Visible;
                    GoToReg.Visibility = Visibility.Visible;
                    Belepes.Visibility = Visibility.Visible;
                    Log.Visibility = Visibility.Visible;
                    Title = "Bejelentkezés";
                    connection.Close();
                    return;
                }
                lekerdezes.Close();

                sql = $"INSERT INTO users (UserName,Password) VALUES ('{Reg_Felhasznalonev_tbox.Text}','{Reg_Jelszo_tbox.Password}')";
                var beszuras = new MySqlCommand(sql, connection).ExecuteNonQuery();
                if (beszuras == 1)
                {
                    MessageBox.Show("Sikeres regisztráció!");
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sikertelen regisztráció!");
                }
                connection.Close();
            }
        }

        private void ToReg(object sender, MouseButtonEventArgs e)
        {
          
            Reg_Felhasznalonev.Visibility = Visibility.Visible;
            Reg_Felhasznalonev_tbox.Visibility = Visibility.Visible;
            Reg_Jelszo_label.Visibility = Visibility.Visible;
            Reg_Jelszo_tbox.Visibility = Visibility.Visible;
            Jelszo_Ujra_label.Visibility = Visibility.Visible;
            Jelszo_Ujra_tbox.Visibility = Visibility.Visible;
            GoToLogIn.Visibility = Visibility.Visible;
            Regisztracio.Visibility = Visibility.Visible;
            Reg.Visibility = Visibility.Visible;

            Felhasznalonev.Visibility = Visibility.Hidden;
            Felhasznalonev_tbox.Visibility = Visibility.Hidden;
            Jelszo_label.Visibility = Visibility.Hidden;
            Jelszo_tbox.Visibility = Visibility.Hidden;
            GoToReg.Visibility = Visibility.Hidden;
            Belepes.Visibility = Visibility.Hidden;
            Log.Visibility = Visibility.Hidden;
            Title = "Regisztráció";
        }

        private void ToLogIn(object sender, MouseButtonEventArgs e)
        {
            Reg_Felhasznalonev.Visibility = Visibility.Hidden;
            Reg_Felhasznalonev_tbox.Visibility = Visibility.Hidden;
            Reg_Jelszo_label.Visibility = Visibility.Hidden;
            Reg_Jelszo_tbox.Visibility = Visibility.Hidden;
            Jelszo_Ujra_label.Visibility = Visibility.Hidden;
            Jelszo_Ujra_tbox.Visibility = Visibility.Hidden;
            GoToLogIn.Visibility = Visibility.Hidden;
            Regisztracio.Visibility = Visibility.Hidden;
            Reg.Visibility = Visibility.Hidden;

            Felhasznalonev.Visibility = Visibility.Visible;
            Felhasznalonev_tbox.Visibility = Visibility.Visible;
            Jelszo_label.Visibility = Visibility.Visible;
            Jelszo_tbox.Visibility = Visibility.Visible;
            GoToReg.Visibility = Visibility.Visible;
            Belepes.Visibility = Visibility.Visible;
            Log.Visibility = Visibility.Visible;
            Title = "Bejelentkezés";
        }

        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}