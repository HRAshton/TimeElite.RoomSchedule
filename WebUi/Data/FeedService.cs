using System.Collections.Generic;
using System.Net.Http;
using AutoMapper;
using BusinessLogic.Queries.GetFeedQuery;
using WebUi.Models.Feed;

namespace WebUi.Data
{
    /// <summary>
    ///     Сервис ленты новостей.
    /// </summary>
    public class FeedService
    {
        private readonly HttpClient HttpClient;

        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="mapper">Автомаппер.</param>
        /// <param name="httpClient">Http-клиент.</param>
        public FeedService(IMapper mapper, HttpClient httpClient)
        {
            Mapper = mapper;
            HttpClient = httpClient;
        }

        private IMapper Mapper { get; }

        /// <summary>
        ///     Получает список постов.
        /// </summary>
        /// <returns>Список постов.</returns>
        public IEnumerable<PostModel> GetPosts()
        {
            var query = new GetFeedQuery(HttpClient);

            var queryResult = query.Execute(null);
            var feed = queryResult.IsSuccessful
                ? Mapper.Map<List<PostModel>>(queryResult.Data)
                : new List<PostModel>();

            return feed;
        }
    }
}