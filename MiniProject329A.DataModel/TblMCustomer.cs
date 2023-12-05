using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_M_Customer")]
    public partial class TblMCustomer
    {
        public TblMCustomer()
        {
            TblMCustomerMembers = new HashSet<TblMCustomerMember>();
            TblTAppointments = new HashSet<TblTAppointment>();
            TblTCustomerChats = new HashSet<TblTCustomerChat>();
            TblTCustomerCustomNominals = new HashSet<TblTCustomerCustomNominal>();
            TblTCustomerRegisteredCards = new HashSet<TblTCustomerRegisteredCard>();
            TblTCustomerWalletWithdraws = new HashSet<TblTCustomerWalletWithdraw>();
            TblTCustomerWallets = new HashSet<TblTCustomerWallet>();
            TblTMedicalItemPurchases = new HashSet<TblTMedicalItemPurchase>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("gender")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Gender { get; set; }
        [Column("blood_group_id")]
        public long? BloodGroupId { get; set; }
        [Column("rhesus_type")]
        [StringLength(5)]
        [Unicode(false)]
        public string? RhesusType { get; set; }
        [Column("height", TypeName = "decimal(18, 2)")]
        public decimal? Height { get; set; }
        [Column("weight", TypeName = "decimal(18, 2)")]
        public decimal? Weight { get; set; }
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
        [InverseProperty("TblMCustomers")]
        public virtual TblMBiodata? Biodata { get; set; }
        [ForeignKey("BloodGroupId")]
        [InverseProperty("TblMCustomers")]
        public virtual TblMBloodGroup? BloodGroup { get; set; }
        [InverseProperty("IdNavigation")]
        public virtual TblTCustomerVa? TblTCustomerVa { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblMCustomerMember> TblMCustomerMembers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTAppointment> TblTAppointments { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTCustomerChat> TblTCustomerChats { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTCustomerCustomNominal> TblTCustomerCustomNominals { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTCustomerRegisteredCard> TblTCustomerRegisteredCards { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTCustomerWalletWithdraw> TblTCustomerWalletWithdraws { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTCustomerWallet> TblTCustomerWallets { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<TblTMedicalItemPurchase> TblTMedicalItemPurchases { get; set; }
    }
}
