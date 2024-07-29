using System;
using rs.model;

namespace rs.Contract
{
	public interface IStory
	{
        Task<List<Story>> GetStoriesAsync(int page, int pageSize);
    }
}

