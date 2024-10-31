using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace PracticaMad.Model.UserDAO
{
    public class UserDaoEntityFramework : GenericDaoEntityFramework<Usuario, Int64>, IUserDao
    {

        public Usuario FindByUsername(string username)
        {
            DbSet<Usuario> users = Context.Set<Usuario>();

            var result =
                (from a in users
                 where a.username == username
                 select a).FirstOrDefault();
            if (result == null)
                throw new InstanceNotFoundException(username, typeof(Usuario).FullName);
            return result;
        }
    }
}
