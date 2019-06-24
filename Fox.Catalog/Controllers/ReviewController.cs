using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Fox.Catalog.Infrastructure.Interfaces;
using Fox.Catalog.Models.ViewModels.Review;
using Fox.Common.Extensions;
using Fox.Common.Infrastructure;
using Fox.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fox.Catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidationModel]
    public class ReviewController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IReviewService ReviewService;

        public ReviewController(IReviewService reviewService, ILogger logger)
        {
            this.ReviewService = reviewService;
            this.Logger = logger;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<ReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetReviewRequest request)
        {
            try
            {
                var reviews = await ReviewService.GetAll(request);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }

        [HttpPost("Save")]
        [ProducesResponseType(typeof(ReviewResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Save([FromBody]CreateReviewRequest request)
        {
            try
            {
                request.UserId = User.GetUserId();

                var result = await ReviewService.Save(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "Fox.Catalog.Api");
                return BadRequest(ex);
            }
        }
    }
}