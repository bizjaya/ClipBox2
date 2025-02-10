using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipBox2
{
    public class MasterData
    {
        public Dictionary<string, Info> Lists { get; set; } = new Dictionary<string, Info>(StringComparer.OrdinalIgnoreCase);
    }
}
