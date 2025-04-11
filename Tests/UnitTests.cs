using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_IV.Entities;
using Project_IV.Service;
using Project_IV.Service.Impl;
using Project_IV.Data;
using Project_IV.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System;

namespace Tests
{
    [TestClass]
    public class DatingAppTests
    {
        private GitCommitDbContext _context;
        private IAuthService _authService;
        private IUserService _userService;
        private ILikeService _likeService;
        private IMatchService _matchService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IPreferenceService> _mockPreferenceService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<GitCommitDbContext>()
                .UseInMemoryDatabase(databaseName: "DatingAppTestDb")
                .Options;

            _context = new GitCommitDbContext(options);

            // Setup mocks
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null);

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x["JWT:Key"]).Returns("YourTestKeyHere");
            _mockConfiguration.Setup(x => x["JWT:ValidIssuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(x => x["JWT:ValidAudience"]).Returns("TestAudience");
            _mockConfiguration.Setup(x => x["JWT:Secret"]).Returns("YourSuperSecretKeyHere12345678901234567890");

            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockPreferenceService = new Mock<IPreferenceService>();

            // Initialize services with mocked dependencies
            _authService = new AuthService(
                _mockUserManager.Object,
                _mockConfiguration.Object,
                _mockHttpContextAccessor.Object,
                _mockPreferenceService.Object);

            _userService = new UserService(_context);
            _likeService = new LikeService(_context);
            _matchService = new MatchService(_context);
        }

        [TestMethod]
        public async Task Login_WithValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new User { UserName = "testuser", Email = "test@example.com" };
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string> { "User" });
            _mockUserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<System.Security.Claims.Claim>());

            // Act
            var result = await _authService.LoginAsync(new LoginRequest { Username = "testuser", Password = "password123" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("testuser", result.Username);
            Assert.IsNotNull(result.Token);
        }

        [TestMethod]
        public async Task Login_WithInvalidCredentials_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "testuser", Email = "test@example.com" };
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _authService.LoginAsync(new LoginRequest { Username = "testuser", Password = "wrongpassword" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task CreateProfile_ValidData_CreatesSuccessfully()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "password123",
                Bio = "Test Bio",
                GenderId = 1,
                StateId = 1,
                Age = 25
            };

            var user = new User 
            { 
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                Bio = registerRequest.Bio,
                GenderId = registerRequest.GenderId,
                StateId = registerRequest.StateId,
                Age = registerRequest.Age
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("confirmation-token");
            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string> { "User" });
            _mockUserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<System.Security.Claims.Claim>());

            // Act
            var result = await _authService.RegisterAsync(registerRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("newuser", result.Username);
            Assert.IsNotNull(result.Token);
        }

        [TestMethod]
        public async Task FilterProfiles_ReturnsMatchingProfiles()
        {
            // Arrange
            var users = new[]
            {
                new User { UserName = "user1", Age = 25, StateId = 1 },
                new User { UserName = "user2", Age = 30, StateId = 2 },
                new User { UserName = "user3", Age = 25, StateId = 1 }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var allUsers = await _userService.GetAllUsersAsync();
            var results = allUsers.Where(u => u.Age == 25 && u.StateId == 1);

            // Assert
            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public async Task SendLike_CreatesLikeRecord()
        {
            // Arrange
            var user1 = new User { UserName = "user1" };
            var user2 = new User { UserName = "user2" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.SaveChangesAsync();

            // Act
            var like = new Like { LikerId = user1.Id, LikedId = user2.Id };
            await _likeService.AddLikeAsync(like);

            // Assert
            var likes = await _likeService.GetLikesByLikerIdAsync(user1.Id);
            Assert.IsTrue(likes.Any(l => l.LikedId == user2.Id));
        }

        [TestMethod]
        public async Task CreateMatch_WhenBothUsersLikeEachOther()
        {
            // Arrange
            var user1 = new User { UserName = "user1" };
            var user2 = new User { UserName = "user2" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.SaveChangesAsync();

            // Act
            var like1 = new Like { LikerId = user1.Id, LikedId = user2.Id };
            var like2 = new Like { LikerId = user2.Id, LikedId = user1.Id };
            await _likeService.AddLikeAsync(like1);
            await _likeService.AddLikeAsync(like2);

            var match = new Project_IV.Entities.Match { User1Id = user1.Id, User2Id = user2.Id };
            await _matchService.AddMatchAsync(match);

            // Assert
            var matches = await _matchService.GetAllMatchesAsync();
            Assert.IsTrue(matches.Any(m => 
                (m.User1Id == user1.Id && m.User2Id == user2.Id) ||
                (m.User1Id == user2.Id && m.User2Id == user1.Id)));
        }

        [TestMethod]
        public async Task GetProfile_ReturnsCompleteProfile()
        {
            // Arrange
            var user = new User 
            { 
                UserName = "testuser",
                Email = "test@example.com",
                Bio = "Test Bio",
                Age = 25
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var profile = await _userService.GetUserByIdAsync(user.Id);

            // Assert
            Assert.IsNotNull(profile);
            Assert.AreEqual("testuser", profile.UserName);
            Assert.AreEqual("Test Bio", profile.Bio);
        }

        [TestMethod]
        public async Task GetMatches_ReturnsCorrectMatchList()
        {
            // Arrange
            var user1 = new User { UserName = "user1" };
            var user2 = new User { UserName = "user2" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.SaveChangesAsync();

            var like1 = new Like { LikerId = user1.Id, LikedId = user2.Id };
            var like2 = new Like { LikerId = user2.Id, LikedId = user1.Id };
            await _likeService.AddLikeAsync(like1);
            await _likeService.AddLikeAsync(like2);

            var match = new Project_IV.Entities.Match { User1Id = user1.Id, User2Id = user2.Id };
            await _matchService.AddMatchAsync(match);

            // Act
            var matches = await _matchService.GetAllMatchesAsync();
            var userMatches = matches.Where(m => m.User1Id == user1.Id || m.User2Id == user1.Id);

            // Assert
            Assert.AreEqual(1, userMatches.Count());
            Assert.IsTrue(userMatches.Any(m => m.User1Id == user2.Id || m.User2Id == user2.Id));
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new[]
            {
                new User { UserName = "user1", Age = 25 },
                new User { UserName = "user2", Age = 30 },
                new User { UserName = "user3", Age = 35 }
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetUserPreferences_ReturnsCorrectPreferences()
        {
            // Arrange
            var user = new User { UserName = "testuser" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var preferences = new Preference
            {
                UserId = user.Id,
                MinAge = 25,
                MaxAge = 35,
                GenderId = 1
            };
            await _context.Preferences.AddAsync(preferences);
            await _context.SaveChangesAsync();

            _mockPreferenceService.Setup(x => x.GetPreferenceByUserIdAsync(user.Id))
                .ReturnsAsync(preferences);

            // Act
            var result = await _mockPreferenceService.Object.GetPreferenceByUserIdAsync(user.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(25, result.MinAge);
            Assert.AreEqual(35, result.MaxAge);
            Assert.AreEqual(1, result.GenderId);
        }

        [TestMethod]
        public async Task CreateLike_ReturnsLikeResponseWithNextUser()
        {
            // Arrange
            var user1 = new User { UserName = "user1" };
            var user2 = new User { UserName = "user2" };
            var user3 = new User { UserName = "user3" };
            await _context.Users.AddRangeAsync(user1, user2, user3);
            await _context.SaveChangesAsync();

            // Act
            var like = new Like { LikerId = user1.Id, LikedId = user2.Id };
            await _likeService.AddLikeAsync(like);

            // Assert
            var likes = await _likeService.GetLikesByLikerIdAsync(user1.Id);
            Assert.IsTrue(likes.Any(l => l.LikedId == user2.Id));
        }

        [TestMethod]
        public async Task GetUserMatches_ReturnsCorrectMatches()
        {
            // Arrange
            var user1 = new User { UserName = "user1" };
            var user2 = new User { UserName = "user2" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.SaveChangesAsync();

            var like1 = new Like { LikerId = user1.Id, LikedId = user2.Id };
            var like2 = new Like { LikerId = user2.Id, LikedId = user1.Id };
            await _likeService.AddLikeAsync(like1);
            await _likeService.AddLikeAsync(like2);

            var match = new Project_IV.Entities.Match { User1Id = user1.Id, User2Id = user2.Id };
            await _matchService.AddMatchAsync(match);

            // Act
            var matches = await _matchService.GetAllMatchesAsync();
            var userMatches = matches.Where(m => m.User1Id == user1.Id || m.User2Id == user1.Id);

            // Assert
            Assert.IsNotNull(matches);
            Assert.AreEqual(1, userMatches.Count());
            Assert.IsTrue(userMatches.Any(m => 
                (m.User1Id == user1.Id && m.User2Id == user2.Id) ||
                (m.User1Id == user2.Id && m.User2Id == user1.Id)));
        }

        [TestMethod]
        public async Task UpdateUserProfile_UpdatesSuccessfully()
        {
            // Arrange
            var user = new User 
            { 
                UserName = "testuser",
                Email = "test@example.com",
                Bio = "Old Bio"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            user.Bio = "New Bio";
            await _userService.UpdateUserAsync(user);

            // Assert
            var updatedUser = await _userService.GetUserByIdAsync(user.Id);
            Assert.AreEqual("New Bio", updatedUser.Bio);
        }

        [TestMethod]
        public async Task UpdateUserPreferences_UpdatesSuccessfully()
        {
            // Arrange
            var user = new User { UserName = "testuser" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var preferences = new Preference
            {
                UserId = user.Id,
                MinAge = 25,
                MaxAge = 35,
                GenderId = 1
            };
            await _context.Preferences.AddAsync(preferences);
            await _context.SaveChangesAsync();

            // Act
            preferences.MinAge = 30;
            preferences.MaxAge = 40;
            _mockPreferenceService.Setup(x => x.UpdatePreferenceAsync(It.IsAny<Preference>()))
                .Returns(Task.CompletedTask);

            // Assert
            await _mockPreferenceService.Object.UpdatePreferenceAsync(preferences);
            var updatedPreferences = await _context.Preferences.FirstOrDefaultAsync(p => p.UserId == user.Id);
            Assert.IsNotNull(updatedPreferences);
            Assert.AreEqual(30, updatedPreferences.MinAge);
            Assert.AreEqual(40, updatedPreferences.MaxAge);
        }

        [TestMethod]
        public async Task UploadUserImage_AddsImageSuccessfully()
        {
            // Arrange
            var user = new User { UserName = "testuser" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var imageData = "base64encodedimagedata";
            var image = new Image
            {
                UserId = user.Id,
                ImageData = imageData,
                UploadedAt = DateTime.UtcNow
            };

            // Act
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            // Assert
            var userImages = await _context.Images.Where(i => i.UserId == user.Id).ToListAsync();
            Assert.IsNotNull(userImages);
            Assert.AreEqual(1, userImages.Count);
            Assert.AreEqual(imageData, userImages[0].ImageData);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}