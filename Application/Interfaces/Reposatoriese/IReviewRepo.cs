using Application.Dtos.ReviewDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface IReviewRepo
    {
        public Task<List<Reviews>> GetAllReview(string productId);
        public Task<Reviews> GetReviewById(string reviewId );
        public Task<List<Reviews>> GetAllReviewByUser(string UserId);
        public Task CreateReview(Reviews review);
        public Task<bool> UpdateReview(Reviews review);
        public Task<bool> DeleteReview(Reviews review);
        public Task Save();
    }
}
