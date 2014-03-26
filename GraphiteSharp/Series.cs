using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphite
{
    [DebuggerDisplay("{Name}: {Datapoints.Count} datapoints")]
    public class Series
    {
        public string Name { get; private set; }
        public ReadOnlyCollection<Datapoint> Datapoints { get; private set; }

        public Series(string name, IList<Datapoint> datapoints)
        {
            Name = name;
            Datapoints = new ReadOnlyCollection<Datapoint>(datapoints);
        }
    }
}
