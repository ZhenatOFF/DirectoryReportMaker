using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReportMaker
{
    public class MimeTypeStatistics
    {
        public string Extension { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
        public long AverageSize { get; set; }
    }
}
