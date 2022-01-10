using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<IActionResult> UserSignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                int result = await this.manager.UserSignUp(signUpModel);
                if (result !=0)
                {
                    return this.Ok(new { Status = true, Message = "Registration is successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Registration is Unsuccessful", data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                int result = await this.manager.Login(loginModel);
                if (result == 2)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Login is successful" });
                }
                else if (result == 1)
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password is incorrect and Login is Unsuccessful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Email is incorrect and Login is Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                int result = await this.manager.ResetPassword(resetPasswordModel);
                if (result!=0)
                {
                    return this.Ok(new { Status = true, Message = "Password reset is successful" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "The Email Id does not exist. Password reset is unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgotpassword")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                int result = this.manager.ForgotPassword(Email);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Email is sent successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Email Id does not exist", data = result });
                    }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
