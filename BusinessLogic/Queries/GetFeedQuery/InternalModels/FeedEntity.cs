namespace BusinessLogic.Queries.GetFeedQuery.InternalModels
{
    /// <summary>
    ///     Сущность ответа сервера.
    /// </summary>
    public class FeedEntity
    {
        /// <summary>
        ///     Список записей новостной ленты.
        /// </summary>
        public PostEntity[] response = new PostEntity[0];
    }
}