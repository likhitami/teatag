using System;
using System.Text;

namespace teatag.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToThaiString(this TimeSpan timespan, bool convertHourToDay = true, bool includeHour = true, bool includeMinute = true, bool includeSecound = true)
        {
            string format = "";
            if (convertHourToDay) format += "d";
            if (includeHour) format += "h";
            if (includeMinute) format += "m";
            if (includeSecound) format += "s";

            StringBuilder sb = new StringBuilder();
            var lastChar = '0';
            foreach (char item in format)
            {
                if (item == 'd')
                {
                    sb.Append(timespan.Days + " วัน ");
                }

                if (item == 'h')
                {
                    if (lastChar == 'd')
                    {
                        sb.Append(timespan.Hours + " ชั่วโมง ");
                    }
                    else
                    {
                        sb.Append(timespan.TotalHours + " ชั่วโมง ");
                    }
                }

                if (item == 'm')
                {
                    if (lastChar == 'h')
                    {
                        if (timespan.Minutes != 0)
                            sb.Append(timespan.Minutes + " นาที ");
                    }
                    else
                    {
                        if (timespan.TotalMinutes != 0)
                            sb.Append(timespan.TotalMinutes + " นาที ");
                    }
                }

                if (item == 's')
                {
                    if (lastChar == 'm')
                    {
                        if (timespan.Seconds != 0)
                            sb.Append(timespan.Seconds + " วินาที ");
                    }
                    else
                    {
                        if (timespan.TotalSeconds != 0)
                            sb.Append(timespan.TotalSeconds + " วินาที ");
                    }
                }
                lastChar = item;
            }
            return sb.ToString().Trim();
        }
    }
}
