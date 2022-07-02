using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam1
{
    public class Store
    {
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public class Statistic
        {
            public Statistic(int year, Product product)
            {
                Year = year;

                if (product is null)
                {
                    throw new ArgumentNullException(nameof(product));
                }
                
                ProductId = product.Id;
                ProductName = product.Name;
            }
            
            public int Year { get; private set; }
            public int ProductId { get; private set; }
            public string ProductName { get; private set; }
            public int Quantity { get; set; }
            public double Cost { get; set; }
        }
        
        /// <summary>
        /// Формирует строку со статистикой продаж продуктов
        /// Сортировка - по убыванию количества проданных продуктов
        /// </summary>
        /// <param name="year">Год, за который подсчитывается статистика</param>
        public string GetProductStatistics(int year)
        {      
            var orders = Orders?.Where(w => w.OrderDate.Year == year);
            
            if (orders is null || Products is null || orders.Count() == 0)
            {
                return $"Нет статистики за {year}г.";
            }

            var statistics = new List<Statistic>();
            foreach (var product in Products)
            {
                var sum = orders.Where(w => w.Items != null).Select(s => s.Items.Where(w => w.ProductId == product.Id).Select(item => item.Quantity).Sum()).Sum();
                statistics.Add(new Statistic(year, product) { Quantity = sum });
            }
            
            var result = default(string);            
            var number = 1;
            foreach (var product in statistics.OrderByDescending(o => o.Quantity))
            {
                result += $"{number}) {product.ProductName} - {product.Quantity} item(s)\r\n";
                number++;
            }
            
            return result ?? $"Нет статистики за {year}г.";
        }

        /// <summary>
        /// Формирует строку со статистикой продаж продуктов по годам
        /// Сортировка - по убыванию годов.
        /// Выводятся все года, в которых были продажи продуктов
        /// </summary>
        public string GetYearsStatistics()
        {
            if (Orders is null || Products is null)
            {
                return $"Нет статистики по годам.";
            }
            
            var statistics = new List<Statistic>();
            foreach (var product in Products)
            {
                var years = Orders.Select(s => s.OrderDate.Year).Distinct();
                foreach (var year in years)
                {
                    var items = Orders.Where(w => w.OrderDate.Year == year && w.Items != null)
                                      .Select(s => s.Items.Where(w => w.ProductId == product.Id).Select(item => item.Quantity).Sum())
                                      .Sum();
                    var cost = items * product.Price;

                    if (items <= 0 || cost <= 0)
                    {
                        continue;
                    }                    
                    statistics.Add(new Statistic(year, product) { Quantity = items, Cost = cost });
                }                
            }
            
            var result = default(string);
            foreach (var product in statistics.OrderByDescending(o => o.Year))
            {
                result += $"{product.Year} - {product.Cost} руб.\r\n";
                result += $"Most selling: {product.ProductName} ({product.Quantity} item(s))\r\n\r\n";
            }

            return result ?? $"Нет статистики по годам.";
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }        
    }

    public class Order
    {
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // ПРИМЕР того, каким образом мы будем заполнять коллекции
            // НЕ является тестовым примером
            var store = new Store
            {
                Products = new List<Product>
            {
                new Product() { Id = 1, Name = "Product 1", Price = 1000d },
                new Product() { Id = 2, Name = "Product 2", Price = 3000d },
                new Product() { Id = 3, Name = "Product 3", Price = 10000d }
            },
                Orders = new List<Order>
            {
                new Order()
                {
                    UserId = 1,
                    OrderDate = DateTime.UtcNow,
                    Items = new List<Order.OrderItem>
                    {
                        new Order.OrderItem() { ProductId = 1, Quantity = 2 }
                    }
                },
                new Order()
                {
                    UserId = 1,
                    OrderDate = DateTime.UtcNow,
                    Items = new List<Order.OrderItem>
                    {
                        new Order.OrderItem() { ProductId = 1, Quantity = 1 },
                        new Order.OrderItem() { ProductId = 2, Quantity = 1 },
                        new Order.OrderItem() { ProductId = 3, Quantity = 1 }
                    }
                }
            }
            };

            Console.WriteLine(store.GetProductStatistics(2021));
            Console.WriteLine(store.GetYearsStatistics());
        }
    }
}