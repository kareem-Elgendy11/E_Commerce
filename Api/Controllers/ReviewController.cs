using Application.Dtos.ReviewDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,user")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private  string GetUSerId()=>User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("All/{productId}")]
        public async Task<IActionResult> GetAllReviews(string productId)
        {
            // Implementation for getting all reviews by product ID
            if(productId == null)
            {
                return BadRequest("Product ID cannot be null.");
            }
            var result = await _reviewService.GetAllReviews(productId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
                return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] createReviewdto dto)
        {
            // Implementation for creating a new review
            var result = await _reviewService.CreateReview(dto, GetUSerId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] updateReviewdto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(reviewId == null)
            {
                return BadRequest("Review ID cannot be null.");
            }
            // Implementation for updating a review
            var result = await _reviewService.UpdateReview(reviewId,  GetUSerId(),dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (reviewId == null)
            {
                return BadRequest("Review ID cannot be null.");
            }
            // Implementation for deleting a review
            var result = await _reviewService.DeleteReview(reviewId, GetUSerId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReview(string reviewId)
        {
            if (reviewId == null)
            {
                return BadRequest("Review ID cannot be null.");
            }
            var result = await _reviewService.GetReviewById(reviewId, GetUSerId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("UserReviews/")]
        public async Task<IActionResult> GetUserReviews()
        {
            var result = await _reviewService.GetAllReviewByUser(GetUSerId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
