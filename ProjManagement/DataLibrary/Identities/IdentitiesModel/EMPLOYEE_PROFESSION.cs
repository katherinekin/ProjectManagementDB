namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.EMPLOYEE_PROFESSION")]
    public partial class EMPLOYEE_PROFESSION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE_PROFESSION()
        {
            EMPLOYEE = new HashSet<EMPLOYEE>();
        }

        [Key]
        public byte EPCode { get; set; }

        [Required]
        [StringLength(20)]
        public string EPDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEE { get; set; }
    }
}
