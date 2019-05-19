// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class AuditableEntity : IAuditableEntity
    {
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        [MaxLength(256)]
        public string LockedBy { get; set; }
        
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string LockedByUserName { get; set; }

        public AuditableEntity()
        {
            if (!string.IsNullOrEmpty(LockedBy))
            {

            }
        }
    }


}
