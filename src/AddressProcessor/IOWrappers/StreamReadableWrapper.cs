using System;
using System.IO;

namespace AddressProcessing.IOWrappers
{
    public class StreamReadableWrapper : IReadable
    {
        private readonly StreamReader _streamReader;

        public StreamReadableWrapper(string filePath)
        {
            _streamReader = File.OpenText(filePath);
        }

        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }

        public void Dispose()
        {
            if (_streamReader != null) _streamReader.Dispose();
        }
    }
}