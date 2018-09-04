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
        DB_RS_SINGGIHEntities2 et = new DB_RS_SINGGIHEntities2();
        public static DataGrid viewpasien;

        public DasboardAdmin()
        {
            InitializeComponent();
            
        }

        //PasienController ps = new PasienController();
        ComboBox poli = new ComboBox();

        //windows loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewPasien(dgPasien);
            ViewPerawat(dgPerawat);
            ViewObat(dgObat);
            viewTypeObat();
            viewSpecialis();
            viewTypePoly();
            viewPoly();
            ViewRuangan(dgRuangan);
            viewTypeRuangan();
            //cmbSpecialis.Visibility = Visibility.Hidden;
            //cmbTypePoli.Visibility = Visibility.Hidden;
            //cmbTypePoli.IsEnabled = false;
            //cmbSpecialis.IsEnabled = false;
            viewObatTransaksi();
            viewPerawatTransaksi();
            viewDokterTransaksi();
            viewJenisPolyTransaksi();
            viewNamaRuanganTransaksi();
            viewJenisRuanganTransaksi();
            viewJenisPolyTransaksi();
            viewTypePolyTransaksi();
            dtTanggalTransaksi.Text = DateTime.Today.ToString();
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

        //Dokter
        public string getJenisKelaminDokter()
        {
            string jk = "";
            if (rbLakiDok.IsChecked == true)
            {
                jk = "Laki-laki";
            } else if (rbPerempuanDok.IsChecked == true)
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

        //clear text perawat
        private void clearTextPerawat()
        {
            txtNamaPer.Clear();
            txtAlamatPer.Clear();
            txtTelpPer.Clear();
            txtEmailPer.Clear();
            rbLakiN.IsChecked = false;
            rbPerempuanN.IsChecked = false;
        }

        //clear text obat
        private void clearTextObat()
        {
            txtNamaObat.Clear();
            txtHargaObat.Clear();
            txtStockObat.Clear();
            cmbTipeObat.SelectedIndex = -1;
            dtObat.Text = DateTime.Today.ToString();
        }

        //clear text dokter
        private void clearTextDokter()
        {
            txtNamaDok.Clear();
            txtAlamatDok.Clear();
            txtNoTelpDok.Clear();
            txtEmailDok.Clear();
            cmbSpecialis.SelectedIndex = -1;
            cmbPoli.SelectedIndex = -1;
            cmbTypePoli.SelectedIndex = -1;
            rbLakiDok.IsChecked = false;
            rbPerempuanDok.IsChecked = false;
            
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
                GENDER = getJenisKelaminPasien(),
                STATUS = 0
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
                string Telp = (dgPerawat.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                txtTelpPer.Text = Telp;
                string Alamat = (dgPerawat.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
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


        //=====================================================Obat=================================================================
        
         
        private void ViewObat(DataGrid DG)
        {
            var medicine = from m in et.MEDICINEs.ToList() join t in et.TYPE_MEDICINE.ToList()
                           on m.TYPE_MEDICINE_ID equals t.TYPE_MEDICINE_ID
                           select m;
            DG.ItemsSource = medicine.ToList();
        }

        private void btnTambahObat_Click(object sender, RoutedEventArgs e)
        {
            
            MEDICINE obat = new MEDICINE()
            {
                NAME = txtNamaObat.Text,
                PRICE = Convert.ToDouble(txtHargaObat.Text),
                STOCK = Convert.ToInt32(txtStockObat.Text),
                EXP = Convert.ToDateTime(dtObat.Text),
                TYPE_MEDICINE_ID = Convert.ToInt32(cmbTipeObat.SelectedValue)
                
                
            };

            try
            {
                et.MEDICINEs.Add(obat);
                et.SaveChanges();
                clearTextObat();
                this.ViewObat(dgObat);
                MessageBox.Show("Tambah Data Obat Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

            }
        }

        //get value typeObat to Combobox
        private void viewTypeObat()
        {
            TYPE_MEDICINE type_obat = new TYPE_MEDICINE();
            cmbTipeObat.DisplayMemberPath = "NAME";
            cmbTipeObat.SelectedValuePath = "TYPE_MEDICINE_ID";

            cmbTipeObat.ItemsSource = et.TYPE_MEDICINE.ToList();
        }

        private void dgObat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = dgObat.SelectedItem;

                string Nama = (dgObat.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                txtNamaObat.Text = Nama;
                string Tipe = (dgObat.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                cmbTipeObat.Text = Tipe;
                string Harga = (dgObat.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                txtHargaObat.Text = Harga;
                string Expired = (dgObat.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                dtObat.Text = Expired;
                string Stock = (dgObat.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                txtStockObat.Text = Stock;
                
            }
            catch (Exception ex)
            {

            }
        }

        private MEDICINE SearchByIdObat(int id)
        {
            var obat = et.MEDICINEs.Find(id);
            if (obat == null)
            {
                MessageBox.Show("ID Obat " + id + " tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return obat;
        }

        private void btnEditObat_Click(object sender, RoutedEventArgs e)
        {
            object item = dgObat.SelectedItem;
            string temp_id = (dgObat.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

            int id = Convert.ToInt32(temp_id);
            MEDICINE obat = SearchByIdObat(id);
            obat.NAME = txtNamaObat.Text;
            obat.TYPE_MEDICINE_ID = Convert.ToInt32(cmbTipeObat.SelectedValue);
            obat.EXP = Convert.ToDateTime(dtObat.Text);
            obat.STOCK = Convert.ToInt32(txtStockObat.Text);
            obat.PRICE = Convert.ToDouble(txtHargaObat.Text);

            et.Entry(obat).State = System.Data.Entity.EntityState.Modified;
            et.SaveChanges();
            clearTextObat();
            this.ViewObat(dgObat);
            MessageBox.Show("Update Data Obat Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnDeleteObat_Click(object sender, RoutedEventArgs e)
        {
            object item = dgObat.SelectedItem;
            string temp_id = (dgObat.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(temp_id);
            MEDICINE obat = SearchByIdObat(id);

            et.Entry(obat).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            clearTextObat();
            this.ViewObat(dgObat);
            MessageBox.Show("Delete Data Obat Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //===========================================Dokter====================================================================

        private void ViewDokterSpecialis(DataGrid DG)
        {
            var dokter = from d in et.DOCTORs.ToList() join p in et.POLies.ToList()
                         on d.POLY_ID equals p.POLY_ID join s in et.SPECIALISTs.ToList()
                         on d.SPECIALIST_ID equals s.SPECIALIST_ID
                         select d;

            DG.ItemsSource = dokter.ToList();
        }

        private void ViewDokterNonSpecialis(DataGrid DG)
        {
            var dokter = from d in et.DOCTORs.ToList()
                         join p in et.POLies.ToList()
                         on d.POLY_ID equals p.POLY_ID where p.NAME == "Umum"
                         select d;

            DG.ItemsSource = dokter.ToList();
        }

        private void ViewDokter(DataGrid DG)
        {
            
           /* var dokter = from d in et.DOCTORs.ToList()
                         join s in et.SPECIALISTs.ToList()
                         on d.SPECIALIST_ID equals s.SPECIALIST_ID
                         join p in et.POLies.ToList()
                         on d.POLY_ID equals p.POLY_ID
                         select new
                         {
                             
                             d.DOCTOR_ID,
                             d.NAME,
                             d.PHONE,
                             d.EMAIL,
                             d.ADDRESS,
                             d.GENDER,
                             p.NAME,
                             s.NAME
                         };


            DG.ItemsSource = dokter.ToList();*/
        }

        private void btnTambahDok_Click(object sender, RoutedEventArgs e)
        {

            if (cmbPoli.Text == "Khusus")
            {
                DOCTOR dokterkhusus = new DOCTOR()
                {
                    NAME = txtNamaDok.Text,
                    EMAIL = txtEmailDok.Text,
                    ADDRESS = txtAlamatDok.Text,
                    PHONE = txtNoTelpDok.Text,
                    GENDER = getJenisKelaminDokter(),
                    SPECIALIST_ID = Convert.ToInt32(cmbSpecialis.SelectedValue),
                    POLY_ID = Convert.ToInt32(cmbPoli.SelectedValue)
                };
                et.DOCTORs.Add(dokterkhusus);
                et.SaveChanges();
                clearTextDokter();
                dgDokter.UnselectAllCells();
                this.ViewDokterSpecialis(dgDokter);
                MessageBox.Show("Tambah Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (cmbPoli.Text == "Umum")
            {
                DOCTOR dokterumum = new DOCTOR()
                {
                    NAME = txtNamaDok.Text,
                    EMAIL = txtEmailDok.Text,
                    ADDRESS = txtAlamatDok.Text,
                    PHONE = txtNoTelpDok.Text,
                    GENDER = getJenisKelaminDokter(),
                    POLY_ID = Convert.ToInt32(cmbPoli.SelectedValue)
                };
                et.DOCTORs.Add(dokterumum);
                et.SaveChanges();
                clearTextDokter();
                dgDokter.UnselectAllCells();
                this.ViewDokterNonSpecialis(dgDokter);
                MessageBox.Show("Tambah Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        //get combobox specialis
        private void viewSpecialis()
        {
            cmbSpecialis.DisplayMemberPath = "NAME";
            cmbSpecialis.SelectedValuePath = "SPECIALIST_ID";

            cmbSpecialis.ItemsSource = et.SPECIALISTs.ToList();
        }

        //get combobox poly
        private void viewPoly()
        {
            cmbPoli.DisplayMemberPath = "NAME";
            cmbPoli.SelectedValuePath = "POLY_ID";

            cmbPoli.ItemsSource = et.POLies.ToList();
        }

        //get all combobox tipe poli
        private void viewTypePoly()
        {
            cmbTypePoli.DisplayMemberPath = "NAME";
            cmbTypePoli.SelectedValuePath = "TYPE_POLY_ID";

            cmbTypePoli.ItemsSource = et.TYPE_POLY.ToList();
        }

        //get combobox type poly umum
        private void viewTypePolyUmum()
        {
            cmbTypePoli.DisplayMemberPath = "NAME";
            cmbTypePoli.SelectedValuePath = "TYPE_POLY_ID";

            cmbTypePoli.ItemsSource = et.TYPE_POLY.Where(tp => tp.POLY_ID == 1005).ToList();
        }

        //get combobox type poly khusus
        private void viewTypePolyKhusus()
        {
            cmbTypePoli.DisplayMemberPath = "NAME";
            cmbTypePoli.SelectedValuePath = "TYPE_POLY_ID";

            cmbTypePoli.ItemsSource = et.TYPE_POLY.Where(tp => tp.POLY_ID == 1006).ToList();
        }

        private DOCTOR SearchByIdDokter(int id)
        {
            var dokter = et.DOCTORs.Find(id);
            if (dokter == null)
            {
                MessageBox.Show("ID Dokter " + id + " tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return dokter;
        }

        private void btnEditDok_Click(object sender, RoutedEventArgs e)
        {
            object item = dgDokter.SelectedItem;
            string temp_id = (dgDokter.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(temp_id);
            
            DOCTOR dokter = SearchByIdDokter(id);

            if (cmbPoli.Text == "Khusus")
            {
                dokter.NAME = txtNamaDok.Text;
                dokter.EMAIL = txtEmailDok.Text;
                dokter.ADDRESS = txtAlamatDok.Text;
                dokter.PHONE = txtNoTelpDok.Text;
                dokter.SPECIALIST_ID = Convert.ToInt32(cmbSpecialis.SelectedValue);
                dokter.POLY_ID = Convert.ToInt32(cmbPoli.SelectedValue);
                dokter.GENDER = getJenisKelaminDokter();
                et.Entry(dokter).State = System.Data.Entity.EntityState.Modified;
                et.SaveChanges();
                clearTextDokter();
                this.ViewDokterSpecialis(dgDokter);
                MessageBox.Show("Update Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else if (cmbPoli.Text == "Umum")
            {
                dokter.NAME = txtNamaDok.Text;
                dokter.EMAIL = txtEmailDok.Text;
                dokter.ADDRESS = txtAlamatDok.Text;
                dokter.PHONE = txtNoTelpDok.Text;
                dokter.POLY_ID = Convert.ToInt32(cmbPoli.SelectedValue);
                dokter.GENDER = getJenisKelaminDokter();
                et.Entry(dokter).State = System.Data.Entity.EntityState.Modified;
                et.SaveChanges();
                clearTextDokter();
                this.ViewDokterNonSpecialis(dgDokter);
                MessageBox.Show("Update Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            
        }

        private void dgDokter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgDokter.SelectedItem;

            try
            {
                string Nama = (dgDokter.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                txtNamaDok.Text = Nama;
                string Email = (dgDokter.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                txtEmailDok.Text = Email;
                string Alamat = (dgDokter.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                txtAlamatDok.Text = Alamat;
                string Telp = (dgDokter.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                txtNoTelpDok.Text = Telp;
                string Specialis = (dgDokter.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                cmbSpecialis.Text = Specialis;
                string Poly = (dgDokter.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                cmbPoli.Text = Poly;
                string JenisKelamin = (dgDokter.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;

                if (JenisKelamin == "Laki-laki")
                {
                    rbLakiDok.IsChecked = true;
                }
                else if (JenisKelamin == "Perempuan")
                {
                    rbPerempuanDok.IsChecked = true;
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void cmbTypePoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmbPoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            /*int umum = 1005;
            int khusus = 1006;

            if (cmbPoli.SelectedItem.ToString() == umum.ToString())
            {
                cmbTypePoli.IsEnabled = true;
                cmbSpecialis.IsEnabled = false;

                viewTypePolyUmum();
                clearTextDokter();
                this.ViewDokter(dgDokter);
                MessageBox.Show("Tambah Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                //cmbTypePoli.ItemsSource = et.TYPE_POLY.Where(tp =>tp.POLY_ID == umum).ToList();
            }
            else if (cmbPoli.SelectedValue.ToString() == khusus.ToString())
            {
                cmbTypePoli.IsEnabled = true;
                cmbSpecialis.IsEnabled = true;

                viewTypePolyKhusus();
                clearTextDokter();
                this.ViewDokter(dgDokter);
                MessageBox.Show("Tambah Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
                //cmbTypePoli.ItemsSource = et.TYPE_POLY.Where(tp => tp.POLY_ID == khusus).ToList();
            }*/

            
        }

        private void btnDeleteDok_Click(object sender, RoutedEventArgs e)
        {
            object item = dgDokter.SelectedItem;

            string temp_id = (dgDokter.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(temp_id);

            DOCTOR dokter = SearchByIdDokter(id);

            et.Entry(dokter).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            if (cmbPoli.Text == "Umum")
            {
                clearTextDokter();
                this.ViewDokterNonSpecialis(dgDokter);
                MessageBox.Show("Delete Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (cmbPoli.Text == "Khusus")
            {
                clearTextDokter();
                this.ViewDokterSpecialis(dgDokter);
                MessageBox.Show("Delete Data Dokter Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void rbSpecialis_Click(object sender, RoutedEventArgs e)
        {
            rbSpecialis.IsChecked = true;
            clearTextDokter();
            ViewDokterSpecialis(dgDokter);
            cmbSpecialis.IsEnabled = true;
        }

        private void rbNonSpecialis_Click(object sender, RoutedEventArgs e)
        {
            rbNonSpecialis.IsChecked = true;
            clearTextDokter();
            ViewDokterNonSpecialis(dgDokter);
            cmbSpecialis.IsEnabled = false;
            
            
        }

        //=======================================================Ruangan======================================================================

        private void ViewRuangan(DataGrid DG)
        {
            var ruangan = from r in et.ROOMs.ToList()
                          join tr in et.TYPE_ROOM.ToList()
                          on r.ROOM_ID equals tr.TYPE_ROOM_ID
                          select r;
            DG.ItemsSource = ruangan.ToList();
        
        }

        private void clearTextRuangan()
        {
            txtNamaRuangan.Clear();
            txtKapasitasRuangan.Clear();
            cmbJenisRuangan.SelectedIndex = -1;
        }

        private void viewTypeRuangan()
        {
            cmbJenisRuangan.DisplayMemberPath = "NAME";
            cmbJenisRuangan.SelectedValuePath = "TYPE_ROOM_ID";

            cmbJenisRuangan.ItemsSource = et.TYPE_ROOM.ToList();
        }

        private void btnTambahRuangan_Click(object sender, RoutedEventArgs e)
        {
            ROOM ruangan = new ROOM() {
                NAME = txtNamaRuangan.Text,
                CAPACITY = Convert.ToInt32(txtKapasitasRuangan.Text),
                TYPE_ROOM_ID = Convert.ToInt32(cmbJenisRuangan.SelectedValue)
            };

            et.ROOMs.Add(ruangan);
            et.SaveChanges();
            clearTextRuangan();
            this.ViewRuangan(dgRuangan);
            MessageBox.Show("Tambah Data Ruangan Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void dgRuangan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgRuangan.SelectedItem;

            try
            {
                string Nama = (dgRuangan.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                txtNamaRuangan.Text = Nama;
                string TipeRuangan = (dgRuangan.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                cmbJenisRuangan.Text = TipeRuangan;
                string Kapasitas = (dgRuangan.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                txtKapasitasRuangan.Text = Kapasitas;

            }
            catch (Exception ex)
            {

            }

        }

        private ROOM SearchByIdRoom(int id)
        {
            var ruangan = et.ROOMs.Find(id);
            if (ruangan == null)
            {
                MessageBox.Show("ID Ruangan " + id + " tidak ditemukan", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return ruangan;
        }

        private void btnEditRuangan_Click(object sender, RoutedEventArgs e)
        {
            object item = dgRuangan.SelectedItem;

            string temp_id = (dgRuangan.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(temp_id);

            ROOM ruangan = SearchByIdRoom(id);
            ruangan.NAME = txtNamaRuangan.Text;
            ruangan.CAPACITY = Convert.ToInt32(txtKapasitasRuangan.Text);
            ruangan.TYPE_ROOM_ID = Convert.ToInt32(cmbJenisRuangan.SelectedValue);

            et.Entry(ruangan).State = System.Data.Entity.EntityState.Modified;
            et.SaveChanges();
            clearTextRuangan();
            this.ViewRuangan(dgRuangan);
            MessageBox.Show("Update Data Ruangan Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDeleteRuangan_Click(object sender, RoutedEventArgs e)
        {
            object item = dgRuangan.SelectedItem;

            string temp_id = (dgRuangan.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(temp_id);

            ROOM ruangan = SearchByIdRoom(id);

            et.Entry(ruangan).State = System.Data.Entity.EntityState.Deleted;
            et.SaveChanges();
            clearTextRuangan();
            this.ViewRuangan(dgRuangan);
            MessageBox.Show("Update Data Ruangan Berhasil !", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            dgRuangan.UnselectAllCells();
        }

        //========================================================Transaksi================================================================

        private void viewBlmTransaksiPasien(DataGrid DG)
        {
            var transaksi = from p in et.PATIENTs.ToList()
                            where p.STATUS == 0
                            select p;

            DG.ItemsSource = transaksi.ToList();
        }

        private void viewSdhTransaksiPasien(DataGrid DG)
        {
            var transaksi = from p in et.PATIENTs.ToList()
                            join t in et.TRANSACTIONs.ToList()
                            on p.PATIENT_ID equals t.PATIENT_ID
                            where p.STATUS == 1
                            select p;

            DG.ItemsSource = transaksi.ToList();
        }

        private void rbBelumTransaksi_Click(object sender, RoutedEventArgs e)
        {
            viewBlmTransaksiPasien(dgTransaksiPasien);
        }

        private void rbSudahTransaksi_Click(object sender, RoutedEventArgs e)
        {
            viewSdhTransaksiPasien(dgTransaksiPasien);
        }

        //get combobox transaksi 
        private void viewTypePolyTransaksi()
        {
            cmbTipePoliTransaksi.DisplayMemberPath = "NAME";
            cmbTipePoliTransaksi.SelectedValuePath = "TYPE_POLY_ID";

            cmbTipePoliTransaksi.ItemsSource = et.TYPE_POLY.ToList();
        }

        private void viewJenisPolyTransaksi()
        {
            cmbJenisPoliTransaksi.DisplayMemberPath = "NAME";
            cmbJenisPoliTransaksi.SelectedValuePath = "POLY_ID";

            cmbJenisPoliTransaksi.ItemsSource = et.POLies.ToList();
        }

        private void viewDokterTransaksi()
        {
            cmbNamaDokterTransaksi.DisplayMemberPath = "NAME";
            cmbNamaDokterTransaksi.SelectedValuePath = "DOCTOR_ID";

            cmbNamaDokterTransaksi.ItemsSource = et.DOCTORs.ToList();
        }

        private void viewPerawatTransaksi()
        {
            cmbNamaPerawatTransaksi.DisplayMemberPath = "NAME";
            cmbNamaPerawatTransaksi.SelectedValuePath = "NURSE_ID";

            cmbNamaPerawatTransaksi.ItemsSource = et.NURSEs.ToList();
        }

        private void viewNamaRuanganTransaksi()
        {
            cmbNamaRuanganTransaksi.DisplayMemberPath = "NAME";
            cmbNamaRuanganTransaksi.SelectedValuePath = "ROOM_ID";

            cmbNamaRuanganTransaksi.ItemsSource = et.ROOMs.ToList();
        }

        private void viewJenisRuanganTransaksi()
        {
            cmbJenisRuanganTransaksi.DisplayMemberPath = "NAME";
            cmbJenisRuanganTransaksi.SelectedValuePath = "TYPE_ROOM_ID";

            cmbJenisRuanganTransaksi.ItemsSource = et.TYPE_ROOM.ToList();
        }

        private void viewObatTransaksi()
        {
            cmbNamaObatTransaksi.DisplayMemberPath = "NAME";
            cmbNamaObatTransaksi.SelectedValuePath = "MEDICINE_ID";

            cmbNamaObatTransaksi.ItemsSource = et.MEDICINEs.ToList();
        }
    }
}
