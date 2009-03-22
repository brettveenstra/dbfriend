using System;

namespace DbFriend.Core.Generator
{
    public interface IDbSolutionGenerator
    {
        void Generate(Action<string> message);
    }
}