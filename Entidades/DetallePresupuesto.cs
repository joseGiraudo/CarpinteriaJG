using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaJG.Entidades
{
    internal class DetallePresupuesto
    {
        public Producto Producto { get; set; }
        public int cantidad { get; set; }

        public DetallePresupuesto(Producto prod, int cantidad)
        {
            this.Producto = prod;
            this.cantidad = cantidad;
        }
    }
}
