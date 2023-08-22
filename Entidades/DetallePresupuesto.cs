using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaJG.Entidades
{
    internal class DetallePresupuesto
    {
        private Producto producto;
        private int cantidad;

        public Producto Producto { get { return producto; } set { producto = value; } }
        public int Cantidad { get { return cantidad; } set {cantidad = value; } }



        public DetallePresupuesto(Producto prod, int cantidad)
        {
            this.producto = prod;
            this.cantidad = cantidad;
        }

        public double CalcularSubTotal()
        {
            return producto.Precio * cantidad;
        }
    }
}
