using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace TicariOtomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMailAdresi.Text = mail;
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesaj = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("yasinuygur10@gmail.com", "yasin047");
            client.Port = 587;
            client.Host= "smtp.gmail.com";
            client.EnableSsl = true;
            mesaj.To.Add(txtMailAdresi.Text);
            mesaj.From=new MailAddress("yasinuygur10@gmail.com");
            mesaj.Subject=txtKonu.Text;
            mesaj.Body=richMesaj.Text;
            client.Send(mesaj);
        }
    }
}
