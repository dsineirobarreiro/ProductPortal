using PracticaMad.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PracticaMad.Model.ValoracionServiceNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using PracticaMad.Model.UserServiceNS;

namespace PracticaMad.Model.ValoracionServiceNS.Tests
{
    [TestClass()]
    public class ValoracionServiceTests
    {

        private static IKernel kernel;
        private static IValoracionService valService;
        private static IUserService userService;

        // Variables used in several tests are initialized here
        private const long compradorId = 1;
        private const long productId = 1;
        private const long vendor = 1;
        private const int rating = 3;
        private const long NON_EXISTENT_VAL_ID = -1;

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
            valService = kernel.Get<IValoracionService>();
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

        #endregion Additional test attributes

        [TestMethod()]
        public void AddValoracionTest()
        {
            Valoracion actual = valService.AddValoracion(compradorId,productId,rating);

            Valoracion expected = valService.FindValoracionByValId(actual.valID);

            Assert.AreEqual(expected.valID, actual.valID);
            Assert.AreEqual(expected.comprador, actual.comprador);
            Assert.AreEqual(expected.puntuacion, actual.puntuacion);
            Assert.AreEqual(expected.producto, actual.producto);
            UserDTO.UserDTO user = userService.Find(1);
            Assert.AreEqual(user.score, 2);
        }

        [TestMethod()]
        public void FindValoracionByVendorTest()
        {
            ValoracionBlock vals;
            int numberOfVals = 1;
            int count = 1;
            int startIndex = 0;

            do
            {
                vals = valService.FindValoracionByVendor(vendor, startIndex, count);
                Assert.IsTrue(vals.Valoraciones.Count <= count);
                startIndex += count;
            } while (vals.ExistMoreValoraciones);

            Assert.AreEqual(numberOfVals,
                startIndex - count + vals.Valoraciones.Count);
        }

        /// <summary>
        ///A test for a non existent Valoracion
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindNonExistentValTest()
        {
            Valoracion actual =
                valService.FindValoracionByValId(NON_EXISTENT_VAL_ID);
        }
    }
}