using CarpinteriaJG.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaJG.Presentacion
{
    public partial class FrmNuevoPresupuesto : Form
    {
        Presupuesto nuevoPresupuesto = null;
        public FrmNuevoPresupuesto()
        {
            InitializeComponent();
            nuevoPresupuesto = new Presupuesto();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtCliente.Text = "Consumidor Final";
            txtDescuento.Text = "0";

            txtCantidad.Text = "1";

            ProximoPresupuesto();
            CargarProductos();
        }

        private void ProximoPresupuesto()
        {
            // creo y configuro la conexion
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1";
            connection.Open();

            // creo y configuro el comando
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_PROXIMO_ID";

            // creo y configuro el parametro porque el SP devuelve un parametro
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@next";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;

            // paso el parametro al comando (para que traiga el resultado del SP)
            command.Parameters.Add(parameter);

            // ejecuto el comando
            command.ExecuteNonQuery();

            connection.Close();

            lblPresupuestoNum.Text = lblPresupuestoNum.Text + " " + parameter.ToString();
        }
        private void CargarProductos()
        {
            // creo y configuro la conexion
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1";
            connection.Open();

            // creo y configuro el comando
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_CONSULTAR_PRODUCTOS";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            connection.Close();

            // cargo el combo box
            cboProducto.DataSource = table;
            cboProducto.ValueMember = table.Columns[0].ColumnName;
            cboProducto.DisplayMember = table.Columns[1].ColumnName;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // agrego productos al dataGridView

            // validar
            if(cboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach ( DataGridViewRow row in dgvDetalle.Rows ) 
            {
                if (row.Cells["colProducto"].Value.ToString().Equals(cboProducto.Text))
                {
                    MessageBox.Show("Este producto ya está presupuestado..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView)cboProducto.SelectedItem;

            int nro = Convert.ToInt32(item.Row.ItemArray[0]);
            string nombre = item.Row.ItemArray[1].ToString();
            double precio = Convert.ToDouble(item.Row.ItemArray[2]);

            Producto prod = new Producto(nro, nombre, precio);

            int cant = Convert.ToInt32(txtCantidad.Text);
            DetallePresupuesto detalle = new DetallePresupuesto(prod, cant);

            nuevoPresupuesto.AgregarDetalle(detalle);
            dgvDetalle.Rows.Add(new object[] { detalle.Producto.NroProducto, 
                                               detalle.Producto.Nombre, 
                                               detalle.Producto.Precio, 
                                               detalle.cantidad,
                                               "Quitar" });

            CalcularTotales();


        }
        private void CalcularTotales()
        {
            txtSubTotal.Text = nuevoPresupuesto.CalcularTotales().ToString();
            if( !string.IsNullOrEmpty(txtDescuento.Text) && int.TryParse(txtDescuento.Text, out _))
            {
                double desc = nuevoPresupuesto.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
                txtTotal.Text = (nuevoPresupuesto.CalcularTotal() - desc).ToString();
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalle.CurrentCell.ColumnIndex == 4)
            {
                nuevoPresupuesto.QuitarDetalle(dgvDetalle.CurrentRow.Index);
                dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);
                CalcularTotales();
            }
        }
    }
}
