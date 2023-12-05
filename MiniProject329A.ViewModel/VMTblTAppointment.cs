using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblTAppointment
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public long? Parent { get; set; }
        public long? DoctorOfficeId { get; set; }
        public string? DoctorName { get; set; }
        public long? DoctorOfficeScheduleId { get; set; }
        public long? DoctorOfficeTreatmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Diagnose { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public List<VMTblTPrescription>? Prescriptions { get; set; }
        public List<VMTblTAppointmentDone>? AppointmentDone {  get; set; }

        public List<VMTblTAppointmentCancellation>? AppointmentCancellation { get; set; }
    }
}
