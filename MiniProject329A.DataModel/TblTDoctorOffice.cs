﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Doctor_Office")]
    public partial class TblTDoctorOffice
    {
        public TblTDoctorOffice()
        {
            TblTAppointments = new HashSet<TblTAppointment>();
            TblTDoctorOfficeTreatments = new HashSet<TblTDoctorOfficeTreatment>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("medical_facility_id")]
        public long? MedicalFacilityId { get; set; }
        [Column("specialization")]
        [StringLength(100)]
        [Unicode(false)]
        public string Specialization { get; set; } = null!;
        [Column("start_date", TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column("end_date", TypeName = "date")]
        public DateTime EndDate { get; set; }
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

        [ForeignKey("MedicalFacilityId")]
        [InverseProperty("TblTDoctorOffices")]
        public virtual TblMMedicalFacility? MedicalFacility { get; set; }
        [InverseProperty("DoctorOffice")]
        public virtual ICollection<TblTAppointment> TblTAppointments { get; set; }
        [InverseProperty("DoctorOffice")]
        public virtual ICollection<TblTDoctorOfficeTreatment> TblTDoctorOfficeTreatments { get; set; }
    }
}
