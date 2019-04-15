namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.EMPLOYEE_TYPE")]
    public partial class EMPLOYEE_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE_TYPE()
        {
            EMPLOYEE = new HashSet<EMPLOYEE>();
        }

        [Key]
        public byte ETCode { get; set; }

        [Required]
        [StringLength(20)]
        public string ETDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEE { get; set; }
    }
}
