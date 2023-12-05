using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Doctor_Office_Treatment_Price")]
    public partial class TblTDoctorOfficeTreatmentPrice
    {
        public TblTDoctorOfficeTreatmentPrice()
        {
            TblTTreatmentDiscounts = new HashSet<TblTTreatmentDiscount>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("doctor_office_treatment_id")]
        public long? DoctorOfficeTreatmentId { get; set; }
        [Column("price", TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }
        [Column("price_start_from", TypeName = "decimal(18, 2)")]
        public decimal? PriceStartFrom { get; set; }
        [Column("price_until_from", TypeName = "decimal(18, 2)")]
        public decimal? PriceUntilFrom { get; set; }
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

        [ForeignKey("DoctorOfficeTreatmentId")]
        [InverseProperty("TblTDoctorOfficeTreatmentPrices")]
        public virtual TblTDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        [InverseProperty("DoctorOfficeTreatmentPrice")]
        public virtual ICollection<TblTTreatmentDiscount> TblTTreatmentDiscounts { get; set; }
    }
}
