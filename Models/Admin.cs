//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShippingManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Admin
    {
        [Required]
        public int AdminId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public System.DateTime DoB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int ContactNumber { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Nullable<bool> Approval { get; set; }
    }
}
