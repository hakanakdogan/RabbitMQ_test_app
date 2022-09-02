using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitDeneme.Models;
using RabbitDeneme.RabbitMQ;
using RabbitDeneme.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRabbitMQProducer _producer;
        private readonly IProductService _productService;

        public ProductController(IRabbitMQProducer producer, IProductService productService)
        {
            _producer = producer;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> ProductList()
        {
            try
            {
                var products = _productService.GetProductList();

                if (products == null) return NotFound("Product'lar Bulunamadı");
                return Ok(products);
            }
            catch (System.Exception)
            {

                return BadRequest("Bir şeyler yanlış gitti");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);

                if (product == null) return NotFound("Product Bulunamadı");
                return Ok(product);
            }
            catch (System.Exception)
            {

                return BadRequest("Bir şeyler yanlış gitti");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                var prodToShow = _productService.AddProduct(product);
                _producer.SendProductMessage(prodToShow);

                return Ok(prodToShow);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            try
            {
                var prodToShow = _productService.UpdateProduct(product);

                return Ok(prodToShow);
            }
            catch (System.Exception)
            {

                return BadRequest("Bir şeyler yanlış gitti");
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            try
            {
                var res = _productService.DeleteProduct(id);
                return Ok(res);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



    }
}
