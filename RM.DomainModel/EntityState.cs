﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DomainModel
{
    public interface IEntity
    {
        EntityState EntityState { get; set; }
    }

    public enum EntityState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
