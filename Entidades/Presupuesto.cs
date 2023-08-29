using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaJG.Entidades
{
    internal class Presupuesto
    {
        private int nroPresupuesto;
        private DateTime fecha;
        private string cliente;
        private double descuento;
        private DateTime fechaBaja;
        private List<DetallePresupuesto> detalles;
        private double total;

        public int NroPresupuesto { get => nroPresupuesto; set => nroPresupuesto = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public double Descuento { get => descuento; set => descuento = value; }
        public DateTime FechaBaja { get => fechaBaja; set => fechaBaja = value; }
        public double Total { get => total; set => total = value; }

        public Presupuesto()
        {
            this.nroPresupuesto = 0;
            this.Fecha = DateTime.Now;
            this.Cliente = String.Empty;
            this.Descuento = 0;
            this.FechaBaja = DateTime.Now;
            this.Total = 0;
            detalles = new List<DetallePresupuesto>();
        }

        public Presupuesto(int nroPresupuesto, DateTime fecha, string cliente, double descuento, DateTime fechaBaja, double total)
        {
            this.nroPresupuesto = nroPresupuesto;
            this.fecha = fecha;
            this.cliente = cliente;
            this.descuento = descuento;
            this.fechaBaja = fechaBaja;
            this.total = total;
        }



         public void AgregarDetalle(DetallePresupuesto detalle)
        {
            detalles.Add(detalle);
        }


        public void QuitarDetalle(int posicion)
        {
            detalles.RemoveAt(posicion);
        }


        public double CalcularTotal()
        {
            double total = 0;
            detalles.ForEach((detalle) => total += detalle.CalcularSubTotal());
            return total;
        }
    }
}
