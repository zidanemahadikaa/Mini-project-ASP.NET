using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Courier_Type")]
    public partial class TblMCourierType
    {
        public TblMCourierType()
        {
            TblTCourierDiscounts = new HashSet<TblTCourierDiscount>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("courier_id")]
        public long? CourierId { get; set; }
        [Column("name")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Name { get; set; }
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
        public bool IsDelete { get; set; }

        [ForeignKey("CourierId")]
        [InverseProperty("TblMCourierTypes")]
        public virtual TblMCourier? Courier { get; set; }
        [InverseProperty("CourierType")]
        public virtual ICollection<TblTCourierDiscount> TblTCourierDiscounts { get; set; }
    }
}
