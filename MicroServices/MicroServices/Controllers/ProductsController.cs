using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Domain.Northwind;
using MicroServices.Infrastructure.RavenDB;

namespace MicroServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : Controller
    {

        [HttpGet]
        //[Route("[action]")]
        //[ProducesResponseType(typeof(Product>), (int)HttpStatusCode.OK)]
        public string Get()

        {
            var prod = new Product();
            using (var session = RavenDBHolder.Store.OpenSession())
            {
                prod = session.Load<Product>("products/77-A");
            }

            return prod.Name;
        }
    }
}
