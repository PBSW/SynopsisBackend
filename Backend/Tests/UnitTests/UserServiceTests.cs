using Application;
using Application.Interface;
using FluentAssertions;
using Moq;

namespace UnitTests;

public class UserServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new UserService(null, new Mock<Imapper>().Object, new Mock<IValidator<User>>().Object);
        
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
        Action action = () => new UserService(new Mock<IUserRepository>().Object, new Mock<Imapper>().Object, null);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'validator')");
    }
    
    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        // Act
        Action action = () => new UserService(new Mock<IUserRepository>().Object, new Mock<Imapper>().Object, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().NotThrow();
    }
    
    // Helper Classes and Methods
    private UserService CreateServiceSetup()
    {
        var userRepoMock = new Mock<IUserRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        var mapper = new Mapper(mapperConfig);
        var validator = new UserValidators();
        
        return new ServiceSetup(userRepoMock.Object, mapper, validator);
    }

    private class ServiceSetup
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        
        public ServiceSetup(IUserRepository userRepoMock, IMapper mapper, IValidator<User> validator)
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