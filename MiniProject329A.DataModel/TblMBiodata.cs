using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Biodata")]
    public partial class TblMBiodata
    {
        public TblMBiodata()
        {
            TblMAdmins = new HashSet<TblMAdmin>();
            TblMBiodataAddresses = new HashSet<TblMBiodataAddress>();
            TblMBiodataAttachments = new HashSet<TblMBiodataAttachment>();
            TblMCustomers = new HashSet<TblMCustomer>();
            TblMDoctors = new HashSet<TblMDoctor>();
            TblMUsers = new HashSet<TblMUser>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("fullname")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Fullname { get; set; }
        [Column("mobile_phone")]
        [StringLength(15)]
        [Unicode(false)]
        public string? MobilePhone { get; set; }
        [Column("image")]
        [MaxLength(1)]
        public byte[]? Image { get; set; }
        [Column("image_path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ImagePath { get; set; }
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

        [InverseProperty("Biodata")]
        public virtual ICollection<TblMAdmin> TblMAdmins { get; set; }
        [InverseProperty("Biodata")]
        public virtual ICollection<TblMBiodataAddress> TblMBiodataAddresses { get; set; }
        [InverseProperty("Biodata")]
        public virtual ICollection<TblMBiodataAttachment> TblMBiodataAttachments { get; set; }
        [InverseProperty("Biodata")]
        public virtual ICollection<TblMCustomer> TblMCustomers { get; set; }
        [InverseProperty("Biodata")]
        public virtual ICollection<TblMDoctor> TblMDoctors { get; set; }
        [InverseProperty("Biodata")]
        public virtual ICollection<TblMUser> TblMUsers { get; set; }
    }
}
