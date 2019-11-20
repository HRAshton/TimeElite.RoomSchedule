using AutoMapper;
using BusinessLogic.Queries.GetCalendarQuery;
using WebUi.Models.Calendar;

namespace WebUi.Data
{
    /// <summary>
    ///     Сервис страницы календаря.
    /// </summary>
    public class CalendarService
    {
        private readonly GetCalendarQuery GetCalendarQuery;

        private readonly IMapper Mapper;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="mapper">Автомаппер.</param>
        /// <param name="getCalendarQuery">Запрос для получения календаря.</param>
        public CalendarService(IMapper mapper, GetCalendarQuery getCalendarQuery)
        {
            Mapper = mapper;
            GetCalendarQuery = getCalendarQuery;
        }

        /// <summary>
        ///     Получает расписание.
        /// </summary>
        /// <returns>Календарь.</returns>
        public CalendarModel GetSchedule()
        {
            var queryResult = GetCalendarQuery.Execute(null);

            var calendarModel = queryResult.IsSuccessful 
                ? Mapper.Map<CalendarModel>(queryResult.Data) 
                : new CalendarModel();

            return calendarModel;
        }
    }
}