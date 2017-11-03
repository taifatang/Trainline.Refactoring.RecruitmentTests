using System;
using System.IO;

namespace AddressProcessing.IOWrappers
{
    public class StreamReaderWrapper : IReadable
    {
        private readonly StreamReader _streamReader;

        public StreamReaderWrapper(string filePath)
        {
            _streamReader = File.OpenText(filePath);
        }

        public StreamReaderWrapper(Stream stream)
        {
            _streamReader = new StreamReader(stream);
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