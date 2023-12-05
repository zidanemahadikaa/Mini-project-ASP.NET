using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblTDoctorOffice
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string Specialization { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }

        public List<VMTblMMedicalFacility>? MedicalFacility { get; set; }

    }
}
