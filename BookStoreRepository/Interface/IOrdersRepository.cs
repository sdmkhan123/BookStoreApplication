using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IOrdersRepository
    {
        string AddOrder(OrdersModel ordersModel);

        List<OrdersModel> RetrieveOrderDetails(int userId);
    }
}