using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.TagDao
{
    public interface ITagDao : IGenericDao<Etiqueta, Int64>
    {

        Etiqueta FindByName(String name);
        List<Comentario> FindCommentsByTagName(String name, int startIndex, int count);

        List<Comentario> FindCommentsByTagId(long TagId, int startIndex, int count);

        int GetCommentNumberOfTag(long TagId);

        List<Etiqueta> GetTagsByBlock(int startIndex, int count);
    }
}
