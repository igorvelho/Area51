using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Domain.Model
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string CategoryName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal PricePerUnit { get; set; }


    }
}
