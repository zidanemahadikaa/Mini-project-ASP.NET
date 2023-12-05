using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblMDoctorEducation
    {
        public long Id { get; set; }

        public long? DoctorId { get; set; }

        public long? EducationLevelId { get; set; }

        public string? EducationLevelName {  get; set; }

        public string? InstituteName { get; set; }

        public string? Major { get; set; }
        
        public string? StartYear { get; set; }
        
        public string? EndYear { get; set; }
        
        public bool? IsLastEducation { get; set; }
        
        public long CreateBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public long? ModifiedBy { get; set; }
        
        public DateTime? ModifiedOn { get; set; }
        
        public long? DeletedBy { get; set; }
        
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

    }
}
