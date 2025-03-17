﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServiceManager
    {
        IBookservice Bookservice { get; }
        IAuthendicationService AuthendicationService { get; }
    }
}
