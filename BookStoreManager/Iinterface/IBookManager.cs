using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        int AddBook(BookModel bookModel);

        int UpdateBookDetails(BookModel bookModel);

        BookModel RetrieveBookDetails(int bookId);

        int DeleteBook(int bookId);

        List<BookModel> GetAllBooks();
    }
}