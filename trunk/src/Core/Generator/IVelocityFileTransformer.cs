namespace DbFriend.Core.Generator
{
    using NVelocity;

    public interface IVelocityFileTransformer
    {
        string Transform(string velocityTemplatePath, VelocityContext velocityContext);
    }
}