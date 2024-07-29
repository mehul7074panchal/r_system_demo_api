using System;
namespace rs.Helper
{
	public interface IHttpHelerClient
	{
        public Task<T?> HttpGetRequest<T>(string url);

    }
}

