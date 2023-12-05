using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Role")]
    public partial class TblMRole
    {
        public TblMRole()
        {
            TblMMenuRoles = new HashSet<TblMMenuRole>();
            TblMUsers = new HashSet<TblMUser>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("code")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Code { get; set; }
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

        [InverseProperty("Role")]
        public virtual ICollection<TblMMenuRole> TblMMenuRoles { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<TblMUser> TblMUsers { get; set; }
    }
}
