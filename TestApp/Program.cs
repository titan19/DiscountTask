/*
 * 
 * Problem:
 * We have a small piece of code which we believe you can make better.
 * The domain of the code is some imagine store which provides discounts for their lovely customers.
 * They want to ask you to refactor this code to simplify the future maintenance
 * Keep in mind that they are crazy with requirements and want to stay pretty flexible on decisions.
 * 
 * Timing: up to you. Avarage time is 60-90 minutes
 * 
 * What's important:
 *   Code maintainability and testability:
 *     - Will it be easy to maintain your design in future? 
 *     - Is it testable in an environment of a real project?
 *   Design flexibility:
 *     - Will it easy to add new discount type? e.g. discount by segment, discount by segment
 *     - Will it easy to add new multiplier type? e.g. multiplier discounts on birthday, on Christmas
 *     - Will it be easy to store discounts and multipliers in a database?
 *     - Is your design follow best practices in software design? e.g. SOLID
 *   Algorithm's correctness:
 *     - Is your changes follow the method's annotation?
 *     
 *   Feel free to add some ToDo for a reviewer of your code.
 *   Feel free to comment your hard decision on your design.
 *   
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using TestApp;
using TestApp.Models;

namespace InterviewCode1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var discountManager = new DiscountManager();
            var customer = new Customer
            {
                FirstName = "Gill",
                LastName = "Bates",
                BirthDay = DateTime.Today,
                Segment = 3
            };

            var orderPrice = 1500;

            var discountPrice = orderPrice - discountManager.CalculateDiscount(orderPrice, customer);

            Console.WriteLine(new String('=', 20));
            Console.WriteLine("Order price :   {0}", orderPrice);
            Console.WriteLine("Discount    :   {0}", -orderPrice + discountPrice);
            Console.WriteLine(new String('=', 20));
            Console.WriteLine("Total price :   {0}", discountPrice);
            Console.WriteLine(new String('=', 20));
            Console.ReadKey();
        }
    }
}
