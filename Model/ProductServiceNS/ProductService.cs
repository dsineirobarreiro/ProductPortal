using Ninject;
using PracticaMad.Model.CommentDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using PracticaMad.Model.ProductoDAO;
using PracticaMad.Model.TagDao;
using System.Security.Principal;
using PracticaMad.Model.TagServiceNS;
using PracticaMad.Model.UserDAO;

namespace PracticaMad.Model.ProductServiceNS
{
    public class ProductService : IProductService
    {
        public ProductService() { }

        [Inject]
        public IProductoDao ProductDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public ITagService TagService { private get; set; }

        [Transactional]
        public ProductBlock FindProductByName(string productName, int startIndex, int count)
        {
            List<Producto> prods =
                ProductDao.FindByProductName(productName, startIndex, count + 1);

            bool existMoreProds = (prods.Count == count + 1);

            if (existMoreProds)
                prods.RemoveAt(count);

            List<ProductoDTO.ProductoDTO> productoDTOs = new List<ProductoDTO.ProductoDTO>();
            foreach (Producto prod in prods) {
                String vendedor = UserDao.Find(prod.vendedor).username;
                productoDTOs.Add(new ProductoDTO.ProductoDTO(prod, vendedor));
            }
            return new ProductBlock(productoDTOs, existMoreProds);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Comentario FindComentario(long comId)
        {
            Comentario com = null;
            com = CommentDao.Find(comId);

            return com;
        }

        [Transactional]
        public Comentario CommentProduct(long usuario, long producto, String text, HashSet<string> tagNames = null)
            {
                Comentario comment = new Comentario();
                comment.usuario = usuario;
                comment.producto = producto;
                comment.text = text;
                DateTime myDateTime = DateTime.Now;
                comment.date = myDateTime;
                CommentDao.Create(comment);
                foreach (string tagName in tagNames ?? new HashSet<string>()) {
                    Etiqueta tag = TagDao.FindByName(tagName);
                    if (tag != null)
                    {
                        CommentDao.AddTagToComment(comment.comID, tag);
                        TagService.UpdateUses(tag.tagID);
                    }
                    else {
                        tag = TagService.CreateTag(tagName);
                        CommentDao.AddTagToComment(comment.comID, tag);
                        TagService.UpdateUses(tag.tagID);
                    }
                }
                return comment;
            }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]

        public Comentario UpdateComment(Comentario comment) {

            if (!CommentDao.Exists(comment.comID))
            {
                throw new InstanceNotFoundException(comment.comID,
                    typeof(Comentario).FullName);
            }
            DateTime myDateTime = DateTime.Now;
            comment.date = myDateTime;
            CommentDao.Update(comment);
            return comment;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void RemoveComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CommentBlock SeeProductComments(long productId, int startIndex, int count) {

            if(!ProductDao.Exists(productId))
            {
                throw new InstanceNotFoundException(productId,
                    typeof(Producto).FullName);
            }

            List<Comentario> comments = CommentDao.FindByProductId(productId, startIndex, count +1);
            bool existMoreComms = (comments.Count == count + 1);
            if (existMoreComms)
                comments.RemoveAt(count);
            List<CommentDTO.CommentDTO> final_comments = new List<CommentDTO.CommentDTO>();
            foreach (Comentario com in comments) {
                final_comments.Add(new CommentDTO.CommentDTO(com, UserDao.Find(com.usuario).username));
            }
            return new CommentBlock(final_comments, existMoreComms);

        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ProductoDTO.ProductoDTO Find(long prodId) {
            Producto prod = ProductDao.Find(prodId);
            return new ProductoDTO.ProductoDTO(prod, UserDao.Find(prod.vendedor).username);
        }

        [Transactional]
        public int GetNumberOfProductsByName(string productName) {
            return ProductDao.GetNumberOfProductsByName(productName);
        }

        [Transactional]
        public int GetNumberOfCommentsByProdId(long prodId) {
            return CommentDao.NumberOfCommentsByProductId(prodId);
        }
    }
}
