using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly Fundoo_Context fundooContext;
        private readonly IConfiguration configuration;
        public UserRepo(Fundoo_Context fundoo , IConfiguration configuration) { 
            this.configuration = configuration;
            this.fundooContext = fundoo;
        }

        public User_Entity Registration(RegistrationModel registration)
        {
            try
            {
                User_Entity user = new User_Entity();

                user.FirstName = registration.FirstName;
                user.LastName = registration.LastName;
                user.Email = registration.Email;
                user.Password = registration.Password;

                fundooContext.UserTable.Add(user);
                fundooContext.SaveChanges();

                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string UserLogin(LoginModel login)
        {
            try
            {
                var user = fundooContext.UserTable.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

                if(user != null)
                {
                    string token = GenerateToken(login.Email, user.Id);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateToken(string email, long UserId)
        {

            try
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                    new Claim("UserID", UserId.ToString()),
                    new Claim("Email", email),

                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //public string GenerateToken(string EmailId, long UserId)
        //{
        //    try
        //    {
        //        var LoginTokenHandler = new JwtSecurityTokenHandler();
        //        var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
        //        var LoginTokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("Email", EmailId.ToString()),
        //                new Claim("UserID", UserId.ToString()),
        //            }),
        //            Expires = DateTime.UtcNow.AddHours(1),
        //            SigningCredentials = new SigningCredentials(LoginTokenKey, SecurityAlgorithms.HmacSha256Signature),
        //        };
        //        var token = LoginTokenHandler.CreateToken(LoginTokenDescriptor);
        //        return LoginTokenHandler.WriteToken(token);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public string ForgotPassword(string email)
        {
            try
            {
                var check = this.fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();

                if (check != null)
                {
                    string token = GenerateToken(check.Email , check.Id);
                    MSMQ msmq = new MSMQ();
                    msmq.SendMessage(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public bool ResetPassword(ResetPasswordModel reset,string Email)
        {
            try
            {
                if (reset.newPassword.Equals(reset.confirmPassword))
                {
                    var result = fundooContext.UserTable.Where(x => x.Email == Email).FirstOrDefault();
                    result.Password = reset.newPassword;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
