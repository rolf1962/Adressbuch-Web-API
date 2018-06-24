using Adressbuch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.DataTransfer
{
    public class PersonSearchDto
    {
        public SearchCriteria<string> Name { get; } = new SearchCriteria<string>();
        public SearchCriteria<string> Vorname { get; } = new SearchCriteria<string>();
        public SearchCriteria<DateTime?> GeburtsdatumVon { get; } = new SearchCriteria<DateTime?>();
        public SearchCriteria<DateTime?> GeburtsdatumBis { get; } = new SearchCriteria<DateTime?>();
    }
}
