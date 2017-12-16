using System;
using System.Linq;
using TestApp.Models;

namespace TestApp
{
    public class DiscountManager
    {
        private DiscountDB db;
        public readonly decimal MaximumPercentageDiscount;
        public DiscountManager(
            decimal maximumPercentageDiscount = 0.9m)
        {
            db = new DiscountDB();
            if (!db.Multipliers.Any())
                Init();
            MaximumPercentageDiscount = maximumPercentageDiscount;
        }
        private void Init()
        {
            db.Multipliers.Add(new Multiplier
            {
                MultiplierId = 1,
                Date = new DateTime(1999, 12, 31),
                MultiplierType = 0,
                Value = 2,
            });
            db.Multipliers.Add(new Multiplier
            {
                MultiplierId = 2,
                MultiplierType = 1,
                Value = 1.5m,
            });
            db.Discounts.Add(new Discount
            {
                DiscountId = 1,
                Additional = 10,
                Percent = 0,
            });
            db.Discounts.Add(new Discount
            {
                DiscountId = 2,
                Additional = 50,
                Percent = 20,
            });
            db.Discounts.Add(new Discount
            {
                DiscountId = 3,
                Additional = 100,
                Percent = 40,
            });
            db.SaveChanges();
        }
        /// <summary>
        /// easy to add Discount
        /// </summary>
        public int AddDiscount(decimal additional, decimal percent)
        {
            int id = db.Discounts.Count() + 1;
            var dis = new Discount
            {
                DiscountId = id,
                Additional = additional,
                Percent = percent
            };
            db.Discounts.Add(dis);
            db.SaveChanges();
            return id;
        }
        /// <summary>
        /// easy to add Multiplier
        /// </summary>
        public int AddMultiplier(
            decimal value,
            int type,
            DateTime? date = null)
        {
            int id = db.Multipliers.Count() + 1;
            var mul = new Multiplier
            {
                MultiplierId = id,
                MultiplierType = type,
                Value = value
            };
            if (date.HasValue)
                mul.Date = date.Value;
            db.Multipliers.Add(mul);
            db.SaveChanges();
            return id;
        }
        /// <summary>
        /// For high-val-cust, mid-val-cust, low-val-cust we provide 10$+0%, 50$+20%, 100$+40% accordingly.
        /// We are doubling discount on customer's birthday.
        /// We are providing 90% discount at most.
        /// </summary>
        public decimal CalculateDiscount(decimal amount, Customer customer)
        {
            decimal result = CalculateDiscountBySegment(amount, customer);
            result *= CalculateMultiplierByDate(customer, DateTime.Now);
            return Math.Min(result, amount * 0.9m);
        }

        private decimal CalculateDiscountBySegment(decimal amount, Customer customer)
        {
            int id = customer.Segment;
            var discount = db.Discounts.Where(a => a.DiscountId == id).FirstOrDefault();
            if (discount == null)
                return 0m;
            return amount * discount.Percent + discount.Additional;
        }

        private decimal CalculateMultiplierByDate(Customer customer, DateTime date)
        {
            var all = db.Multipliers.Where(filter =>
                                                (filter.MultiplierType == 0 && //for general type
                                                filter.Date.HasValue &&
                                                filter.Date.Value.Day == date.Day &&
                                                filter.Date.Value.Month == date.Month
                                                ) ||
                                                (filter.MultiplierType == 1 && //for customer birthday type
                                                customer.BirthDay.HasValue &&
                                                customer.BirthDay.Value.Day == date.Day &&
                                                customer.BirthDay.Value.Month == date.Month
                                                ));
            if (all.Count() == 0)
                return 1m;
            var result = 0m;
            foreach (var item in all)
                result += item.Value;
            return result;
        }
    }
}
