using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IOrdersRepository
    {
        string AddOrder(OrdersModel ordersModel);
    }
}