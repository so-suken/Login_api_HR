namespace PersonnelInformationWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BelongTo")]
    public partial class BelongTo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BelongID { get; set; }

        public int? EmployeeID { get; set; }

        public int? DepartmentID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
