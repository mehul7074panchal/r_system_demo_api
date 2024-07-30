using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using rs.Contract;
using rs.model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : Controller
    {
        private readonly ILogger<StoryController> _logger;
        private readonly IStory _story;

        public StoryController(ILogger<StoryController> logger, IStory story)
        {
            _logger = logger;
            _story = story;
        }


        /// <summary>
        /// Fetch a paginated list of stories.
        /// </summary>
        /// <param name="page">The page number to fetch.</param>
        /// <param name="pageSize">The number of stories per page.</param>
        /// <returns>A response containing the list of stories.</returns>
        [Route("GetStory/{page}/{pageSize}")]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> GetStory(int page, int pageSize)
        {
            try
            {
                if (page <= 0 || pageSize <= 0) {
                    return BadRequest(new Response
                    {
                        Status = "Fail",
                        Message = "Invalid argument",
                    });
                }


                var stories = await _story.GetStoriesAsync(page, pageSize);
                return await Task.FromResult(Ok(new Response
                {
                    Results = stories

                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoryController , GetStory");
                return BadRequest(new Response
                {
                    Status = "Fail",
                    Message = "Invalid argument",
                });
            }
             
        }
    }
}

