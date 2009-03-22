using System.IO;
using NVelocity;

namespace DbFriend.Core.Generator
{
    public interface IConfiguredVelocityEngine
    {
        void MergeTemplate(string templateName, VelocityContext velocityContext, TextWriter writer);
    }
}