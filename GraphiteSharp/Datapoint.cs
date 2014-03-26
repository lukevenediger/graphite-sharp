using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphite
{
    [DebuggerDisplay("{DateTime}: {Value}")]
    public class Datapoint
    {
        public DateTime DateTime { get; private set; }
        public int Value { get; private set; }

        public Datapoint(long epoch, int value)
        {
            DateTime = Utility.EpochToDateTime(epoch);
            Value = value;
        }
    }
}
