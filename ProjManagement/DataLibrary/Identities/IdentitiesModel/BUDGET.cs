namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.BUDGET")]
    public partial class BUDGET
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BProject_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal Estimated_Income { get; set; }

        public decimal? Estimated_Expense { get; set; }

        public decimal? Estimated_Profit { get; set; }

        public virtual PROJECT PROJECT { get; set; }
    }
}
