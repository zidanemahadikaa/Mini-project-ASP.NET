using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Appointment")]
    public partial class TblTAppointment
    {
        public TblTAppointment()
        {
            TblTAppointmentCancellations = new HashSet<TblTAppointmentCancellation>();
            TblTAppointmentDones = new HashSet<TblTAppointmentDone>();
            TblTAppointmentRescheduleHistories = new HashSet<TblTAppointmentRescheduleHistory>();
            TblTPrescriptions = new HashSet<TblTPrescription>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("doctor_office_id")]
        public long? DoctorOfficeId { get; set; }
        [Column("doctor_office_schedule_id")]
        public long? DoctorOfficeScheduleId { get; set; }
        [Column("doctor_office_treatment_id")]
        public long? DoctorOfficeTreatmentId { get; set; }
        [Column("appointment_date", TypeName = "date")]
        public DateTime? AppointmentDate { get; set; }
        [Column("create_by")]
        public long CreateBy { get; set; }
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

        [ForeignKey("CustomerId")]
        [InverseProperty("TblTAppointments")]
        public virtual TblMCustomer? Customer { get; set; }
        [ForeignKey("DoctorOfficeId")]
        [InverseProperty("TblTAppointments")]
        public virtual TblTDoctorOffice? DoctorOffice { get; set; }
        [ForeignKey("DoctorOfficeScheduleId")]
        [InverseProperty("TblTAppointments")]
        public virtual TblTDoctorOfficeSchedule? DoctorOfficeSchedule { get; set; }
        [ForeignKey("DoctorOfficeTreatmentId")]
        [InverseProperty("TblTAppointments")]
        public virtual TblTDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<TblTAppointmentCancellation> TblTAppointmentCancellations { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<TblTAppointmentDone> TblTAppointmentDones { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<TblTAppointmentRescheduleHistory> TblTAppointmentRescheduleHistories { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<TblTPrescription> TblTPrescriptions { get; set; }
    }
}
