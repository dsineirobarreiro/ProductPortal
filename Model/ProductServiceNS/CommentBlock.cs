using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.ProductServiceNS
{
    public class CommentBlock
    {
        public List<CommentDTO.CommentDTO> Comentarios { get; private set; }
        public bool ExistMoreProducts { get; private set; }

        public CommentBlock(List<CommentDTO.CommentDTO> comments, bool existMoreProducts)
        {
            this.Comentarios = comments;
            this.ExistMoreProducts = existMoreProducts;
        }
    }
}
