namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.ACTIVITIES")]
    public partial class ACTIVITIES
    {
        public int? AEmployee_ID { get; set; }

        public int? AProject_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 0)]
        public decimal Weekly_Hours { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Week_Date { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
