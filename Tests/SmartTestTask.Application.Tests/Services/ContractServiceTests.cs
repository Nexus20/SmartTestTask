using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SmartTestTask.Application.Exceptions;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Requests;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Application.Services;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Tests.Services;

[TestFixture]
public class ContractServiceTests
{
    private IMapper _mapper = null!;
    private IContractService _contractService = null!;
    private Mock<IContractRepository> _contractRepositoryMock = null!;
    private Mock<IRepository<IndustrialPremise>> _industrialPremiseRepositoryMock = null!;
    private Mock<IRepository<TechnicalEquipmentType>> _technicalEquipmentTypeRepositoryMock = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _mapper = UnitTestsHelper.GetMapper();
    }

    [SetUp]
    public void SetUp()
    {
        _contractRepositoryMock = new Mock<IContractRepository>();
        _industrialPremiseRepositoryMock = new Mock<IRepository<IndustrialPremise>>();
        _technicalEquipmentTypeRepositoryMock = new Mock<IRepository<TechnicalEquipmentType>>();

        _contractService = new ContractService(_contractRepositoryMock.Object, _industrialPremiseRepositoryMock.Object, _technicalEquipmentTypeRepositoryMock.Object, _mapper);
    }

    [Test]
    [TestCase("1")]
    [TestCase("2")]
    [TestCase("3")]
    public async Task CreateContractAsync_WhenTechnicalEquipmentTypeDoesNotExist_ThrowsException(string technicalEquipmentTypeCode)
    {
        // Arrange
        var expectedMessage = $"Invalid technical equipment type code {technicalEquipmentTypeCode}. Technical equipment type with such code doesn't exist";
        
        _technicalEquipmentTypeRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<TechnicalEquipmentType, bool>>>()))
            .ReturnsAsync((TechnicalEquipmentType?)null);

        var request = new CreateContractRequest()
        {
            TechnicalEquipmentTypeCode = technicalEquipmentTypeCode
        };
        
        // Act
        var action = async () => await _contractService.CreateContractAsync(request);

        // Assert
        await action.Should().ThrowAsync<ValidationException>()
            .WithMessage(expectedMessage);
    }
    
    [Test]
    [TestCase("TTET-1", "1")]
    [TestCase("TTET-1", "2")]
    [TestCase("TTET-1", "3")]
    public async Task CreateContractAsync_WhenIndustrialPremiseDoesNotExist_ThrowsException(string technicalEquipmentTypeCode, string industrialPremiseCode)
    {
        // Arrange
        var expectedMessage = $"Invalid industrial premise code {industrialPremiseCode}. Industrial premise with such code doesn't exist";
        
        _technicalEquipmentTypeRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<TechnicalEquipmentType, bool>>>()))
            .ReturnsAsync(_testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode));
        
        _industrialPremiseRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<IndustrialPremise, bool>>>()))
            .ReturnsAsync((IndustrialPremise?)null);

        var request = new CreateContractRequest()
        {
            TechnicalEquipmentTypeCode = technicalEquipmentTypeCode,
            IndustrialPremiseCode = industrialPremiseCode
        };
        
        // Act
        var action = async () => await _contractService.CreateContractAsync(request);

        // Assert
        await action.Should().ThrowAsync<ValidationException>()
            .WithMessage(expectedMessage);
    }
    
    [Test]
    [TestCase("TTET-1")]
    public async Task CreateContractAsync_WhenAreaCannotBeAllocated_ThrowsException(string technicalEquipmentTypeCode)
    {
        // Arrange
        var testIndustrialPremise = new IndustrialPremise()
        {
            Id = "5",
            Area = 10,
            Code = "TIP-5",
            Name = "Test industrial premise 5",
            Contracts = new List<Contract>()
            {
                new()
                {
                    IndustrialPremiseId = "5",
                    Count = 1,
                    TechnicalEquipmentType = _testTechnicalEquipmentTypes[0],
                    TechnicalEquipmentTypeId = "1"
                }
            }
        };

        var request = new CreateContractRequest()
        {
            TechnicalEquipmentTypeCode = technicalEquipmentTypeCode,
            IndustrialPremiseCode = testIndustrialPremise.Code,
            Count = 10
        };
        
        var testEquipmentType = _testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode);
        var occupiedArea = testIndustrialPremise.Contracts?.Sum(x => x.TechnicalEquipmentType.Area * x.Count) ?? 0;
        var freeArea = testIndustrialPremise.Area - occupiedArea;
        var expectedMessage = $"Requested area of {testEquipmentType.Area * request.Count} can't be allocated. Area available is: {freeArea}";
        
        _technicalEquipmentTypeRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<TechnicalEquipmentType, bool>>>()))
            .ReturnsAsync(_testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode));
        
        _industrialPremiseRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<IndustrialPremise, bool>>>()))
            .ReturnsAsync(testIndustrialPremise);

        // Act
        var action = async () => await _contractService.CreateContractAsync(request);

        // Assert
        await action.Should().ThrowAsync<ValidationException>()
            .WithMessage(expectedMessage);
    }

    [Test]
    [TestCase("TTET-1", "TEP-1", 10)]
    [TestCase("TTET-2", "TEP-2", 3)]
    [TestCase("TTET-3", "TEP-3", 8)]
    public async Task CreateContractAsync_WhenThereIsNoException_CreatesContract(string technicalEquipmentTypeCode, string industrialPremiseCode, int count)
    {
        // Arrange

        var request = new CreateContractRequest()
        {
            TechnicalEquipmentTypeCode = technicalEquipmentTypeCode,
            IndustrialPremiseCode = industrialPremiseCode,
            Count = count
        };

        var expectedResult = new ContractResult()
        {
            Count = count,
            IndustrialPremiseName = _testIndustrialPremises.First(x => x.Code == industrialPremiseCode).Name,
            TechnicalEquipmentTypeName =
                _testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode).Name,
        };

        _technicalEquipmentTypeRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<TechnicalEquipmentType, bool>>>()))
            .ReturnsAsync(_testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode));
        
        _industrialPremiseRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<IndustrialPremise, bool>>>()))
            .ReturnsAsync(_testIndustrialPremises.First(x => x.Code == industrialPremiseCode));

        _contractRepositoryMock.Setup(m =>
                m.GetSingleByExpressionAsync(It.IsAny<Expression<Func<Contract, bool>>>()))
            .ReturnsAsync(new Contract()
            {
                Count = request.Count,
                TechnicalEquipmentType = _testTechnicalEquipmentTypes.First(x => x.Code == technicalEquipmentTypeCode),
                IndustrialPremise = _testIndustrialPremises.First(x => x.Code == industrialPremiseCode)
            });

        // Act
        var actualResult = await _contractService.CreateContractAsync(request);

        // Assert
        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public async Task GetContractsAsync_WhenDataIsPresent_ReturnsContracts()
    {
        // Arrange
        var testContracts = GetTestContracts();

        _contractRepositoryMock.Setup(m => m.GetAsync())
            .ReturnsAsync(testContracts);

        // Act
        var actualResult = await _contractService.GetContractsAsync();

        // Assert
        actualResult.Any().Should().BeTrue();
        actualResult.Should().BeEquivalentTo(_testContractResults);
    }
    
    [Test]
    public async Task GetContractsAsync_WhenThereIsNoDataIsPresent_ReturnsEmptyList()
    {
        // Arrange
        _contractRepositoryMock.Setup(m => m.GetAsync())
            .ReturnsAsync(new List<Contract>());

        // Act
        var actualResult = await _contractService.GetContractsAsync();

        // Assert
        actualResult.Any().Should().BeFalse();
    }

    private List<Contract> GetTestContracts()
    {
        return new List<Contract>()
        {
            new()
            {
                Id = "1",
                Count = 10,
                IndustrialPremiseId = "1",
                IndustrialPremise = _testIndustrialPremises[0],
                TechnicalEquipmentTypeId = "3",
                TechnicalEquipmentType = _testTechnicalEquipmentTypes[2]
            },
            new()
            {
                Id = "2",
                Count = 2,
                IndustrialPremiseId = "1",
                IndustrialPremise = _testIndustrialPremises[0],
                TechnicalEquipmentTypeId = "2",
                TechnicalEquipmentType = _testTechnicalEquipmentTypes[1]
            },
            new()
            {
                Id = "3",
                Count = 5,
                IndustrialPremiseId = "2",
                IndustrialPremise = _testIndustrialPremises[1],
                TechnicalEquipmentTypeId = "1",
                TechnicalEquipmentType = _testTechnicalEquipmentTypes[0]
            },
            new()
            {
                Id = "4",
                Count = 8,
                IndustrialPremiseId = "3",
                IndustrialPremise = _testIndustrialPremises[2],
                TechnicalEquipmentTypeId = "2",
                TechnicalEquipmentType = _testTechnicalEquipmentTypes[1]
            },
            new()
            {
                Id = "5",
                Count = 1,
                IndustrialPremiseId = "3",
                IndustrialPremise = _testIndustrialPremises[2],
                TechnicalEquipmentTypeId = "1",
                TechnicalEquipmentType = _testTechnicalEquipmentTypes[0]
            }
        };
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

    private readonly List<IndustrialPremise> _testIndustrialPremises = new List<IndustrialPremise>()
    {
        new()
        {
            Id = "1",
            Area = 100,
            Code = "TEP-1",
            Name = "Test premise 1"
        },
        new()
        {
            Id = "2",
            Area = 50,
            Code = "TEP-2",
            Name = "Test premise 2"
        },
        new()
        {
            Id = "3",
            Area = 96,
            Code = "TEP-3",
            Name = "Test premise 3"
        },
    };
    
    private readonly List<TechnicalEquipmentType> _testTechnicalEquipmentTypes = new List<TechnicalEquipmentType>()
    {
        new()
        {
            Id = "1",
            Area = 10,
            Code = "TTET-1",
            Name = "Test equipment type 1"
        },
        new()
        {
            Id = "2",
            Area = 12,
            Code = "TTET-2",
            Name = "Test equipment type 2"
        },
        new()
        {
            Id = "3",
            Area = 8,
            Code = "TTET-3",
            Name = "Test equipment type 3"
        },
    };
}