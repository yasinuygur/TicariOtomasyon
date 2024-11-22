using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void personelListe()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_personeller",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelListe();
            sehirListesi();
            temizle();
        }
        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select SEHIR from tbl_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_personeller (ad,soyad,telefon,tc,MAIL, IL,ILCE, adres,gorev) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",msktel.Text);
            komut.Parameters.AddWithValue("@p4",mskTc.Text);
            komut.Parameters.AddWithValue("@p5",txtMail.Text);
            komut.Parameters.AddWithValue("@p6",cmbIl.Text);
            komut.Parameters.AddWithValue("@p7",cmbIlce.Text);
            komut.Parameters.AddWithValue("@p8",richAdres.Text);
            komut.Parameters.AddWithValue("@p9",txtGorevi.Text);

            komut.ExecuteNonQuery();

            MessageBox.Show("Personel sisteme eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            personelListe();
            temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            txtGorevi.Text = "";
            mskTc.Text = "";
            msktel.Text = "";
            richAdres.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtAd.Focus();
            bgl.baglanti().Close();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                txtGorevi.Text = dr["GOREV"].ToString();
                mskTc.Text = dr["TC"].ToString();
                msktel.Text = dr["TELEFON"].ToString();
                richAdres.Text = dr["ADRES"].ToString();
                
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TBL_PERSONELLER where ID=" + txtId.Text, bgl.baglanti());

            //komut.ExecuteNonQuery();

            DialogResult durum = MessageBox.Show("Personeli silmek istediğinize emin misiniz?", "Personel Silme", MessageBoxButtons.YesNo);
            if (durum == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Personel sistemden silindi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (durum == DialogResult.No)
            {
                MessageBox.Show("Personel sistemde tutulmaya devam edecek...");
            }

            
            bgl.baglanti().Close();
            personelListe();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_personeller set AD=@p1,soyad=@p2,MAIL=@p3,gorev=@p4,tc=@p5,telefon=@p6,adres=@p7,IL=@p8,ILCE=@p9 where ID=" + txtId.Text,bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",txtMail.Text);
            komut.Parameters.AddWithValue("@p4",txtGorevi.Text);
            komut.Parameters.AddWithValue("@p5",mskTc.Text);
            komut.Parameters.AddWithValue("@p6",msktel.Text);
            komut.Parameters.AddWithValue("@p7",richAdres.Text);
            komut.Parameters.AddWithValue("@p8",cmbIl.Text);
            komut.Parameters.AddWithValue("@p9",cmbIlce.Text);

            komut.ExecuteNonQuery();

            MessageBox.Show("Personel sistemde güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            personelListe();
            temizle();
        }
    }
}
