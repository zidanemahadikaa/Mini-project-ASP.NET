using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MiniProject329A.ViewModel
{
    public class VMTblMBiodata
    {
        public long Id { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }

        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}