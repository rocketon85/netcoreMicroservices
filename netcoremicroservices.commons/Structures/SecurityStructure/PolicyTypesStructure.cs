using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Commons.Structures.SecurityStructure
{
    public abstract class PolicyTypesBaseStructure
    {
        public abstract string ViewDetail { get; }
        public abstract string ViewList { get; }
        public abstract string Edit { get; }
        public abstract string Delete { get; }
        public abstract string Add { get; }
    }
}
