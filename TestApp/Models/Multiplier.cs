using System;

namespace TestApp.Models
{
    public class Multiplier
    {
        public int MultiplierId { get; set; }
        public decimal Value { get; set; }
        public DateTime? Date { get; set; }
        /// <summary>
        /// Type of personal date
        /// 0 - General
        /// 1 - Birthday
        /// </summary>
        public int MultiplierType { get; set; }
    }
}
