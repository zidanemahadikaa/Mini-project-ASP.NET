using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblMMenuRole
    {
        public long Id { get; set; }
        public long? MenuId { get; set; }
        public string? NamaMenu { get; set; }
        public string? url { get; set; }
        public long? RoleId { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

    }
}
