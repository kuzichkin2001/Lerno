namespace Lerno.Shared.Enums
{
    public enum UserStatus
    {
        /// <summary>
        /// Статус неопределён
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// В процессе регистрации
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// Активирован, наделён всем необходимым функционалом
        /// </summary>
        Activated = 2,

        /// <summary>
        /// Заблокирован, доступ к системе ограничен
        /// </summary>
        Blocked = 3
    }
}
