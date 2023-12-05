using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblTToken
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public long? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public bool? IsExpired { get; set; }
        public string? UsedFor { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }
    }
}
