﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Medical_Facility_Category")]
    public partial class TblMMedicalFacilityCategory
    {
        public TblMMedicalFacilityCategory()
        {
            TblMMedicalFacilities = new HashSet<TblMMedicalFacility>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
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

        [InverseProperty("MedicalFacilityCategory")]
        public virtual ICollection<TblMMedicalFacility> TblMMedicalFacilities { get; set; }
    }
}
