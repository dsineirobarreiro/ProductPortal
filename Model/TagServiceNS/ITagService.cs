using Es.Udc.DotNet.ModelUtil.Transactions;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.TagDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.TagServiceNS
{
    public interface ITagService
    {

        ITagDao TagDao { set; }

        [Transactional]
        Etiqueta CreateTag(String tag);

        [Transactional]
        Etiqueta FindById(long tagId);

        [Transactional]
        void UpdateUses(long tagId);

        [Transactional]
        CommentBlock GetCommentsByTag(long tagId, int startIndex, int count);

        [Transactional]
        CommentBlock GetCommentsByTagName(string name, int startIndex, int count);

        [Transactional]
        TagBlock GetTagsByBlock(int startIndex, int count);
        }
}
