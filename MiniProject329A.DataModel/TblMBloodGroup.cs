﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Blood_Group")]
    public partial class TblMBloodGroup
    {
        public TblMBloodGroup()
        {
            TblMCustomers = new HashSet<TblMCustomer>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("code")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Code { get; set; }
        [Column("descrtiption")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
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

        [InverseProperty("BloodGroup")]
        public virtual ICollection<TblMCustomer> TblMCustomers { get; set; }
    }
}
