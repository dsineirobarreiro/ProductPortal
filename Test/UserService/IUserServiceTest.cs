using PracticaMad.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using PracticaMad.Model.UserServiceNS.Util;

namespace PracticaMad.Model.UserServiceNS.Tests
{
    /// <summary>
    ///This is a test class for IAccountServiceTest and is intended
    ///to contain all IAccountServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IUserServiceTest
    {
        private static IKernel kernel;
        private static IUserService UserService;

        // Variables used in several tests are initialized here
        private const long userId = 123456;

        private const double balance = 10;
        private const long NON_EXISTENT_ACCOUNT_ID = -1;
        private const long NON_EXISTENT_USER_ID = -1;

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
            UserService = kernel.Get<IUserService>();
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
        ///A test for CreateAccount
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            Usuario user = UserService.RegisterUser("die", "pwd",new UserProfileDetails ("Diego", "García López", "diegogarciaaa@gmail.com"));
            Assert.IsTrue(user.email == "diegogarciaaa@gmail.com");
            Assert.IsTrue(user.username == "die");
            Assert.IsTrue(user.pwd == PasswordEncrypter.Crypt("pwd"));
            Assert.IsTrue(user.nombre == "Diego");
            Assert.IsTrue(user.apellidos == "García López");
            Assert.IsNull(user.idioma);
            Assert.IsNull(user.nacionalidad);
            Assert.IsNotNull(user.userID);
        }

        [TestMethod()]
        public void LoginUserTest() {

            Usuario user = UserService.RegisterUser("die", "pwd", new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com"));

            LoginResult res = UserService.LoginUser("die", "pwd", true);
            Assert.IsTrue(res.Username == user.username);
            Assert.IsTrue(res.EncryptedPassword == user.pwd);
        }

        [TestMethod()]
        public void UpdateUserTest() {
            UserProfileDetails profileDetails =new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");
            Usuario user = UserService.RegisterUser("die", "pwd", profileDetails);

            UserProfileDetails newProfileDetails = new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");

            UserService.UpdateUser(user.userID, newProfileDetails);

            UserDTO.UserDTO user2 = UserService.Find(user.userID);

            Assert.IsTrue(user2.email == newProfileDetails.Email);
        }

        [TestMethod()]
        public void FindTest() {
            UserProfileDetails profileDetails = new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");
            Usuario user = UserService.RegisterUser("die", "pwd", profileDetails);
            UserDTO.UserDTO user2 = UserService.Find(user.userID);

            Assert.IsTrue(user.userID == user2.userID);
            Assert.IsTrue(user.username == user2.username);
            Assert.IsTrue(user.nombre == user2.nombre);
            Assert.IsTrue(user.apellidos == user2.apellidos);
            Assert.IsTrue(user.email == user2.email);
        }

        [TestMethod()]
        public void FindByUsernameTest() {
            UserProfileDetails profileDetails = new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");
            Usuario user = UserService.RegisterUser("die", "pwd", profileDetails);
            UserDTO.UserDTO user2 = UserService.FindByUsername(user.username);

            Assert.IsTrue(user.userID == user2.userID);
            Assert.IsTrue(user.username == user2.username);
            Assert.IsTrue(user.nombre == user2.nombre);
            Assert.IsTrue(user.apellidos == user2.apellidos);
            Assert.IsTrue(user.email == user2.email);
        }

        [TestMethod()]
        public void FindProfileDetailsTest() {
            UserProfileDetails profileDetails = new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");
            Usuario user = UserService.RegisterUser("die", "pwd", profileDetails);
            UserProfileDetails profileDetails2 = UserService.FindUserProfileDetails(user.userID);
            Assert.AreEqual(profileDetails,profileDetails2);
        }

        [TestMethod()]
        public void ChangePasswordTest()
        {
            UserProfileDetails profileDetails = new UserProfileDetails("Diego", "García López", "diegogarciaaa@gmail.com");
            Usuario user = UserService.RegisterUser("die", "pwd", profileDetails);
            UserService.ChangePassword(user.userID,"pwd","patata");
            Assert.IsTrue(user.pwd == PasswordEncrypter.Crypt("patata"));
        }
    }
}