using InventoryApp.DTO;
using InventoryApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly DBContext DBContext;

        public TransactionController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetTransactions")]
        public async Task<ActionResult<List<TransactionsDTO>>> Get()
        {
            //var Products = await DBContext.Products; // TODO: Add Where condition
            var List = await DBContext.Transactions.Select(
                s => new TransactionsDTO
                {
                    IdTransactions = s.IdTransactions,
                    TypeTransaction = s.TypeTransaction,
                    Quantity = s.Quantity,
                    Value = s.Value,
                    Date_Transaction = s.Date_Transaction,
                    Products = new List<ProductDTO>()
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                List.ForEach(async x =>
                {
                    var productsIds = await DBContext.TransactionProducts.Where(y => y.IdTransactions == x.IdTransactions).Select(z => z.IdProduct).ToListAsync();
                    List<ProductDTO> products = new List<ProductDTO>();
                    productsIds.ForEach(async z => products.Add(await DBContext.Products.Where(p => p.IdProduct == z).Select(
                    s => new ProductDTO
                    {
                        IdProduct = s.IdProduct,
                        Name = s.Name,
                        CostProduct = s.CostProduct,
                        CostSell = s.CostSell,
                        Unit = s.Unit,
                        IdTypeProduct = s.IdTypeProduct
                    }
                    ).FirstOrDefaultAsync()));
                    x.Products = products;
                });
                return List;
            }
        }

        [HttpGet("GetTransactionById")]
        public async Task<ActionResult<TransactionsDTO>> GetTransactionById(int Id)
        {
            TransactionsDTO Transaction = await DBContext.Transactions.Select(
                    s => new TransactionsDTO
                    {
                        IdTransactions = s.IdTransactions,
                        TypeTransaction = s.TypeTransaction,
                        Quantity = s.Quantity,
                        Value = s.Value,
                        Date_Transaction = s.Date_Transaction,
                        Products = new List<ProductDTO>()
                    })
                .FirstOrDefaultAsync(s => s.IdTransactions == Id);

            if (Transaction == null)
            {
                return NotFound();
            }
            else
            {
                var productsIds = await DBContext.TransactionProducts.Where(y => y.IdTransactions == Transaction.IdTransactions).Select(z => z.IdProduct).ToListAsync();
                List<ProductDTO> products = new List<ProductDTO>();
                productsIds.ForEach(async z => products.Add(await DBContext.Products.Where(p => p.IdProduct == z).Select(
                s => new ProductDTO
                {
                    IdProduct = s.IdProduct,
                    Name = s.Name,
                    CostProduct = s.CostProduct,
                    CostSell = s.CostSell,
                    Unit = s.Unit,
                    IdTypeProduct = s.IdTypeProduct
                }
                ).FirstOrDefaultAsync()));
                Transaction.Products = products;
                return Transaction;
            }
        }

        [HttpPost("InsertTransaction")]
        public async Task<HttpStatusCode> InsertTransaction(TransactionsDTO Transaction)
        {
            var entity = new Transactions()
            {
                IdTransactions = Transaction.IdTransactions,
                TypeTransaction = Transaction.TypeTransaction,
                Quantity = Transaction.Quantity,
                Value = Transaction.Value,
                Date_Transaction = Transaction.Date_Transaction,
                Products = Transaction.Products.Select(s => new Product
                {
                    IdProduct = s.IdProduct,
                    Name = s.Name,
                    CostProduct = s.CostProduct,
                    CostSell = s.CostSell,
                    Unit = s.Unit,
                    IdTypeProduct = s.IdTypeProduct
                }).ToList()
            };
            DBContext.Transactions.Add(entity);

            entity.Products.ForEach(x =>
            {
                var prod = new Product
                {
                    IdProduct = x.IdProduct,
                    Name = x.Name,
                    CostProduct = x.CostProduct,
                    CostSell = x.CostSell,
                    Unit = x.Unit,
                    IdTypeProduct = x.IdTypeProduct
                };
                DBContext.Products.Add(prod);
            });

            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateTransaction")]
        public async Task<HttpStatusCode> UpdateTransaction(TransactionsDTO Transaction)
        {
            var entity = await DBContext.Transactions.FirstOrDefaultAsync(s => s.IdTransactions == Transaction.IdTransactions);

            entity.TypeTransaction = Transaction.TypeTransaction;
            entity.Quantity = Transaction.Quantity;
            entity.Value = Transaction.Value;
            entity.Date_Transaction = Transaction.Date_Transaction;
            entity.Products = Transaction.Products.Select(s => new Product
            {
                IdProduct = s.IdProduct,
                Name = s.Name,
                CostProduct = s.CostProduct,
                CostSell = s.CostSell,
                Unit = s.Unit,
                IdTypeProduct = s.IdTypeProduct
            }).ToList();

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteTransaction/{Id}")]
        public async Task<HttpStatusCode> DeleteTransaction(int Id)
        {
            var entity = new Transactions()
            {
                IdTransactions = Id
            };
            DBContext.Transactions.Attach(entity);
            DBContext.Transactions.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
