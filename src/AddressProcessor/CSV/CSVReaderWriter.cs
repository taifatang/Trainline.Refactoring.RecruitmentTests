using System;
using System.Linq;
using AddressProcessing.Exceptions;
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
        //user interface instead of concrete type
        private IReadable _reader;
        private IWritable _writer;

        //constructor for DI 
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

        //would like to remove this, kept for backward comp
        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string path, Mode mode)
        {
            //throw new excpetion 
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Path can not be empty");
            }

            //switch for better clarity, would like to refactor to not use enum
            switch (mode)
            {
                case Mode.Read:
                    _reader = new StreamReaderWrapper(path);
                    break;
                case Mode.Write:
                    _writer = new StreamWriterWrapper(path);
                    break;
                default:
                    //changed to custom exception instead of Exception
                    throw new UnknownFileModeException("Unknown file mode for " + path);
            }
        }

        public void Write(params string[] columns)
        {
            //extract method to for clarity
            var row = CreateRowAsString(columns);
            _writer.WriteLine(row);
        }


        public bool Read(string column1, string column2)
        {
            //would like to deprecate as it doesn't do much and misleading
            //old readline behaviour will invoke ReadLine();

            //if performed in 
            //while(reader.Read(null, null))
            //{
            //    reader.Read(out string a, out string b);
            //}
            //1 lines is skipped for every 2 lines.
            //intent is not clear

            //also removed pointless assignment, on this occasion string are pass by value
            var columns = ReadNextRow();

            return columns.Length > 0;
        }

        public bool Read(out string column1, out string column2)
        {
            //refactor to allow logic reuse
            var columns = ReadNextRow();

            if (columns == null || columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            column1 = columns[0].Text;
            column2 = columns[1].Text;

            return true;
        }

        public ReaderResult ReadLine()
        {
            //new method, return a result plus column instead of primitives
            return new ReaderResult { Columns = ReadNextRow() };
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
            //implement IDisposable for using block
            if (_reader != null) _reader.Dispose();
            if (_writer != null) _writer.Dispose();
        }

        private static string CreateRowAsString(string[] columns)
        {
            var output = string.Empty;

            for (int i = 0; i < columns.Length; i++)
            {
                output += columns[i];

                var isNotLastColumn = (columns.Length - 1) != i;

                if (isNotLastColumn)
                {
                    output += Character.Tab;
                }
            }

            return output;
        }
    }
}
