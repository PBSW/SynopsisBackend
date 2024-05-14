using Application;
using Application.Interface;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Shared;

namespace UnitTests;

public class ToDoServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(null, new Mock<IMapper>().Object, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'toDoRepository')");
    }
    
    [Fact]
    public void CreateService_WithNullMapper_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, null, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'mapper')");
    }
    
    [Fact]
    public void CreateService_WithNullValidator_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, new Mock<IMapper>().Object, null);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'validator')");
    }
    
    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, new Mock<IMapper>().Object, new Mock<IValidator<User>>().Object);
        
        // Assert
        action.Should().NotThrow();
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var userRepoMock = new Mock<IToDoRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = new Mapper(mapperConfig);
        var validator = new ToDoValidator();
        
        return new ServiceSetup(toDoRepoMock, mapper, validator);
    }

    private class ServiceSetup
    {
        private readonly Mock<IToDoRepository> _toDoRepoMock;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        
        public ServiceSetup(Mock<IUserRepository> toDoRepoMock, IMapper mapper, IValidator<User> validator)
        {
            _toDoRepoMock = toDoRepoMock;
            _mapper = mapper;
            _validator = validator;
        }
        
        public UserService CreateService()
        {
            return new UserService(_toDoRepoMock.Object, _mapper, _validator);
        }
        
        public Mock<IUserRepository> GetUserRepoMock()
        {
            return _toDoRepoMock;
        }
    }
}