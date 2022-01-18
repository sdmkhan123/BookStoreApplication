using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controller
{
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersManager ordersManager;

        public OrdersController(IOrdersManager ordersManager)
        {
            this.ordersManager = ordersManager;
        }

        [HttpPost]
        [Route("addOrders")]
        public IActionResult AddOrder([FromBody] OrdersModel ordersModel)
        {
            try
            {
                var result = this.ordersManager.AddOrder(ordersModel);
                if (result.Equals("Ordered successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult RetrieveOrderDetails(int userId) ////frombody attribute says value read from body of the request
        {
            try
            {
                var result = this.ordersManager.RetrieveOrderDetails(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
