﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class Period
    {
        public List<Day> Days { get; set; }

        public Period()
        {
            this.Days = new List<Day>();
        }
    }
}
