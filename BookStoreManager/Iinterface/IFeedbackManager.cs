using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IFeedbackManager
    {
        string AddFeedback(FeedbackModel feedbackModel);

        List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}