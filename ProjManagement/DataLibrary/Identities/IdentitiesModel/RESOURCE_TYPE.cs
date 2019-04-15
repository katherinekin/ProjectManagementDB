namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.RESOURCE_TYPE")]
    public partial class RESOURCE_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RESOURCE_TYPE()
        {
            RESOURCES = new HashSet<RESOURCES>();
        }

        [Key]
        public byte RTCode { get; set; }

        [Required]
        [StringLength(20)]
        public string RTDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESOURCES> RESOURCES { get; set; }
    }
}
