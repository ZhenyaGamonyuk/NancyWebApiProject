using Moq;
using Nancy.Testing;
using NancyWebApiProject.Modules;
using NancyWebApiProject.Services;
using NUnit.Framework;

namespace NancyWebApiProject.Tests.Modules
{
    [TestFixture]
    internal sealed class ArticleModuleTests
    {
        private Mock<IArticleService> _articleServiceMock;
        private ArticleModule _articleModule;
        private Browser _browser;

        [SetUp]
        public void SetUp()
        {
            _articleServiceMock = new Mock<IArticleService>();
            _articleModule = new ArticleModule(_articleServiceMock.Object);
            _browser = new Browser(with => with.Module(_articleModule));
        }

        [TestCase("/")]
        [TestCase("/list/home")]
        [TestCase("/list/home/first")]
        [TestCase("/list/home/2020-02-15")]
        [TestCase("/article/39Af7IB")]
        [TestCase("/group/home")]
        public void ShouldReturnStatusOkIfRouteExists(string route)
        {
            // Act
            BrowserResponse result = _browser.Get(route).Result;

            // Assert
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
        }
    }
}
