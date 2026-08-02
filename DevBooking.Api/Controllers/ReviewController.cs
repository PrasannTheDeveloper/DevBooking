using System.Security.Claims;
using DevBooking.Application.DTOs.Review;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateReview(CreateReviewRequest request)
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (clientId == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = await _reviewService.CreateReviewAsync(clientId, request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-booking/{bookingId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByBooking(int bookingId)
        {
            var review = await _reviewService.GetByBookingIdAsync(bookingId);

            if (review == null)
            {
                return NotFound("No review found for this booking.");
            }

            return Ok(review);
        }

        [HttpGet("by-developer/{developerProfileId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDeveloper(int developerProfileId)
        {
            var reviews = await _reviewService.GetByDeveloperProfileIdAsync(developerProfileId);
            return Ok(reviews);
        }

        [HttpGet("average-rating/{developerProfileId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAverageRating(int developerProfileId)
        {
            var average = await _reviewService.GetAverageRatingAsync(developerProfileId);
            return Ok(new { developerProfileId, averageRating = average });
        }

        [HttpDelete("{reviewId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (clientId == null)
            {
                return Unauthorized();
            }

            try
            {
                await _reviewService.DeleteReviewAsync(clientId, reviewId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{reviewId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateReview(int reviewId, UpdateReviewRequest request)
        {
            var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (clientId == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = await _reviewService.UpdateReviewAsync(clientId, reviewId, request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}