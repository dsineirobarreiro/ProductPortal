using Es.Udc.DotNet.ModelUtil.Transactions;
using PracticaMad.Model.CommentDao;
using System;
using System.Collections.Generic;

namespace PracticaMad.Model.ProductServiceNS
{
    public interface IProductService
    {
        ICommentDao CommentDao { set; }

        [Transactional]
        ProductBlock FindProductByName(string productName, int startIndex, int count);

        [Transactional]
        Comentario FindComentario(long comId);

        [Transactional]
        Comentario CommentProduct(long usuario, long producto, String text, HashSet<string> tagIDs = null);

        [Transactional]
        Comentario UpdateComment(Comentario comment);

        [Transactional]
        void RemoveComment(long commentId);

        [Transactional]
        CommentBlock SeeProductComments(long productId, int startIndex, int count);

        [Transactional]
        ProductoDTO.ProductoDTO Find(long prodId);

        [Transactional]
        int GetNumberOfProductsByName(string productName);
        [Transactional]
        int GetNumberOfCommentsByProdId(long prodId);
    }
}
