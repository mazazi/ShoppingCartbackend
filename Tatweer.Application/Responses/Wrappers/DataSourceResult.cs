using System.Collections;

namespace  Tatweer.Application.Responses.Wrappers
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
    }
}
