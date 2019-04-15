namespace DataLibrary.Identities.IdentitiesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PM.LOGIN")]
    public partial class LOGIN
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LEmployee_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }
    }
}
