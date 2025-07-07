using FluentAssertions;
using InstrumentService.DataAccess.Entities;
using InstrumentService.IntegrationTests.Builders;
using InstrumentService.IntegrationTests.Consumers;
using InstrumentService.IntegrationTests.Extensions;
using MassTransit.Testing;
using MongoDB.Driver;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.IntegrationTests.Tests;

[Collection("InstrumentTests")]
public class CreateInstrumentTests(CustomWebApplicationFactory factory) : InstrumentTestBase(factory)
{
    [Fact]
    public async Task CreateInstrument_ShouldSucceed_WhenRequestIsValid()
    {
        // Arrange
        var guitarModel = new GuitarRequestModelBuilder().Build();
        var content = guitarModel.ToHttpContent();

        var filter = Builders<Instrument>.Filter.Eq(instrument => instrument.Name, guitarModel.Name);


        // Act
        var response = await Client.PostAsync("/instruments", content);

        var createdInstrument = await InstrumentsCollection.Find(filter).SingleOrDefaultAsync();

        var expectedInstrument = new GuitarExpectationBuilder()
            .FromRequest(guitarModel)
            .WithId(createdInstrument.Id)
            .WithOwner(createdInstrument.OwnerId)
            .Build();

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        createdInstrument.Should().BeEquivalentTo(expectedInstrument);
    }

    [Fact]
    public async Task CreateInstrument_ShouldProduce_InstrumentCreatedEvent_WhenRequestIsValid()
    {
        // Arrange
        var guitarModel = new GuitarRequestModelBuilder().Build();
        var content = guitarModel.ToHttpContent();

        var massTransitHarness = Factory.Services.GetTestHarness();
        await massTransitHarness.Start();

        var filter = Builders<Instrument>.Filter.Eq(instrument => instrument.Name, guitarModel.Name);


        // Act
        var response = await Client.PostAsync("/instruments", content);

        var createdInstrument = await InstrumentsCollection.Find(filter).SingleOrDefaultAsync();

        var expectedInstrument = new GuitarExpectationBuilder()
            .FromRequest(guitarModel)
            .WithId(createdInstrument.Id)
            .WithOwner(createdInstrument.OwnerId)
            .Build();

        var isEventPublished = await massTransitHarness.Published.Any<InstrumentCreated>();
        var consumerHarness = massTransitHarness.GetConsumerHarness<InstrumentCreatedConsumer>();
        var isEventConsumed = await consumerHarness.Consumed.Any<InstrumentCreated>(x =>
        {
            var message = x.Context.Message;
            return message.InstrumentId == createdInstrument.Id;
        });
        await massTransitHarness.Stop();

        // Assert
        createdInstrument.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
        createdInstrument.Should().BeEquivalentTo(expectedInstrument);
        isEventPublished.Should().BeTrue();
        isEventConsumed.Should().BeTrue();
    }
}