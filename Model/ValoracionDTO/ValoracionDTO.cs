using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.ValoracionDTO
{
    public class ValoracionDTO
    {
        public long valID { get; set; }
        public string comprador { get; set; }
        public long producto { get; set; }
        public double puntuacion { get; set; }
        public string comentario { get; set; }
        public System.DateTime date { get; set; }

        public ValoracionDTO(Valoracion valoracion, string username) {
            valID = valoracion.valID;
            comprador = username;
            producto = valoracion.producto;
            puntuacion = valoracion.puntuacion;
            comentario = valoracion.comentario;
            date = valoracion.date;
        }
    }
}
