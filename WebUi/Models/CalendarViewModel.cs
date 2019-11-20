using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUi.Models.Calendar;
using WebUi.Models.Feed;

namespace WebUi.Models
{
    /// <summary>
    ///     Модель страницы расписания.
    /// </summary>
    public class CalendarViewModel : PageModel
    {
        /// <summary>
        ///     Календарь.
        /// </summary>
        public CalendarModel CalendarModel { get; set; }

        /// <summary>
        ///     Посты новостной ленты.
        /// </summary>
        public IEnumerable<PostModel> Posts { get; set; }
    }
}