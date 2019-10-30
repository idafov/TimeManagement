using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeClock
{
    class Program
    {
        static void Main()
        {

            //TODO, add this as a time window period -> not possible to work outside this
            var blockStart = new TimeSpan(0, 7, 0, 0);
            var blockEnd = new TimeSpan(0, 20, 0, 0);

            string line = Console.ReadLine();
            DateTime dt;
            //30.10.2019 08:25
            DateTime.TryParseExact(line, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out dt);

            string lineOut = Console.ReadLine();
            DateTime dtOut;
            //30.10.2019 17:05
            DateTime.TryParseExact(lineOut, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out dtOut);

            Console.WriteLine();

            var listOfTimeLogs = new List<TimeLog>
            {
                new TimeLog {EntryDateTime = dt,EntryType = EntryTypes.In},
              
                new TimeLog {EntryDateTime = dtOut,EntryType = EntryTypes.Out}
            };


            var timeBlocks = new List<TimeBlock>();
            for (var x = 0; x < listOfTimeLogs.Count; x += 2)
            {

                timeBlocks.Add(new TimeBlock
                {
                    BlockType = BlockTypes.Working,
                    In = listOfTimeLogs[x],
                    Out = listOfTimeLogs[x + 1]
                });
            }


            foreach (var block in timeBlocks)
            {
                var lineTitle = block.BlockType.ToString();

                if (block.Duration >= TimeSpan.FromMinutes(520))
                {
                    Console.WriteLine($"            {block}\n            Time at work: {block.Duration.ToString(@"hh\:mm")}");
                }
                else
                {
                    Console.WriteLine("You worked less than 8h");
                }                  
            }

           

            var workingTime = timeBlocks.Where(b => b.BlockType == BlockTypes.Working)
                    .Aggregate(new TimeSpan(0), (p, v) => p.Add(v.Duration)).Subtract((new TimeSpan(0, 40, 0)));



            Console.WriteLine($"\nTotal Working/Productive Hours: {workingTime.ToString(@"hh\:mm")}");

            if (workingTime >= TimeSpan.FromMinutes(480))
            {

                Console.WriteLine($"   Plus time is {workingTime.Subtract(new TimeSpan(7, 50, 0)).Subtract(new TimeSpan(0, 10, 0)).ToString(@"hh\:mm")}");
            }
            else
            {
                var a = TimeSpan.FromMinutes(480) - workingTime;
                Console.WriteLine($"----------WARNING----------\nYOU ARE on MINUS {a.ToString(@"hh\:mm")}");
            }
            Console.WriteLine();
            Console.ReadKey();
           
        }
    }
}