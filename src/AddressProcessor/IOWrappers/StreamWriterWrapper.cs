using System;
using System.IO;

namespace AddressProcessing.IOWrappers
{
    public class StreamWriterWrapper : IWritable
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterWrapper(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            _streamWriter = fileInfo.CreateText();
        }

        public StreamWriterWrapper(Stream stream)
        {
            _streamWriter = new StreamWriter(stream);
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
