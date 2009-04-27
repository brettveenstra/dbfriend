namespace DbFriend.Core
{
    /// <summary>
    /// Mapper pattern
    /// </summary>
    /// <typeparam name="TFrom">The type of the From.</typeparam>
    /// <typeparam name="TTarget">The type of the To.</typeparam>
    public interface IMapper<TFrom, TTarget>
    {
        /// <summary>
        /// Maps from type to a target type.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        TTarget MapFrom(TFrom from);
    }
}