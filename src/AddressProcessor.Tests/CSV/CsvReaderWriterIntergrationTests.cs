using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressProcessing.CSV;
using AddressProcessing.IOWrappers;
using AddressProcessing.Models;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    public class CsvReaderWriterIntergrationTests
    {
        private readonly string _testDataPath = Path.Combine(Directory.GetCurrentDirectory() + @"\test_data\contacts.csv");

        [Test]
        public void Read_File_On_Disk()
        {
            using (var readerWriter = new CSVReaderWriter(new StreamReaderWrapper(_testDataPath)))
            {
                var firstColumn = string.Empty;
                var secondColumn = string.Empty;

                var result = readerWriter.Read(out firstColumn, out secondColumn);

                Assert.That(result, Is.EqualTo(true));
                Assert.That(firstColumn, Is.EqualTo("Shelby Macias"));
                Assert.That(secondColumn, Is.EqualTo("3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|England"));
            }
        }

        [Test]
        public void Write_To_File_On_Disk()
        {
            using (var readerWriter = new CSVReaderWriter(new StreamWriterWrapper(_testDataPath)))
            {
                var payload = "hello\tworld\ttest\tone\two\tthree";

                readerWriter.Write(payload);
            }

            using (var readerWriter = new CSVReaderWriter(new StreamReaderWrapper(_testDataPath)))
            {
                var readerResult = readerWriter.ReadLine();

                var rows = new List<Column[]>();

                while (readerResult.HasResult)
                {
                    rows.Add(readerResult.Columns);
                    readerResult = readerWriter.ReadLine();
                }

                var newRow = rows.Last();

                Assert.That(newRow[0].Text, Is.EqualTo("hello"));
                Assert.That(newRow[1].Text, Is.EqualTo("world"));
                Assert.That(newRow[5].Text, Is.EqualTo("three"));
            }
        }
    }
}