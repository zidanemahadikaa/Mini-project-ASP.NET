using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Doctor")]
    public partial class TblMDoctor
    {
        public TblMDoctor()
        {
            TblMDoctorEducations = new HashSet<TblMDoctorEducation>();
            TblTCurrentDoctorSpecializations = new HashSet<TblTCurrentDoctorSpecialization>();
            TblTCustomerChats = new HashSet<TblTCustomerChat>();
            TblTDoctorOfficeSchedules = new HashSet<TblTDoctorOfficeSchedule>();
            TblTDoctorTreatments = new HashSet<TblTDoctorTreatment>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("str")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Str { get; set; }
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
        [InverseProperty("TblMDoctors")]
        public virtual TblMBiodata? Biodata { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<TblMDoctorEducation> TblMDoctorEducations { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<TblTCurrentDoctorSpecialization> TblTCurrentDoctorSpecializations { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<TblTCustomerChat> TblTCustomerChats { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<TblTDoctorOfficeSchedule> TblTDoctorOfficeSchedules { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<TblTDoctorTreatment> TblTDoctorTreatments { get; set; }
    }
}
