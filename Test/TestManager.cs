using Ninject;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.UserDAO;
using PracticaMad.Model.ValoracionDAO;
using PracticaMad.Model.ValoracionServiceNS;
using System.Configuration;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.ProductoDAO;
using PracticaMad.Model.CommentDao;
using System.Data.Entity;
using PracticaMad.Model.TagServiceNS;
using PracticaMad.Model.TagDao;
using PracticaMad.Model.PuntuacionDAO;

namespace PracticaMad.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserDao>().To<UserDaoEntityFramework>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IProductoDao>().To<ProductoDaoEntityFramework>();
            kernel.Bind<ICommentDao>().To<CommentDaoEntityFramework>();
            kernel.Bind<IValoracionService>().To<ValoracionService>();
            kernel.Bind<IValoracionDAO>().To<ValoracionDAOEntityFramework>();
            kernel.Bind<ITagService>().To<TagService>();
            kernel.Bind<ITagDao>().To<TagDaoEntityFramework>();
            kernel.Bind<IPuntuacionDAO>().To<PuntuacionDAOEntityFramework>();
            string connectionString = ConfigurationManager.ConnectionStrings["madEntitiesDiego"].ConnectionString;
            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}