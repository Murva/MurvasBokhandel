﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityModel
{
    public class role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}