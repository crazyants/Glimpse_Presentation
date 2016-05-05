using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiehl.App.Data.Models
{
    public class Feature : IAmLookupItem
    {
        public static string Clients = "Clients";
        public static string Payees = "Payees";
        public static string Funds = "Funds";
        public static string Restitution = "Restitution";

        public int Id { get; set; }

        [Required, Index(IsUnique = true),
         MaxLength(255), MinLength(1)]
        public string Name { get; set; }

        [Required,
         MaxLength(255), MinLength(1)]
        public string Description { get; set; }
    }
}