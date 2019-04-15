namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.RESOURCES")]
    public partial class RESOURCES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RESOURCES()
        {
            PROJ_RESOURCES = new HashSet<PROJ_RESOURCES>();
        }

        [Key]
        [StringLength(15)]
        public string Resource_Name { get; set; }

        public byte? Rtype { get; set; }

        [Required]
        [StringLength(200)]
        public string Rdescription { get; set; }

        public byte? Rstatus { get; set; }

        public decimal Amount_Available { get; set; }

        public decimal Total_Cost { get; set; }

        public virtual RESOURCE_STATUS RESOURCE_STATUS { get; set; }

        public virtual RESOURCE_TYPE RESOURCE_TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROJ_RESOURCES> PROJ_RESOURCES { get; set; }
    }
}
