using System;
using System.IO;
using System.Threading;
using AddressProcessing.CSV;
using AddressProcessing.Models;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVReaderWriterShould
    {
        private readonly string _testDataPath = Path.Combine(Directory.GetCurrentDirectory() + @"\test_data\contacts.csv");
        private CSVReaderWriterForAnnotation _readerWriter;

        [SetUp]
        public void SetUp()
        {
            _readerWriter = new CSVReaderWriterForAnnotation();
        }

        [TearDown]
        public void TearDown()
        {
            _readerWriter.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Characteristic Testing

        [TestCase(CSVReaderWriter.Mode.Write)]
        [TestCase(CSVReaderWriter.Mode.Read)]
        public void Do_Not_Throw_If_Mode_Is_Supported(CSVReaderWriter.Mode mode)
        {
            var readerWriter = new CSVReaderWriter();

            Assert.DoesNotThrow(() =>
            {
                readerWriter.Open(_testDataPath, mode);
            });
        }

        [Test]
        public void Throw_If_Mode_Is_Not_Supported()
        {
            var readerWriter = new CSVReaderWriter();

            Assert.Throws<UnknownFileModeException>(() =>
            {
                readerWriter.Open(_testDataPath, 0);
            });
        }

        [Test]
        public void Read_Line()
        {
            _readerWriter.Open(_testDataPath, CSVReaderWriterForAnnotation.Mode.Read);

            var result = _readerWriter.Read("", "");

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Ignore]
        public void Return_False_If_Columns_Are_Empty()
        {
            //untestable because last line is null. originally code did you perform check hence line 70
            //line.split(separator) thrown nullReferenceException
        }

        [Test]
        public void Read_Output_Line()
        {
            _readerWriter.Open(_testDataPath, CSVReaderWriterForAnnotation.Mode.Read);

            var result = _readerWriter.Read(out string firstColumn, out string secondColumn);


            Assert.That(result, Is.EqualTo(true));
            Assert.That(firstColumn, Is.EqualTo("Shelby Macias"));
            Assert.That(secondColumn, Is.EqualTo("3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|England"));
        }

        #endregion

        //[Test]
        //public void Read_First_Line_Of_File()
        //{
        //    var _readerWriter = new CSVReaderWriter();
        //    _readerWriter.Open(_testDataPath, CSVReaderWriter.Mode.Read);

        //    var firstColumn = string.Empty;
        //    var secondColumn = string.Empty;

        //    _readerWriter.Read(out firstColumn, out secondColumn);

        //    Assert.That(firstColumn, Is.EqualTo("Shelby Macias"));
        //}

        //[Test]
        //public void Read_Last_Line_Of_File()
        //{
        //    var _readerWriter = new CSVReaderWriter();
        //    _readerWriter.Open(_testDataPath, CSVReaderWriter.Mode.Read);

        //    var firstColumn = string.Empty;
        //    var secondColumn = string.Empty;

        //    while (_readerWriter.Read(out firstColumn, out secondColumn))
        //    {

        //    }

        //    Assert.That(firstColumn, Is.EqualTo("Leila Neal"));
        //}
    }
}
