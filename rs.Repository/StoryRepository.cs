using Microsoft.Extensions.Caching.Memory;
using rs.Contract;
using rs.Helper;
using rs.model;

namespace rs.Repository
{
	public class StoryRepository : IStory
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpHelerClient _httpHelerClient;


        public StoryRepository(IMemoryCache cache, IHttpHelerClient httpHelerClient)
		{
            _cache = cache;
            _httpHelerClient = httpHelerClient;

        }

        public async Task<List<Story>> GetStoriesAsync(int page, int pageSize)
        {
            var storiesIds = _cache.Get<List<int>>(HttpHelerClient.case_key_stories).Take(200).ToList() ?? new List<int>();
            var startIndex = (page - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize, storiesIds.Count);
            var stories = new List<Story>();
            if (startIndex >  endIndex) {

                throw new ArgumentException("Invalid data");
            }
            for(int i = startIndex; i < endIndex; i++)
            {
                try
                {
                    var story = await _httpHelerClient.HttpGetRequest<Story>($"item/{storiesIds[i]}");
                    if (story != null)
                    {
                        stories.Add(story);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            return stories;
        }
    }
}

