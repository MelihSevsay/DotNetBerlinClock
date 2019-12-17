using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public interface ITimeConverter
    {
        string ConvertTime(string aTime);
    }
}
