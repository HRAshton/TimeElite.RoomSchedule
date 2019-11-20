using System;

namespace WebUi.Models.Feed
{
    /// <summary>
    ///     Модель записи новостной ленты.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        ///     Текст.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     Дата.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Количество лойсов.
        /// </summary>
        public ushort Likes { get; set; }

        /// <summary>
        ///     Имя последнего лойснувшего.
        /// </summary>
        public string LastLike { get; set; } = string.Empty;

        /// <summary>
        ///     Ссылка на запись.
        /// </summary>
        public string Link { get; set; } = string.Empty;

        /// <summary>
        ///     Ссылка на первое прикрепленное изображение.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
    }
}