using System;

namespace AddressProcessing.IOWrappers
{
    public interface IWritable: IDisposable
    {
        void WriteLine(string output);
    }
}