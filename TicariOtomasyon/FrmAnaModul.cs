using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmUrunler fru;
        private void btnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fru = new FrmUrunler();
            fru.MdiParent = this;
            fru.Show();
        }
        FrmMusteriler frm;
        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm = new FrmMusteriler();
            frm.MdiParent = this;
            frm.Show();
        }
        FrmAnaModul fra;
        private void btnAnasayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fra = new FrmAnaModul();
            fra.MdiParent = this;
            fra.Show();
        }
        FrmFirmalar frf;
        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frf == null)
            {
                frf = new FrmFirmalar();
                frf.MdiParent = this;
                frf.Show();
            }
        }

        FrmPersonel frp;
        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frp == null)
            {
                frp = new FrmPersonel();
                frp.MdiParent = this;
                frp.Show();
            }
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {

        }
        FrmRehber frr;
        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frr == null)
            {
                frr = new FrmRehber();
                frr.MdiParent = this;
                frr.Show();
            }
        }
        FrmGiderler frg;
        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frg == null)
            {
                frg = new FrmGiderler();
                frg.MdiParent = this;
                frg.Show();
            }
            
        }
        FrmBankalar frb;
        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frb == null)
            {
                frb = new FrmBankalar();
                frb.MdiParent = this;
                frb.Show();
            }
        }
    }
}
