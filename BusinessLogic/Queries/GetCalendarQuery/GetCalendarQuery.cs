using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLogic.Extensions;
using BusinessLogic.Queries.GetCalendarQuery.InternalModels;
using Ical.Net;
using RaspTpuIcalConverter;

namespace BusinessLogic.Queries.GetCalendarQuery
{
    /// <summary>
    ///     Запрос для получения календаря.
    /// </summary>
    public class GetCalendarQuery : QueryBase<object, CalendarEntity>
    {
        private const string EliteRoomUrl = "https://rasp.tpu.ru/pomeschenie_1960/2019/11/view.html";
        private readonly HttpClient HttpClient;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="httpClient">Http-клиент.</param>
        public GetCalendarQuery(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        ///     Выполнить.
        /// </summary>
        /// <returns>Модель страницы календаря.</returns>
        public override QueryResult<CalendarEntity> Execute(object _)
        {
            var legend = GetCalendarLegend();
            var calendarMatrix = GetCalendarMatrix();

            var result = GetSuccessfulResult(new CalendarEntity
            {
                Legend = legend,
                Matrix = calendarMatrix
            });

            return result;
        }

        private static List<CalendarLegendItemEntity> GetCalendarLegend()
        {
            return CalendarSources.Sources
                .Select(src => new CalendarLegendItemEntity
                {
                    Name = src.Item1,
                    Color = src.Item2,
                    iCalUrl = src.Item3
                })
                .ToList();
        }

        private CalendarDayEntity[,] GetCalendarMatrix()
        {
            var calendarMatrix = new CalendarDayEntity[5, 7];

            List<(Task<Calendar>, Color)> tasks = new List<(Task<Calendar>, Color)>();
            foreach (var (_, color, link) in CalendarSources.Sources)
            {
                var task = Task.Run(() =>
                {
                    Calendar calendar;
                    if (link == string.Empty)
                    {
                        calendar = new RaspTruIcalConverter(HttpClient)
                            .GetByLink(EliteRoomUrl, 1, 3);
                    }
                    else
                    {
                        var iCalendarString = HttpClient.GetStringAsync(link).Result;
                        calendar = Calendar.Load(iCalendarString);
                    }

                    return calendar;
                });

                tasks.Add((task, color));
            }

            Task.WaitAll(tasks.Select(x => x.Item1).ToArray<Task>());
            foreach (var (task, color) in tasks) AddCalendarEventsToMatrix(task.Result, color, ref calendarMatrix);

            return calendarMatrix;
        }

        private static void AddCalendarEventsToMatrix(Calendar cal, Color color,
            ref CalendarDayEntity[,] calendarMatrix)
        {
            var firstMonday = DateTime.Today.AddDays(-7).StartOfWeek(DayOfWeek.Monday);
            for (var deltaDay = 0; deltaDay < 5 * 7; deltaDay++)
            {
                var day = firstMonday.AddDays(deltaDay);
                var eventEntities = cal.Events
                    .Where(x => x.DtStart.Date == day.Date)
                    .Select(x => new CalendarEventEntity
                    {
                        Color = color,
                        Date = x.DtStart.AsSystemLocal,
                        Name = x.Name,
                        Place = x.Location
                    })
                    .OrderBy(x => x.Date)
                    .ToList();


                if (calendarMatrix[deltaDay / 7, deltaDay % 7] == null)
                    calendarMatrix[deltaDay / 7, deltaDay % 7] = new CalendarDayEntity
                    {
                        Date = day,
                        Events = new List<CalendarEventEntity>()
                    };

                calendarMatrix[deltaDay / 7, deltaDay % 7].Events.AddRange(eventEntities);
            }
        }
    }
}