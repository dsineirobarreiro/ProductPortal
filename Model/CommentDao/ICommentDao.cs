using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace PracticaMad.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comentario, Int64>
    {
        List<Comentario> FindByProductId(long productId, int startIndex, int count);

        void AddTagToComment(long CommentId, Etiqueta tag);

        int NumberOfCommentsByProductId(long productId);

    }
}
