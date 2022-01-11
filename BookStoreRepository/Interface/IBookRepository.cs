using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        int AddBook(BookModel bookModel);

        public int UpdateBookDetails(BookModel bookModel);

        BookModel RetrieveBookDetails(int bookId);

    }
}