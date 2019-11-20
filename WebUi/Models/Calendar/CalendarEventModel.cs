using System;
using System.Drawing;

namespace WebUi.Models.Calendar
{
    /// <summary>
    ///     Вью-модель события календаря.
    /// </summary>
    public class CalendarEventModel
    {
        /// <summary>
        ///     Дата.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Цвет.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Место.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        ///     Цвет в формате Hex.
        /// </summary>
        public string ColorHex => $"#{Color.R:X2}{Color.G:X2}{Color.B:X2}";

        /// <summary>
        ///     Является ли днём рождения.
        /// </summary>
        public bool IsBirthday => Name.Contains("др ", StringComparison.OrdinalIgnoreCase)
                                  || Name.Contains("рождения", StringComparison.OrdinalIgnoreCase)
                                  || Name.Contains("днюша", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        ///     Прошло ли.
        /// </summary>
        public bool IsOutdated => Date < DateTime.Now;

        /// <summary>
        ///     Общая информация.
        /// </summary>
        public string Summary => $"{(Date.Hour > 0 ? Date.ToShortTimeString() : "")} {Name}";
    }
}