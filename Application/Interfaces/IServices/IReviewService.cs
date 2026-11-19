using Application.Dtos.ProductDto;
using Application.Dtos.ReviewDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IReviewService
    {
        public Task<ApiResponse<ReviewAllDto>> GetReviewById(string reviewId, string UserId);
        public Task<ApiResponse<List<ReviewAllDto>>> GetAllReviews(string productId);
        public Task<ApiResponse<List<ReviewAllDto>>> GetAllReviewByUser(string UserId);
        public Task<ApiResponse<string>> CreateReview(createReviewdto dto, string UserId);
        public Task<ApiResponse<string>> UpdateReview(string ReviewId,string UserId, updateReviewdto dto);
        public Task<ApiResponse<string>> DeleteReview(string reviewId, string UserId);
    }
}
