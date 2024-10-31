using PracticaMad.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PracticaMad.Model.ProductServiceNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using PracticaMad.Model.UserServiceNS;

namespace PracticaMad.Model.TagServiceNS.Tests
{
    /// <summary>
    ///This is a test class for IAccountServiceTest and is intended
    ///to contain all IAccountServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ITagServiceTest
    {
        private static IKernel kernel;
        private static ITagService TagService;
        private static IProductService productService;
        private static IUserService userService;

        //Due to the limited precision of floating point numbers, the equality
        //operator may provide unexpected results if two numbers are close to
        //each other (e.g. 25 and 25.00000000001). In order to solve this
        //issue, a small margin of error (delta) can be allowed.
        private const double delta = 0.00001;

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
            TagService = kernel.Get<ITagService>();
            productService = kernel.Get<IProductService>();
            userService = kernel.Get<IUserService>();
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
        #endregion
        [TestMethod()]
        public void CreateTagTest()
        {
            Etiqueta tag = TagService.CreateTag("chulo");

            Assert.IsTrue(tag.nombre == "chulo");
            Assert.IsTrue(tag.numUsos == 0);
            Assert.IsNotNull(tag.tagID);
        }

        /*El tag con id 1 incluye una etiqueta con el nombre guay y 0 usos. */

        [TestMethod()]
        public void FindByIdTest() {
            Etiqueta tag = TagService.FindById(1);
            Assert.AreEqual(tag.nombre, "guay");
        }

        [TestMethod()]
        public void UpdateUsesTest() {
            Etiqueta tag = TagService.FindById(1);
            Assert.AreEqual(1, tag.numUsos);

            HashSet<string> tags = new HashSet<string>();
            tags.Add("guay");

            productService.CommentProduct(1, 2, "bueno, tampoco está tan mal", tags);
            
            tag = TagService.FindById(1);
            Assert.AreEqual(1, tag.numUsos);
        }

        [TestMethod()]
        public void GetCommentsByTagTest() {

            Etiqueta tag = TagService.FindById(1);
            int startIndex = 0;
            int count = 1;
            Comentario comment = productService.CommentProduct(1,2,"bueno, tampoco está tan mal", new HashSet<string> { "guay"});
            CommentDTO.CommentDTO final_comm = new CommentDTO.CommentDTO(comment, userService.Find(1).username);
            List<CommentDTO.CommentDTO> comments = new List<CommentDTO.CommentDTO>
            {
                final_comm
            };

            CommentBlock comms2 = TagService.GetCommentsByTag(1,startIndex, count);
            CommentBlock comms3 = TagService.GetCommentsByTagName(tag.nombre, startIndex, count);

            Assert.AreEqual(comments.Count(), 1);
            Assert.AreEqual(comments.Count(), comms2.Comentarios.Count());
            Assert.AreEqual(comments.Count(), comms3.Comentarios.Count());
            Console.WriteLine(comments[0].usuario); Console.WriteLine(comms2.Comentarios[0].usuario);
            Assert.AreEqual(comments[0].comID, comms2.Comentarios[0].comID);
            Assert.AreEqual(comments[0].comID, comms3.Comentarios[0].comID);
        }
    }
}
