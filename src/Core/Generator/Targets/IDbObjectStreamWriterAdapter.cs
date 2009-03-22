using System;

namespace DbFriend.Core.Generator.Targets
{
    public interface IDbObjectStreamWriterAdapter : IDisposable
    {
        string Write();
    }
}