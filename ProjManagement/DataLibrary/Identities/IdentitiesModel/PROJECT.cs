namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.PROJECT")]
    public partial class PROJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROJECT()
        {
            ACTIVITIES = new HashSet<ACTIVITIES>();
            BUDGET = new HashSet<BUDGET>();
            PROJ_RESOURCES = new HashSet<PROJ_RESOURCES>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Project_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Pname { get; set; }

        [StringLength(20)]
        public string PDname { get; set; }

        [Required]
        [StringLength(20)]
        public string Client { get; set; }

        [Required]
        [StringLength(200)]
        public string Pdescription { get; set; }

        [StringLength(200)]
        public string Deliverables { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Open_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Close_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Completion_Date { get; set; }

        public byte? Pstatus { get; set; }

        [StringLength(200)]
        public string Collaborators { get; set; }

        public virtual DEPARTMENT DEPARTMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVITIES> ACTIVITIES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUDGET> BUDGET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROJ_RESOURCES> PROJ_RESOURCES { get; set; }

        public virtual PROJECT_STATUS PROJECT_STATUS { get; set; }
    }
}
