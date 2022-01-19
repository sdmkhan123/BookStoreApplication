using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IFeedbackRepository
    {
        string AddFeedback(FeedbackModel feedbackModel);
    }
}