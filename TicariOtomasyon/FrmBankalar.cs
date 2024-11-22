using DevExpress.XtraEditors.Design;
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
    public partial class FrmBankalar : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmBankalar()
        {
            InitializeComponent();
        }
        void bankaListele()
        {
            //SqlDataAdapter dataAdapter = new SqlDataAdapter("execute BankaBilgileri", bgl.baglanti());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_bankalar", bgl.baglanti());
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            bankaListele();
            temizle();
            sehirListesi();
            firmaListesi();
        }
        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select SEHIR from TBL_ILLER", bgl.baglanti());
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
            txtSube.Text = "";
            msktel.Text = "";
            mskTarih.Text = "";
            txtIban.Text = "";
            txtHesapNo.Text = "";
            cmbIl.Text = "";
            cmbIlce.Text = "";
            txtYetkili.Text = "";
            txtHesapTuru.Text = "";
            //cmbFirma.Text = "";
        }
        void firmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,AD from tbl_fırmalar",bgl.baglanti());
            da.Fill(dt);
            cmbFirma.Properties.ValueMember = "ID";
            cmbFirma.Properties.DisplayMember = "AD";
            cmbFirma.Properties.DataSource = dt;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("insert into tbl_bankalar (bankaadı, ıl, ılce, sube, ıban, hesapno, yetkılı, telefon, tarıh, hesapturu,fırmaıd) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)",bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutEkle.Parameters.AddWithValue("@p2", cmbIl.Text);
            komutEkle.Parameters.AddWithValue("@p3", cmbIlce.Text);
            komutEkle.Parameters.AddWithValue("@p4", txtSube.Text);
            komutEkle.Parameters.AddWithValue("@p5", txtIban.Text);
            komutEkle.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            komutEkle.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komutEkle.Parameters.AddWithValue("@p8", msktel.Text);
            komutEkle.Parameters.AddWithValue("@p9", mskTarih.Text);
            komutEkle.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            komutEkle.Parameters.AddWithValue("@p11", cmbFirma.EditValue);

            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka sisteme eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bankaListele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["BANKAADI"].ToString();
                cmbIl.Text = dr["IL"].ToString();
                cmbIlce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                txtIban.Text = dr["IBAN"].ToString();
                txtHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                msktel.Text = dr["TELEFON"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();
                //cmbFirma.Text = dr["fırmaıd"].ToString();
            }
        }
    }
}
