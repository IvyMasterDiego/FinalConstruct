using FinalProjectConstruct.Class.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Windows.Shapes;

namespace FinalProjectConstruct
{
    public partial class MainForm : Form
    {
        string txtecnrypt;
        PersonInformation person = new PersonInformation();

        public MainForm()
        {
            InitializeComponent();
            dgvPerson.DataSource = person.GetPersons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            TextEncrypt.IV = Encoding.ASCII.GetBytes("dkr*vcd@");
            TextEncrypt.Key = Encoding.ASCII.GetBytes("regshajshsdrkrmwcbsgskfe");
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
                QRCodeGenerator qRCode = new QRCodeGenerator();
                ASCIIEncoding encoding = new ASCIIEncoding();

                var createcode = qRCode.CreateQrCode(encoding.GetBytes(txtecnrypt), QRCodeGenerator.ECCLevel.L);
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
            StringBuilder sb = new StringBuilder();
            sb.Append(txtCedula.Text + "||");
            sb.Append(txtNombre.Text + "||");
            sb.Append(txtApellido.Text + "||");
            sb.Append(txtCelular.Text + "||");
            sb.Append(txtDireccion.Text + "||");

            return sb.ToString().Encrypttext() + "###";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            person.Cedula = txtCedula.Text;
            person.Nombre = txtNombre.Text;
            person.Apellido = txtApellido.Text;
            person.Celular = txtCelular.Text;
            person.Direccion = txtDireccion.Text;

            var success = person.InsertPerson(person);
            dgvPerson.DataSource = person.GetPersons();
            if (success)
            {
                ClearControls();
                MessageBox.Show("Agregado correctamente");
            }
            else
            {
                MessageBox.Show("Ocurrio un error. Intente otra vez");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            person.Id = int.Parse(txtId.Text);
            person.Cedula = txtCedula.Text;
            person.Nombre = txtNombre.Text;
            person.Apellido = txtApellido.Text;
            person.Celular = txtCelular.Text;
            person.Direccion = txtDireccion.Text;

            var success = person.UpdatePerson(person);
            dgvPerson.DataSource = person.GetPersons();
            if (success)
            {
                ClearControls();
                MessageBox.Show("Actualizado correctamente");
            }
            else
            {
                MessageBox.Show("Ocurrio un error. Intente otra vez");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            person.Cedula = txtCedula.Text;
            var success = person.DeletePerson(person);
            dgvPerson.DataSource = person.GetPersons();
            if (success)
            {
                ClearControls();
                MessageBox.Show("Actualizado correctamente");
            }
            else
            {
                MessageBox.Show("Ocurrio un error. Intente otra vez");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtId.Text = String.Empty;
            txtCedula.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtCelular.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        private void dgvPerson_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var index = e.RowIndex;
            txtId.Text = dgvPerson.Rows[index].Cells[0].Value.ToString();
            txtCedula.Text = dgvPerson.Rows[index].Cells[1].Value.ToString();
            txtNombre.Text = dgvPerson.Rows[index].Cells[2].Value.ToString();
            txtApellido.Text = dgvPerson.Rows[index].Cells[3].Value.ToString();
            txtCelular.Text = dgvPerson.Rows[index].Cells[4].Value.ToString();
            txtDireccion.Text = dgvPerson.Rows[index].Cells[5].Value.ToString();
        }
    }
}
