namespace BusinessLogic
{
    /// <summary>
    ///     Предоставляет секреты приложения.
    /// </summary>
    public static class SecretsManager
    {
        /// <summary>
        ///     Токен пользователя ВК.
        /// </summary>
        public static string VkApiKey { get; set; } = string.Empty;
    }
}