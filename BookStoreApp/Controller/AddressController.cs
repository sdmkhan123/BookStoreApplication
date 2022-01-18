using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager addressManager;

        public AddressController(IAddressManager addressManager)
        {
            this.addressManager = addressManager;
        }

        [HttpPost]
        [Route("api/AddUserAddress")]
        public IActionResult AddAddress([FromBody] AddressModel addressModel)
        {
            try
            {
                int result = this.addressManager.AddAddress(addressModel);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Added New User Address Successfully !", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add user address, Try again!", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/updateaddress")]
        public IActionResult UpdateAddress([FromBody] AddressModel addressModel)
        {
            int result = this.addressManager.UpdateAddress(addressModel);
            try
            {
                if (result == 1)
                {
                    return this.Ok(new  { Status = true, Message = "Address updated successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to update address", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
