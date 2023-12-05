using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblMCustomerMember
    {
        public long Id { get; set; }
        public long? ParentBiodataId { get; set; }
        public string? ParentName { get; set; }
        public string? CustomerName { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? DobCustomer { get; set; }
        //public List<VMTblMCustomer?> Customer { get; set; }
        public long? CustomerRelationId { get; set; }
        public string? CustomerRelationName { get; set; }
        public long CreateBy { get; set; }        
        public DateTime CreateOn { get; set; }        
        public long? ModifiedBy { get; set; }        
        public DateTime? ModifiedOn { get; set; }        
        public long? DeletedBy { get; set; }         
        public DateTime? DeletedOn { get; set; }        
        public bool IsDelete { get; set; }
    }
}
