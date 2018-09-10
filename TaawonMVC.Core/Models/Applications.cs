﻿using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaawonMVC.Models
{
  public class Applications:FullAuditedEntity
    {
        public Applications()
        {
            CreationTime = Clock.Now;
            fullName = "";
            phoneNumber1 = 0;
            phoneNumber2 = 0;
            isThereFundingOrPreviousRestoration = 0;
            previousRestorationSource = "";
            isThereInterestedRepairingEntity = 0;
            interestedRepairingEntityName = "";
            housingSince = Clock.Now;
            interventionTypeId = 0;
            otherOwnershipType = "";
            otherRestorationType = "";
            propertyStatusDescription = "";
            requiredRestoration = "";

        }

        [Required]
        public int buildingId { get; set; }

        [Required]
        [MaxLength(255)]
        public string fullName { get; set; }

        [Required]
        public int phoneNumber1 { get; set; }

        public int? phoneNumber2 { get; set; }
        
        [Required]
        public byte isThereFundingOrPreviousRestoration { get; set; }

        [Required]
        [MaxLength(255)]
        public string previousRestorationSource { get; set; }

        [Required]
        public byte isThereInterestedRepairingEntity { get; set; }

        [Required]
        [MaxLength(255)]
        public string interestedRepairingEntityName { get; set; }

        [Required]
        public DateTime housingSince { get; set; }

        [Required]
       // [ForeignKey("InterventionType")]
        public int interventionTypeId { get; set; }
        public virtual InterventionType interventionType { get; set; }

        [CanBeNull]
        public byte[] restorationTypeIds { get; set; }

        [MaxLength(255)]
        public string otherOwnershipType { get; set; }

        [MaxLength(255)]
        public string otherRestorationType { get; set; }

        public string propertyStatusDescription { get; set; }

        public string requiredRestoration { get; set; }





    }
}
