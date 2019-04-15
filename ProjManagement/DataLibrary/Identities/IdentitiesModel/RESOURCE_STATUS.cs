namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.RESOURCE_STATUS")]
    public partial class RESOURCE_STATUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RESOURCE_STATUS()
        {
            RESOURCES = new HashSet<RESOURCES>();
        }

        [Key]
        public byte RSCode { get; set; }

        [Required]
        [StringLength(20)]
        public string RSDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESOURCES> RESOURCES { get; set; }
    }
}
