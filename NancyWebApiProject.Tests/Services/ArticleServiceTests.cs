using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using NancyWebApiProject.DataProviders;
using NancyWebApiProject.Models;
using NancyWebApiProject.Services;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace NancyWebApiProject.Tests.Services
{
    [TestFixture]
    internal sealed class ArticleServiceTests
    {
        private Mock<IDataProvider> _dataProviderMock;

        private IArticleService _articleService;

        private JToken[] _testArticles;

        [SetUp]
        public void SetUp()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Fake/fakeData.json");
            string jsonString = File.ReadAllText(path, Encoding.UTF8);
            _testArticles = JObject.Parse(jsonString).GetValue("results").ToObject<JToken[]>();

            _dataProviderMock = new Mock<IDataProvider>();

            _articleService = new ArticleService(_dataProviderMock.Object);
        }

        [Test]
        public void GetManyBySection_CallsGetResultDataOnce()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(new JToken[0]);

            // Act
            _articleService.GetManyBySection(It.IsAny<string>());

            // Assert
            _dataProviderMock.Verify(i => i.GetResultData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetManyBySection_ReturnsCorrectData()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(_testArticles);

            // Act
            IEnumerable<ArticleView> result = _articleService.GetManyBySection(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testArticles.Count(), result.Count());
        }

        [Test]
        public void GetFirstItem_CallsGetResultDataOnce()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(new JToken[0]);

            // Act
            _articleService.GetManyBySection(It.IsAny<string>());

            // Assert
            _dataProviderMock.Verify(i => i.GetResultData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetFirstItem_ReturnsCorrectData()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(_testArticles);

            // Act
            ArticleView result = _articleService.GetFirstItem(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetManySortedByUpdatedDate_CallsGetResultDataOnce()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(new JToken[0]);

            // Act
            _articleService.GetManySortedByUpdatedDate(It.IsAny<string>(), It.IsAny<DateTime>());

            // Assert
            _dataProviderMock.Verify(i => i.GetResultData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetManySortedByUpdatedDate_ReturnsCorrectDataSortedByDate()
        {
            // Arrange
            DateTime date = new DateTime(2020,02,15);

            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>()))
                .Returns(_testArticles);

            // Act
            IEnumerable<ArticleView> result = _articleService.GetManySortedByUpdatedDate(It.IsAny<string>(), date);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(46, result.Count());
        }

        [Test]
        public void GetSortedByShortUrl_CallsGetResultDataOnce()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(new JToken[0]);

            // Act
            _articleService.GetSortedByShortUrl(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            _dataProviderMock.Verify(i => i.GetResultData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSortedByShortUrl_ReturnsCorrectDataSortedByShortUrl()
        {
            // Arrange
            string shortUrl = "39Af7IB";

            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>()))
                .Returns(_testArticles);

            // Act
            ArticleView result = _articleService.GetSortedByShortUrl(It.IsAny<string>(), shortUrl);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetGroupedByDate_CallsGetResultDataOnce()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(new JToken[0]);

            // Act
            _articleService.GetGroupedByDate(It.IsAny<string>());

            // Assert
            _dataProviderMock.Verify(i => i.GetResultData(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetGroupedByDate_ReturnsCorrectData()
        {
            // Arrange
            _dataProviderMock.Setup(i => i.GetResultData(It.IsAny<string>())).Returns(_testArticles);

            // Act
            IEnumerable<ArticleGroupByDateView> result = _articleService.GetGroupedByDate(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
        }
    }
}
