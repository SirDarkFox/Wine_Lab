using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Wine_Lab.Data.Models
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; set; }
    }
}
