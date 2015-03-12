using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DomainModel
{
    public partial class Supplier: IEntity
    {
        public EntityState EntityState { get; set; }
    }

    public partial class SupplierContactDetail : IEntity
    {
        public EntityState EntityState { get; set; }
    }
}
