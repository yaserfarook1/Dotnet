using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; } // Amount of the transaction
        public string TransactionDate { get; set; } // Date of the transaction
        public string TransactionType { get; set; } // Type of transaction (e.g., "AddFunds", "Transfer", "Payment")
        public string Description { get; set; } // Reason or description of the transaction
        public int UserId { get; set; } // Foreign key to User

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }
    }
}