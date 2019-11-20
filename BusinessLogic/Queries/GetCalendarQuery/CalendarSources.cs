using System;
using System.Collections.Generic;
using System.Drawing;

namespace BusinessLogic.Queries.GetCalendarQuery
{
    /// <summary>
    ///     Источники календарей.
    /// </summary>
    public static class CalendarSources
    {
        /// <summary>
        ///     Источники календарей.
        /// </summary>
        public static List<Tuple<string, Color, string>> Sources = new List<Tuple<string, Color, string>>
        {
            // https://calendar.google.com/calendar/ical/hel9lk4ot8tcbftothf5i323sk%40group.calendar.google.com/public/basic.ics
            new Tuple<string, Color, string>("Мероприятия Актива", Color.FromArgb(4149685),
                "https://tinyurl.com/EliteActiveEvents"),

            // https://calendar.google.com/calendar/ical/5pfa971ga73uhqjhifcd8qvp54%40group.calendar.google.com/public/basic.ics
            new Tuple<string, Color, string>("Мероприятия Отдела", Color.FromArgb(16011550),
                "https://tinyurl.com/EliteDepartEvents"),

            new Tuple<string, Color, string>("Пары", Color.FromArgb(8172354), string.Empty),

            // https://calendar.google.com/calendar/ical/t3kmoc7fho106776c63ki9fvvg%40group.calendar.google.com/public/basic.ics
            new Tuple<string, Color, string>("Прочие записи", Color.FromArgb(11771355),
                "https://tinyurl.com/EliteOtherEvents")
        };
    }
}