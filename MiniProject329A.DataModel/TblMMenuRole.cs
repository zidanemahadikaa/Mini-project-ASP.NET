using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Menu_Role")]
    public partial class TblMMenuRole
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("menu_id")]
        public long? MenuId { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
        [Column("create_by")]
        public long CreateBy { get; set; }
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

        [ForeignKey("MenuId")]
        [InverseProperty("TblMMenuRoles")]
        public virtual TblMMenu? Menu { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("TblMMenuRoles")]
        public virtual TblMRole? Role { get; set; }
    }
}
