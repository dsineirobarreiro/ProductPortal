using PracticaMad.Model.UserDAO;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;

namespace PracticaMad.Model.UserServiceNS
{
    public interface IUserService
    {
        IUserDao UserDao { set; }

        [Transactional]
        Usuario RegisterUser(String username, String password, UserProfileDetails userProfileDetails);

        [Transactional]
        void UpdateUser(long userId, UserProfileDetails userProfileDetails);

        [Transactional]
        LoginResult LoginUser(String username, String password, bool passwordIsEncrypted);

        [Transactional]
        void UpdateUserScore(String username, double score);

        [Transactional]
        UserDTO.UserDTO Find(long userId);

        [Transactional]
        UserDTO.UserDTO FindByUsername(string username);

        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        [Transactional]
        void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword);
    }
}
