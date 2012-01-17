using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface IEntity
    {
        DateTime CreatedDate { get; set; }

        DateTime? UpdatedDate { get; set; }

        DateTime? DeletedDate { get; set; }

        bool Deleted { get; set; }
    }
}
