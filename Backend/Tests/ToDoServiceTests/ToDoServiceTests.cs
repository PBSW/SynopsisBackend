using Application;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Shared;
using Shared.DTOs.Create;
using Shared.DTOs.Response;
using Shared.Helpers;


namespace UnitTests;

public class ToDoServiceTests
{
    [Fact]
    public void CreateService_WithNullRepo_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(null, new Mock<IMapper>().Object, new Mock<IValidator<ToDoList>>().Object, new Mock<IHttpRepository>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'toDoRepository')");
    }
    
    [Fact]
    public void CreateService_WithNullMapper_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, null, new Mock<IValidator<ToDoList>>().Object, new Mock<IHttpRepository>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'mapper')");
    }
    
    [Fact]
    public void CreateService_WithNullValidator_ShouldThrowNullExceptionWithMessage()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, new Mock<IMapper>().Object, null, new Mock<IHttpRepository>().Object);
        
        // Assert
        action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'validator')");
    }
    
    [Fact]
    public void CreateService_WithValidParameters_ShouldNotThrowException()
    {
        // Act
        Action action = () => new ToDoService(new Mock<IToDoRepository>().Object, new Mock<IMapper>().Object, new Mock<IValidator<ToDoList>>().Object, new Mock<IHttpRepository>().Object);
        
        // Assert
        action.Should().NotThrow();
    }
    
    // Create Tests
    [Fact]
    public async void CreateToDoListAsync_WithValidToDoList_ShouldReturnToDoListResponse()
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var createList = new ToDoListCreate
        {
            Title = "Test Title",
        };
        
        // Act
        var result = await service.CreateToDoListAsync(createList);
        
        // Assert
        result.Should().BeOfType<ToDoListResponse>();
    }

    [Fact]
    public async void CreateToDoListAsync_WithValidToDoList_ShouldNotThrowError()
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var createList = new ToDoListCreate
        {
            Title = "Test Title",
        };
        
        // Act
        Func<Task> action = async () => await service.CreateToDoListAsync(createList);
        
        // Assert
        await action.Should().NotThrowAsync();
    }
    
    [Fact]
    public async void CreateToDoListAsync_WithNullToDoList_ShouldThrowNullExceptionWithMessage()
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        // Act
        Func<Task> action = async () => await service.CreateToDoListAsync(null);
        
        // Assert
        await action.Should().ThrowAsync<NullReferenceException>().WithMessage("ToDoListCreate is null");
    }
    
    [Theory]
    [InlineData("", "Title is required")]
    [InlineData(" ", "Title is required")]
    public async void CreateToDoListAsynch_WithInvalidTitle_ShouldThrowValidationExceptionWithMessage(string title, string errorMessage)
    {
        // Arrange
        var serviceSetup = CreateServiceSetup();
        var service = serviceSetup.CreateService();
        
        var createList = new ToDoListCreate
        {
            Title = title,
        };
        
        // Act
        Func<Task> action = async () => await service.CreateToDoListAsync(createList);
        
        // Assert
        await action.Should().ThrowAsync<ValidationException>().WithMessage(errorMessage);
    }
    
    // Helper Classes and Methods
    private ServiceSetup CreateServiceSetup()
    {
        var toDoRepoMock = new Mock<IToDoRepository>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
        var mapper = new Mapper(mapperConfig);
        var validator = new ToDoValidator();
        var httpRepoMock = new Mock<IHttpRepository>();
        
        return new ServiceSetup(toDoRepoMock, mapper, validator, httpRepoMock);
    }

    private class ServiceSetup
    {
        private readonly Mock<IToDoRepository> _toDoRepoMock;
        private readonly IMapper _mapper;
        private readonly IValidator<ToDoList> _validator;
        private readonly Mock<IHttpRepository> _httpRepoMock;
        
        public ServiceSetup(Mock<IToDoRepository> toDoRepoMock, IMapper mapper, IValidator<ToDoList> validator, Mock<IHttpRepository> httpRepoMock)
        {
            _toDoRepoMock = toDoRepoMock;
            _mapper = mapper;
            _validator = validator;
            _httpRepoMock = httpRepoMock;
        }

        public ToDoService CreateService()
        {
            return new ToDoService(_toDoRepoMock.Object, _mapper, _validator, _httpRepoMock.Object);
        }
        
        public Mock<IToDoRepository> GetToDoRepoMock()
        {
            return _toDoRepoMock;
        }
    }
}