using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Admin")]
    public partial class TblMAdmin
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("code")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Code { get; set; }
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

        [ForeignKey("BiodataId")]
        [InverseProperty("TblMAdmins")]
        public virtual TblMBiodata? Biodata { get; set; }
    }
}
