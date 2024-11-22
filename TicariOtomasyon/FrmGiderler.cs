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
using DevExpress.Utils.Win;

namespace TicariOtomasyon
{
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_GIDERLER",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtElektrik.Text = "";
            txtId.Text = "";
            txtInternet.Text = "";
            txtMaas.Text = "";
            txtSu.Text = "";
            cmbAy.Text = "";
            cmbYil.Text = "";
            richNotlar.Text = "";
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderListesi();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER (ay, YIL, ELEKTRIK, su, DOGALGAZ, INTERNET, MAASLAR, ekstra,notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());

            komut.Parameters.AddWithValue("@p1",cmbAy.Text);
            komut.Parameters.AddWithValue("@p2",cmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9",richNotlar.Text);

            komut.ExecuteNonQuery();

            MessageBox.Show("Giderler sisteme eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            giderListesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                cmbAy.Text = dr["AY"].ToString();
                cmbYil.Text = dr["YIL"].ToString();
                txtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["su"].ToString();
                txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtInternet.Text = dr["INTERNET"].ToString();
                txtMaas.Text = dr["MAASLAR"].ToString();
                txtEkstra.Text = dr["ekstra"].ToString();
                richNotlar.Text = dr["notlar"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from TBL_GIDERLER where ID=" + txtId.Text, bgl.baglanti());

            DialogResult durum = MessageBox.Show("Silmek istediğinize emin misiniz?", "Gider Silme", MessageBoxButtons.YesNo);
            if (durum == DialogResult.Yes)
            {
                komutSil.ExecuteNonQuery();
                MessageBox.Show("Gider sistemden silindi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (durum == DialogResult.No)
            {
                MessageBox.Show("Gider sistemde tutulmaya devam edecek...");
            }
            bgl.baglanti().Close();
            giderListesi();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle= new SqlCommand("update TBL_GIDERLER set AY=@p1, YIL=@p2, ELEKTRIK=@p3, su=@p4, DOGALGAZ=@p5, INTERNET=@p6, MAASLAR=@p7, ekstra=@p8,notlar=@p9 where ID=" + txtId.Text, bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@p1",cmbAy.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", cmbYil.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komutGuncelle.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komutGuncelle.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            komutGuncelle.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            komutGuncelle.Parameters.AddWithValue("@p7", decimal.Parse(txtMaas.Text));
            komutGuncelle.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komutGuncelle.Parameters.AddWithValue("@p9", richNotlar.Text);

            komutGuncelle.ExecuteNonQuery();

            MessageBox.Show("Gider bilgileri güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();
            giderListesi();
            temizle();
        }
    }
}
