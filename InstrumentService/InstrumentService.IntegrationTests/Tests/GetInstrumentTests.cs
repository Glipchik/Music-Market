using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using InstrumentService.Business.Models.Response;
using InstrumentService.IntegrationTests.Builders;
using InstrumentService.IntegrationTests.Constants;
using InstrumentService.IntegrationTests.Consumers;
using InstrumentService.IntegrationTests.Extensions;
using InstrumentService.IntegrationTests.Utils;
using MassTransit.Testing;
using MongoDB.Driver;
using Shared.Messaging.Contracts.Events.Instrument;
using Xunit;

namespace InstrumentService.IntegrationTests.Tests;

[Collection("InstrumentTests")]
public class GetInstrumentTests(CustomWebApplicationFactory factory) : InstrumentTestBase(factory)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new PropertyModelJsonConverter() },
        PropertyNameCaseInsensitive = true,
    };

    [Fact]
    public async Task GetById_ShouldReturnInstrumentResponseModel_WhenInstrumentExists()
    {
        // Arrange
        var createModel = new GuitarRequestModelBuilder().Build();
        var createContent = createModel.ToHttpContent();

        var massTransitHarness = Factory.Services.GetTestHarness();
        await massTransitHarness.Start();

        var createResponse = await Client.PostAsync("/instruments", createContent);
        createResponse.IsSuccessStatusCode.Should().BeTrue();

        var createdInstrument = await InstrumentsCollection
            .Find(instrument => instrument.Name == createModel.Name)
            .SingleOrDefaultAsync();

        createdInstrument.Should().NotBeNull();

        var expectedModel = new InstrumentResponseModelBuilder()
            .WithMinioEndpoint(Factory.MinioEndpoint)
            .FromGuitarRequest(createModel)
            .WithId(createdInstrument.Id)
            .Build();

        // Act
        var getResponse = await Client.GetAsync($"/instruments/{createdInstrument.Id}");
        getResponse.IsSuccessStatusCode.Should().BeTrue();

        var json = await getResponse.Content.ReadAsStringAsync();
        var actualModel = JsonSerializer.Deserialize<InstrumentResponseModel>(json, _jsonSerializerOptions);

        var consumerHarness = massTransitHarness.GetConsumerHarness<InstrumentViewedConsumer>();
        var isEventConsumed = await consumerHarness.Consumed.Any<InstrumentViewed>(receivedMessage =>
        {
            var message = receivedMessage.Context.Message;
            return message.InstrumentId == createdInstrument.Id;
        });
        var isEventPublished = await massTransitHarness.Published.Any<InstrumentViewed>();
        await massTransitHarness.Stop();

        // Assert
        actualModel.Should().NotBeNull();
        actualModel.Should().BeEquivalentTo(expectedModel);
        isEventPublished.Should().BeTrue();
        isEventConsumed.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenInstrumentDoesNotExist()
    {
        // Act
        var response = await Client.GetAsync($"/instruments/{InstrumentTestConstants.NonExistentInstrumentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}