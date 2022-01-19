using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IFeedbackManager
    {
        string AddFeedback(FeedbackModel feedbackModel);
    }
}