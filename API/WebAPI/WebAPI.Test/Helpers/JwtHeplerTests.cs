namespace WebAPI.Test.Helpers
{
    using Models.Dto;

    using NUnit.Framework;

    using WebAPI.Helpers;

    [TestFixture]
    public class JwtHeplerTests
    {
        private JwtHelper jwtHelper;

        private const string SecretKey = "Pass phrase to get JWT to implement authorization and authentification";

        [SetUp]
        public void Init()
        {
            jwtHelper = new JwtHelper();
        }

        [Test]
        public void GetResponseWithToken_WhenOnlyUserIsNull_ReturnModelWithZeroValues()
        {
            var result = jwtHelper.GetResponseWithToken(null, SecretKey);

            Assert.That(string.IsNullOrEmpty(result.Token));
            Assert.That(result.User == null);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetResponseWithToken_WhenUserAndSecretKeyIsNull_ReturnModelWithZeroValues(string secretKey)
        {
            var result = jwtHelper.GetResponseWithToken(null, secretKey);

            Assert.That(string.IsNullOrEmpty(result.Token));
            Assert.That(result.User == null);
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetResponseWithToken_WhenOnlySecretKeyIsNull_ReturnModelWithZeroValues(string secretKey)
        {
            var user = new UserDto { Email = "1" };
            var result = jwtHelper.GetResponseWithToken(user, secretKey);

            Assert.That(string.IsNullOrEmpty(result.Token));
            Assert.That(result.User == null);
        }

        [Test]
        public void GetResponseWithToken_WhenSetAllNeccesaryValues_ReturnResult()
        {
            var user = new UserDto { Email = "1" };
            var result = jwtHelper.GetResponseWithToken(user, SecretKey);

            Assert.That(!string.IsNullOrEmpty(result.Token));
            Assert.That(result.User != null && result.User.Email == "1");
        }
    }
}
