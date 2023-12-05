﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Customer_Va")]
    public partial class TblTCustomerVa
    {
        public TblTCustomerVa()
        {
            TblTCustomerVaHistories = new HashSet<TblTCustomerVaHistory>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("va_number")]
        [StringLength(30)]
        [Unicode(false)]
        public string? VaNumber { get; set; }
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

        [ForeignKey("Id")]
        [InverseProperty("TblTCustomerVa")]
        public virtual TblMCustomer IdNavigation { get; set; } = null!;
        [InverseProperty("CustomerVa")]
        public virtual ICollection<TblTCustomerVaHistory> TblTCustomerVaHistories { get; set; }
    }
}
