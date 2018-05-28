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
        /// <param name="supplier">Fornecedor</param>
        /// <returns>Lista de produtos</returns>
        [HttpGet("{supplier}")]
        //[Route("[action]")]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public object GetProdutctBySupplierName(string supplier)

        {
            var products = new List<Product>();
            using (var session = RavenDBHolder.Store.OpenSession())
            {
                products = session
                    .Query<ProductViewModelIndex.Result, ProductViewModelIndex>()
                    .Where(x=> x.SupplierName == supplier)
                    .OfType<Product>()
                    .ToList();
            }

            return products;
        }
    }
}
