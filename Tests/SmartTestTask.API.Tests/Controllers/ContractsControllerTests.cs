using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SmartTestTask.API.Controllers;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Requests;
using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.API.Tests.Controllers;

[TestFixture]
public class ContractsControllerTests
{
    private ContractsController _contractsController = null!;
    private Mock<IContractService> _contractServiceMock = null!;
    
    [SetUp]
    public void SetUp()
    {
        _contractServiceMock = new Mock<IContractService>();
        _contractsController = new ContractsController(_contractServiceMock.Object);
    }

    [Test]
    public async Task Get_WhenNoErrorInServiceOccured_ReturnsOkObjectResult()
    {
        // Arrange
        _contractServiceMock.Setup(m => m.GetContractsAsync())
            .ReturnsAsync(_testContractResults);

        // Act
        var actualResult = await _contractsController.Get() as OkObjectResult;

        // Assert
        actualResult.Should().NotBeNull();
        actualResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Test]
    public async Task Get_WhenNoErrorInServiceOccured_ReturnsContracts()
    {
        // Arrange
        _contractServiceMock.Setup(m => m.GetContractsAsync())
            .ReturnsAsync(_testContractResults);

        // Act
        var actualResult = await _contractsController.Get() as OkObjectResult;

        // Assert
        actualResult.Should().NotBeNull();
        actualResult!.Value.Should().BeEquivalentTo(_testContractResults);
    }
    
    [Test]
    public async Task Post_WhenNoErrorInServiceOccured_ReturnsStatus201()
    {
        // Arrange
        _contractServiceMock.Setup(m => m.CreateContractAsync(It.IsAny<CreateContractRequest>()))
            .ReturnsAsync(_testContractResults[0]);

        // Act
        var actualResult = await _contractsController.Post(Mock.Of<CreateContractRequest>()) as ObjectResult;

        // Assert
        actualResult.Should().NotBeNull();
        actualResult!.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Test]
    public async Task Post_WhenNoErrorInServiceOccured_ReturnsNewContractResult()
    {
        // Arrange
        _contractServiceMock.Setup(m => m.CreateContractAsync(It.IsAny<CreateContractRequest>()))
            .ReturnsAsync(_testContractResults[0]);

        // Act
        var actualResult = await _contractsController.Post(Mock.Of<CreateContractRequest>()) as ObjectResult;

        // Assert
        actualResult.Should().NotBeNull();
        actualResult!.Value.Should().BeEquivalentTo(_testContractResults[0]);
    }

    private readonly List<ContractResult> _testContractResults = new List<ContractResult>()
    {
        new()
        {
            Count = 10,
            IndustrialPremiseName = "Test premise 1",
            TechnicalEquipmentTypeName = "Test equipment type 3"
        },
        new()
        {
            Count = 2,
            IndustrialPremiseName = "Test premise 1",
            TechnicalEquipmentTypeName = "Test equipment type 2"
        },
        new()
        {
            Count = 5,
            IndustrialPremiseName = "Test premise 2",
            TechnicalEquipmentTypeName = "Test equipment type 1"
        },
        new()
        {
            Count = 8,
            IndustrialPremiseName = "Test premise 3",
            TechnicalEquipmentTypeName = "Test equipment type 2"
        },
        new()
        {
            Count = 1,
            IndustrialPremiseName = "Test premise 3",
            TechnicalEquipmentTypeName = "Test equipment type 1"
        },
    };
}