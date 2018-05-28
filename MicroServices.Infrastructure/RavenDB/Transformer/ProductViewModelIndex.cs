using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroServices.Domain.Model;
using MicroServices.Domain.Northwind;
using Raven.Client.Documents.Indexes;

namespace MicroServices.Infrastructure.RavenDB.Transformer
{
    public class ProductViewModelIndex : AbstractIndexCreationTask<Product>
    {
        public class Result
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string SupplierName { get; set; }
            public string SupplierPhone { get; set; }
            public string CategoryName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal PricePerUnit { get; set; }
        }

        public ProductViewModelIndex()
        {
            Map = products => from product in products
                              //let supplier = LoadDocument<Supplier>(product.Supplier)
                              //let category = LoadDocument<Category>(product.Category)
                select new
                {
                   Id =  product.Id,
                   Name =  product.Name,
                   CategoryName = LoadDocument<Category>(product.Category).Name,
                   PricePerUnit = product.PricePerUnit,
                   QuantityPerUnit = product.QuantityPerUnit,
                   SupplierPhone = LoadDocument<Supplier>(product.Supplier).Phone,
                   SupplierName = LoadDocument<Supplier>(product.Supplier).Name
                };
        }
    }
}
