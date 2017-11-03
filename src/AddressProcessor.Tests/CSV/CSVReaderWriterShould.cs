using System.IO;
using AddressProcessing.CSV;
using AddressProcessing.IOWrappers;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    public class CsvReaderWriterShould
    {
        private  Stream _testData;
        private CSVReaderWriter _readerWriter;

        [SetUp]
        public void SetUp()
        {
            string test = "Hello world";
            _testData = new MemoryStream();
            StreamWriter writer = new StreamWriter( _testData );
            writer.Write( test );
            writer.Flush();

            _readerWriter = new CSVReaderWriter();
        }

        [TearDown]
        public void TearDown()
        {
            _readerWriter.Close();
            _readerWriter = null;
        }
        [Test]
        public void Read_First_Line_Of_File()
        {
            var _readerWriter = new CSVReaderWriter(new StreamReaderWrapper(_testData));

            var result = _readerWriter.ReadLine();

            Assert.That(result.HasResult, Is.EqualTo("Hello world"));
            Assert.That(result.Columns[0], Is.EqualTo("Hello world"));
        }

        [Test]
        public void Read_Last_Line_Of_File()
        {
            var _readerWriter = new CSVReaderWriter();

            var firstColumn = string.Empty;
            var secondColumn = string.Empty;

            while (_readerWriter.Read(out firstColumn, out secondColumn))
            {

            }

            Assert.That(firstColumn, Is.EqualTo("Leila Neal"));
        }
    }
}
