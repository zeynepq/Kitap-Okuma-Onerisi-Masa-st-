using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitapOkumaveOnerisiUygulamasi
{
    public partial class Kitaplar : Form
    {
        public Kitaplar()
        {
            InitializeComponent();
            KitaplarıListele();
            EniyiKitapListele();
            PopülerKitapListele();
            yeniKitapListele();
        }

        public string baglanti = "Server=localhost;Database=booksdatabase;port=3306;persistsecurityinfo=True;SslMode=none;Uid=root;Pwd='1234';";

        private void grdKitapListele_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KitapOkuma kitapOku = new KitapOkuma();
            kitapOku.Show();
            this.Hide();
        }

        private void grdEniyiKitaplar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KitapOkuma kitapOku = new KitapOkuma();
            kitapOku.Show();
            this.Hide();
        }

        private void grdPopülerKitaplar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KitapOkuma kitapOku = new KitapOkuma();
            kitapOku.Show();
            this.Hide();
        }

        private void KitaplarıListele()
        {
            /*DataGridViewImageColumn Resim = new DataGridViewImageColumn();
            grdKitapListeleme.Columns.Add(Resim);
            Resim.HeaderText = "Resim";
            */

            MySqlConnection kitapBaglanti = new MySqlConnection(baglanti);
            DataTable dt = new DataTable();
            string kitapadi = "select  `Image-URL-S`,booktitle,bookauthor,ısbn from booksdatabase.tbl_bxbooks";
            kitapBaglanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(kitapadi, kitapBaglanti);
            da.Fill(dt);
            grdKitapListele.DataSource = dt;

            kitapBaglanti.Close();
        }

        private void EniyiKitapListele()
        {
            /*Tüm kitapları isbn kodlarına göre gruplanır. Kitapların ortalama puan değerleri bulunur. 
             * Kitapların ortalama değerleri büyükten küçüğe sıralanarak en iyi 10 kitabın listelenmesi sağlanır
             */

            MySqlConnection eniyikitapbaglanti = new MySqlConnection(baglanti);
            DataTable dt = new DataTable();
            string eniyikitapSorgu = "SELECT tbl_bxbooks.booktitle,AVG(rating) as ortalamaPuan from tbl_bxbooks join tbl_bxbookrating on tbl_bxbooks.ısbn = tbl_bxbookrating.ısbn Group By tbl_bxbookrating.ısbn ORDER BY ortalamaPuan DESC LIMIT 10;";
            eniyikitapbaglanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(eniyikitapSorgu, eniyikitapbaglanti);
            da.Fill(dt);
            grdEniyiKitaplar.DataSource = dt;
            eniyikitapbaglanti.Close();
        }
        
        private void PopülerKitapListele()
        {
            /*Puanlanan kitapların bulunduğu “tbl_bxbookrating” tablo isbn kodlarına göre gruplanır.
             * Aynı isbn den kaç tane puanlamış bulunur. 
             * Bulunan bu değerler büyükten küçüğe sıralanır ve 10 tanesi popüler kitaplar sekmesinde listelenir. 
             */

            MySqlConnection Popülerbaglanti = new MySqlConnection(baglanti);
            DataTable dt = new DataTable();
            string popülerSorgu = "SELECT tbl_bxbooks.booktitle, COUNT(*) as adet from tbl_bxbooks join tbl_bxbookrating on tbl_bxbooks.ısbn = tbl_bxbookrating.ısbn Group By tbl_bxbookrating.ısbn ORDER BY adet DESC LIMIT 10;";
            Popülerbaglanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(popülerSorgu, Popülerbaglanti);
            da.Fill(dt);
            grdPopülerKitaplar.DataSource = dt;
            Popülerbaglanti.Close();
        }
        
        public void yeniKitapListele()
        {
            string eklenenisbn = YöneticiPaneli.kitapisbn;
            MySqlConnection yenikitapbaglanti = new MySqlConnection(baglanti);
            string yeniSorgu = "SELECT booktitle from tbl_bxbooks where '" + eklenenisbn + "'= ısbn;";
            DataTable dt = new DataTable();
            yenikitapbaglanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(yeniSorgu,yenikitapbaglanti);
            da.Fill(dt);
            grdYeniKitap.DataSource = dt;
            yenikitapbaglanti.Close();
        }
    }
}
