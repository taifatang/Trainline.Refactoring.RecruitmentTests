using AddressProcessing.Models;

namespace AddressProcessing.CSV
{
    public class ReaderResult
    {
        public bool HasResult
        {
            get { return Columns != null && Columns.Length > 0; }
        }
        public Column[] Columns { get; set; }
    }
}