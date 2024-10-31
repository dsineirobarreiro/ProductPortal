using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using PracticaMad.Model.PuntuacionDAO;
using PracticaMad.Model.UserDAO;
using PracticaMad.Model.UserServiceNS.Exceptions;
using PracticaMad.Model.UserServiceNS.Util;
using System;

namespace PracticaMad.Model.UserServiceNS
{
    public class UserService : IUserService
    {
        public UserService() { }

        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public IPuntuacionDAO PuntuacionDao { private get; set; }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public Usuario RegisterUser(String username, String password, UserProfileDetails userProfileDetails)
        {
            try
            {
                UserDao.FindByUsername(username);

                throw new DuplicateInstanceException(username,
                    typeof(Usuario).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(password);

                Usuario user = new Usuario();

                user.username = username;
                user.pwd = encryptedPassword;
                user.nombre = userProfileDetails.FirstName;
                user.apellidos = userProfileDetails.Lastname;
                user.email = userProfileDetails.Email;
                user.idioma = userProfileDetails.Language;
                user.nacionalidad = userProfileDetails.Country;

                UserDao.Create(user);
                Puntuacion score = new Puntuacion();
                score.numValoraciones = 0;
                score.puntuacion1 = 0;
                score.userId = user.userID;
                PuntuacionDao.Create(score);
                return user;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUser(long userId, UserProfileDetails userProfileDetails)
        {
            Usuario user = UserDao.Find(userId);

            user.nombre = userProfileDetails.FirstName;
            user.apellidos = userProfileDetails.Lastname;
            user.email = userProfileDetails.Email;
            user.idioma = userProfileDetails.Language;
            user.nacionalidad = userProfileDetails.Country;
            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult LoginUser(String username, String password, bool passwordIsEncrypted)
        {
            Usuario user = UserDao.FindByUsername(username);

            String storedPassword = user.pwd;

            if (!passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(username);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(username);
                }
            }

            return new LoginResult(user.userID, user.username,
                storedPassword, user.idioma, user.nacionalidad);

        }
        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public void UpdateUserScore(String username, double score) {
            Usuario user = UserDao.FindByUsername(username);
            Puntuacion puntuacion = PuntuacionDao.Find(user.userID);
            double prev_score = puntuacion.puntuacion1;
            prev_score = (double)(prev_score * puntuacion.numValoraciones);
            double final_score = (double)((prev_score + score) / (puntuacion.numValoraciones + 1));
            puntuacion.puntuacion1 = final_score;
            puntuacion.numValoraciones++;
            PuntuacionDao.Update(puntuacion);
        }
        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public UserDTO.UserDTO Find(long userId) {
            Usuario user = UserDao.Find(userId);
            Puntuacion puntuacion = PuntuacionDao.Find(userId);
            return new UserDTO.UserDTO(user, puntuacion.puntuacion1, (int)puntuacion.numValoraciones);
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public UserDTO.UserDTO FindByUsername(string username)
        {
            Usuario user = UserDao.FindByUsername(username);
            Puntuacion puntuacion = PuntuacionDao.Find(user.userID);
            return new UserDTO.UserDTO(user, puntuacion.puntuacion1, (int)puntuacion.numValoraciones);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            Usuario userProfile = UserDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.nombre,
                    userProfile.apellidos, userProfile.email,
                    userProfile.idioma, userProfile.nacionalidad);

            return userProfileDetails;
        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            Usuario userProfile = UserDao.Find(userProfileId);
            String storedPassword = userProfile.pwd;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.username);
            }

            userProfile.pwd =
            PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(userProfile);
        }
    }
}
