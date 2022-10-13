using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Windows.Shapes;

namespace FinalProjectConstruct
{
    public partial class Form1 : Form
    {
        string txtecnrypt;
        string fotopath;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Text) && string.IsNullOrEmpty(txtNombre.Text) &&
                string.IsNullOrEmpty(txtApellido.Text) && string.IsNullOrEmpty(txtCelular.Text) &&
                string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Debe completar los campos antes de generar el QR", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                txtecnrypt = this.EncryptText();
                QRCoder.QRCodeGenerator qRCode = new QRCoder.QRCodeGenerator();
                ASCIIEncoding encoding = new ASCIIEncoding();

                var createcode = qRCode.CreateQrCode(encoding.GetBytes(txtecnrypt), QRCoder.QRCodeGenerator.ECCLevel.L);
                PngByteQRCode png = new PngByteQRCode();
                png.SetQRCodeData(createcode);
                var arr = png.GetGraphic(10);
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(arr, 0, arr.Length);
                Bitmap bitmap = new Bitmap(memoryStream);
                pbQrCode.Image = bitmap;
            }
        }

        private string EncryptText()
        {
            //string someString = Encoding.ASCII.GetString(imagearray);
            //string base64photo = ConvertImage(path);
            // byte[] encoding = Encoding.UTF8.GetBytes(base64photo);
            //string compress = Compress(base64photo);

            //listBox1.Items = imagearray.CopyTo(imagearray, 993);
            StringBuilder sb = new StringBuilder();
            sb.Append(txtCedula.Text + "||");
            sb.Append(txtNombre.Text + "||");
            sb.Append(txtApellido.Text + "||");
            sb.Append(txtCelular.Text + "||");
            sb.Append(txtDireccion.Text + "||");
            //sb.Append("1999-12-19" + "||");
            //sb.Append("12345678901" + "||");
            //sb.Append("2023-12-31" + "||");
            //sb.Append("card" + "||");
            //sb.Append("2023-12-31" + "||");
            //sb.Append(base64photo + "|||");
            //sb.Append("1234567890" + "||");
            //txtNoEncrypt.Text = sb.ToString();
            // var test = compress;
            // pictureBox2.Image = LoadBase64(base64photo);
            return sb.ToString().Encrypttext() + "###";

        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pbPhotoface.Image = new Bitmap(open.FileName);
                fotopath = open.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TextEncrypt.IV = Encoding.ASCII.GetBytes("dkr*vcd@");
            TextEncrypt.Key = Encoding.ASCII.GetBytes("regshajshsdrkrmwcbsgskfe");
        }
    }
}
