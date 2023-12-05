using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Medical_Item_Purchase")]
    public partial class TblTMedicalItemPurchase
    {
        public TblTMedicalItemPurchase()
        {
            TblTMedicalItemPurchaseDetails = new HashSet<TblTMedicalItemPurchaseDetail>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("payment_method_id")]
        public long? PaymentMethodId { get; set; }
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

        [ForeignKey("CustomerId")]
        [InverseProperty("TblTMedicalItemPurchases")]
        public virtual TblMCustomer? Customer { get; set; }
        [ForeignKey("PaymentMethodId")]
        [InverseProperty("TblTMedicalItemPurchases")]
        public virtual TblMPaymentMethod? PaymentMethod { get; set; }
        [InverseProperty("MedicalItemPurchase")]
        public virtual ICollection<TblTMedicalItemPurchaseDetail> TblTMedicalItemPurchaseDetails { get; set; }
    }
}
