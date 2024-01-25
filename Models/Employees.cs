namespace PersonnelInformationWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }

        public int? Height { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? Weight { get; set; }

        public int? HireFiscalYear { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(2)]
        public string BloodType { get; set; }

        [StringLength(10)]
        public string PassWords { get; set; }
    }
}
