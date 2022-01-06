using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult UserSignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                int result = this.manager.UserSignUp(signUpModel);
                if (result !=0)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration is successful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration is Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                int result = this.manager.Login(loginModel);
                if (result != 0)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Login is successful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login is Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
