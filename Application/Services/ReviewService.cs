using Application.Dtos.ReviewDto;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo repo;
        private readonly IProductRepo productRepo;
        private readonly IMapper mapper;

        public ReviewService(IReviewRepo repo, IProductRepo productRepo, IMapper mapper)
        {
            this.repo = repo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<string>> CreateReview(createReviewdto dto,string UserID)
        {
            try
            {
                if (dto == null)
                {
                    return ApiResponse<string>.Faild("Invalid review data.");
                }
                var product = await productRepo.GetProductById(dto.ProductId);
                if (product == null)
                {
                    return ApiResponse<string>.Faild("Product not found.");
                }
                var mapreview = mapper.Map<Reviews>(dto);
                mapreview.UserId = UserID;
                await repo.CreateReview(mapreview);
                await repo.Save();
                return ApiResponse<string>.Success("Review created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return ApiResponse<string>.Faild("An error occurred while creating the review.");
            }   
        }

        public async Task<ApiResponse<string>> DeleteReview(string reviewId, string UserId)
        {
            if(string.IsNullOrEmpty(reviewId))
            {
                return ApiResponse<string>.Faild("Invalid review ID.");
            }
            // Implementation for deleting review
            var foundreview = await repo.GetReviewById(reviewId);
            if (foundreview == null)
            {
                return ApiResponse<string>.Faild("Review not found.");
            }
            if (foundreview.UserId != UserId)
            {
                return ApiResponse<string>.Faild("You are not authorized to delete this review.");
            }
            await repo.DeleteReview(foundreview);
            await repo.Save();
            return ApiResponse<string>.Success("Review deleted successfully.");
        }

        public async Task<ApiResponse<List<ReviewAllDto>>> GetAllReviews(string productId)
        {
            var reviews = await repo.GetAllReview(productId);
            var reviewDtos = reviews.Select(r =>new ReviewAllDto
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                UserName = r.User!.UserName!
            }).ToList();
            return ApiResponse<List<ReviewAllDto>>.Success(reviewDtos);
        }
        public async Task<ApiResponse<List<ReviewAllDto>>> GetAllReviewByUser(string UserId)
        {
            var reviews = await repo.GetAllReviewByUser(UserId);
            var reviewDtos = reviews.Select(r => new ReviewAllDto
            {
                Id = r.Id,
                Comment = r.Comment,
                Rating = r.Rating,
                UserName = r.User!.UserName!
            }).ToList();
            return ApiResponse<List<ReviewAllDto>>.Success(reviewDtos);
        }

        public async Task<ApiResponse<ReviewAllDto>> GetReviewById(string reviewId,string UserId)
        {
            var review = await repo.GetReviewById(reviewId);

            if (review == null)
            {
                return ApiResponse<ReviewAllDto>.Faild("Review not found.");
            }
            var mapreview = mapper.Map<ReviewAllDto>(review);
            mapreview.UserName = review.User!.UserName!; // Assuming the User entity has a UserName property
            return ApiResponse<ReviewAllDto>.Success(mapreview);
        }

        public async  Task<ApiResponse<string>> UpdateReview(string ReviewId, string UserId, updateReviewdto dto)
        {
            var review = await repo.GetReviewById(ReviewId); 
            if(review.UserId != UserId)
            {
                return ApiResponse<string>.Faild("You are not authorized to update this review.");
            }
            if (review == null)
            {
                return ApiResponse<string>.Faild("Review not found.");
            }
            // Update the review properties with the new values from dto
            if (dto.Comment != null)
            { review.Comment = dto.Comment; }
            if (dto.Rating !=-1)
            { review.Rating = dto.Rating; }
            await repo.UpdateReview(review);
            await repo.Save();
            return ApiResponse<string>.Success("Review updated successfully.");
        }
    }
}
