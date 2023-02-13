using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Core.Data
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
