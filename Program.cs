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



            var listOfTimeLogs = new List<TimeLog>
            {
                new TimeLog {EntryDateTime = new DateTime(2019,10,04,8,25,0),EntryType = EntryTypes.In},
              
                new TimeLog {EntryDateTime = new DateTime(2019,10,04,17,5,0),EntryType = EntryTypes.Out}
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
                    Console.WriteLine($"            {block}\n            Length: {block.Duration.ToString(@"hh\:mm")}");
                }
                else
                {
                    Console.WriteLine("You worked less than 8h");
                }                  
            }

           

            var workingTime = timeBlocks.Where(b => b.BlockType == BlockTypes.Working)
                    .Aggregate(new TimeSpan(0), (p, v) => p.Add(v.Duration)).Subtract((new TimeSpan(0, 40, 0)));



            Console.WriteLine($"\nTotal Working Hours: {workingTime.ToString(@"hh\:mm")}");

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