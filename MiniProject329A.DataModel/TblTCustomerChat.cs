using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Customer_Chat")]
    public partial class TblTCustomerChat
    {
        public TblTCustomerChat()
        {
            TblTCustomerChatHistories = new HashSet<TblTCustomerChatHistory>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("doctor_id")]
        public long? DoctorId { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("TblTCustomerChats")]
        public virtual TblMCustomer? Customer { get; set; }
        [ForeignKey("DoctorId")]
        [InverseProperty("TblTCustomerChats")]
        public virtual TblMDoctor? Doctor { get; set; }
        [InverseProperty("CustomerChat")]
        public virtual ICollection<TblTCustomerChatHistory> TblTCustomerChatHistories { get; set; }
    }
}
