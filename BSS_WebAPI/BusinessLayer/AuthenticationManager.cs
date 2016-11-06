using System;
using System.Collections.Generic;
using System.Linq;
using BSS_DataAccess;
using BSS_WebAPI.Models.DTO;
using BSS_WebAPI.Utils;
using BSS_WebAPI.Models;

namespace BSS_WebAPI.BusinessLayer
{
    public class LoginManager
    {
        private BeSmartSportsEntities _context { get; } = new BeSmartSportsEntities();

        public Login Verify(Login login)
        {
            if (string.IsNullOrEmpty(login.username) || string.IsNullOrEmpty(login.password))
            {
                login.errors.Add("Please provide username and password!");
                return login;
            }

            var user = _context.UserLogin(login.username, login.password).FirstOrDefault();

            if (user == null)
            {
                login.errors.Add("User does not exist!");
                return login;
            }

            if (!user.IsActive)
            {
                login.errors.Add("User is locked!");
                return login;
            }

            if (user.ROLE == "unknown")
            {
                login.errors.Add("User is not linked to any company!");
                return login;
            }

            if (!login.password.Equals(user.Password))
            {
                login.errors.Add("Incorrect password!");
                return login;
            }

            if (!user.IsEmailVerified) //inactive user 
            {
                login.errors.Add(GlobalSettings.Instance.InvalidLoginMessage);
                return login;
            }

            login.password = string.Empty;
            login.userId = user.Id;
            login.role = user.ROLE;
            login.userGuid = user.Guid;
            login.email = user.Email;
            //login.logoPath = _context.COMPANies.First(x=>x.CONTACT_USERID == user.ID).LOGO_PATH;

            return login;
        }

        public Login ActivateUser(string guid, string token, IDataAccessRepository<Models.DTO.User, int> userRepository)
        {
            var login = new Login();

            var user = _context.Users.Where(x => x.Guid == new Guid(guid)).FirstOrDefault();
            if (user == null)
            {
                login.errors.Add("User does not exist!");
                return login;
            }

            //if (user.IS_LOCKED)
            //{
            //    login.errors.Add("User is locked!");
            //    return login;
            //}

            if (user.IsEmailVerified)
            {
                login.errors.Add("User has already been approved!");
                return login;
            }


            if (!token.Equals(user.Token))
            {
                login.errors.Add("Incorrect security token is specified!");
                return login;
            }

            user.IsEmailVerified = true;
          //  userRepository.Put(user.Id, user);

            login.username = user.FirstName;
            return login;

        }
    }
}