using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.User;

namespace AnalyticsService.Business.Consumers.User;

public class UserInstrumentCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<UserInstrumentCreated>
{
    public async Task Consume(ConsumeContext<UserInstrumentCreated> context)
    {
        var userId = context.Message.UserId;

        var userStat = await unitOfWork.UserStatRepository.GetByIdAsync(userId, context.CancellationToken);

        if (userStat is null)
        {
            userStat = new UserStat { UserId = userId, InstrumentsCreated = 1 };
            await unitOfWork.UserStatRepository.AddAsync(userStat, context.CancellationToken);
        }
        else
        {
            userStat.InstrumentsCreated++;
        }

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}