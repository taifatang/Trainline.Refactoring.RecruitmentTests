using System;
using System.IO;

namespace AddressProcessing.IOWrappers
{
    public class StreamWritableWrapper : IWritable
    {
        private readonly StreamWriter _streamWriter;

        public StreamWritableWrapper(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            _streamWriter = fileInfo.CreateText();
        }

        public void WriteLine(string output)
        {
            _streamWriter.WriteLine(output);
        }

        public void Dispose()
        {
            if (_streamWriter != null) _streamWriter.Dispose();
        }
    }
}
