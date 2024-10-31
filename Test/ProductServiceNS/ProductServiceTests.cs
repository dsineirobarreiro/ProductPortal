using PracticaMad.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PracticaMad.Model.ProductServiceNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace PracticaMad.Model.ProductServiceNS.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private static IKernel kernel;
        private static IProductService prodService;

        // Variables used in several tests are initialized here
        private const long comId = 1;
        private const string prodName = "mesa";
        private const long userId = 1;
        private const long prodId = 2;
        private const string com = "comment";

        private const long NON_EXISTENT_COM_ID = -1;
        private const long NON_EXISTENT_PROD_ID = -1;

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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            prodService = kernel.Get<IProductService>();
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
        ///A test for FindProductByName
        ///</summary>
        [TestMethod()]
        public void FindProductByNameTest()
        {

            ProductBlock prods;
            int numberOfProducts = 1;
            int count = 1;
            int startIndex = 0;

            do
            {
                prods = prodService.FindProductByName(prodName, startIndex, count);
                Assert.IsTrue(prods.Products.Count <= count);
                startIndex += count;
            } while (prods.Products.Count == count);

            Assert.AreEqual(numberOfProducts,
                startIndex - count + prods.Products.Count);
        }

        [TestMethod()]
        public void CommentProductTest()
        {
            Comentario actual = prodService.CommentProduct(userId, prodId, com);

            Comentario expected = prodService.FindComentario(actual.comID);

            Assert.AreEqual(expected.comID, actual.comID);
            Assert.AreEqual(expected.usuario, actual.usuario);
            Assert.AreEqual(expected.text, actual.text);
            Assert.AreEqual(expected.producto, actual.producto);
        }

        [TestMethod()]
        public void UpdateCommentTest()
        {

            Comentario old = prodService.FindComentario(comId);
            old.text = "com updated";
            Comentario updated = prodService.UpdateComment(old);

            Assert.AreEqual(old.comID, updated.comID);
            Assert.AreEqual(old.usuario, updated.usuario);
            Assert.AreEqual(old.text, updated.text);
            Assert.AreEqual(old.producto, updated.producto);
        }

     /*   [TestMethod()]
        public void RemoveCommentTest()
        {
            prodService.RemoveComment(comId);
            Assert.IsNull(prodService.FindComentario(comId));
        }*/

        [TestMethod()]
        public void SeeProductCommentsTest()
        {

            CommentBlock comms;
            int numberOfProducts = 4;
            int count = 1;
            int startIndex = 0;

            do
            {
                comms = prodService.SeeProductComments(prodId, startIndex, count);
                Assert.IsTrue(comms.Comentarios.Count <= count);
                startIndex += count;
            } while (comms.Comentarios.Count == count);

            Assert.AreEqual(numberOfProducts,
                startIndex - count + comms.Comentarios.Count);
        }

        /// <summary>
        ///A test for a non existent Comentario
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindNonExistentComTest()
        {
            Comentario actual =
                prodService.FindComentario(NON_EXISTENT_COM_ID);
        }

    }
}