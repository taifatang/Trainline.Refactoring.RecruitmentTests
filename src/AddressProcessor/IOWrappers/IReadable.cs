using System;

namespace AddressProcessing.IOWrappers
{
    public interface IReadable : IDisposable
    {
        string ReadLine();
    }
}