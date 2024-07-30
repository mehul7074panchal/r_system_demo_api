using System;
using rs.model;

namespace rs.Contract
{
	public interface IStory
	{
        /// <summary>
        /// Asynchronously fetch a paginated list of stories.
        /// </summary>
        /// <param name="page">The page number to retrieve. Must be a positive integer.</param>
        /// <param name="pageSize">The number of stories to retrieve per page. Must be a positive integer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of stories for the specified page.</returns>
        /// <exception cref="ArgumentException">Thrown when either <paramref name="page"/> or <paramref name="pageSize"/> is invalid</exception>
        Task<List<Story>> GetStoriesAsync(int page, int pageSize);
    }
}

