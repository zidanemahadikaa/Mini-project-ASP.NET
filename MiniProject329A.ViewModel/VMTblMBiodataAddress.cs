using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblMBiodataAddress
    {
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string? Label { get; set; }
        public string? Recipient { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public long? LocationId { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        //public virtual TblMBiodata? Biodata { get; set; }
       
        //public virtual TblMLocation? Location { get; set; }
    }
}
