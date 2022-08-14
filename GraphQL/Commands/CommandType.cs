using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any executable command");

            descriptor
               .Field(c => c.Platform)
               .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
               .UseDbContext<AppDbContext>()
               .Description("This is the platform to which the command belongs.");

            descriptor
                .Field(c => c.HowTo)
                .Description("Represents a way how to call this command");
        }

        private class Resolvers
        {
            public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext context)
            {
                return context.Platforms.FirstOrDefault(p => p.IdPlatform == command.PlatformId);
            }
        }
    }
}