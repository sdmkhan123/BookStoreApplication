using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class BookController : ControllerBase
    {
        private readonly IBookManager manager;
        public BookController(IBookManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/AddBook")]
        public IActionResult AddBook([FromBody] BookModel bookmodel)
        {
            try
            {
                var result = this.manager.AddBook(bookmodel);
                if (result != 0)
                {

                    return this.Ok(new { Status = true, Message = "New Book added Successfully !", data = result });
                }
                else
                {

                    return this.BadRequest(new  { Status = false, Message = "Failed to add new book", data = result });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/updateBook")]
        public IActionResult UpdateBookDetails([FromBody] BookModel bookModel)
        {
            try
            {
                int result = this.manager.UpdateBookDetails(bookModel);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Details Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Update book details Unsuccessful", data = result });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/getBookDetails")]
        public IActionResult RetrieveBookDetails(int bookId)
        {
            try
            {
                BookModel result = this.manager.RetrieveBookDetails(bookId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Retrieval of book details succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<BookModel>() { Status = false, Message = "Bookid doesnt exists", Data = result });
                }

            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                int result = this.manager.DeleteBook(bookId);
                if (result == 1)
                {
                    return this.Ok(new { Status = true, Message = "Book details deleted successfully", Data = result });
                    }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Bookid does not exists", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/getAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.manager.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<object>() { Status = true, Message = "Retrieval all book details succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No book exists" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
