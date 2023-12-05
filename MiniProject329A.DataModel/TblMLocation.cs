using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Location")]
    public partial class TblMLocation
    {
        public TblMLocation()
        {
            TblMBiodataAddresses = new HashSet<TblMBiodataAddress>();
            TblMMedicalFacilities = new HashSet<TblMMedicalFacility>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("parent_id")]
        public long? ParentId { get; set; }
        [Column("location_level_id")]
        public long? LocationLevelId { get; set; }
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
        [ForeignKey("LocationLevelId")]
        [InverseProperty("TblMLocations")]
        public virtual TblMLocationLevel? LocationLevel { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<TblMBiodataAddress> TblMBiodataAddresses { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<TblMMedicalFacility> TblMMedicalFacilities { get; set; }
    }
}
