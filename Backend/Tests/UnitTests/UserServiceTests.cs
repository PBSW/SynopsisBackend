using Application;
using Application.Interface;
using Application.Validator;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Shared;
using Shared.DTOs.Create;
using Shared.Helpers;

namespace UnitTests;

public class UserServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new UserService(null, new Mock<IMapper>().Object, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'userRepository')");
    }
    
    [Fact]
    public void CreateService_WithNullMapper_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new UserService(new Mock<IUserRepository>().Object, null, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'mapper')");
    }
    
    [Fact]
    public void CreateService_WithNullValidator_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object, null);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'validator')");
    }
    
    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        // Act
        Action action = () => new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().NotThrow();
    }

    // Creation Tests
    
    [Fact]
    public async void CreateUser_WithValidUser_ShouldReturnUserResponse()
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        var userCreate = new UserCreate
        {
            FirstName = "John",
            LastName = "Doe",
            Mail = "Test@Email.com"
        };
        
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Mail = "Test@Email.com",
        };
        
        serviceSetup.GetUserRepoMock().Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(user);

        // Act
        Func<Task> action = async () => await service.CreateUser(userCreate);
        
        // Assert
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async void CreateUser_WithNullUser_ShouldThrowNullReferenceExceptionWithMessage()
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        // Act
        Func<Task> action = async () => await service.CreateUser(null);
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("Value cannot be null. (Parameter 'user')");
    }

    [Theory]
    [InlineData(null, "First name is null")]
    [InlineData("", "First name is required")]
    [InlineData(" ", "First name is required")]
    public async void CreateUser_WithInvalidFirstName_ShouldThrowValidationExceptionWithMessage(string firstName,
        string errorMessage)
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var userCreate = new UserCreate
        {
            FirstName = firstName,
            LastName = "Doe",
            Mail = "Test@Email.com"
        };
            
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Mail = "Test@Email.com" 
        };
        
        // Act
        Func<Task> action = async () => await service.CreateUser(userCreate);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }

    [Theory]
    [InlineData(null, "Last name is required")]
    [InlineData("", "Last name is required")]
    [InlineData(" ", "Last name is required")]
    public async void CreateUser_WithInvalidLastName_ShouldThrowValidationExceptionWithMessage(string lastName,
        string errorMessage)
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var userCreate = new UserCreate
        {
            FirstName = "John",
            LastName = lastName,
            Mail = "Test@Email.com"
        };
            
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = lastName,
            Mail = "Test@Email.com" 
        };
        
        // Act
        Func<Task> action = async () => await service.CreateUser(userCreate);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }

    [Theory]
    [InlineData(null, "Email can not be null")]
    [InlineData("", "Email is required")]
    [InlineData(" ", "Email is required")]
    [InlineData("test", "Email is not a valid email address")]
    public async void CreateUser_WithInvalidMail_ShouldThrowValidationExceptionWithMessage(string mail, string message)
    {
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var userCreate = new UserCreate
        {
            FirstName = "John",
            LastName = "Doe",
            Mail = mail
        };
        
        var user = new User
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Mail = mail
        };
        
        // Act
        Func<Task> action = async () => await service.CreateUser(userCreate);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(message);
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var userRepoMock = new Mock<IUserRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = new Mapper(mapperConfig);
        var validator = new UserValidator();
        
        return new ServiceSetup(userRepoMock, mapper, validator);
    }

    private class ServiceSetup
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        
        public ServiceSetup(Mock<IUserRepository> userRepoMock, IMapper mapper, IValidator<User> validator)
        {
            _userRepoMock = userRepoMock;
            _mapper = mapper;
            _validator = validator;
        }
        
        public UserService CreateService()
        {
            return new UserService(_userRepoMock.Object, _mapper, _validator);
        }
        
        public Mock<IUserRepository> GetUserRepoMock()
        {
            return _userRepoMock;
        }
    }
}