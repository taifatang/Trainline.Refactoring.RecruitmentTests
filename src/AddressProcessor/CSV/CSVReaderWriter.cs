using System;
using System.IO;
using System.Linq;
using AddressProcessing.IOWrappers;
using AddressProcessing.Models;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter : IDisposable
    {
        private IReadable _reader;
        private IWritable _writer;

        public CSVReaderWriter() { }
        public CSVReaderWriter(IReadable reader)
        {
            _reader = reader;
        }
        public CSVReaderWriter(IWritable writer)
        {
            _writer = writer;
        }
        public CSVReaderWriter(IReadable reader, IWritable writer)
        {
            _reader = reader;
            _writer = writer;
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string path, Mode mode)
        {
            switch (mode)
            {
                case Mode.Read:
                    _reader = new StreamReaderWrapper(path);
                    break;
                case Mode.Write:
                    _writer = new StreamWriterWrapper(path);
                    break;
                default:
                    throw new UnknownFileModeException("Unknown file mode for " + path);
            }
        }

        public void Write(params string[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                var output = columns[i];

                var isNotLastColumn = (columns.Length - 1) != i;

                if (isNotLastColumn)
                {
                    output += Character.Tab;
                }
                _writer.WriteLine(output);
            }
        }

        public bool Read(string column1, string column2)
        {
            var columns = ReadNextRow();

            return columns.Length > 0;
        }

        public bool Read(out string column1, out string column2)
        {
            var columns = ReadNextRow();

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            column1 = columns[0].Text;
            column2 = columns[1].Text;

            return true;
        }

        private Column[] ReadNextRow()
        {
            var line = _reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            var columns = line.Split(Character.Tab)
                .Select(x => new Column { Text = x })
                .ToArray();

            return columns;
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_reader != null) _reader.Dispose();
            if (_writer != null) _writer.Dispose();
        }
    }
}
