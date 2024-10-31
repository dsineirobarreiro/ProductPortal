using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using PracticaMad.Model.CommentDao;
using PracticaMad.Model.ProductoDAO;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.PuntuacionDAO;
using PracticaMad.Model.TagDao;
using PracticaMad.Model.TagServiceNS;
using PracticaMad.Model.UserDAO;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.ValoracionDAO;
using PracticaMad.Model.ValoracionServiceNS;
using System.Configuration;
using System.Data.Entity;

namespace PracticaMad.Web.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel();
            
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserDao>().To<UserDaoEntityFramework>();
            kernel.Bind<IPuntuacionDAO>().To<PuntuacionDAOEntityFramework>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IProductoDao>().To<ProductoDaoEntityFramework>();
            kernel.Bind<IValoracionService>().To<ValoracionService>();
            kernel.Bind<IValoracionDAO>().To<ValoracionDAOEntityFramework>();
            kernel.Bind<ICommentDao>().To<CommentDaoEntityFramework>();
            kernel.Bind<ITagService>().To<TagService>();
            kernel.Bind<ITagDao>().To<TagDaoEntityFramework>();
            string connectionString = ConfigurationManager.ConnectionStrings["madEntitiesDiego"].ConnectionString;
            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}