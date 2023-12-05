﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject329A.DataModel
{
    [Table("Tbl_T_Customer_Chat_History")]
    public partial class TblTCustomerChatHistory
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_chat_id")]
        public long? CustomerChatId { get; set; }
        [Column("chat_content", TypeName = "text")]
        public string? ChatContent { get; set; }
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

        [ForeignKey("CustomerChatId")]
        [InverseProperty("TblTCustomerChatHistories")]
        public virtual TblTCustomerChat? CustomerChat { get; set; }
    }
}
