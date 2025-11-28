using System.Runtime.CompilerServices;
using AspNetCoreGeneratedDocument;
using MyStore.Context;
using MyStore.Entities;

namespace MyStore.Repositories 
{
    public class OrderRepository : GenericRepository<Order>// Repository for managing orders
    {
        private readonly AppDbContext _dbcontext;
        public OrderRepository(AppDbContext dbcontext) : base(dbcontext) // constructor
        {
            _dbcontext = dbcontext;
        }   

        public override async Task AddAsync(Order order)
        {
            using var transaction = await _dbcontext.Database.BeginTransactionAsync(); // Start a database transaction

            try
            {
                foreach (var detail in order.OrderItems)
                {
                    var product = await _dbcontext.Products.FindAsync(detail.ProductId);
                    product.Stock -= detail.Quantity; // Reduce stock by the quantity ordered
                }

                await _dbcontext.Order.AddAsync(order); // Add the order to the database
                await _dbcontext.SaveChangesAsync(); // Save changes to the database
                await transaction.CommitAsync(); // Commit the transaction  
            }
            catch
            {
                await transaction.RollbackAsync(); // Rollback the transaction in case of an error
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}