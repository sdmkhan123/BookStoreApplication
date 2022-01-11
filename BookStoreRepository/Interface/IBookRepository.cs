using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        int AddBook(BookModel bookModel);
    }
}