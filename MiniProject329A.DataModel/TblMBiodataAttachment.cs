using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Biodata_Attachment")]
    public partial class TblMBiodataAttachment
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("file_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? FileName { get; set; }
        [Column("file_path")]
        [StringLength(100)]
        [Unicode(false)]
        public string? FilePath { get; set; }
        [Column("file_size")]
        public int? FileSize { get; set; }
        [Column("file")]
        [MaxLength(1)]
        public byte[]? File { get; set; }
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

        [ForeignKey("BiodataId")]
        [InverseProperty("TblMBiodataAttachments")]
        public virtual TblMBiodata? Biodata { get; set; }
    }
}
