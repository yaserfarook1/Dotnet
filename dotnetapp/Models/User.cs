using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; } // Represents the user's wallet balance

        [JsonIgnore]
        public ICollection<Transaction>? Transactions { get; set; }
    }
}