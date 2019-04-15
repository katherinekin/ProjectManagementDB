namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.EMPLOYEE_STATUS")]
    public partial class EMPLOYEE_STATUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE_STATUS()
        {
            EMPLOYEE = new HashSet<EMPLOYEE>();
        }

        [Key]
        public byte ESCode { get; set; }

        [Required]
        [StringLength(20)]
        public string ESDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEE { get; set; }
    }
}
