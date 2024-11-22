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
using DevExpress.Utils.Frames;
using DevExpress.XtraBars;

namespace TicariOtomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_URUNLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //verileri kaydetme
            SqlCommand komut = new SqlCommand("insert into tbl_urunler (urunad,marka,model,YIL,adet,ALISFIYAT,SATISFIYAT,detay)values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", mskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(numUDAdet.Value.ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text));
            komut.Parameters.AddWithValue("@p8", richDetay.Text);
            komut.ExecuteNonQuery();

            bgl.baglanti().Close();

            MessageBox.Show("Ürün sisteme eklendi...");
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSil = new SqlCommand("delete from tbl_urunler where ID=@p1", bgl.baglanti());
            komutSil.Parameters.AddWithValue("@p1",txtId.Text);
            komutSil.ExecuteNonQuery();

            bgl.baglanti().Close();

            MessageBox.Show("Ürün silindi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            txtId.Text = dr["ID"].ToString();
            txtAd.Text = dr["URUNAD"].ToString();
            txtMarka.Text = dr["MARKA"].ToString();
            txtModel.Text = dr["MODEL"].ToString();
            txtSatis.Text = dr["SATISFIYAT"].ToString();
            txtAlis.Text = dr["ALISFIYAT"].ToString();
            mskYil.Text = dr["YIL"].ToString();
            numUDAdet.Text = dr["ADET"].ToString();
            richDetay.Text = dr["DETAY"].ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("update tbl_urunler set urunad=@p1,marka=@p2,model=@p3,satısfıyat=@p4,alısfıyat=@p5,yıl=@p6,adet=@p7,detay=@p8 where ID=@p9", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@p1",txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@p2",txtMarka.Text);
            komutGuncelle.Parameters.AddWithValue("@p3",txtModel.Text);
            komutGuncelle.Parameters.AddWithValue("@p4",decimal.Parse(txtSatis.Text));
            komutGuncelle.Parameters.AddWithValue("@p5",decimal.Parse(txtAlis.Text));
            komutGuncelle.Parameters.AddWithValue("@p6",mskYil.Text);
            komutGuncelle.Parameters.AddWithValue("@p7",int.Parse(numUDAdet.Text));
            komutGuncelle.Parameters.AddWithValue("@p8",richDetay.Text);
            komutGuncelle.Parameters.AddWithValue("@p9",txtId.Text);

            komutGuncelle.ExecuteNonQuery();

            MessageBox.Show("Ürün güncelleme işlemi başarılı...","Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
