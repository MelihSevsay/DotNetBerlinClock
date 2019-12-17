using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock
{
    public interface ITimeConverter
    {
        Task<string> ConvertTime(string aTime);
    }
}
