using AutoMapper;
using Moq;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;
using TaskFlow.Api.Services;

public class UserServiceTests
{
    /// <summary>
    /// Tests the GetUserAsync method of UserServices to ensure it returns a user when the user exists.
    /// This test uses Moq to create a mock of IUserRepository and IMapper, simulating the behavior of the repository and mapper.
    /// The test checks that the returned UserReadDto matches the expected values set up in the mock repository and mapper.
    /// If the user exists in the repository, the service should return a UserReadDto with the correct Id and Username. 
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetUserAsync_ReturnsUser_WhenUserExists()
    {
        //Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();

        var user = new User { Id = 1, Username = "Test User" };
        var userReadDto = new UserReadDto { Id = 1, Username = "Test User" };

        mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);
        mockMapper.Setup(mapper => mapper.Map<UserReadDto>(user)).Returns(userReadDto);

        var service = new UserServices(mockRepository.Object, mockMapper.Object);

        //Act
        var result = await service.GetUserAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test User", result.Username);
    }
}