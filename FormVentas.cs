
using System;
using System.Drawing;
using System.Windows.Forms;

namespace surtidoraSmart
{
    public partial class FormVentas : Form
    {
        private TextBox txtProducto;
        private NumericUpDown numCantidad;
        private NumericUpDown numPrecio;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private decimal total = 0m;

        public FormVentas()
        {
            InitializeComponent();
            CrearInterfaz();
        }

        private void CrearInterfaz()
        {
            this.Text = "Surtidora Smart - Facturacion";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            Label titulo = new Label();
            titulo.Text = "SURTIDORA SMART";
            titulo.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            titulo.ForeColor = Color.DarkGreen;
            titulo.Location = new Point(25, 15);
            titulo.AutoSize = true;
            this.Controls.Add(titulo);

            Label subtitulo = new Label();
            subtitulo.Text = "MODULO DE VENTAS Y FACTURACION";
            subtitulo.Location = new Point(30, 65);
            subtitulo.AutoSize = true;
            this.Controls.Add(subtitulo);

            Label lblProd = new Label();
            lblProd.Text = "Nombre del producto:";
            lblProd.Location = new Point(30, 115);
            lblProd.AutoSize = true;
            this.Controls.Add(lblProd);

            txtProducto = new TextBox();
            txtProducto.Location = new Point(30, 145);
            txtProducto.Size = new Size(230, 30);
            this.Controls.Add(txtProducto);

            Label lblCant = new Label();
            lblCant.Text = "Cantidad:";
            lblCant.Location = new Point(280, 115);
            lblCant.AutoSize = true;
            this.Controls.Add(lblCant);

            numCantidad = new NumericUpDown();
            numCantidad.Location = new Point(280, 145);
            numCantidad.Minimum = 1;
            numCantidad.Maximum = 100000;
            numCantidad.Value = 1;
            numCantidad.Width = 100;
            this.Controls.Add(numCantidad);

            Label lblPrecio = new Label();
            lblPrecio.Text = "Precio unitario (RD$):";
            lblPrecio.Location = new Point(405, 115);
            lblPrecio.AutoSize = true;
            this.Controls.Add(lblPrecio);

            numPrecio = new NumericUpDown();
            numPrecio.Location = new Point(405, 145);
            numPrecio.Minimum = 0;
            numPrecio.Maximum = 1000000;
            numPrecio.DecimalPlaces = 2;
            numPrecio.Increment = 5;
            numPrecio.Width = 140;
            this.Controls.Add(numPrecio);

            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar producto";
            btnAgregar.Location = new Point(575, 140);
            btnAgregar.Size = new Size(170, 38);
            btnAgregar.BackColor = Color.DarkGreen;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Click += BtnAgregar_Click;
            this.Controls.Add(btnAgregar);

            dgvDetalle = new DataGridView();
            dgvDetalle.Location = new Point(30, 205);
            dgvDetalle.Size = new Size(865, 270);
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.ReadOnly = true;
            dgvDetalle.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.MultiSelect = false;
            dgvDetalle.RowHeadersVisible = false;

            dgvDetalle.Columns.Add("Producto", "Producto");
            dgvDetalle.Columns.Add("Cantidad", "Cantidad");
            dgvDetalle.Columns.Add("Precio", "Precio unitario");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal");

            this.Controls.Add(dgvDetalle);

            lblTotal = new Label();
            lblTotal.Text = "TOTAL: RD$ 0.00";
            lblTotal.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTotal.ForeColor = Color.DarkGreen;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(560, 495);
            this.Controls.Add(lblTotal);

            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar producto";
            btnEliminar.Location = new Point(30, 495);
            btnEliminar.Size = new Size(170, 40);
            btnEliminar.Click += BtnEliminar_Click;
            this.Controls.Add(btnEliminar);

            Button btnNueva = new Button();
            btnNueva.Text = "Nueva venta";
            btnNueva.Location = new Point(215, 495);
            btnNueva.Size = new Size(140, 40);
            btnNueva.Click += BtnNueva_Click;
            this.Controls.Add(btnNueva);

            Button btnCobrar = new Button();
            btnCobrar.Text = "Cobrar / Finalizar";
            btnCobrar.Location = new Point(560, 545);
            btnCobrar.Size = new Size(200, 42);
            btnCobrar.BackColor = Color.DarkGreen;
            btnCobrar.ForeColor = Color.White;
            btnCobrar.FlatStyle = FlatStyle.Flat;
            btnCobrar.Click += BtnCobrar_Click;
            this.Controls.Add(btnCobrar);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string producto = txtProducto.Text.Trim();

            if (producto == "")
            {
                MessageBox.Show("Escribe el nombre del producto.");
                txtProducto.Focus();
                return;
            }

            decimal cantidad = numCantidad.Value;
            decimal precio = numPrecio.Value;
            decimal subtotal = cantidad * precio;

            dgvDetalle.Rows.Add(
                producto,
                cantidad,
                precio.ToString("N2"),
                subtotal.ToString("N2")
            );

            total += subtotal;
            ActualizarTotal();

            txtProducto.Clear();
            numCantidad.Value = 1;
            numPrecio.Value = 0;
            txtProducto.Focus();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            DataGridViewRow fila = dgvDetalle.SelectedRows[0];

            decimal subtotal = Convert.ToDecimal(
                fila.Cells["Subtotal"].Value
            );

            total -= subtotal;
            if (total < 0)
                total = 0;

            dgvDetalle.Rows.Remove(fila);
            ActualizarTotal();
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            dgvDetalle.Rows.Clear();
            total = 0;
            ActualizarTotal();
            txtProducto.Clear();
            numCantidad.Value = 1;
            numPrecio.Value = 0;
            txtProducto.Focus();
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto a la venta.");
                return;
            }

            MessageBox.Show(
                "Venta preparada correctamente.\n\n" +
                "Total: RD$ " + total.ToString("N2") +
                "\n\nTodavia falta conectar la base de datos " +
                "para guardar la factura.",
                "Surtidora Smart",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "TOTAL: RD$ " + total.ToString("N2");
        }
    }
}
