using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressProcessing.CSV;
using AddressProcessing.IOWrappers;
using AddressProcessing.Models;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    internal class CsvReaderWriterIntergrationTests
    {
        [Test]
        public void Read_File_On_Disk()
        {
            var path = CreateNewTestFile("test-1");
            using (var readerWriter = new CSVReaderWriter(new StreamReaderWrapper(path)))
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
            var path = CreateNewTestFile("test-2");
            using (var readerWriter = new CSVReaderWriter(new StreamWriterWrapper(path)))
            {
                var payload = "hello\tworld\ttest\tone\two\tthree";

                readerWriter.Write(payload);
            }

            using (var readerWriter = new CSVReaderWriter(new StreamReaderWrapper(path)))
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
        //create new test file for test, this would prevent locking by read or write 
        //and give each test a new state
        private static string CreateNewTestFile(string name)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var original = Path.Combine(currentDirectory + @"\test_data\contacts.csv");
            var copy = Path.Combine(currentDirectory + @"\test_data\" + name + ".csv");
            File.Copy(original, copy, true);

            return copy;
        }
    }
}