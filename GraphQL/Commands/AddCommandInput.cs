using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL.Commands
{
    public record AddCommandInput(string commandLine, string howTo, int platformId);
}