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
using System.Windows.Shapes;
using RumahSakit.Controller;
using RumahSakit.Model;
using System.Data;

namespace RumahSakit.View
{
    /// <summary>
    /// Interaction logic for DasboardAdmin.xaml
    /// </summary>
    public partial class DasboardAdmin : Window
    {
        DB_RS_SINGGIHEntities1 et = new DB_RS_SINGGIHEntities1();
        public static DataGrid viewpasien;

        public DasboardAdmin()
        {
            InitializeComponent();
            
        }

        //PasienController ps = new PasienController();

        //windows loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewPasien(dgPasien);
        }

        //Pasien
        public string getJenisKelaminP()
        {
            string jk = "";
            if (rbLaki.IsChecked==true)
            {
                jk = "Laki-laki";
            }
            else if (rbPerempuan.IsChecked==true)
            {
                jk = "Perempuan";
            }
            return jk;
            
        }

        //clear textbox
        private void clearText()
        {
            txtNamaP.Clear();
            txtAlamatP.Clear();
            txtTelpP.Clear();
            txtUmurP.Clear();
            rbLaki.IsChecked=false;
            rbPerempuan.IsChecked = false;
            dtTanggalLahirP.Text = DateTime.Today.ToString();
        }

        //Tampil Data Pasien
        private void viewPasien(DataGrid DG)
        {
            
            DG.ItemsSource = et.PATIENTs.OrderBy(p => p.PATIENT_ID).ToList();
        }

        //Insert Data Pasien
        private void btnTambah_Click(object sender, RoutedEventArgs e)
        {
            
            PATIENT pasien = new PATIENT()
            {
                NAME = txtNamaP.Text,
                BIRTH = Convert.ToDateTime(dtTanggalLahirP.Text),
                ADDRESS = txtAlamatP.Text,
                PHONE = txtTelpP.Text,
                AGE = Convert.ToInt32(txtUmurP.Text),
                GENDER = getJenisKelaminP()
            };

            try
            {
                et.PATIENTs.Add(pasien);
                et.SaveChanges();
                clearText();
                this.viewPasien(dgPasien);
                MessageBox.Show("Tambah Data Pasien Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                
            }
        }

        //get Data dari kolom
        private void dgPasien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*DataGrid dg = (DataGrid)sender;
            DataRowView dipilih = dg.SelectedItem as DataRowView;

            if (dipilih != null)
            {
                txtNamaP.Text = dipilih["Nama"].ToString();
                dtTanggalLahirP.Text = dipilih["Tanggal Lahir"].ToString();
                txtAlamatP.Text = dipilih["Alamat"].ToString();
                txtTelpP.Text = dipilih["No Telp"].ToString();
                txtUmurP.Text = dipilih["Umur"].ToString();
                if (dipilih["Jenis Kelamin"].ToString() == "Laki-laki")
                {
                    rbLaki.IsChecked = true;
                }
                else if (dipilih["GENDER"].ToString() == "Perempuan")
                {
                    rbPerempuan.IsChecked = true;
                }
            }*/
            try
            {
                object item = dgPasien.SelectedItem;
                string Nama = (dgPasien.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                txtNamaP.Text = Nama;
                string TanggalLahir = (dgPasien.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                dtTanggalLahirP.Text = TanggalLahir;
                string Alamat = (dgPasien.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                txtAlamatP.Text = Alamat;
                string Telp = (dgPasien.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                txtTelpP.Text = Telp;
                string Umur = (dgPasien.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                txtUmurP.Text = Umur;
                string JenisKelamin = (dgPasien.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;

                if (JenisKelamin == "Laki-laki")
                {
                    rbLaki.IsChecked = true;
                }
                else if (JenisKelamin == "Perempuan")
                {
                    rbPerempuan.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private PATIENT SearchById(int id)
        {
            
            var pasien = et.PATIENTs.Find(id);
            if (pasien == null)
            {
                MessageBox.Show("ID Pasien " + id + " tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return pasien;
        }

        //Update Pasien
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            object item = dgPasien.SelectedItem;
            string temp_id = (dgPasien.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);

            PATIENT pasien = SearchById(id);
            pasien.NAME = txtNamaP.Text;
            pasien.ADDRESS = txtAlamatP.Text;
            pasien.BIRTH = Convert.ToDateTime(dtTanggalLahirP.Text);
            pasien.AGE = Convert.ToInt32(txtUmurP.Text);
            pasien.GENDER = getJenisKelaminP();

            et.Entry(pasien).State = System.Data.Entity.EntityState.Modified;
            et.SaveChanges();
            clearText();
            this.viewPasien(dgPasien);
            MessageBox.Show("Update Data Pasien Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Delete Pasien
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgPasien.SelectedItem;
            string temp_id = (dgPasien.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);

            PATIENT pasien = SearchById(id);
            et.Entry(pasien).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            clearText();
            this.viewPasien(dgPasien);
            MessageBox.Show("Data Pasien Berhasil di hapus !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            

        }

        
    }


}
