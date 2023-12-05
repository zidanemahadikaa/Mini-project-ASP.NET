using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MiniProject329A.ViewModel
{
    public class VMTblMDoctor
    {
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string? Image_Path { get; set; }

        public IFormFile? ImageFile {  get; set; }
        public string? Str { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }


        public List<VMTblMDoctorEducation>? DoctorEducation {  get; set; }
        
        public List<VMTblTCurrentDoctorSpecialization>? CurrentDoctorSpecialization { get; set; }
        public VMTblMBiodata? Biodata {  get; set; }

        public List<VMTblTDoctorOfficeSchedule>? DoctorOfficeSchedule { get; set; }

        public List<VMTblTDoctorOffice>? DoctorOffice {  get; set; }
    }
}
