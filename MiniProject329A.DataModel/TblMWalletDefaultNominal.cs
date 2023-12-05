using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Wallet_Default_Nominal")]
    public partial class TblMWalletDefaultNominal
    {
        public TblMWalletDefaultNominal()
        {
            TblTCustomerWalletWithdraws = new HashSet<TblTCustomerWalletWithdraw>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("nominal")]
        public int? Nominal { get; set; }
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

        [InverseProperty("WalletDefaultNominal")]
        public virtual ICollection<TblTCustomerWalletWithdraw> TblTCustomerWalletWithdraws { get; set; }
    }
}
