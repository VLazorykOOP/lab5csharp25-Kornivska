using System;
using System.Collections.Generic;

namespace Lab5CSharp
{
    abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public abstract void Show();
        public abstract bool IsValid();
    }

    class FoodProduct : Product
    {
        public DateTime ManufactureDate { get; set; }
        public int ShelfLifeDays { get; set; }

        public FoodProduct(string name, double price, DateTime manufactureDate, int shelfLifeDays)
            : base(name, price)
        {
            ManufactureDate = manufactureDate;
            ShelfLifeDays = shelfLifeDays;
        }

        public override void Show()
        {
            Console.WriteLine($"Продукт: {Name}, Ціна: {Price}, Дата виготовлення: {ManufactureDate:d}, Термін: {ShelfLifeDays} днів");
        }

        public override bool IsValid()
        {
            return (DateTime.Now - ManufactureDate).Days <= ShelfLifeDays;
        }
    }

    class Batch : Product
    {
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int ShelfLifeDays { get; set; }

        public Batch(string name, double price, int quantity, DateTime manufactureDate, int shelfLifeDays)
            : base(name, price)
        {
            Quantity = quantity;
            ManufactureDate = manufactureDate;
            ShelfLifeDays = shelfLifeDays;
        }

        public override void Show()
        {
            Console.WriteLine($"Партія: {Name}, Ціна: {Price}, Кількість: {Quantity}, Виготовлено: {ManufactureDate:d}, Термін: {ShelfLifeDays} днів");
        }

        public override bool IsValid()
        {
            return (DateTime.Now - ManufactureDate).Days <= ShelfLifeDays;
        }
    }

    class Kit : Product
    {
        public List<string> Items { get; set; }

        public Kit(string name, double price, List<string> items)
            : base(name, price)
        {
            Items = items;
        }

        public override void Show()
        {
            Console.WriteLine($"Комплект: {Name}, Ціна: {Price}, Продукти: {string.Join(", ", Items)}");
        }

        public override bool IsValid() => true;
    }

    class PrintedEdition
    {
        public string Title;
        public virtual void Show() => Console.WriteLine($"Друковане видання: {Title}");
    }

    class Book : PrintedEdition
    {
        public string Author;
        public override void Show() => Console.WriteLine($"Книга: {Title}, Автор: {Author}");
    }

    class Magazine : PrintedEdition
    {
        public int Issue;
        public override void Show() => Console.WriteLine($"Журнал: {Title}, Випуск: {Issue}");
    }

    class Textbook : Book
    {
        public string Subject;
        public override void Show() => Console.WriteLine($"Підручник: {Title}, Автор: {Author}, Предмет: {Subject}");
    }

    struct Pupil
    {
        public string Surname, Name, Patronymic;
        public string Class;
        public string Phone;
        public int Math, Physics, Russian, Literature;
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nОберіть завдання (1-4), або 0 для виходу:");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var book = new Book { Title = "Кобзар", Author = "Шевченко" };
                        var mag = new Magazine { Title = "Наука і життя", Issue = 3 };
                        var text = new Textbook { Title = "Фізика 10", Author = "Іванов", Subject = "Фізика" };
                        book.Show(); mag.Show(); text.Show();
                        break;
                    case "2":
                        var b1 = new Book { Title = "Казки", Author = "Грімм" };
                        b1.Show();
                        break;
                    case "3":
                        Product[] products =
                        {
                            new FoodProduct("Молоко", 30, DateTime.Now.AddDays(-2), 7),
                            new Batch("Сирки", 10, 20, DateTime.Now.AddDays(-10), 5),
                            new Kit("Сніданок", 50, new List<string> { "Хліб", "Сир", "Кава" })
                        };
                        foreach (var p in products) { p.Show(); Console.WriteLine($"Придатний: {p.IsValid()}"); }
                        break;
                    case "4":
                        var pupils = new List<Pupil>
                        {
                            new Pupil { Surname = "Іванов", Name = "Іван", Patronymic = "Іванович", Class = "10-Б", Phone = "123", Math = 3, Physics = 2, Russian = 4, Literature = 5 },
                            new Pupil { Surname = "Петренко", Name = "Петро", Patronymic = "Петрович", Class = "10-Б", Phone = "456", Math = 2, Physics = 2, Russian = 2, Literature = 2 }
                        };
                        pupils.RemoveAll(p => p.Math == 2 || p.Physics == 2 || p.Russian == 2 || p.Literature == 2);
                        pupils.Insert(0, new Pupil { Surname = "Новий", Name = "Учень", Patronymic = "Іванович", Class = "10-Б", Phone = "999", Math = 5, Physics = 5, Russian = 5, Literature = 5 });
                        foreach (var p in pupils)
                            Console.WriteLine($"{p.Surname} {p.Name} {p.Patronymic}, Клас: {p.Class}, Тел: {p.Phone}, Оцінки: {p.Math},{p.Physics},{p.Russian},{p.Literature}");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір"); break;
                }
            }
        }
    }
}
