using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public class OrdersManager : IOrdersManager
    {
        private readonly IOrdersRepository ordersRepository;

        public OrdersManager(IOrdersRepository orderRepository)
        {
            this.ordersRepository = orderRepository;
        }

        public string AddOrder(OrdersModel ordersModel)
        {
            try
            {
                return this.ordersRepository.AddOrder(ordersModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<OrdersModel> RetrieveOrderDetails(int userId)
        {
            try
            {
                return this.ordersRepository.RetrieveOrderDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
