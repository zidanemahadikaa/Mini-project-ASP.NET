using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Appointment_Reschedule_History")]
    public partial class TblTAppointmentRescheduleHistory
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("appointment_id")]
        public long? AppointmentId { get; set; }
        [Column("doctor_office_schedule_id")]
        public long? DoctorOfficeScheduleId { get; set; }
        [Column("doctor_office_treatment_id")]
        public long? DoctorOfficeTreatmentId { get; set; }
        [Column("appointment_date", TypeName = "datetime")]
        public DateTime? AppointmentDate { get; set; }
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
        public bool IsDelete { get; set; }

        [ForeignKey("AppointmentId")]
        [InverseProperty("TblTAppointmentRescheduleHistories")]
        public virtual TblTAppointment? Appointment { get; set; }
        [ForeignKey("DoctorOfficeScheduleId")]
        [InverseProperty("TblTAppointmentRescheduleHistories")]
        public virtual TblTDoctorOfficeSchedule? DoctorOfficeSchedule { get; set; }
        [ForeignKey("DoctorOfficeTreatmentId")]
        [InverseProperty("TblTAppointmentRescheduleHistories")]
        public virtual TblTDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
    }
}
