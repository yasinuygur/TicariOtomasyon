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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmaListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

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

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("select FIRMAKOD1 from tbl_kodlar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                richOzelKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtMail.Text = "";
            txtOzelKod1.Text = "";
            txtOzelKod2.Text = "";
            txtOzelKod3.Text = "";
            txtSektor.Text = "";
            txtVergiD.Text = "";
            txtYetkili.Text = "";
            txtYGorev.Text = "";
            mskFax.Text = "";
            mskTc.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            mskTel3.Text = "";
            richAdres.Text = "";
            //richOzelKod1.Text = "";
            //richOzelKod2.Text = "";
            //richOzelKod3.Text = "";
            //cmbIl.Text = "";
            //cmbIlce.Text = "";
            txtAd.Focus();

            bgl.baglanti().Close();
        }

        
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmaListesi();
            carikodaciklamalar();
            sehirListesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                mskTc.Text = dr["YETKILITC"].ToString();
                txtYGorev.Text = dr["YETKILISTATU"].ToString();
                txtSektor.Text = dr["SEKTOR"].ToString();
                mskTel1.Text = dr["TELEFON1"].ToString();
                mskTel2.Text = dr["TELEFON2"].ToString();
                mskTel3.Text = dr["TELEFON3"].ToString();
                mskFax.Text = dr["FAX"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                richAdres.Text = dr["ADRES"].ToString();
                txtVergiD.Text = dr["VERGIDAIRE"].ToString();
                txtOzelKod1.Text = dr["OZELKOD1"].ToString();
                txtOzelKod2.Text = dr["OZELKOD2"].ToString();
                txtOzelKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());

            komutEkle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutEkle.Parameters.AddWithValue("@p2", txtYGorev.Text);
            komutEkle.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komutEkle.Parameters.AddWithValue("@p4", mskTc.Text);
            komutEkle.Parameters.AddWithValue("@p5", txtSektor.Text);
            komutEkle.Parameters.AddWithValue("@p6", mskTel1.Text);
            komutEkle.Parameters.AddWithValue("@p7", mskTel2.Text);
            komutEkle.Parameters.AddWithValue("@p8", mskTel3.Text);
            komutEkle.Parameters.AddWithValue("@p9", txtMail.Text);
            komutEkle.Parameters.AddWithValue("@p10", mskFax.Text);
            komutEkle.Parameters.AddWithValue("@p11", cmbIl.Text);
            komutEkle.Parameters.AddWithValue("@p12", cmbIlce.Text);
            komutEkle.Parameters.AddWithValue("@p13", txtVergiD.Text);
            komutEkle.Parameters.AddWithValue("@p14", richAdres.Text);
            komutEkle.Parameters.AddWithValue("@p15", txtOzelKod1.Text);
            komutEkle.Parameters.AddWithValue("@p16", txtOzelKod2.Text);
            komutEkle.Parameters.AddWithValue("@p17", txtOzelKod3.Text);

            komutEkle.ExecuteNonQuery();

            MessageBox.Show("Firma sisteme eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            firmaListesi();
            temizle();
        }

        private void cmbIl_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from TBL_FIRMALAR where ID="+txtId.Text, bgl.baglanti());
            

            DialogResult durum = MessageBox.Show("Firmayı silmek istediğinize emin misiniz?", "Firma Silme", MessageBoxButtons.YesNo);
            if (durum == DialogResult.Yes)
            {
                komutSil.ExecuteNonQuery();
                MessageBox.Show("Firma sistemden silindi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (durum == DialogResult.No)
            {
                MessageBox.Show("Firma sistemde tutulmaya devam edecek...");
            }

            //MessageBox.Show("Firma sistemden silindi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            bgl.baglanti().Close();
            firmaListesi();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 where ID=" + txtId.Text, bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", txtYGorev.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komutGuncelle.Parameters.AddWithValue("@p4", mskTc.Text);
            komutGuncelle.Parameters.AddWithValue("@p5", txtSektor.Text);
            komutGuncelle.Parameters.AddWithValue("@p6", mskTel1.Text);
            komutGuncelle.Parameters.AddWithValue("@p7", mskTel2.Text);
            komutGuncelle.Parameters.AddWithValue("@p8", mskTel3.Text);
            komutGuncelle.Parameters.AddWithValue("@p9", txtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@p10", mskFax.Text);
            komutGuncelle.Parameters.AddWithValue("@p11", cmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@p12", cmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@p13", txtVergiD.Text);
            komutGuncelle.Parameters.AddWithValue("@p14", richAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@p15", txtOzelKod1.Text);
            komutGuncelle.Parameters.AddWithValue("@p16", txtOzelKod2.Text);
            komutGuncelle.Parameters.AddWithValue("@p17", txtOzelKod3.Text);

            komutGuncelle.ExecuteNonQuery();

            MessageBox.Show("Firma sistemde güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            firmaListesi();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
