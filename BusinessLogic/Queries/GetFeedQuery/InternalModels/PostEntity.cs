using System;
using BusinessLogic.Converters;
using Newtonsoft.Json;

namespace BusinessLogic.Queries.GetFeedQuery.InternalModels
{
    /// <summary>
    ///     Сущность записи новостной ленты.
    /// </summary>
    public class PostEntity
    {
        /// <summary>
        ///     Текст.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     Дата.
        /// </summary>
        [JsonConverter(typeof(MicrosecondEpochConverter))]
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
        ///     Ссылка на первое изображение.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
    }
}