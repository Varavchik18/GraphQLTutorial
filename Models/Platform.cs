using System.ComponentModel.DataAnnotations;

namespace CommanderGQL.Models
{
    public class Platform
    {
        [Key]
        public int IdPlatform { get; set; }
        [Required]
        public string PlatformName { get; set; }
        public string LicenseKey { get; set; }
    }
}