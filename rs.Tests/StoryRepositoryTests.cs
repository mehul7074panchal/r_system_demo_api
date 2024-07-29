using System;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json.Linq;
using rs.Helper;
using rs.model;
using rs.Repository;

namespace rs.Tests
{
	public class StoryRepositoryTests
	{
        private Mock<IMemoryCache> _mockCache;
        private Mock<IHttpHelerClient> _mokHttp;
        private StoryRepository _story;

        [SetUp]
        public void SetUp()
        {
            _mockCache = new Mock<IMemoryCache>();
            var ids = new List<int>();
            var stories = new List<Story>();
            for (int i = 1; i <= 200; i++)
            {
                ids.Add(i);
                stories.Add(new Story { Id = i, Title = "Story-" + i, URL = "Story-" + i+".com" });
            }
            object cacheEntry;
            _mockCache.Setup(m => m.TryGetValue(It.IsAny<object>(), out cacheEntry))
                      .Callback(new TryGetValueCallback<object, object>((object key, out object value) => value = ids))
                      .Returns(true);
            _mokHttp = new Mock<IHttpHelerClient>();
            _mokHttp.Setup(m => m.HttpGetRequest<Story>(It.IsAny<string>())).Returns(Task.FromResult(new Story { Id = 1, Title = "Story-1", URL = "Story-1.com" }));
            _story = new StoryRepository(_mockCache.Object, _mokHttp.Object);
        }
        private delegate void TryGetValueCallback<TKey, TValue>(TKey key, out TValue value);

        [Test]
        [TestCase(-1, -1)]
        [TestCase(20, 200)]
        [TestCase(30, 500)]
        public void GetStoriesAsync_WithInvalidPageOrPageSize_ThrowsArgumentExceptionn(int page, int pageSize)
        {
            Assert.That(() => _story.GetStoriesAsync(page, pageSize), Throws.ArgumentException);
        }

        [Test]
        [TestCase(1, 10)]
        [TestCase(2, 10)]
        public async Task GetStoriesAsync_WithInvalidPageOrPageSize_GetListOfStory(int page, int pageSize)
        {
            var list = await _story.GetStoriesAsync(page, pageSize);
            Assert.That(() => list, !Is.Null);
        }
    }
}

