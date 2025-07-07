using System.Net;
using FluentAssertions;
using InstrumentService.IntegrationTests.Builders;
using InstrumentService.IntegrationTests.Constants;
using InstrumentService.IntegrationTests.Consumers;
using InstrumentService.IntegrationTests.Extensions;
using InstrumentService.IntegrationTests.Factories;
using MassTransit.Testing;
using MongoDB.Driver;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.IntegrationTests.Tests;

[Collection("InstrumentTests")]
public class DeleteInstrumentTests(CustomWebApplicationFactory factory) : InstrumentTestBase(factory)
{
    [Fact]
    public async Task DeleteInstrument_ShouldSucceed_WhenAllPreconditionsAreMet()
    {
        // Arrange
        var createModel = new GuitarRequestModelBuilder().Build();
        var createContent = GuitarRequestJsonFactory.CreateFromGuitarModel(createModel);

        var massTransitHarness = Factory.Services.GetTestHarness();
        await massTransitHarness.Start();

        var createResponse = await Client.PostAsync("/instruments", createContent);
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdInstrument = await InstrumentsCollection
            .Find(instrument => instrument.Name == createModel.Name)
            .SingleOrDefaultAsync();

        createdInstrument.Should().NotBeNull();

        // Act
        var deleteResponse = await Client.DeleteAsync($"/instruments/{createdInstrument.Id}");

        var returnedInstrumentId = await deleteResponse.Content.ReadAsStringAsync();
        returnedInstrumentId.Should().Be(createdInstrument.Id);

        var deletedInstrument = await InstrumentsCollection
            .Find(instrument => instrument.Id == returnedInstrumentId)
            .FirstOrDefaultAsync();

        var consumerHarness = massTransitHarness.GetConsumerHarness<InstrumentDeletedConsumer>();
        var isEventConsumed = await consumerHarness.Consumed.Any<InstrumentDeleted>(receivedMessage =>
        {
            var message = receivedMessage.Context.Message;
            return message.InstrumentId == createdInstrument.Id;
        });
        var isEventPublished = await massTransitHarness.Published.Any<InstrumentDeleted>();
        await massTransitHarness.Stop();

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deletedInstrument.Should().BeNull();
        isEventPublished.Should().BeTrue();
        isEventConsumed.Should().BeTrue();
    }


    [Fact]
    public async Task DeleteInstrument_ShouldReturn404_WhenInstrumentNotFound()
    {
        // Act
        var deleteResponse =
            await Client.DeleteAsync($"/instruments/{InstrumentTestConstants.NonExistentInstrumentId}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteInstrument_ShouldReturn403_WhenUserIsNotOwner()
    {
        // Arrange
        var createModel = new GuitarRequestModelBuilder().Build();
        var createContent = GuitarRequestJsonFactory.CreateFromGuitarModel(createModel);

        var createResponse = await Client.PostAsync("/instruments", createContent);
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdInstrument = await InstrumentsCollection
            .Find(i => i.Name == createModel.Name)
            .SingleOrDefaultAsync();

        createdInstrument.Should().NotBeNull();

        var otherUserClient = Factory.CreateAuthenticatedClient(userId: AuthTestConstants.OtherUserId);

        // Act
        var deleteResponse = await otherUserClient.DeleteAsync($"/instruments/{createdInstrument.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}