﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class joinDivisionImagesSizes
    {
        public Divisions Divisions { get; set; }
        public DivisionImagesSizes DivisionImagesSizes { get; set; }
        public ImageSizes ImageSizes { get; set; }
        public DivisionImages DivisionImages { get; set; }
    }
}