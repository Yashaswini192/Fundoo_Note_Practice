using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public User_Entity Registration(RegistrationModel registration);

        public string UserLogin(LoginModel login);

        public string ForgotPassword(string email);

        public bool ResetPassword(ResetPasswordModel reset, string Email);
    }
}
