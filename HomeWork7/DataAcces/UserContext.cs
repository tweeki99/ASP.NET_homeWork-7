using HomeWork7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeWork7.DataAcces
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            Role admin = new Role { Name = "admin" };
            Role user = new Role { Name = "user" };
            db.Roles.Add(admin);
            db.Roles.Add(user);
            db.Users.Add(new User
            {
                Email = "admin",
                Password = "123456",
                Role = admin
            });
            db.Posts.AddRange(new List<Post>()
            {
                new Post
                {
                    ImgPath = "http://s.4pda.to/SIdyo8U5br7A42YEfmJ49SZ9P6pz2vnqHNCxe.png",
                    Title = "«Известный блогер разобрал новые AirPods Pro",
                    Text = "Недавно эксперты iFixit признали беспроводные наушники AirPods Pro неремонтопригодными. Известный технолоблогер Зак Нильсон решил проверить это утверждение. Вооружившись своим фирменным ножом и другими инструментами, он взялся за разборку «яблочной» новинки. "
                },
                new Post
                {
                    ImgPath = "http://s.4pda.to/SIdymCKKEtYJRoQ2tZTVJbiHbWilHrUm4Ymw.png",
                    Title = "Microsoft Surface Pro 7 разочаровал экспертов по ремонту гаджетов",
                    Text = "В этом году Microsoft решила исправить проблему низкой ремонтопригодности своих устройств семейства Surface. Так, ноутбук Surface Laptop 3 получил от iFixit 5 баллов из 10 вместо нуля, как у предшественников. Surface Pro X и вовсе получил самый высокий балл в истории редмондской корпорации — 6. Воодушевлённые такими результатами, эксперты портала iFixit взялись за разборку нового Surface Pro 7. "
                },
                new Post
                {
                    ImgPath = "http://s.4pda.to/SIdytlKqUz2snEJfyKLARPKtByVCV99Oz2yEz1X.jpg",
                    Title = "Первый взгляд на vivo V17: кто на новенького в среднем классе?",
                    Text = "Компания vivo привезла в Россию новый смартфон V17. Это гаджет среднего ценового сегмента с приятным набором характеристик, выделяющийся на фоне бесчисленных конкурентов дизайном. Мы провели с новинкой несколько часов и готовы рассказать о первых впечатлениях."
                },
                new Post
                {
                    ImgPath = "http://s.4pda.to/SIdyz0lUbrz0JeHZHmA6403juJ0vJFXDoUlWrp.jpg",
                    Title = "Инсайды #1962: HUAWEI Harmony OS, Apple iPhone SE 2, электронная книга Xiaomi",
                    Text = "В новом выпуске Инсайдов: OPPO анонсировала фирменную оболочку ColorOS 7; HUAWEI выпустит Harmony OS для смартфонов в 2020-м; известный аналитик рассказал о будущих новинках Apple; Xiaomi выпустит альтернативу электронной книге Amazon Kindle."
                }
            });
            base.Seed(db);
        }
    }
}