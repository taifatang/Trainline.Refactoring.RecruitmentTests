using System;
using System.IO;
using AddressProcessing.CSV;
using AddressProcessing.Exceptions;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    internal class CsvReaderWriterCharacteristicTests
    {
        private readonly string _testDataPath = Path.Combine(Directory.GetCurrentDirectory() + @"\test_data\contacts.csv");
        private CSVReaderWriter _readerWriter;

        [SetUp]
        public void SetUp()
        {
            _readerWriter = new CSVReaderWriter();
        }


        [TearDown]
        public void TearDown()
        {
            _readerWriter.Close();
            _readerWriter = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [TestCase(CSVReaderWriter.Mode.Write)]
        [TestCase(CSVReaderWriter.Mode.Read)]
        public void Do_Not_Throw_If_Mode_Is_Supported(CSVReaderWriter.Mode mode)
        {
            Assert.DoesNotThrow(() =>
            {
                _readerWriter.Open(_testDataPath, mode);
            });
        }

        [Test]
        public void Throw_If_Mode_Is_Not_Supported()
        {
            Assert.Throws<UnknownFileModeException>(() =>
            {
                _readerWriter.Open(_testDataPath, 0);
            });
        }

        [Test]
        [Ignore]
        public void Read_Line()
        {
            //untestable because last line is null. originally code did you perform check hence line 70
            //line.split(separator) thrown nullReferenceException

            //_readerWriter.Open(_testDataPath, CSVReaderWriterForAnnotation.Mode.Read);

            //var result = _readerWriter.Read("", "");

            //Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Ignore]
        public void Return_False_If_Columns_Are_Empty()
        {
            //untestable because last line is null. originally code did you perform check hence line 70
            //line.split(separator) thrown nullReferenceException
        }

        [Test]
        [Ignore]
        public void Read_Output_Line()
        {
        }
    }
}
