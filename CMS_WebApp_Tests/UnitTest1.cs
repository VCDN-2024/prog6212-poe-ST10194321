using CMS_WebApps.Controllers;
using CMS_WebApps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace CMS_Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<HttpContext> _mockHttpContext;
        private Dictionary<string, string> _sessionData;

        [SetUp]
        public void Setup()
        {
            // Setup the session data and HttpContext
            _sessionData = new Dictionary<string, string>();
            _mockHttpContext = CreateMockHttpContext(_sessionData);

            // Initialize the controller with mocked HttpContext
            _controller = new AccountController
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }

        private Mock<HttpContext> CreateMockHttpContext(Dictionary<string, string> sessionData)
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockSession.Setup(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()))
                       .Callback<string, string>((key, value) => sessionData[key] = value);

            mockSession.Setup(s => s.GetString(It.IsAny<string>()))
                       .Returns<string>(key => sessionData.ContainsKey(key) ? sessionData[key] : null);

            mockHttpContext.Setup(c => c.Session).Returns(mockSession.Object);

            return mockHttpContext;
        }

        [Test]
        public void RegisterView_ShouldReturnView()
        {
            // Act
            var result = _controller.RegisterView();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>()); // Use NUnit's Is.InstanceOf
        }

       

    }
}
