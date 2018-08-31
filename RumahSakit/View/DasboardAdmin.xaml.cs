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
using System.Data.Entity.Validation;

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
            ViewPerawat(dgPerawat);
        }

        //Pasien
        public string getJenisKelaminPasien()
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

        //Perawat
        public string getJenisKelaminPerawat()
        {
            string jk = "";
            if (rbLakiN.IsChecked == true)
            {
                jk = "Laki-laki";
            }
            else if (rbPerempuanN.IsChecked == true)
            {
                jk = "Perempuan";
            }
            return jk;

        }

        //clear text pasien
        private void clearTextPasien()
        {
            txtNamaP.Clear();
            txtAlamatP.Clear();
            txtTelpP.Clear();
            txtUmurP.Clear();
            rbLaki.IsChecked=false;
            rbPerempuan.IsChecked = false;
            dtTanggalLahirP.Text = DateTime.Today.ToString();
        }

        //
        private void clearTextPerawat()
        {
            txtNamaPer.Clear();
            txtAlamatPer.Clear();
            txtTelpPer.Clear();
            txtEmailPer.Clear();
            rbLakiN.IsChecked = false;
            rbPerempuanN.IsChecked = false;

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
                GENDER = getJenisKelaminPasien()
            };

            try
            {
                et.PATIENTs.Add(pasien);
                et.SaveChanges();
                clearTextPasien();
                this.viewPasien(dgPasien);
                MessageBox.Show("Tambah Data Pasien Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                
            }
        }

        //get Data dari kolom Pasien
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

        private PATIENT SearchByIdPasien(int id)
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

            PATIENT pasien = SearchByIdPasien(id);
            pasien.NAME = txtNamaP.Text;
            pasien.ADDRESS = txtAlamatP.Text;
            pasien.BIRTH = Convert.ToDateTime(dtTanggalLahirP.Text);
            pasien.AGE = Convert.ToInt32(txtUmurP.Text);
            pasien.GENDER = getJenisKelaminPasien();

            et.Entry(pasien).State = System.Data.Entity.EntityState.Modified;
            et.SaveChanges();
            clearTextPasien();
            this.viewPasien(dgPasien);
            MessageBox.Show("Update Data Pasien Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Delete Pasien
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            object item = dgPasien.SelectedItem;
            string temp_id = (dgPasien.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);

            PATIENT pasien = SearchByIdPasien(id);
            et.Entry(pasien).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            clearTextPasien();
            this.viewPasien(dgPasien);
            MessageBox.Show("Data Pasien Berhasil di hapus !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            

        }


        //================================================================Nurse===============================================================



        private void ViewPerawat(DataGrid DG)
        {
            DG.ItemsSource = et.NURSEs.OrderBy(p => p.NURSE_ID).ToList();
        }

        
        private void btnTambahN_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private NURSE SearchByIdPerawat(int id)
        {
            var perawat = et.NURSEs.Find(id);
            if (perawat == null)
            {
                MessageBox.Show("ID Pasien " + id + " tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return perawat;
        }

        // Tambah Perawat
        private void btnTambahN_Click_1(object sender, RoutedEventArgs e)
        {
            NURSE perawat = new NURSE()
            {
                NAME = txtNamaPer.Text,
                ADDRESS = txtAlamatPer.Text,
                EMAIL = txtEmailPer.Text,
                GENDER = getJenisKelaminPerawat(),
                PHONE = txtTelpPer.Text
            };
            try
            {
                et.NURSEs.Add(perawat);
                et.SaveChanges();
                clearTextPerawat();
                this.ViewPerawat(dgPerawat);
                MessageBox.Show("Tambah Data Perawat Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

            }
        }

        //get kolom perawat
        private void dgPerawat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = dgPerawat.SelectedItem;
               
                string Nama = (dgPerawat.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                txtNamaPer.Text = Nama;
                string Email = (dgPerawat.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                txtEmailPer.Text = Email;
                string Telp = (dgPerawat.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                txtTelpPer.Text = Telp;
                string Alamat = (dgPerawat.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                txtAlamatPer.Text = Alamat;
                string JenisKelamin = (dgPerawat.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;

                if (JenisKelamin == "Laki-laki")
                {
                    rbLakiN.IsChecked = true;
                }
                else if (JenisKelamin == "Perempuan")
                {
                    rbPerempuanN.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        //update data perawat
        private void btnEditN_Click(object sender, RoutedEventArgs e)
        {
            object item = dgPerawat.SelectedItem;
            string temp_id = (dgPerawat.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);

            NURSE perawat = SearchByIdPerawat(id);
            perawat.NAME = txtNamaPer.Text;
            perawat.PHONE = txtTelpPer.Text;
            perawat.EMAIL = txtEmailPer.Text;
            perawat.ADDRESS = txtAlamatPer.Text;
            perawat.GENDER = getJenisKelaminPerawat();

            try
            {
                et.Entry(perawat).State = System.Data.Entity.EntityState.Modified;
                et.SaveChanges();
                clearTextPerawat();
                this.ViewPerawat(dgPerawat);
                MessageBox.Show("Update Data Perawat Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //delete data perawat
        private void btnDeleteN_Click(object sender, RoutedEventArgs e)
        {
            object item = dgPerawat.SelectedItem;
            string temp_id = (dgPerawat.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);
            NURSE perawat = SearchByIdPerawat(id);
            et.Entry(perawat).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            clearTextPerawat();
            this.ViewPerawat(dgPerawat);
            MessageBox.Show("Data Perawat Berhasil di hapus !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


}
