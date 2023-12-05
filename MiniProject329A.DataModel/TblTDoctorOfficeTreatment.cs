using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Doctor_Office_Treatment")]
    public partial class TblTDoctorOfficeTreatment
    {
        public TblTDoctorOfficeTreatment()
        {
            TblTAppointmentRescheduleHistories = new HashSet<TblTAppointmentRescheduleHistory>();
            TblTAppointments = new HashSet<TblTAppointment>();
            TblTDoctorOfficeTreatmentPrices = new HashSet<TblTDoctorOfficeTreatmentPrice>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_treatment_id")]
        public long? DoctorTreatmentId { get; set; }
        [Column("doctor_office_id")]
        public long? DoctorOfficeId { get; set; }
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

        [ForeignKey("DoctorOfficeId")]
        [InverseProperty("TblTDoctorOfficeTreatments")]
        public virtual TblTDoctorOffice? DoctorOffice { get; set; }
        [ForeignKey("DoctorTreatmentId")]
        [InverseProperty("TblTDoctorOfficeTreatments")]
        public virtual TblTDoctorTreatment? DoctorTreatment { get; set; }
        [InverseProperty("DoctorOfficeTreatment")]
        public virtual ICollection<TblTAppointmentRescheduleHistory> TblTAppointmentRescheduleHistories { get; set; }
        [InverseProperty("DoctorOfficeTreatment")]
        public virtual ICollection<TblTAppointment> TblTAppointments { get; set; }
        [InverseProperty("DoctorOfficeTreatment")]
        public virtual ICollection<TblTDoctorOfficeTreatmentPrice> TblTDoctorOfficeTreatmentPrices { get; set; }
    }
}
