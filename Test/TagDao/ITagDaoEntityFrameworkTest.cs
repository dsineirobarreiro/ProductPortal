using PracticaMad.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PracticaMad.Model;
using PracticaMad.Model.TagDao;
using System.Collections.Generic;
using System.Transactions;

namespace PracticaMad.Model.TagDao.Tests
{
    [TestClass()]
    public class ITagDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static ITagDao TagDao;


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
            TagDao = kernel.Get<ITagDao>();
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
        public void FindByNameTest()
        {
            string name = "chulo";
            Etiqueta tag = new Etiqueta();
            tag.nombre = "chulo";
            TagDao.Create(tag);

            Etiqueta tag2 = TagDao.FindByName(name);

            Assert.IsTrue(tag.Equals(tag2));
        }
    }
}