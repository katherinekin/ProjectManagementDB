namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.PROJ_RESOURCES")]
    public partial class PROJ_RESOURCES
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RProject_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string Resource_Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Amount_Needed { get; set; }

        public virtual PROJECT PROJECT { get; set; }

        public virtual RESOURCES RESOURCES { get; set; }
    }
}
