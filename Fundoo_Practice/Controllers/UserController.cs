using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using System;
using System.Linq;

namespace Fundoo_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(RegistrationModel registration)
        {
          
                var result = userBusiness.Registration(registration);

                if (result != null)
                {
                    return Ok(new { success = true, message = "SuccessFull", data = result });
                }
                else 
                {
                return BadRequest(new { success = false, message = "UnsuccessFull" });
                }
          
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                var res = userBusiness.UserLogin(login);

                if (res != null)
                {
                    return Ok(new { success = true, message = "SuccessFull", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "UnSuccessFull" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = userBusiness.ForgotPassword(email);

                if(result != null)
                {
                    return Ok(new { success = true, message = "Reset Link is Sent to your Registered Email", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Email is not Registered" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword")]

        public IActionResult ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value.ToString();
                var res = userBusiness.ResetPassword(resetPassword, email);

                if(res != null)
                {
                    return Ok(new { success = true, message = "Password has been reset", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password is not changed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
