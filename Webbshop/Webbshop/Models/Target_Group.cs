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

    public partial class Target_Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Target_Group()
        {
            this.Product = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Display(Name = "M�lgrupp")]
        [Required(ErrorMessage = "M�lgrupp beh�vs")]
        [RegularExpression(@"^[A-Z������]+[-a-zA-Z_/\\.,������\s]*$", ErrorMessage = "Inga siffror och b�rja med versal")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "M�ste inneh�lla minst 2 teckan och mest 20")]
        public string Target_Group1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
