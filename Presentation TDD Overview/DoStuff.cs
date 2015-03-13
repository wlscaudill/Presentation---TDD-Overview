namespace Presentation_TDD_Overview
{
    using System;

    public class DoStuff : IDoStuff
    {
        private readonly Func<DateTime> nowProvider;

        private readonly ISaveEvents saver;

        public DoStuff(Func<DateTime> nowProvider, ISaveEvents saver)
        {
            this.nowProvider = nowProvider;
            this.saver = saver;
        }

        public decimal Divide(decimal numerator, decimal divisor)
        {
            return numerator / divisor;
        }

        public void FireEvent()
        {
            var now = nowProvider();
            if (DateTime.IsLeapYear(now.Year))
            {
                this.saver.Save(now.Year);
            }
        }
    }
}
