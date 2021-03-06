﻿using System.ComponentModel.DataAnnotations;

namespace FeatureConfig.Data.Entities
{
    public class FeatureValue
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
