﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMTblTDoctorOfficeSchedule
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? MedicalFacilityScheduleId { get; set; }
        public int? Slot { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDelete { get; set; }

        public List<VMTblTAppointment>? Appointment {  get; set; }

        public List<VMTblMMedicalFacilitySchedule>? MedicalFacilitySchedule {  get; set; }
    }
}
