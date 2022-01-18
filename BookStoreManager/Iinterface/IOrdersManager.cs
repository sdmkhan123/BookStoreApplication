using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IOrdersManager
    {
        string AddOrder(OrdersModel ordersModel);

        List<OrdersModel> RetrieveOrderDetails(int userId);
    }
}