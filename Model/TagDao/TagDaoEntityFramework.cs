using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Etiqueta, Int64>, ITagDao
    {
        public Etiqueta FindByName(String name) {
            DbSet<Etiqueta> tags = Context.Set<Etiqueta>();

            var result =
                (from a in tags
                 where a.nombre == name
                 select a).FirstOrDefault();

            return result;
        }

        public List<Comentario> FindCommentsByTagName(String name, int startIndex, int count)
        {
            DbSet<Etiqueta> tags = Context.Set<Etiqueta>();

            var result =
                (from a in tags
                 where a.nombre == name
                 select a.Comentario).FirstOrDefault().Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<Comentario> FindCommentsByTagId(long TagId, int startIndex, int count) {
            DbSet<Etiqueta> tags = Context.Set<Etiqueta>();

            var result =
                    (from a in tags
                     where a.tagID == TagId
                     select a.Comentario).FirstOrDefault().Skip(startIndex).Take(count).ToList();
            return result;
        }

        public int GetCommentNumberOfTag(long TagId) {
            DbSet<Etiqueta> tags = Context.Set<Etiqueta>();
            var result = (
                from a in tags where a.tagID == TagId select a.Comentario).Count();
            return result;
        }

        public List<Etiqueta> GetTagsByBlock(int startIndex, int count) {
            DbSet<Etiqueta> tags = Context.Set<Etiqueta>();
            var result = (
                from a in tags orderby a.numUsos descending select a).Skip(startIndex).Take(count).ToList<Etiqueta>();
            return result;
        }
    }
}
