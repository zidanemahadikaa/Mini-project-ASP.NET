﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Doctor_Treatment")]
    public partial class TblTDoctorTreatment
    {
        public TblTDoctorTreatment()
        {
            TblTDoctorOfficeTreatments = new HashSet<TblTDoctorOfficeTreatment>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool? IsDelete { get; set; }

        [ForeignKey("DoctorId")]
        [InverseProperty("TblTDoctorTreatments")]
        public virtual TblMDoctor? Doctor { get; set; }
        [InverseProperty("DoctorTreatment")]
        public virtual ICollection<TblTDoctorOfficeTreatment> TblTDoctorOfficeTreatments { get; set; }
    }
}
