using InventoryApp.DTO;
using InventoryApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DBContext DBContext;

        public ProductController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            var List = await DBContext.Products.Select(
                s => new ProductDTO
                {
                    IdProduct = s.IdProduct,
                    Name = s.Name,
                    CostProduct = s.CostProduct,
                    CostSell = s.CostSell,
                    Unit = s.Unit,
                    IdTypeProduct = s.IdTypeProduct
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetProductById")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int Id)
        {
            ProductDTO Product = await DBContext.Products.Select(
                    s => new ProductDTO
                    {
                        IdProduct = s.IdProduct,
                        Name = s.Name,
                        CostProduct = s.CostProduct,
                        CostSell = s.CostSell,
                        Unit = s.Unit,
                        IdTypeProduct = s.IdTypeProduct
                    })
                .FirstOrDefaultAsync(s => s.IdProduct == Id);

            if (Product == null)
            {
                return NotFound();
            }
            else
            {
                return Product;
            }
        }

        [HttpPost("InsertProduct")]
        public async Task<HttpStatusCode> InsertProduct(ProductDTO Product)
        {
            var entity = new Product()
            {
                IdProduct = Product.IdProduct,
                Name = Product.Name,
                CostProduct = Product.CostProduct,
                CostSell = Product.CostSell,
                Unit = Product.Unit,
                IdTypeProduct = Product.IdTypeProduct
            };

            DBContext.Products.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateProduct")]
        public async Task<HttpStatusCode> UpdateProduct(ProductDTO Product)
        {
            var entity = await DBContext.Products.FirstOrDefaultAsync(s => s.IdProduct == Product.IdProduct);

            entity.Name = Product.Name;
            entity.CostProduct = Product.CostProduct;
            entity.CostSell = Product.CostSell;
            entity.Unit = Product.Unit;
            entity.IdTypeProduct = Product.IdTypeProduct;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<HttpStatusCode> DeleteProduct(int Id)
        {
            var entity = new Product()
            {
                IdProduct = Id
            };
            DBContext.Products.Attach(entity);
            DBContext.Products.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
