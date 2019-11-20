using AutoMapper;
using BusinessLogic.Queries.GetCalendarQuery;
using WebUi.Models.Calendar;

namespace WebUi.Data
{
    /// <summary>
    ///     ������ �������� ���������.
    /// </summary>
    public class CalendarService
    {
        private readonly GetCalendarQuery GetCalendarQuery;

        private readonly IMapper Mapper;

        /// <summary>
        ///     �����������.
        /// </summary>
        /// <param name="mapper">����������.</param>
        /// <param name="getCalendarQuery">������ ��� ��������� ���������.</param>
        public CalendarService(IMapper mapper, GetCalendarQuery getCalendarQuery)
        {
            Mapper = mapper;
            GetCalendarQuery = getCalendarQuery;
        }

        /// <summary>
        ///     �������� ����������.
        /// </summary>
        /// <returns>���������.</returns>
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