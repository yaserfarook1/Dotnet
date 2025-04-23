using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Exceptions;
namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Get transactions by user ID
       [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByUserId(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("User not found");

            var transactions = await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return transactions;
        }

        // 2. Add a transaction
         [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] Transaction transaction)
        {
            var user = await _context.Users.FindAsync(transaction.UserId);
            if (user == null) return NotFound("User not found");
 
            if ((transaction.TransactionType.ToLower() == "withdrawal" && transaction.Amount > user.Balance) || (transaction.TransactionType.ToLower() == "withdrawal" && transaction.Amount <= 0))
            {
                throw new InsufficientBalanceException();
            }
 
            if (transaction.TransactionType.ToLower() == "withdrawal")
            {
                user.Balance -= transaction.Amount;
            }
            else if (transaction.TransactionType.ToLower() == "deposit")
            {
                user.Balance += transaction.Amount;
            }
 
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetTransactionsByUserId), new { userId = transaction.UserId }, transaction);
        }

       // 3. Filter transactions by type (TransactionType is required)
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Transaction>>> FilterTransactionsByType(string transactionType)
        {
            // Filter transactions by the provided transaction type
            var transactions = await _context.Transactions
                                            .Where(t => t.TransactionType == transactionType)
                                            .ToListAsync();

            // If no transactions match the specified type, return a NotFound response
            if (!transactions.Any())
            {
                return NoContent();
            }

            return transactions;
        }
    }
}
