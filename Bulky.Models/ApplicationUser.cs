﻿using Bulky.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        [Display(Name = "Post Code")]
        public string? PostalCode { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
