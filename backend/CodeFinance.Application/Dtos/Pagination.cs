using CodeFinance.Domain.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Application.Dtos
{
    public class Pagination : IPagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
