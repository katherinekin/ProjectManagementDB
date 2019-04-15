namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.EMPLOYEE")]
    public partial class EMPLOYEE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE()
        {
            DEPARTMENT1 = new HashSet<DEPARTMENT>();
            ACTIVITIES = new HashSet<ACTIVITIES>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Employee_ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Fname { get; set; }

        [Required]
        [StringLength(15)]
        public string Lname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Of_Birth { get; set; }

        public int Ssn { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public byte? Type { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public int Salary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Start_Date { get; set; }

        public byte? Estatus { get; set; }

        [StringLength(20)]
        public string EDname { get; set; }

        public byte? Profession { get; set; }

        public int? Super_Ssn { get; set; }

        public virtual DEPARTMENT DEPARTMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEPARTMENT> DEPARTMENT1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVITIES> ACTIVITIES { get; set; }

        public virtual LOGIN LOGIN { get; set; }

        public virtual EMPLOYEE_PROFESSION EMPLOYEE_PROFESSION { get; set; }

        public virtual EMPLOYEE_STATUS EMPLOYEE_STATUS { get; set; }

        public virtual EMPLOYEE_TYPE EMPLOYEE_TYPE { get; set; }
    }
}
