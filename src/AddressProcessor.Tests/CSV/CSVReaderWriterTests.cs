using System.IO;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVReaderWriterShould
    {
        private string _testDataPath = Path.Combine(Directory.GetCurrentDirectory() + @"\test_data\contacts.csv");

        [Test]
        public void Read_First_Line_Of_File()
        {
            var readerWriter = new CSVReaderWriter();
            readerWriter.Open(_testDataPath, CSVReaderWriter.Mode.Read);

            var firstColumn = string.Empty;
            var secondColumn = string.Empty;

            readerWriter.Read(out firstColumn, out secondColumn);

            Assert.That(firstColumn, Is.EqualTo("Shelby Macias"));
        }

        [Test]
        public void Read_Last_Line_Of_File()
        {
            var readerWriter = new CSVReaderWriter();
            readerWriter.Open(_testDataPath, CSVReaderWriter.Mode.Read);

            var firstColumn = string.Empty;
            var secondColumn = string.Empty;

            while (readerWriter.Read(out firstColumn, out secondColumn))
            {
                
            }

            Assert.That(firstColumn, Is.EqualTo("Leila Neal"));
        }
    }
}
