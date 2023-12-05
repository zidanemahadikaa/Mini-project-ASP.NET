using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Courier_Discount")]
    public partial class TblTCourierDiscount
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("courier_type_id")]
        public long? CourierTypeId { get; set; }
        [Column("value", TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
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

        [ForeignKey("CourierTypeId")]
        [InverseProperty("TblTCourierDiscounts")]
        public virtual TblMCourierType? CourierType { get; set; }
    }
}
