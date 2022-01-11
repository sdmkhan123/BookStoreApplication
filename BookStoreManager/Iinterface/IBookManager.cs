using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        int AddBook(BookModel bookModel);
    }
}