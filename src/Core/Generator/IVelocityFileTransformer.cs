using System.Collections;

namespace DbFriend.Core.Generator
{
    public interface IVelocityFileTransformer
    {
        string Transform(string velocityTemplatePath, Hashtable velocityContext);
    }
}