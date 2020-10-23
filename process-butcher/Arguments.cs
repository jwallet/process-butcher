using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace process_butcher
{
    internal class Arguments
    {
        internal readonly string master;
        internal readonly string slave;

        internal Arguments(string[] args)
        {
            var props = args.Where(x => x.StartsWith("--") && x.Contains("=")).Select(x => x.Substring(2).Split("=")).ToDictionary(x => x.First(), x => x.Last());

            master = props.ContainsKey("master") ? props["master"] : string.Empty;
            slave = props.ContainsKey("slave") ? props["slave"] : string.Empty;
        }
    }
}
