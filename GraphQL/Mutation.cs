using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context,
                                    [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var createdPlatform = new Platform()
            {
                PlatformName = input.name
            };

            context.Platforms.Add(createdPlatform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), createdPlatform, cancellationToken);

            return new AddPlatformPayload(createdPlatform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context,
                                    [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            var createdCommand = new Command()
            {
                CommandLine = input.commandLine,
                HowTo = input.howTo,
                PlatformId = input.platformId
            };

            context.Commands.Add(createdCommand);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnCommandAdded), createdCommand, cancellationToken);

            return new AddCommandPayload(createdCommand);
        }
    }
}