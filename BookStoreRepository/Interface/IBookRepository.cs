using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        int AddBook(BookModel bookModel);

        public int UpdateBookDetails(BookModel bookModel);

        BookModel RetrieveBookDetails(int bookId);

        int DeleteBook(int bookId);

        List<BookModel> GetAllBooks();

    }
}