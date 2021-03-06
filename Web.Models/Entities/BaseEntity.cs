﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Entities
{
    public abstract class BaseEntity<TId>
    {
        [Key]
        [Column(Order=1)]
        public TId Id { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
