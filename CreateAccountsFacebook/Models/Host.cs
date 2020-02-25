﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccountsProject.Models
{
    public class Host
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Host()
        {

        }

        public Host(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
