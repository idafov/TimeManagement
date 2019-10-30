using System;

namespace TimeClock
{
    public enum BlockTypes
    {
        Working,
        Break
    }

    public class TimeBlock
    {
        public BlockTypes BlockType;
        public TimeLog In;
        public TimeLog Out;

        public TimeSpan Duration
        {
            get
            {
                return Out.EntryDateTime.Subtract(In.EntryDateTime);
            }
        }

        public override string ToString()
        {
            return $"Entry time: {In.EntryDateTime:HH:mm}\n            Leave time: {Out.EntryDateTime:HH:mm}";
        }

    }
}