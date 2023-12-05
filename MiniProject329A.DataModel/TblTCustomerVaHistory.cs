﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Customer_Va_History")]
    public partial class TblTCustomerVaHistory
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_va_id")]
        public long? CustomerVaId { get; set; }
        [Column("amount", TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column("expired_on", TypeName = "datetime")]
        public DateTime? ExpiredOn { get; set; }
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

        [ForeignKey("CustomerVaId")]
        [InverseProperty("TblTCustomerVaHistories")]
        public virtual TblTCustomerVa? CustomerVa { get; set; }
    }
}
