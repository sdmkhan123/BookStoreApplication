using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IOrdersManager
    {
        string AddOrder(OrdersModel ordersModel);
    }
}