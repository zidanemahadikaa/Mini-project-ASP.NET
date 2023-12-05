using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiniProject329A.DataModel
{
    public partial class MiniProject329AContext : DbContext
    {
        public MiniProject329AContext()
        {
        }

        public MiniProject329AContext(DbContextOptions<MiniProject329AContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblMAdmin> TblMAdmins { get; set; } = null!;
        public virtual DbSet<TblMBank> TblMBanks { get; set; } = null!;
        public virtual DbSet<TblMBiodataAddress> TblMBiodataAddresses { get; set; } = null!;
        public virtual DbSet<TblMBiodataAttachment> TblMBiodataAttachments { get; set; } = null!;
        public virtual DbSet<TblMBiodata> TblMBiodata { get; set; } = null!;
        public virtual DbSet<TblMBloodGroup> TblMBloodGroups { get; set; } = null!;
        public virtual DbSet<TblMCourier> TblMCouriers { get; set; } = null!;
        public virtual DbSet<TblMCourierType> TblMCourierTypes { get; set; } = null!;
        public virtual DbSet<TblMCustomer> TblMCustomers { get; set; } = null!;
        public virtual DbSet<TblMCustomerMember> TblMCustomerMembers { get; set; } = null!;
        public virtual DbSet<TblMCustomerRelation> TblMCustomerRelations { get; set; } = null!;
        public virtual DbSet<TblMDoctor> TblMDoctors { get; set; } = null!;
        public virtual DbSet<TblMDoctorEducation> TblMDoctorEducations { get; set; } = null!;
        public virtual DbSet<TblMEducationLevel> TblMEducationLevels { get; set; } = null!;
        public virtual DbSet<TblMLocation> TblMLocations { get; set; } = null!;
        public virtual DbSet<TblMLocationLevel> TblMLocationLevels { get; set; } = null!;
        public virtual DbSet<TblMMedicalFacility> TblMMedicalFacilities { get; set; } = null!;
        public virtual DbSet<TblMMedicalFacilityCategory> TblMMedicalFacilityCategories { get; set; } = null!;
        public virtual DbSet<TblMMedicalFacilitySchedule> TblMMedicalFacilitySchedules { get; set; } = null!;
        public virtual DbSet<TblMMedicalItem> TblMMedicalItems { get; set; } = null!;
        public virtual DbSet<TblMMedicalItemCategory> TblMMedicalItemCategories { get; set; } = null!;
        public virtual DbSet<TblMMedicalItemSegmentation> TblMMedicalItemSegmentations { get; set; } = null!;
        public virtual DbSet<TblMMenu> TblMMenus { get; set; } = null!;
        public virtual DbSet<TblMMenuRole> TblMMenuRoles { get; set; } = null!;
        public virtual DbSet<TblMPaymentMethod> TblMPaymentMethods { get; set; } = null!;
        public virtual DbSet<TblMRole> TblMRoles { get; set; } = null!;
        public virtual DbSet<TblMSpecialization> TblMSpecializations { get; set; } = null!;
        public virtual DbSet<TblMUser> TblMUsers { get; set; } = null!;
        public virtual DbSet<TblMWalletDefaultNominal> TblMWalletDefaultNominals { get; set; } = null!;
        public virtual DbSet<TblTAppointment> TblTAppointments { get; set; } = null!;
        public virtual DbSet<TblTAppointmentCancellation> TblTAppointmentCancellations { get; set; } = null!;
        public virtual DbSet<TblTAppointmentDone> TblTAppointmentDones { get; set; } = null!;
        public virtual DbSet<TblTAppointmentRescheduleHistory> TblTAppointmentRescheduleHistories { get; set; } = null!;
        public virtual DbSet<TblTCourierDiscount> TblTCourierDiscounts { get; set; } = null!;
        public virtual DbSet<TblTCurrentDoctorSpecialization> TblTCurrentDoctorSpecializations { get; set; } = null!;
        public virtual DbSet<TblTCustomerChat> TblTCustomerChats { get; set; } = null!;
        public virtual DbSet<TblTCustomerChatHistory> TblTCustomerChatHistories { get; set; } = null!;
        public virtual DbSet<TblTCustomerCustomNominal> TblTCustomerCustomNominals { get; set; } = null!;
        public virtual DbSet<TblTCustomerRegisteredCard> TblTCustomerRegisteredCards { get; set; } = null!;
        public virtual DbSet<TblTCustomerVa> TblTCustomerVas { get; set; } = null!;
        public virtual DbSet<TblTCustomerVaHistory> TblTCustomerVaHistories { get; set; } = null!;
        public virtual DbSet<TblTCustomerWallet> TblTCustomerWallets { get; set; } = null!;
        public virtual DbSet<TblTCustomerWalletTopUp> TblTCustomerWalletTopUps { get; set; } = null!;
        public virtual DbSet<TblTCustomerWalletWithdraw> TblTCustomerWalletWithdraws { get; set; } = null!;
        public virtual DbSet<TblTDoctorOffice> TblTDoctorOffices { get; set; } = null!;
        public virtual DbSet<TblTDoctorOfficeSchedule> TblTDoctorOfficeSchedules { get; set; } = null!;
        public virtual DbSet<TblTDoctorOfficeTreatment> TblTDoctorOfficeTreatments { get; set; } = null!;
        public virtual DbSet<TblTDoctorOfficeTreatmentPrice> TblTDoctorOfficeTreatmentPrices { get; set; } = null!;
        public virtual DbSet<TblTDoctorTreatment> TblTDoctorTreatments { get; set; } = null!;
        public virtual DbSet<TblTMedicalItemPurchase> TblTMedicalItemPurchases { get; set; } = null!;
        public virtual DbSet<TblTMedicalItemPurchaseDetail> TblTMedicalItemPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TblTPrescription> TblTPrescriptions { get; set; } = null!;
        public virtual DbSet<TblTResetPassword> TblTResetPasswords { get; set; } = null!;
        public virtual DbSet<TblTToken> TblTTokens { get; set; } = null!;
        public virtual DbSet<TblTTreatmentDiscount> TblTTreatmentDiscounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Initial Catalog=MiniProject329A; User Id= sa; Password=P@ssw0rd; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblMAdmin>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMAdmins)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_Admin_Tbl_M_Biodata");
            });

            modelBuilder.Entity<TblMBank>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMBiodataAddress>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMBiodataAddresses)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_Biodata_Address_Tbl_M_Biodata");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblMBiodataAddresses)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Tbl_M_Biodata_Address_Tbl_M_Location");
            });

            modelBuilder.Entity<TblMBiodataAttachment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.File).IsFixedLength();

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMBiodataAttachments)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_Biodata_Attachment_Tbl_M_Biodata");
            });

            modelBuilder.Entity<TblMBiodata>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image).IsFixedLength();
            });

            modelBuilder.Entity<TblMBloodGroup>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMCourier>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMCourierType>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.TblMCourierTypes)
                    .HasForeignKey(d => d.CourierId)
                    .HasConstraintName("FK_Tbl_M_Courier_Type_Tbl_M_Courier");
            });

            modelBuilder.Entity<TblMCustomer>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMCustomers)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_Customer_Tbl_M_Biodata");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.TblMCustomers)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_Tbl_M_Customer_Tbl_M_Blood_Group");
            });

            modelBuilder.Entity<TblMCustomerMember>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblMCustomerMembers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_M_Customer_Member_Tbl_M_Customer");

                entity.HasOne(d => d.CustomerRelation)
                    .WithMany(p => p.TblMCustomerMembers)
                    .HasForeignKey(d => d.CustomerRelationId)
                    .HasConstraintName("FK_Tbl_M_Customer_Member_Tbl_M_Customer_Relation");
            });

            modelBuilder.Entity<TblMCustomerRelation>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMDoctor>(entity =>
            {
                entity.Property(e => e.CreateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMDoctors)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_Doctor_Tbl_M_Biodata");
            });

            modelBuilder.Entity<TblMDoctorEducation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsLastEducation).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TblMDoctorEducations)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Tbl_M_Doctor_Education_Tbl_M_Doctor");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.TblMDoctorEducations)
                    .HasForeignKey(d => d.EducationLevelId)
                    .HasConstraintName("FK_Tbl_M_Doctor_Education_Tbl_M_Education_Level1");
            });

            modelBuilder.Entity<TblMEducationLevel>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMLocation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.LocationLevel)
                    .WithMany(p => p.TblMLocations)
                    .HasForeignKey(d => d.LocationLevelId)
                    .HasConstraintName("FK_Tbl_M_Location_Tbl_M_Location_Level");
            });

            modelBuilder.Entity<TblMLocationLevel>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMMedicalFacility>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblMMedicalFacilities)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Tbl_M_Medical_Facility_Tbl_M_Location");

                entity.HasOne(d => d.MedicalFacilityCategory)
                    .WithMany(p => p.TblMMedicalFacilities)
                    .HasForeignKey(d => d.MedicalFacilityCategoryId)
                    .HasConstraintName("FK_Tbl_M_Medical_Facility_Tbl_M_Medical_Facility_Category");
            });

            modelBuilder.Entity<TblMMedicalFacilityCategory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMMedicalFacilitySchedule>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MedicalFacility)
                    .WithMany(p => p.TblMMedicalFacilitySchedules)
                    .HasForeignKey(d => d.MedicalFacilityId)
                    .HasConstraintName("FK_Tbl_M_Medical_Facility_Schedule_Tbl_M_Medical_Facility");
            });

            modelBuilder.Entity<TblMMedicalItem>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image).IsFixedLength();

                entity.HasOne(d => d.MedicalItemCategory)
                    .WithMany(p => p.TblMMedicalItems)
                    .HasForeignKey(d => d.MedicalItemCategoryId)
                    .HasConstraintName("FK_Tbl_M_Medical_Item_Tbl_M_Medical_Item_Category");

                entity.HasOne(d => d.MedicalItemSegmentation)
                    .WithMany(p => p.TblMMedicalItems)
                    .HasForeignKey(d => d.MedicalItemSegmentationId)
                    .HasConstraintName("FK_Tbl_M_Medical_Item_Tbl_M_Medical_Item_Segmentation");
            });

            modelBuilder.Entity<TblMMedicalItemCategory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMMedicalItemSegmentation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMMenu>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMMenuRole>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblMMenuRoles)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_Tbl_M_Menu_Role_Tbl_M_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblMMenuRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_M_Menu_Role_Tbl_M_Role");
            });

            modelBuilder.Entity<TblMPaymentMethod>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMRole>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMSpecialization>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblMUser>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Biodata)
                    .WithMany(p => p.TblMUsers)
                    .HasForeignKey(d => d.BiodataId)
                    .HasConstraintName("FK_Tbl_M_User_Tbl_M_Biodata");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblMUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Tbl_M_User_Tbl_M_Role");
            });

            modelBuilder.Entity<TblMWalletDefaultNominal>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTAppointment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTAppointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Tbl_M_Customer");

                entity.HasOne(d => d.DoctorOffice)
                    .WithMany(p => p.TblTAppointments)
                    .HasForeignKey(d => d.DoctorOfficeId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Tbl_T_Doctor_Office");

                entity.HasOne(d => d.DoctorOfficeSchedule)
                    .WithMany(p => p.TblTAppointments)
                    .HasForeignKey(d => d.DoctorOfficeScheduleId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Tbl_T_Doctor_Office_Schedule");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TblTAppointments)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Tbl_T_Doctor_Office_Treatment1");
            });

            modelBuilder.Entity<TblTAppointmentCancellation>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblTAppointmentCancellations)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Cancellation_Tbl_T_Appointment");
            });

            modelBuilder.Entity<TblTAppointmentDone>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblTAppointmentDones)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Tbl_T_Appointment_done_Tbl_T_Appointment");
            });

            modelBuilder.Entity<TblTAppointmentRescheduleHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblTAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Reschedule_History_Tbl_T_Appointment");

                entity.HasOne(d => d.DoctorOfficeSchedule)
                    .WithMany(p => p.TblTAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.DoctorOfficeScheduleId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Reschedule_History_Tbl_T_Doctor_Office_Schedule");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TblTAppointmentRescheduleHistories)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_Tbl_T_Appointment_Reschedule_History_Tbl_T_Doctor_Office_Treatment");
            });

            modelBuilder.Entity<TblTCourierDiscount>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CourierType)
                    .WithMany(p => p.TblTCourierDiscounts)
                    .HasForeignKey(d => d.CourierTypeId)
                    .HasConstraintName("FK_Tbl_T_Courier_Discount_Tbl_M_Courier_Type");
            });

            modelBuilder.Entity<TblTCurrentDoctorSpecialization>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TblTCurrentDoctorSpecializations)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Tbl_T_Current_Doctor_Specialization_Tbl_M_Doctor");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.TblTCurrentDoctorSpecializations)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK_Tbl_T_Current_Doctor_Specialization_Tbl_M_Specialization");
            });

            modelBuilder.Entity<TblTCustomerChat>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTCustomerChats)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Customer_Chat_Tbl_M_Customer");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TblTCustomerChats)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Tbl_T_Customer_Chat_Tbl_M_Doctor");
            });

            modelBuilder.Entity<TblTCustomerChatHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CustomerChat)
                    .WithMany(p => p.TblTCustomerChatHistories)
                    .HasForeignKey(d => d.CustomerChatId)
                    .HasConstraintName("FK_Tbl_T_Customer_Chat_History_Tbl_T_Customer_Chat");
            });

            modelBuilder.Entity<TblTCustomerCustomNominal>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTCustomerCustomNominals)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Customer_Custom_Nominal_Tbl_M_Customer");
            });

            modelBuilder.Entity<TblTCustomerRegisteredCard>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTCustomerRegisteredCards)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Customer_Registered_Card_Tbl_M_Customer");
            });

            modelBuilder.Entity<TblTCustomerVa>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.TblTCustomerVa)
                    .HasForeignKey<TblTCustomerVa>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_T_Customer_Va_Tbl_M_Customer");
            });

            modelBuilder.Entity<TblTCustomerVaHistory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CustomerVa)
                    .WithMany(p => p.TblTCustomerVaHistories)
                    .HasForeignKey(d => d.CustomerVaId)
                    .HasConstraintName("FK_Tbl_T_Customer_Va_History_Tbl_T_Customer_Va");
            });

            modelBuilder.Entity<TblTCustomerWallet>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTCustomerWallets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Customer_Wallet_Tbl_M_Customer");
            });

            modelBuilder.Entity<TblTCustomerWalletTopUp>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CustomerWallet)
                    .WithMany(p => p.TblTCustomerWalletTopUps)
                    .HasForeignKey(d => d.CustomerWalletId)
                    .HasConstraintName("FK_Tbl_T_Customer_Wallet_top_Up_Tbl_T_Customer_Wallet");
            });

            modelBuilder.Entity<TblTCustomerWalletWithdraw>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTCustomerWalletWithdraws)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_T_Customer_Wallet_Withdraw_Tbl_M_Customer");

                entity.HasOne(d => d.WalletDefaultNominal)
                    .WithMany(p => p.TblTCustomerWalletWithdraws)
                    .HasForeignKey(d => d.WalletDefaultNominalId)
                    .HasConstraintName("FK_Tbl_T_Customer_Wallet_Withdraw_Tbl_M_Wallet_Default_Nominal");
            });

            modelBuilder.Entity<TblTDoctorOffice>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.MedicalFacility)
                    .WithMany(p => p.TblTDoctorOffices)
                    .HasForeignKey(d => d.MedicalFacilityId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Office_Tbl_M_Medical_Facility");
            });

            modelBuilder.Entity<TblTDoctorOfficeSchedule>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TblTDoctorOfficeSchedules)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Office_Schedule_Tbl_M_Doctor");
            });

            modelBuilder.Entity<TblTDoctorOfficeTreatment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.DoctorOffice)
                    .WithMany(p => p.TblTDoctorOfficeTreatments)
                    .HasForeignKey(d => d.DoctorOfficeId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Office_Treatment_Tbl_T_Doctor_Office");

                entity.HasOne(d => d.DoctorTreatment)
                    .WithMany(p => p.TblTDoctorOfficeTreatments)
                    .HasForeignKey(d => d.DoctorTreatmentId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Office_Treatment_Tbl_T_Doctor_Treatment");
            });

            modelBuilder.Entity<TblTDoctorOfficeTreatmentPrice>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.DoctorOfficeTreatment)
                    .WithMany(p => p.TblTDoctorOfficeTreatmentPrices)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Office_Treatment_Price_Tbl_T_Doctor_Office_Treatment");
            });

            modelBuilder.Entity<TblTDoctorTreatment>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TblTDoctorTreatments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Tbl_T_Doctor_Treatment_Tbl_M_Doctor");
            });

            modelBuilder.Entity<TblTMedicalItemPurchase>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTMedicalItemPurchases)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Tbl_T_Medical_Item_Purchase_Tbl_M_Customer");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.TblTMedicalItemPurchases)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_Tbl_T_Medical_Item_Purchase_Tbl_M_Payment_Method");
            });

            modelBuilder.Entity<TblTMedicalItemPurchaseDetail>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Courir)
                    .WithMany(p => p.TblTMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.CourirId)
                    .HasConstraintName("FK_Tbl_T_Medical_Item_Purchase_Detail_Tbl_M_Courier");

                entity.HasOne(d => d.MedicalItem)
                    .WithMany(p => p.TblTMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.MedicalItemId)
                    .HasConstraintName("FK_Tbl_T_Medical_Item_Purchase_Detail_Tbl_M_Medical_Item");

                entity.HasOne(d => d.MedicalItemPurchase)
                    .WithMany(p => p.TblTMedicalItemPurchaseDetails)
                    .HasForeignKey(d => d.MedicalItemPurchaseId)
                    .HasConstraintName("FK_Tbl_T_Medical_Item_Purchase_Detail_Tbl_T_Medical_Item_Purchase");
            });

            modelBuilder.Entity<TblTPrescription>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TblTPrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Tbl_T_Prescription_Tbl_T_Appointment");

                entity.HasOne(d => d.MedicalItem)
                    .WithMany(p => p.TblTPrescriptions)
                    .HasForeignKey(d => d.MedicalItemId)
                    .HasConstraintName("FK_Tbl_T_Prescription_Tbl_M_Medical_Item");
            });

            modelBuilder.Entity<TblTResetPassword>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTToken>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tbl_T_Token_Tbl_M_User");
            });

            modelBuilder.Entity<TblTTreatmentDiscount>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.DoctorOfficeTreatmentPrice)
                    .WithMany(p => p.TblTTreatmentDiscounts)
                    .HasForeignKey(d => d.DoctorOfficeTreatmentPriceId)
                    .HasConstraintName("FK_Tbl_T_Treatment_Discount_Tbl_T_Doctor_Office_Treatment_Price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
