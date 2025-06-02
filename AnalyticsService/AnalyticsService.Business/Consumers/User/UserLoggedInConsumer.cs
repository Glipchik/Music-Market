using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.User;

namespace AnalyticsService.Business.Consumers.User;

public class UserLoggedInConsumer(IUnitOfWork unitOfWork) : IConsumer<UserLoggedIn>
{
    public async Task Consume(ConsumeContext<UserLoggedIn> context)
    {
        var userId = context.Message.UserId;

        var userStat = await unitOfWork.UserStatRepository
            .GetByIdAsync(userId, context.CancellationToken);

        if (userStat is null)
        {
            userStat = new UserStat { UserId = userId, TotalLogins = 1 };
            await unitOfWork.UserStatRepository.AddAsync(userStat, context.CancellationToken);
        }
        else
        {
            userStat.TotalLogins++;
        }

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}