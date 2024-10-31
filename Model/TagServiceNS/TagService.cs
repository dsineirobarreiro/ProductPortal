using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.TagDao;
using PracticaMad.Model.UserDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PracticaMad.Model.TagServiceNS
{
    public class TagService : ITagService
    {
        public TagService() { }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public Etiqueta CreateTag(String tag)
        {
            Etiqueta tag2 = TagDao.FindByName(tag);
            if (tag2 == null)
            {
                Etiqueta etiqueta = new Etiqueta();
                etiqueta.nombre = tag;
                etiqueta.numUsos = 0;
                TagDao.Create(etiqueta);
                return etiqueta;
            }
            else
                throw new DuplicateInstanceException(tag, typeof(Etiqueta).FullName);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Etiqueta FindById(long tagId)
        {
            Etiqueta tag = null;
            tag = TagDao.Find(tagId);

            return tag;
        }
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CommentBlock GetCommentsByTag(long tagId, int startIndex, int count)
        {
            List<Comentario> comms = TagDao.FindCommentsByTagId(tagId, startIndex, count + 1);

            bool existMoreComms = (comms.Count == count + 1);

            if (existMoreComms)
                comms.RemoveAt(count);
            List<CommentDTO.CommentDTO> final_comments = new List<CommentDTO.CommentDTO>();
            foreach (Comentario com in comms)
            {
                final_comments.Add(new CommentDTO.CommentDTO(com, UserDao.Find(com.usuario).username));
            }
            return new CommentBlock(final_comments, existMoreComms);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public CommentBlock GetCommentsByTagName(string name, int startIndex, int count) //Throw exception
        {
            List<Comentario> comms = TagDao.FindCommentsByTagName(name, startIndex, count + 1);

            bool existMoreComms = (comms.Count == count + 1);

            if (existMoreComms)
                comms.RemoveAt(count);
            List<CommentDTO.CommentDTO> final_comments = new List<CommentDTO.CommentDTO>();
            foreach (Comentario com in comms)
            {
                final_comments.Add(new CommentDTO.CommentDTO(com, UserDao.Find(com.usuario).username));
            }
            return new CommentBlock(final_comments, existMoreComms);
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUses(long tagId) {
            Etiqueta tag = FindById(tagId);
            if (tag == null)
                throw new InstanceNotFoundException(tagId,
                    typeof(Etiqueta).FullName);
            tag.numUsos = TagDao.GetCommentNumberOfTag(tagId);
            TagDao.Update(tag);
        }
        [Transactional]
        public TagBlock GetTagsByBlock(int startIndex, int count) {

            List<Etiqueta> tags =  TagDao.GetTagsByBlock(startIndex, count + 1);

            bool existMoreTags = (tags.Count == count + 1);

            if (existMoreTags)
                tags.RemoveAt(count);

            return new TagBlock(tags, existMoreTags);
        }
    }
}
