using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Reset_Password")]
    public partial class TblTResetPassword
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("old_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? OldPassword { get; set; }
        [Column("new_password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? NewPassword { get; set; }
        [Column("reset_for")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ResetFor { get; set; }
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
        public bool? IsDelete { get; set; }
    }
}
