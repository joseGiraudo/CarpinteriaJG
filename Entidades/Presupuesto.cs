using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaJG.Entidades
{
    internal class Presupuesto
    {
        public DetallePresupuesto Detalle { get; set; }



        public void AgregarDetalle(DetallePresupuesto detalle)
        {

        }

        public void QuitarDetalle(int posicion)
        {

        }

        public double CalcularTotal()
        {
            double total = 0;

            return total;
        }

        public double CalcularTotales()
        {
            double total = 0;

            return total;
        }
    }
}
