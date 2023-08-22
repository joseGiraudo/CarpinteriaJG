using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaJG.Entidades
{
    internal class Producto
    {
        private int nroProducto;
        private string nombre;
        private double precio;

        public int NroProducto { get { return nroProducto; } set { nroProducto = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public double Precio { get { return precio; } set { precio = value; } }

        public Producto(int nro, string nombre, double precio) {
            this.nroProducto = nro;
            this.nombre = nombre;
            this.precio = precio;
        }
    }
}
