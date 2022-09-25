using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauve.VisualStudio.Community.Options
{
    internal class StandardizationConfiguration
    {
        public bool FormatDocument { get; set; }
        public bool RemoveAndSortUsings { get; set; }
        public bool RegionalizeContents { get; set; }
        public bool AlphabetizeContents { get; set; }
        public bool SaveAll { get; set; }
    }
}
