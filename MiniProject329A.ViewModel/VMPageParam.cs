﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{

    public class VMPageParam
    {
        public string orderBy {  get; set; }   
        public int? pageNumber { get; set; }
        public string? filter { get; set; }
    }
}
