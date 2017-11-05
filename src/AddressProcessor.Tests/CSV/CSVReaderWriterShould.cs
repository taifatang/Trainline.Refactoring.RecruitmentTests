using System.IO;
using AddressProcessing.CSV;
using AddressProcessing.IOWrappers;
using Moq;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    public class CsvReaderWriterShould
    {
        private CSVReaderWriter _csvReaderWriter;
        private Mock<IReadable> _reader;
        private Mock<IWritable> _writer;

        [SetUp]
        public void SetUp()
        {
            _reader = new Mock<IReadable>();
            _writer = new Mock<IWritable>();

            _csvReaderWriter = new CSVReaderWriter(_reader.Object, _writer.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _csvReaderWriter.Close();
            _csvReaderWriter = null;
        }

        [Test]
        public void Write_Line()
        {
            _csvReaderWriter.Write("Hello world");

            _writer.Verify(x=> x.WriteLine(It.IsAny<string>()),Times.Once);
        }

        [Test]
        public void Append_Tab_On_Write_Operation_Between_Columns()
        {
            _csvReaderWriter.Write("Hello", "world");

            _writer.Verify(x => x.WriteLine("Hello\tworld"), Times.Once);
        }

        [Test]
        public void Read_Line()
        {
            _reader.Setup(x => x.ReadLine()).Returns("Hello world");

            _csvReaderWriter.ReadLine();

            _reader.Verify(x => x.ReadLine());
        }
        

    }
}
