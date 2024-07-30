using System;
namespace rs.Helper
{
	public interface IHttpHelerClient
	{
        /// <summary>
        /// Asynchronously sends an HTTP GET request to the specified URL and deserializes the response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which the response data should be deserialized.</typeparam>
        /// <param name="url">The URL to send the HTTP GET request to.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the deserialized response 
        /// of type <typeparamref name="T"/>. Returns <c>null</c> if the request fails or the response cannot be deserialized.
        /// </returns>
        public Task<T?> HttpGetRequest<T>(string url);

    }
}

