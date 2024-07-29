

using Microsoft.AspNetCore.Mvc.Testing;

namespace rs.Tests
{
    [TestFixture]
    public class StoryControllerTest
	{
        private HttpClient? _client;

        [SetUp]
        public void SetUp()
        {
            var appFactory = new WebApplicationFactory<Program>();
            _client = appFactory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(1,0)]
        [TestCase(-1,-1)]
        [TestCase(20, 200)]
        [TestCase(30, 500)]
        public async Task GetStory_ReturnBadRequest_WhenInavlidPageOrPageSize(int page, int pageSzie)
        {
           
            // Act
            var response = await _client.GetAsync($"/api/story/GetStory/{page}/{pageSzie}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

        [Test]
        [TestCase(1, 10)]
        [TestCase(1, 50)]
        [TestCase(2, 20)]
        public async Task GetStory_ReturnOk_WhenvalidData(int page, int pageSzie)
        {

            // Act
            var response = await _client.GetAsync($"/api/story/GetStory/{page}/{pageSzie}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
    }
}

