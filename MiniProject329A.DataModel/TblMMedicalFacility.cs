using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Medical_Facility")]
    public partial class TblMMedicalFacility
    {
        public TblMMedicalFacility()
        {
            TblMMedicalFacilitySchedules = new HashSet<TblMMedicalFacilitySchedule>();
            TblTDoctorOffices = new HashSet<TblTDoctorOffice>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("medical_facility_category_id")]
        public long? MedicalFacilityCategoryId { get; set; }
        [Column("location_id")]
        public long? LocationId { get; set; }
        [Column("full_address", TypeName = "text")]
        public string? FullAddress { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("phone_code")]
        [StringLength(10)]
        [Unicode(false)]
        public string? PhoneCode { get; set; }
        [Column("phone")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("fax")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Fax { get; set; }
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

        [ForeignKey("LocationId")]
        [InverseProperty("TblMMedicalFacilities")]
        public virtual TblMLocation? Location { get; set; }
        [ForeignKey("MedicalFacilityCategoryId")]
        [InverseProperty("TblMMedicalFacilities")]
        public virtual TblMMedicalFacilityCategory? MedicalFacilityCategory { get; set; }
        [InverseProperty("MedicalFacility")]
        public virtual ICollection<TblMMedicalFacilitySchedule> TblMMedicalFacilitySchedules { get; set; }
        [InverseProperty("MedicalFacility")]
        public virtual ICollection<TblTDoctorOffice> TblTDoctorOffices { get; set; }
    }
}
