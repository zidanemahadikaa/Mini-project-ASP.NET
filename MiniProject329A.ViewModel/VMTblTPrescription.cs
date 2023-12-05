using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblTPrescription
    {
        public long Id { get; set; }
        public long? AppointmentId { get; set; }
        public long? MedicalItemId { get; set; }
        public string? Dosage { get; set; }
        public string? Directions { get; set; }
        public string? Time { get; set; }
        public string? Notes { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
