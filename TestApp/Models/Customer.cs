using System;

namespace TestApp.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 1 - Low valuable customer
        /// 2 - Medium valuable customer
        /// 3 - High valuable customer
        /// </summary>
        public int Segment { get; set; }
    }

}
