using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Tatweer.API.Controllers;
using Tatweer.Application.Commands.Products;
using Tatweer.Application.Responses.Products;
using Tatweer.Application.Services;
using System.Net;
using System.Threading.Tasks;
using Tatweer.Application.Models;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IProductQuery> _productQueryMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _productQueryMock = new Mock<IProductQuery>();
        _controller = new ProductController(_mediatorMock.Object, _productQueryMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnResult_WhenCommandIsValid()
    {
        // Arrange
        var command = new SaveProductCommand { Name = "NewProduct",IsVisible=true,Price = 14,Qty = 10 };
        var result = Result.Success();
        _mediatorMock.Setup(m => m.Send(It.IsAny<SaveProductCommand>(), default))
            .ReturnsAsync(result);

        // Act
        var response = await _controller.Create(command);

        // Assert
        response.Should().Be(result);
    }

    [Fact]
    public async Task Create_ShouldReturnProblem_WhenCommandFails()
    {
        // Arrange
        var command = new SaveProductCommand { Name = "NewProduct" };
        var result =  Result.Failure("Error"); 
        _mediatorMock.Setup(m => m.Send(It.IsAny<SaveProductCommand>(), default))
            .ReturnsAsync(result);

        // Act
        var response = await _controller.Create(command);

        // Assert
        var problemDetails = response.Should().BeOfType<ProblemDetails>().Which;
        problemDetails.Status.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var product = new ProductsDto { Id = productId, Name = "ExistingProduct" };
        _productQueryMock.Setup(q => q.GetByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var response = await _controller.GetById(productId);

        // Assert
        var okResult = response.Result.Should().BeOfType<OkObjectResult>().Which;
        okResult.Value.Should().Be(product);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 1;
        _productQueryMock.Setup(q => q.GetByIdAsync(productId))
            .ReturnsAsync((ProductsDto)null);

        // Act
        var response = await _controller.GetById(productId);

        // Assert
        response.Result.Should().BeOfType<NotFoundResult>();
    }


}
