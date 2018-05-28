using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Domain.Model;
using MicroServices.Domain.Northwind;
using MicroServices.Infrastructure.RavenDB;
using MicroServices.Infrastructure.RavenDB.Transformer;

namespace MicroServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : Controller
    {
        /// <summary>
        /// Efetua uma busca de produtos pelo fornecedor informado
        /// </summary>
        /// <example>Tokyo Traders</example>
        /// <param name="supplier">Fornecedor</param>
        /// <returns>Lista de produtos com informações do fornecedor e categoria</returns>
        [HttpGet, Route("[action]/{supplier}", Name = "GetProductBySupplierName")]
        [ProducesResponseType(typeof(List<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ProductBySupplierName([FromRoute] string supplier)

        {
            var productsVM = new List<ProductViewModel>();
            using (var session = RavenDBHolder.Store.OpenSession())
            {
                var products = session
                    .Query<ProductViewModelIndex.Result, ProductViewModelIndex>()
                    .Where(x=> x.SupplierName == supplier)
                    .OfType<Product>()
                    .ToList();

                foreach (var product in products)
                {
                    var prd = new ProductViewModel
                    {
                        Name = product.Name,
                        Id = product.Id,
                        SupplierPhone = session.Load<Supplier>(product.Supplier).Phone,
                        QuantityPerUnit = product.QuantityPerUnit,
                        PricePerUnit = product.PricePerUnit,
                        SupplierName = session.Load<Supplier>(product.Supplier).Name,
                        CategoryName = session.Load<Category>(product.Category).Name
                    };
                    productsVM.Add(prd);
                }
            }

            return Ok(productsVM);
        }

        /// <summary>
        /// Efetua uma busca de produtos pelo nome informado
        /// </summary>
        /// <example>Flotemysost</example>
        /// <param name="productName">Nome do produto</param>
        /// <returns>Produto</returns>
        [HttpGet, Route("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ProductByName([FromRoute] string productName)

        {
            var product = new Product();
            using (var session = RavenDBHolder.Store.OpenSession())
            {
                product = session.Query<Product>().Where(p => p.Name == productName).First();
            }

            return Ok(product);
        }
    }
}
