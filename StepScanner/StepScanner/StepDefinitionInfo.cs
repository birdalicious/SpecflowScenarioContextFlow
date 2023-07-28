using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StepScanner
{
    public record StepDefinitionInfo
    {
        public required string StepType { get; set; }
        public required string Regex { get; set; }
        public required string MethodType { get; set; }
        public required string ClassName { get; set; }
    }
}
