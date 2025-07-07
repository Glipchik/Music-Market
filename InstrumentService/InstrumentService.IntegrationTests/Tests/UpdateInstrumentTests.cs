using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using InstrumentService.IntegrationTests.Builders;
using InstrumentService.IntegrationTests.Constants;
using InstrumentService.IntegrationTests.Extensions;
using InstrumentService.IntegrationTests.Factories;
using MongoDB.Driver;

namespace InstrumentService.IntegrationTests.Tests;

[Collection("InstrumentTests")]
public class UpdateInstrumentTests(CustomWebApplicationFactory factory) : InstrumentTestBase(factory)
{
    [Fact]
    public async Task UpdateInstrument_ShouldSucceed_WhenAllPreconditionsAreMet()
    {
        // Arrange
        var createModel = new GuitarRequestModelBuilder().Build();
        var createContent = GuitarRequestJsonFactory.CreateFromGuitarModel(createModel);

        var createResponse = await Client.PostAsync("/instruments", createContent);
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdInstrument = await InstrumentsCollection
            .Find(instrument => instrument.Name == createModel.Name)
            .SingleOrDefaultAsync();

        var updateModel = new GuitarRequestModelBuilder()
            .WithName("Updated Guitar")
            .WithManufacturer("Updated Manufacturer")
            .Build();

        var updateContent = GuitarRequestJsonFactory.CreateFromGuitarModel(updateModel);

        // Act
        var updateResponse = await Client.PutAsync($"/instruments/{createdInstrument.Id}", updateContent);

        // Assert
        updateResponse.IsSuccessStatusCode.Should().BeTrue();

        var updatedInstrument = await InstrumentsCollection
            .Find(instrument => instrument.Id == createdInstrument.Id)
            .SingleOrDefaultAsync();

        var expectedInstrument = new GuitarExpectationBuilder()
            .FromRequest(updateModel)
            .WithId(createdInstrument.Id)
            .WithOwner(createdInstrument.OwnerId)
            .Build();

        updatedInstrument.Should().BeEquivalentTo(expectedInstrument);
    }

    [Fact]
    public async Task UpdateInstrument_ShouldReturnNotFound_WhenInstrumentDoesNotExist()
    {
        // Arrange
        var updateModel = new GuitarRequestModelBuilder()
            .WithName("Ghost Guitar")
            .WithManufacturer("Phantom Corp")
            .Build();

        var updateContent = GuitarRequestJsonFactory.CreateFromGuitarModel(updateModel);

        // Act
        var updateResponse = await Client.PutAsync($"/instruments/{InstrumentTestConstants.NonExistentInstrumentId}",
            updateContent);

        // Assert
        updateResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateInstrument_ShouldReturnForbidden_WhenUserIsNotOwner()
    {
        // Arrange
        var createModel = new GuitarRequestModelBuilder().Build();
        var createContent = GuitarRequestJsonFactory.CreateFromGuitarModel(createModel);

        var createResponse = await Client.PostAsync("/instruments", createContent);
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdInstrument = await InstrumentsCollection
            .Find(x => x.Name == createModel.Name)
            .SingleOrDefaultAsync();

        var otherUserClient = Factory.CreateAuthenticatedClient(userId: AuthTestConstants.OtherUserId);

        var updateModel = new GuitarRequestModelBuilder()
            .WithName("Should Fail")
            .WithManufacturer("Wrong User Inc.")
            .Build();

        var updateContent = GuitarRequestJsonFactory.CreateFromGuitarModel(updateModel);

        // Act
        var updateResponse = await otherUserClient.PutAsync($"/instruments/{createdInstrument.Id}", updateContent);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateInstrument_ShouldReturnBadRequest_WhenRequestBodyIsInvalid()
    {
        // Arrange
        var invalidModel = new { Type = "guitar" };
        var content = JsonContent.Create(invalidModel);

        // Act
        var response = await Client.PutAsync($"/instruments/{InstrumentTestConstants.InstrumentId}", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateInstrument_ShouldReturnInternalServerError_WhenRequestBodyHasNoTypeField()
    {
        // Arrange
        var invalidModel = new { };
        var content = JsonContent.Create(invalidModel);

        // Act
        var response = await Client.PutAsync($"/instruments/{InstrumentTestConstants.InstrumentId}", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}