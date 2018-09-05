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
using RumahSakit.View;
using RumahSakit.Controller;

namespace RumahSakit
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

        LoginController lc = new LoginController();
        public static string tempUsername;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            tempUsername = txtUsername.Text;
            if (lc.cekLogin(txtUsername.Text, txtPassword.Password) == true)
            {
                MessageBox.Show("Login Berhasil", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                DasboardAdmin admin = new DasboardAdmin();
                admin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username and Password not match", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            
        }
    }
}
