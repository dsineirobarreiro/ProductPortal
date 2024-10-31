using System;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace PracticaMad.Model.UserDAO
{
    public interface IUserDao : IGenericDao<Usuario, Int64>
    {

        Usuario FindByUsername(String username);
    }
}
