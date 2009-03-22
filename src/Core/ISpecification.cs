namespace DbFriend.Core
{
    public interface ISpecification
    {
        bool IsSatisfiedBy<T>(T subject);
    }
}