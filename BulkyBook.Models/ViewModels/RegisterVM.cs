using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        [DisplayName("Steet Address")]

        public string? StreetAddress { get; set; } 
        public string? City { get; set; } 
        public string? State { get; set; } 
        public string? PostalCode { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string? Role { get; set; } 

        [ValidateNever]
        public IEnumerable<SelectListItem>RoleList { get; set; }

    }
}
