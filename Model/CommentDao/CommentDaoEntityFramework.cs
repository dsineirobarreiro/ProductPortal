using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Entity.Core;

namespace PracticaMad.Model.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comentario, Int64>, ICommentDao
    {
        public List<Comentario> FindByProductId(long productId, int startIndex, int count) {

            DbSet<Comentario> comentarios = Context.Set<Comentario>();

            var result =
                (from a in comentarios
                 where a.producto == productId
                 orderby a.date descending
                 select a).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int NumberOfCommentsByProductId(long productId)
        {

            DbSet<Comentario> comentarios = Context.Set<Comentario>();

            var result =
                (from a in comentarios
                 where a.producto == productId
                 orderby a.comID
                 select a).Count();

            return result;
        }
        public void AddTagToComment(long CommentId, Etiqueta tag)
        {
            Comentario comment = Find(CommentId);
            comment.Etiqueta.Add(tag);
            Update(comment);
        }
    }
}
