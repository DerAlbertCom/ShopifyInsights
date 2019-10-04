﻿using System.ComponentModel.DataAnnotations;

 namespace ShopInsights.Core.Models
{
    public class MetaFieldModel
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string Namespace { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
