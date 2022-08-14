using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a CLI");

            descriptor
                .Field(p => p.LicenseKey)
                .Description("Represents a purchased, valid license for a platform");

            descriptor
                .Field(p => p.PlatformName)
                .Description("This is description for platformName");

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of available commands for this platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(p => p.PlatformId == platform.IdPlatform);
            }
        }
    }
}