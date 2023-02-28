using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Domain.Entities;
using SmartTestTask.Infrastructure.Repositories;

namespace SmartTestTask.Infrastructure.Tests.Repositories;

[TestFixture]
public class ContractRepositoryTests
{
    private ApplicationDbContext _dbContext = null!;
    private IContractRepository _contractRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _dbContext = new ApplicationDbContext(UnitTestsHelper.GetUnitTestDbOptions());
        _contractRepository = new ContractRepository(_dbContext);
    }

    [Test]
    public async Task CreateContractAsync_Always_CreatesContract()
    {
        // Arrange
        var expectedResult = await _dbContext.Contracts.CountAsync() + 1;

        // Act
        await _contractRepository.CreateContractAsync(new Contract()
        {
            IndustrialPremiseId = "1",
            TechnicalEquipmentTypeId = "2",
            Count = 10
        });
        var actualResult = await _dbContext.Contracts.CountAsync();

        // Assert
        actualResult.Should().Be(expectedResult);
    }

    [Test]
    public async Task GetAsync_WhenDataIsPresent_ReturnsContractsList()
    {
        // Arrange
        var expectedResult = await _dbContext.Contracts
            .Include(x => x.IndustrialPremise)
            .Include(x => x.TechnicalEquipmentType)
            .AsNoTracking()
            .ToListAsync();;

        // Act
        var actualResult = await _contractRepository.GetAsync();;

        // Assert
        actualResult.Should().BeEquivalentTo(expectedResult, o => o.IgnoringCyclicReferences());
    }
    
    [Test]
    public async Task GetAsync_WhenThereIsNoData_ReturnsEmptyList()
    {
        // Arrange
        var contracts = await _dbContext.Contracts.ToListAsync();
        _dbContext.Contracts.RemoveRange(contracts);
        await _dbContext.SaveChangesAsync();

        // Act
        var actualResult = await _contractRepository.GetAsync();;

        // Assert
        actualResult.Should().BeEmpty();
    }

    [Test]
    public async Task GetSingleByExpressionAsync_WhenContractIsPresent_ReturnsContractByExpression()
    {
        // Arrange
        var expectedResult = await _dbContext.Contracts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == "1");

        // Act
        var actualResult = await _contractRepository.GetSingleByExpressionAsync(x => x.Id == "1");;

        // Assert
        actualResult.Should().BeEquivalentTo(expectedResult);
    }
    
    [Test]
    public async Task GetSingleByExpressionAsync_WhenNoContractFound_ReturnsNull()
    {
        // Arrange

        // Act
        var actualResult = await _contractRepository.GetSingleByExpressionAsync(x => x.Id == "1234");;

        // Assert
        actualResult.Should().BeNull();
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
}