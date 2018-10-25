using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly BeaversDbContext _beaversDbContext;

        public FeedbackRepository(BeaversDbContext beaversDbContext)
        {
            _beaversDbContext = beaversDbContext;
        }


        public void AddFeedback(Feedback feedback)
        {
            _beaversDbContext.Feedbacks.Add(feedback);
            _beaversDbContext.SaveChanges();
        }
    }
}
