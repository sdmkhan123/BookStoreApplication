using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedbackManager;

        public FeedbackController(IFeedbackManager feedbackManager)
        {
            this.feedbackManager = feedbackManager;
        }

        [HttpPost]
        [Route("api/addFeedbacks")]
        public IActionResult AddFeedback([FromBody] FeedbackModel feedbackModel)
        {
            try
            {
                string result = this.feedbackManager.AddFeedback(feedbackModel);
                if (result.Equals("Feedback added successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
