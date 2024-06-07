using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Tests.Services
{
    [TestFixture]
    public class JwtServiceTests
    {
        private JwtService _jwtService;
        private User _testUser;

        [SetUp]
        public void Setup()
        {
            _jwtService = new JwtService();
            _testUser = new User
            {
                Id = 1,
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                IsSuperUser = false
            };
        }

        [Test]
        public void GenerateToken_User_ReturnsValidToken()
        {
            // Arrange
            var expectedEmail = _testUser.Email;

            // Act
            var token = _jwtService.GenerateToken(_testUser);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOf<string>(token);
            Assert.That(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value, Is.EqualTo(expectedEmail));
        }

        [Test]
        public void GenerateToken_User_HasCorrectClaims()
        {
            // Act
            var token = _jwtService.GenerateToken(_testUser);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.IsNotNull(jwtToken);
            Assert.That(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value, Is.EqualTo(_testUser.Email));
            Assert.IsNotEmpty(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value);
        }

        [Test]
        public void GenerateToken_User_HasCorrectExpiry()
        {
            // Act
            var token = _jwtService.GenerateToken(_testUser);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.IsNotNull(jwtToken);
            Assert.Greater(jwtToken.ValidTo, DateTime.UtcNow.AddMinutes(119));
            Assert.Less(jwtToken.ValidTo, DateTime.UtcNow.AddMinutes(121));
        }
    }
}
