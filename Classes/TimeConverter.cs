using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Contracts.Extentions;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {

        int hour;
        int minute;
        int second;

        public string ConvertTime(string aTime)
        {

            string[] levels = new string[5] { "", "", "", "", "" };
            bool tryParse = true;
            var timeSections = aTime.Split(':');

            tryParse &= int.TryParse(timeSections[0], out hour);
            tryParse &= int.TryParse(timeSections[1], out minute);
            tryParse &= int.TryParse(timeSections[2], out second);

            if (!tryParse)
                throw new ArgumentOutOfRangeException();

            List<Task> tasks = new List<Task>() {
            TopLevelLampAsync(levels),
            FirstLevelLampAsync(levels),
            SecondLevelLampAsync(levels),
            ThirdLevelLampAsync(levels),
            ForthLevelLampAsync(levels)
            };

            var exc = tasks.ToArray();
            Task.WaitAll(exc);

            //Join with then specific delimater.
            return string.Join("\r\n", levels);

        }

        //Set state of Top Level Lamp
        public async Task TopLevelLampAsync(string[] levels)
        {
            await Task.Run(() => 
                levels[0] = (second % 2) == 0 ? "Y" : "O"
                ).ConfigureAwait(false);
        }

        //Set state of First Level Lamp
        public async Task FirstLevelLampAsync(string[] levels)
        {
            await Task.Run(() =>
            {
                int division = hour / 5;
                levels[1] = levels[1].PadLeft(division, 'R');
                levels[1] = levels[1].PadRight(4, 'O');
            }).ConfigureAwait(false);
        }

        //Set state of Second Level Lamp
        public async Task SecondLevelLampAsync(string[] levels)
        {


            await Task.Run(() =>
            {
                int remaining = hour % 5;
                levels[2] = levels[2].PadLeft(remaining, 'R');
                levels[2] = levels[2].PadRight(4, 'O');
            }).ConfigureAwait(false);

        }

        //Set state of Thirth Level Lamp
        public async Task ThirdLevelLampAsync(string[] levels)
        {

            await Task.Run(() =>
            {
                int division = minute / 5;
                levels[3] = levels[3].PadLeft(division, 'Y');
                levels[3] = levels[3].PadRight(11, 'O');
                if (levels[3].ElementAt(2) == 'Y')
                {
                    levels[3] = levels[3].ReplaceAtIndexOf('R', 2);
                }
                if (levels[3].ElementAt(5) == 'Y')
                {
                    levels[3] = levels[3].ReplaceAtIndexOf('R', 5);
                }
                if (levels[3].ElementAt(8) == 'Y')
                {
                    levels[3] = levels[3].ReplaceAtIndexOf('R', 8);
                }
            }).ConfigureAwait(false);

        }

        //Set state of Forth Level Lamp
        public async Task ForthLevelLampAsync(string[] levels)
        {

            await Task.Run(() =>
            {
                int remaining_min = minute % 5;
                levels[4] = levels[4].PadLeft(remaining_min, 'Y');
                levels[4] = levels[4].PadRight(4, 'O');
            }).ConfigureAwait(false);

        }


    }
}
