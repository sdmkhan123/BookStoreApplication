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
        [Route("updateBook")]
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
    }
}
