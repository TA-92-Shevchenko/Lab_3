using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            void SQLWhere()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var users = db.People.Where(p => p.FirstName == "Ivan");
                    foreach (Person user in users)
                        Console.WriteLine($"{user.Id} {user.ClientId}");
                }
            }
            void SQLSELECT()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {

                    var users = db.People.Select(p => new
                    {
                        Name = p.LastName,
                        Id = p.Id,
                        ClientId = p.ClientId
                    });
                    foreach (var user in users)
                        Console.WriteLine($"{user.Id} {user.Name} {user.ClientId}");
                }
            }
            void SQLOrder()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var users = db.People.OrderBy(p => p.FirstName);
                    foreach (var user in users)
                        Console.WriteLine($"{user.Id} {user.ClientId} {user.FirstName}");
                }
            }
            void SQLJoin()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var users = db.People.Join(db.Clients, 
                        u => u.ClientId, 
                        c => c.Id, 
                        (u, c) => new 
                        {
                            Name = u.FirstName,
                            number = c.PhoneNumber,
                            id = u.ClientId
                        });
                    foreach (var u in users)
                        Console.WriteLine($"{u.id} {u.Name} {u.number}");
                }
            }
            void SQLGroup()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var groups = from u in db.People
                                 group u by u.FirstName into g
                                 select new
                                 {
                                     g.Key,
                                     Count = g.Count()
                                 };
                    foreach (var group in groups)
                    {
                        Console.WriteLine($"{group.Key} - {group.Count}");
                    }
                }
            }
            void SQLUnion()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var users = db.People.Where(u => u.Id < 5)
                        .Union(db.People.Where(u => u.Id > 0));
                    foreach (var user in users)
                        Console.WriteLine(user.FirstName);
                }
            }
            void SQLInterselect()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var users = db.People.Where(u => u.Id > 1)
                        .Intersect(db.People.Where(u => u.FirstName.Contains("John")));
                    foreach (var user in users)
                        Console.WriteLine(user.Id);
                }
            }
            void SQLExept()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var selector1 = db.People.Where(u => u.Id > 1); 
                    var selector2 = db.People.Where(u => u.FirstName.Contains("John")); 
                    var users = selector1.Except(selector2); 

                    foreach (var user in users)
                        Console.WriteLine(user.FirstName);
                }
            }
            void SQLCount()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    int number1 = db.People.Count();
                  
                    int number2 = db.People.Count(u => u.FirstName.Contains("John"));

                    Console.WriteLine("Всього: "+number1);
                    Console.WriteLine("Таких значень "+number2);
                }
            }
          

            void SQL()
            {
                using (PostOfficeContext db = new PostOfficeContext())
                {
                    var a = from o in db.People
                              join c in db.Parcels
                              on o.ClientId equals c.SenderId
                              select new { pacel = c.Id, id = o.Id, Name = o.FirstName };

                    var b = a.GroupBy(x => x.id)
                                   .Select(x => new {id = x.Key, Count = x.Count() });
                    
                    int temp = b.Max(x => x.Count);
                    
                    var li = b.Where(a=>a.Count==temp);

                    foreach (var j in li)
                        Console.WriteLine("Person ID: "+j.id + " Count: " +j.Count);


                }
            }
            
            SQL();

        }
    }
}