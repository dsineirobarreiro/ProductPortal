using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.CommentDTO
{
    public class CommentDTO
    {
        public long comID { get; set; }
        public string usuario { get; set; }
        public long producto { get; set; }
        public System.DateTime date { get; set; }
        public string text { get; set; }

        public CommentDTO(Comentario comment, string username) {
            comID = comment.comID;
            usuario = username;
            producto = comment.producto;
            date = comment.date;
            text = comment.text;
        }


    }
}
