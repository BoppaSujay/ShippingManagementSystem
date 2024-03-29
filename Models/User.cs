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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.CargoBookings = new HashSet<CargoBooking>();
            this.Feedbacks = new HashSet<Feedback>();
            this.PassengerBookings = new HashSet<PassengerBooking>();
        }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage = "Name must be between 3 and 50 characters")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public System.DateTime DoB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int ContactNumber { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CargoBooking> CargoBookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassengerBooking> PassengerBookings { get; set; }
    }
}
