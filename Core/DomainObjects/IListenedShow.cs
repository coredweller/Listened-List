using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface IListenedShow : IEntity
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        Guid ShowId { get; set; }
        string Notes { get; set; }
        double? Stars { get; set; }
        int Status { get; set; }
    }
}
