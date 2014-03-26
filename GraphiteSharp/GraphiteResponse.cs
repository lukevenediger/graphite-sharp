using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphite
{
    [DebuggerDisplay("{Targets.Count} targets")]
    public class GraphiteResponse
    {
        public string SourceUrl { get; private set; }
        public ReadOnlyCollection<Series> Targets { get; private set; }

        public GraphiteResponse(string sourceUrl, IList<Series> targets)
        {
            SourceUrl = sourceUrl;
            Targets = new ReadOnlyCollection<Series>(targets);
        }
    }
}
