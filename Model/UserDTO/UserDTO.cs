using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.UserDTO
{
    public class UserDTO
    {
        public long userID { get; private set; }
        public string username { get; private set; }
        public string pwd { get; private set; }
        public string nombre { get; private set; }
        public string apellidos { get; private set; }
        public string email { get; private set; }
        public string idioma { get; private set; }
        public string nacionalidad { get; private set; }
        public double score { get; private set; }
        public int numValoraciones { get; private set; }

        public UserDTO(Usuario user, double score, int numVal) {
            userID = user.userID;
            username = user.username;
            pwd = user.pwd;
            nombre = user.nombre;
            apellidos = user.apellidos;
            email = user.email;
            idioma = user.idioma;
            nacionalidad = user.nacionalidad;
            this.score = score;
            this.numValoraciones = numVal;
        }
    }
}
