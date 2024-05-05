using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductSpecParams
    {
        private const int MaxSize = 10;
        private int pageSize = 5;
        public int PageSize {
            get { return pageSize; }
            set { pageSize = value > MaxSize ? MaxSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? sort {  get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }

    }
}
