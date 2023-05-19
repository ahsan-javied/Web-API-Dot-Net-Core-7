using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Domain.Entites
{
    public abstract class BaseModel
    {
        public bool IsDeleted { get; set; }
        public virtual int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
