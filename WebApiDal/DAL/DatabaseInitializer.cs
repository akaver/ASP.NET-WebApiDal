using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace DAL
{
    //    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            var autoDetectChangesEnabled = context.Configuration.AutoDetectChangesEnabled;
            // much-much faster for bulk inserts!!!!
            context.Configuration.AutoDetectChangesEnabled = false;

            SeedIdentity(context);
            SeedArticles(context);
            SeedContacts(context);


            // restore state
            context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            base.Seed(context);
        }

        private void SeedContacts(DataBaseContext context)
        {
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Skype", "en", "Skype", "ContactType.0")
            });
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Email", "en", "Email", "ContactType.0")
            });
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Phone", "en", "Phone", "ContactType.0")
            });

            context.SaveChanges();

            // fill database with random names and data

            //undocumented way to get directory
            var path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string lastNamesFull = null;
            string firstNamesFull = null;
            string middleNamesFull = null;
            string placesFull = null;
            string countriesFull = null;
            
            if (File.Exists(path + "\\names.json"))
            {
                lastNamesFull = File.ReadAllText(path + "\\names.json");
            }
            if (File.Exists(path + "\\countries.json"))
            {
                countriesFull = File.ReadAllText(path + "\\countries.json");
            }
            if (File.Exists(path + "\\first-names.json"))
            {
                firstNamesFull = File.ReadAllText(path + "\\first-names.json");
            }
            if (File.Exists(path + "\\middle-names.json"))
            {
                middleNamesFull = File.ReadAllText(path + "\\middle-names.json");
            }
            if (File.Exists(path + "\\places.json"))
            {
                placesFull = File.ReadAllText(path + "\\places.json");
            }

            var jsonObjCountries = JObject.Parse(countriesFull);
            var jsonArrayCountries = (JArray) jsonObjCountries["countries"]["country"];
            List<string> countryList = jsonArrayCountries.Select(a => (string) a["countryName"]).ToList();

            List<string> placesList = JArray.Parse(placesFull).ToObject<List<string>>();
            List<string> lastNamesList = JArray.Parse(lastNamesFull).ToObject<List<string>>();
            List<string> firstNamesList = JArray.Parse(firstNamesFull).ToObject<List<string>>();
            List<string> middleNamesList = JArray.Parse(middleNamesFull).ToObject<List<string>>();

            Random r = new Random();


            var skypeId = context.ContactTypes.FirstOrDefault(a => a.ContactTypeName.Value == "Skype")?.ContactTypeId ?? 0;
            var emailId = context.ContactTypes.FirstOrDefault(a => a.ContactTypeName.Value == "Email")?.ContactTypeId ?? 0;
            var userId = context.UsersInt.FirstOrDefault(a => a.Email == "1@eesti.ee")?.Id ?? 0;

            var counter = 0;
            foreach (var lastName in lastNamesList.Take(200))
            {
                var firstName = firstNamesList[r.Next(0, firstNamesList.Count)];

                context.Persons.Add(new Person()
                {
                    Firstname = firstName,
                    Lastname = lastName,
                    UserId = userId,

                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactTypeId = skypeId, 
                            ContactValue = middleNamesList[r.Next(0, middleNamesList.Count)]+"."+placesList[r.Next(0, placesList.Count)]
                        },
                        new Contact()
                        {
                            ContactTypeId = emailId, 
                            ContactValue = firstName+"."+lastName+"@"+countryList[r.Next(0, countryList.Count)]+".com"
                        }
                    },

                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0,365*50))),
                    Time = DateTime.Now.Subtract(TimeSpan.FromMinutes(r.Next(0, 12*60))),
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                    Date2 = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                    Time2 = DateTime.Now.Subtract(TimeSpan.FromMinutes(r.Next(0, 12*60))),
                    DateTime2 = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                });

                //Save after every X records
                counter++;
                if (counter % 100 == 0)
                    context.SaveChanges();
            }
            // save the remaining
            context.SaveChanges();

        }

        private void SeedArticles(DataBaseContext context)
        {
            var articleHeadLine = "<h1>ASP.NET</h1>";
            var articleBody =
                "<p class=\"lead\">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.<br/>" +
                "As a demo, here is simple Contact application - log in and save your contacts!</p>";
            var article = new Article()
            {
                ArticleName = "HomeIndex",
                ArticleHeadline =
                    new MultiLangString(articleHeadLine, "en", articleHeadLine, "Article.HomeIndex.ArticleHeadline"),
                ArticleBody = new MultiLangString(articleBody, "en", articleBody, "Article.HomeIndex.ArticleBody")
            };
            context.Articles.Add(article);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "<h1>ASP.NET on suurepärane!</h1>",
                Culture = "et",
                MultiLangString = article.ArticleHeadline
            });

            context.Translations.Add(new Translation()
            {
                Value =
                    "<p class=\"lead\">ASP.NET on tasuta veebiraamistik suurepäraste veebide arendamiseks kasutades HTML-i, CSS-i, ja JavaScript-i.<br/>" +
                    "Demona on siin lihtne Kontaktirakendus - logi sisse ja salvesta enda kontakte</p>",
                Culture = "et",
                MultiLangString = article.ArticleBody
            });
            context.SaveChanges();
        }

        private void SeedIdentity(DataBaseContext context)
        {
            var pwdHasher = new PasswordHasher();

            // Roles
            context.RolesInt.Add(new RoleInt()
            {
                Name = "Admin"
            });

            context.SaveChanges();

            // Users
            context.UsersInt.Add(new UserInt()
            {
                UserName = "1@eesti.ee",
                Email = "1@eesti.ee",
                FirstName = "Super",
                LastName = "User",
                PasswordHash = pwdHasher.HashPassword("a"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            // Users in Roles
            context.UserRolesInt.Add(new UserRoleInt()
            {
                User = context.UsersInt.FirstOrDefault(a => a.UserName == "1@eesti.ee"),
                Role = context.RolesInt.FirstOrDefault(a => a.Name == "Admin")
            });

            context.SaveChanges();
        }
    }
}