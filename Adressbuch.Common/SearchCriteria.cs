using Adressbuch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Common
{
    public class SearchCriteria<T>
    {
        public T Value { get; set; }
        public LogicalOperators LogicalOperator { get; set; }
        public bool IsSpecified { get; set; }

        public static ICollection<LogicalOperators> ValidOperators
        {
            get
            {
                List<LogicalOperators> returnValue = null;
                string typeName = typeof(T).Name.StartsWith("Nullable") ? Nullable.GetUnderlyingType(typeof(T)).Name : typeof(T).Name;

                var lOs = Enum.GetValues(typeof(LogicalOperators));
                switch (typeName)
                {
                    case nameof(String):
                        returnValue = new List<LogicalOperators>(lOs.Cast<LogicalOperators>().Where(lo =>
                        lo == LogicalOperators.Contains ||
                        lo == LogicalOperators.EndsWith ||
                        lo == LogicalOperators.Equals ||
                        lo == LogicalOperators.StartsWith));
                        break;
                    case nameof(DateTime):
                    case "Nullable`1":
                        returnValue = new List<LogicalOperators>(lOs.Cast<LogicalOperators>().Where(lo =>
                        lo == LogicalOperators.Equals ||
                        lo == LogicalOperators.GreaterThan ||
                        lo == LogicalOperators.GreaterThanOrEqual ||
                        lo == LogicalOperators.LessThan ||
                        lo == LogicalOperators.LessThanOrEqual));
                        break;
                    default:
                        returnValue = new List<LogicalOperators>(lOs.Cast<LogicalOperators>());
                        break;
                }
                return returnValue;
            }
        }
    }
}
