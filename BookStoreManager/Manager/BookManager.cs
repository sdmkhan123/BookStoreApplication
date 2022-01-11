using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {

        private readonly IBookRepository bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public int AddBook(BookModel bookModel)
        {
            try
            {
                return this.bookRepository.AddBook(bookModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                return this.bookRepository.UpdateBookDetails(bookModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
