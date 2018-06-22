using Adressbuch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.DataTransfer
{
    public class SearchCriteria<T>
    {
        public T Value { get; set; }
        public LogicalOperators LogicalOperator { get; set; }
        public bool IsSpecified { get; set; }
    }
}
