//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webbshop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Order = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail kr�vs f�r att skapa en anv�ndare.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Din e-mail m�ste inneh�lla ett '@'")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "M�ste inneh�lla minst 5 tecken och som mest 30")]
        public string Email { get; set; }

        [Display(Name = "F�rnamn")]
        [Required(ErrorMessage = "Fyll i ditt f�rnamn")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s]*$", ErrorMessage = "Inga siffror och b�rja med versal")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "M�ste inneh�lla minst 2 teckan och mest 20")]
        public string First_Name { get; set; }

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Fyll i ditt efternamn")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s]*$", ErrorMessage = "Inga siffror och b�rja med versal")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "M�ste inneh�lla minst 2 teckan och mest 20")]
        public string Last_Name { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Du beh�ver fylla i ditt telefonnummer f�r att skapa en anv�ndare")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Ditt telefonnummer f�r bara inneh�lla siffror.")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "M�ste inneh�lla minst 7 tecken, och som mest 20")]
        public string Phone_number { get; set; }

        [Display(Name = "Adress")]
        [Required(ErrorMessage = "Du beh�ver fylla i din adress f�r att skapa en anv�ndare")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s\d]*$", ErrorMessage = "B�rja din adress med versal")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "M�ste inneh�lla minst 10 teckan och mest 25")]
        public string Address { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Du beh�ver fylla i postnummer f�r att skapa en anv�ndare")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Ditt postnummer f�r bara inneh�lla siffror")]
        [StringLength(7, MinimumLength = 4, ErrorMessage = "M�ste inneh�lla minst 4 tecken, och mest 7")]
        public string Postal_Code { get; set; }

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Ort beh�vs f�r att skapa en anv�ndare")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s]*$", ErrorMessage = "Inga siffror och b�rja med versal")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "M�ste inneh�lla minst 2 teckan och mest 20")]
        public string City { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "Land beh�vs f�r att skapa en anv�ndare")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s]*$", ErrorMessage = "Inga siffror och b�rja med versal")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "M�ste inneh�lla minst 2 teckan och mest 20")]
        public string Country { get; set; }

        [Display(Name = "L�senord")]
        [Required(ErrorMessage = "Du beh�ver ett l�senord f�r att skapa en anv�ndare")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "M�ste inneh�lla minst 8 teckan och mest 20")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
