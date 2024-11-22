using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TicariOtomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_musterIler", bgl.baglanti());
            dataAdapter.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
        }
        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select SEHIR from TBL_ILLER",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            msktel.Text = "";
            msktel2.Text = "";
            mskTc.Text = "";
            txtMail.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            richAdres.Text = "";
            txtVergiD.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD, SOYAD,telefon,telefon2,tc,MAIL,IL,ILCE,adres,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",msktel.Text);
            komut.Parameters.AddWithValue("@p4",msktel.Text);
            komut.Parameters.AddWithValue("@p5",mskTc.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7",cmbIl.Text);
            komut.Parameters.AddWithValue("@p8", cmbIlce.Text);
            komut.Parameters.AddWithValue("@p9",richAdres.Text);
            komut.Parameters.AddWithValue("@p10",txtVergiD.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            listele();
            temizle();
        }

        private void cmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select ILCE from TBL_ILCELER where SEHIR=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbIl.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIlce.Properties.Items.Add(dr[0]);
            }
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
                msktel.Text = dr["TELEFON"].ToString();
                msktel2.Text = dr["TELEFON2"].ToString();
                mskTc.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString(); 
                richAdres.Text = dr["ADRES"].ToString();
                txtVergiD.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from TBL_MUSTERILER where ID="+txtId.Text,bgl.baglanti());
            DialogResult durum = MessageBox.Show("Silmek istediğinize emin misiniz?","Müşteri Silme",MessageBoxButtons.YesNo);
            if (durum == DialogResult.Yes)
            {
                komutSil.ExecuteNonQuery();
                MessageBox.Show("Müşteri sistemden silindi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (durum == DialogResult.No)
            {
                MessageBox.Show("Müşteri sistemde tutulmaya devam edecek...");
            }
            bgl.baglanti().Close();
            //MessageBox.Show("Müşteri sistemden silindi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("update TBL_MUSTERILER set AD=@p1,soyad=@p2,telefon=@p3,telefon2=@p4,tc=@p5,MAIL=@p6,IL=@p7,ILCE=@p8,adres=@p9,VERGIDAIRE=@p10 where ID=" + txtId.Text,bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@p3",msktel.Text);
            komutGuncelle.Parameters.AddWithValue("@p4",msktel2.Text);
            komutGuncelle.Parameters.AddWithValue("@p5",mskTc.Text);
            komutGuncelle.Parameters.AddWithValue("@p6",txtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@p7",cmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@p8",cmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@p9",richAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@p10",txtVergiD.Text);

            komutGuncelle.ExecuteNonQuery();

            MessageBox.Show("Müşteri bilgileri güncellendi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
