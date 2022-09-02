using RabbitDeneme.Data;
using RabbitDeneme.Models;
using System.Collections.Generic;
using System.Linq;

namespace RabbitDeneme.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product product)
        {
            var res = _context.Products.Add(product);
            _context.SaveChanges();
            return res.Entity;
        }

        public bool DeleteProduct(int id)
        {
            var prod = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();

            var res = _context.Products.Remove(prod);

            _context.SaveChanges();

            return res != null ? true : false;
        }

        public Product GetProductById(int id)
        {
             
            return _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductList()
        {
            return _context.Products.ToList();
        }

        public Product UpdateProduct(Product product)
        {
            var res = _context.Products.Update(product);
            _context.SaveChanges();
            return res.Entity;
        }
    }
}
