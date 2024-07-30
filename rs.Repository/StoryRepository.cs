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

        /// <inheritdoc />
        public async Task<List<Story>> GetStoriesAsync(int page, int pageSize)
        {
            var storiesIds = _cache.Get<List<int>>(HttpHelerClient.case_key_stories).Take(200).ToList() ?? new List<int>();
            var startIndex = (page - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize, storiesIds.Count);

            if (startIndex >= endIndex)
            {
                throw new ArgumentException("Invalid data");
            }

            var storyTasks = new List<Task<Story?>>();

            for (int i = startIndex; i < endIndex; i++)
            {
                var storyTask = _httpHelerClient.HttpGetRequest<Story>($"item/{storiesIds[i]}");
                storyTasks.Add(storyTask);
            }

            try
            {
                var storiesArray = await Task.WhenAll(storyTasks);
                var stories = storiesArray.Where(story => story != null).ToList();
                return stories!;
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }
    }
}

