using System;
using System.Collections.Generic;
using System.Linq;

namespace teatag.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkingHours(this DateTime dateTime, TimeSpan time, TimeSpan workingHourStart, TimeSpan workingHourEnd, bool sameDay = true, BussinessDayConfig bussinessDayConfig = null)
        {
            var startDate = dateTime.Date + workingHourStart;
            var endDate = dateTime.Date + workingHourEnd;

            if (dateTime > endDate || (dateTime + time > endDate && sameDay))
            {
                startDate = startDate.AddBusinessDays(1, bussinessDayConfig);
                endDate = endDate.AddBusinessDays(1, bussinessDayConfig);

                dateTime = startDate;
            }

            var date = dateTime + time;
            if (date > endDate)
            {
                var diff = date - endDate;
                var remain = time - diff;
                return startDate.AddBusinessDays(1, bussinessDayConfig).AddWorkingHours(diff, workingHourStart, workingHourEnd);
            }
            else
                return date;
        }
        public static DateTime AddWorkingHours(this DateTime dateTime, TimeSpan time, string workingHourStart = "9:00", string workingHourEnd = "18:00", bool sameDay = true, BussinessDayConfig bussinessDayConfig = null)
        {
            var start = TimeSpan.Parse(workingHourStart);
            var end = TimeSpan.Parse(workingHourEnd);
            return dateTime.AddWorkingHours(time, start, end, sameDay, bussinessDayConfig);
        }
        public static DateTime AddWorkingHours(this DateTime dateTime, string time, string workingHourStart = "9:00", string workingHourEnd = "18:00", bool sameDay = true, BussinessDayConfig bussinessDayConfig = null)
        {
            var t = TimeSpan.Parse(time);
            var start = TimeSpan.Parse(workingHourStart);
            var end = TimeSpan.Parse(workingHourEnd);
            return dateTime.AddWorkingHours(t, start, end, sameDay, bussinessDayConfig);
        }

        public static DateTime AddBusinessDays(this DateTime date, int day)
        {
            return date.AddBusinessDays(day, new BussinessDayConfig());
        }
        public static DateTime AddBusinessDays(this DateTime date, int day, BussinessDayConfig config)
        {
            if (config == null)
                config = new BussinessDayConfig();

            var currentDate = date;
            var sign = Math.Sign(day);
            var unsignDays = Math.Abs(day);

            var workingDay = config.WorkingDay();
            var holiday = config.Holiday.Select(x => x.Date).ToList();

            while (unsignDays > 0)
            {
                currentDate = currentDate.AddDays(sign);

                if (workingDay.Any(x => x == currentDate.DayOfWeek) && !holiday.Any(c => c == currentDate.Date)) /* if working day. */
                {
                    unsignDays -= 1;
                }
            }
            return currentDate;
        }

        public static DateTime? AddBusinessDays(this DateTime? date, int day)
        {
            if (date.HasValue)
            {
                return date.Value.AddBusinessDays(day);
            }
            else
                return date;
        }
        public static DateTime? AddBusinessDays(this DateTime? date, int day, BussinessDayConfig config)
        {
            if (date.HasValue)
            {
                return date.Value.AddBusinessDays(day, config);
            }
            else
                return date;
        }

        public static DateTimeOffset AddBusinessDays(this DateTimeOffset date, int day)
        {
            return date.AddBusinessDays(day, new BussinessDayConfig());
        }
        public static DateTimeOffset AddBusinessDays(this DateTimeOffset date, int day, BussinessDayConfig config)
        {
            if (config == null)
                config = new BussinessDayConfig();

            var currentDate = date;
            var sign = Math.Sign(day);
            var unsignDays = Math.Abs(day);

            var workingDay = config.WorkingDay();
            var holiday = config.Holiday.Select(x => x.Date).ToList();

            while (unsignDays > 0)
            {
                currentDate = currentDate.AddDays(sign);
                if (workingDay.Any(x => x == currentDate.DayOfWeek) && !holiday.Any(c => c == currentDate.Date)) /* if working day. */
                {
                    unsignDays -= 1;
                }
            }
            return currentDate;
        }

        public static DateTimeOffset? AddBusinessDays(this DateTimeOffset? date, int day)
        {
            return date.AddBusinessDays(day, new BussinessDayConfig());
        }
        public static DateTimeOffset? AddBusinessDays(this DateTimeOffset? date, int day, BussinessDayConfig config)
        {
            if (date.HasValue)
            {
                return date.Value.AddBusinessDays(day, config);
            }
            else
            {
                return date;
            }
        }

        public static string ddMMyyyy(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy");
        }
        public static string ddMMyyyy24H(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ddMMyyyy(this DateTime? datetime, string defaultValue = "")
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString("dd/MM/yyyy");
            }
            else
                return defaultValue;
        }
        public static string ddMMyyyy24H(this DateTime? datetime, string defaultValue = "")
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString("dd/MM/yyyy HH:mm");
            }
            else
                return defaultValue;
        }

        public class BussinessDayConfig
        {
            private List<DayOfWeek> workingDay = new List<DayOfWeek>();
            private List<DateTime> _holiday;

            public List<DateTime> Holiday
            {
                get { return _holiday; }
                set { _holiday = value; }
            }

            public BussinessDayConfig(bool Sunday = false, bool Monday = true, bool Tuesday = true, bool Wednesday = true, bool Thursday = true, bool Friday = true, bool Saturday = false)
            {
                if (Sunday)
                    workingDay.Add(DayOfWeek.Sunday);
                if (Monday)
                    workingDay.Add(DayOfWeek.Monday);
                if (Tuesday)
                    workingDay.Add(DayOfWeek.Tuesday);
                if (Wednesday)
                    workingDay.Add(DayOfWeek.Wednesday);
                if (Thursday)
                    workingDay.Add(DayOfWeek.Thursday);
                if (Friday)
                    workingDay.Add(DayOfWeek.Friday);
                if (Saturday)
                    workingDay.Add(DayOfWeek.Saturday);

                this.Holiday = new List<DateTime>();
            }

            public List<DayOfWeek> WorkingDay()
            {
                return workingDay;
            }
        }
    }
}
