using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo repo;

        public UserBusiness(IUserRepo repo)
        {
            this.repo = repo;
        }

        public User_Entity Registration(RegistrationModel registration)
        {
            try
            {
                return repo.Registration(registration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User_Entity UserLogin(LoginModel login)
        {
            try
            {
                return repo.UserLogin(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return repo.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(ResetPasswordModel reset, string Email)
        {
            try
            {
                return repo.ResetPassword(reset, Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
