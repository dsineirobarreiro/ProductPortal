using Ninject;
using PracticaMad.Model.UserDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.ProductoDTO
{
    public class ProductoDTO
    {
        public long prodId { get; private set; }
        public String nombre { get; private set; }
        public String categoria { get; private set; }
        public DateTime date { get; private set; }
        public byte[] imagen { get; private set; }
        public string descripcion { get; private set; }

        public String vendedor { get; private set; }


        public ProductoDTO(long prodId, String nombre, String categoria, DateTime date, String vendedor, byte[] imagen = null, string descripcion = null) {
            this.prodId = prodId;
            this.nombre = nombre;
            this.categoria = categoria;
            this.date = date;
            this.vendedor = vendedor;
            this.imagen = imagen;
            this.descripcion = descripcion;
        }

        public ProductoDTO(Producto prod, String vendedor) {
            prodId = prod.productID;
            nombre = prod.nombre;
            categoria = prod.categoria;
            date = prod.date;
            imagen = prod.imagen;
            descripcion = prod.descripcion;
            this.vendedor = vendedor;
        }
    }
}
