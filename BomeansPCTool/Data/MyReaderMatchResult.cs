using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bomeans.IRNet;

namespace BomeansPCTool
{
    public class MyReaderMatchResult
    {
        public ReaderMatchResult MatchResult { get; set; }
        public byte[] RawLearningData { get; set; }
    }
}
