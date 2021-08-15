using System.ComponentModel.DataAnnotations;

namespace TrelloAutomation.API.API.DataContracts
{
    /// <summary>
    /// User datacontract summary to be replaced
    /// </summary>
    public class User
    {
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        public Address Address { get; set; }

    }
}
