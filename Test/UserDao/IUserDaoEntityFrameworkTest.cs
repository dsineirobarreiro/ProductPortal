using PracticaMad.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PracticaMad.Model;
using PracticaMad.Model.UserDAO;
using System.Collections.Generic;
using System.Transactions;

namespace PracticaMad.Model.UserDAO.Tests
{
    [TestClass()]
    public class IUserDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static IUserDao userDao;

        private TransactionScope transactionScope;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userDao = kernel.Get<IUserDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
        }

        #endregion Additional test attributes

        /// <summary>
        /// A test for FindByUserId method.
        /// </summary>
        [TestMethod()]
        public void FindByUserNameText() {
            string username = "die";
            Usuario user = new Usuario();
            user.apellidos = "García López";
            user.email = "diegogarciaaa@gmail.com";
            user.username = "die";
            user.nombre = "Diego";
            user.pwd = "omaigad";
            userDao.Create(user);
            Usuario user2 = userDao.FindByUsername(username);

            Assert.IsTrue(user2.Equals(user));

        }
    }
}