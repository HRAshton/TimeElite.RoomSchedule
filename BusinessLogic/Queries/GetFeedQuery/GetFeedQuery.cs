using System;
using System.Collections.Generic;
using System.Net.Http;
using BusinessLogic.Queries.GetFeedQuery.InternalModels;
using Newtonsoft.Json;

namespace BusinessLogic.Queries.GetFeedQuery
{
    public class GetFeedQuery : QueryBase<object, IEnumerable<PostEntity>>
    {
        private readonly string RequestUri =
            "https://api.vk.com/method/execute.GetPosts?v=5.103?access_token=" + SecretsManager.VkApiKey;

        private readonly HttpClient HttpClient;

        /// <summary>
        ///     Запрос для получения записей новостной ленты.
        /// </summary>
        /// <param name="httpClient">Http-клиент.</param>
        public GetFeedQuery(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        ///     Выполнить запрос.
        /// </summary>
        /// <returns>Список постов.</returns>
        public override QueryResult<IEnumerable<PostEntity>> Execute(object _)
        {
            var feedJson = HttpClient.GetStringAsync(RequestUri).Result;
            var feedModel = JsonConvert.DeserializeObject<FeedEntity>(feedJson);

            var result = feedModel.response == null 
                ? GetFailedResult("Feed: no data returned.") 
                : GetSuccessfulResult(feedModel.response);

            return result;
        }
    }
}