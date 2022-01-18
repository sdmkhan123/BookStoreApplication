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
    }
}
