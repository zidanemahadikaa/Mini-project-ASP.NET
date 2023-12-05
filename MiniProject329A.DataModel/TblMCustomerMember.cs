﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Customer_Member")]
    public partial class TblMCustomerMember
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("parent_biodata_id")]
        public long? ParentBiodataId { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("customer_relation_id")]
        public long? CustomerRelationId { get; set; }
        [Column("create_by")]
        public long CreateBy { get; set; }
        [Column("create_on", TypeName = "datetime")]
        public DateTime CreateOn { get; set; }
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
        [InverseProperty("TblMCustomerMembers")]
        public virtual TblMCustomer? Customer { get; set; }
        [ForeignKey("CustomerRelationId")]
        [InverseProperty("TblMCustomerMembers")]
        public virtual TblMCustomerRelation? CustomerRelation { get; set; }
    }
}
