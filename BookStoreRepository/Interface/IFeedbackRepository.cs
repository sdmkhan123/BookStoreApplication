using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IFeedbackRepository
    {
        string AddFeedback(FeedbackModel feedbackModel);

        List<FeedbackModel> RetrieveOrderDetails(int bookId);
    }
}