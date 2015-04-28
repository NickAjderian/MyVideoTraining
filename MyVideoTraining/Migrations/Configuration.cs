namespace MyVideoTraining.Migrations
{
    using MyVideoTraining.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<MyVideoTraining.Models.MVTDataModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public static void Seed(MyVideoTraining.Models.MVTDataModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //http://media.rawvoice.com/lse_residences/richmedia.lse.ac.uk/residences/LDN_SOEAPS_StaySafeAtUni.mp4

            if (!context.AssignmentTypes.Any())
            {
                context.AssignmentTypes.AddOrUpdate(
                    x => x.AssignmentTypeName,
                    new AssignmentType { AssignmentTypeName = "Fire Safety" }
                    );

                context.Medias.AddOrUpdate(
                    x => x.Url,
                    new Media { MediaName = "Fire Safety", Url = "http://media.rawvoice.com/lse_residences/richmedia.lse.ac.uk/residences/LDN_SOEAPS_StaySafeAtUni.mp4" }
                    );

                context.MediaQuestions.AddOrUpdate(
                    x => x.Question,
                    new MediaQuestion { Question = "Fire Safety 1", Responses = "Yes, I agree;Play Again;No, I Disagree" },
                    new MediaQuestion { Question = "Fire Safety 2", Responses = "Please sign here" },
                    new MediaQuestion { Question = "Fire Safety 3", Responses = "0-100" }
                    );
                context.SaveChanges();

                var at1 = context
                    .AssignmentTypes
                    .Include(a => a.Medias.Select(m => m.Questions)) //include 2 levels
                    .OrderBy(x => x.AssignmentTypeId).FirstOrDefault();

                if (!at1.Medias.Any())
                {
                    at1.Medias.Add(context.Medias.FirstOrDefault());
                    context.SaveChanges(); //do you have to save changes here?
                }

                var m1 = context.Medias.Include("Questions").FirstOrDefault(x => x.AssignmentTypeId == at1.AssignmentTypeId);


                if (!m1.Questions.Any())
                {
                    foreach (var q in context.MediaQuestions.Take(3))
                    {
                        m1.Questions.Add(q);
                    }
                }
                context.SaveChanges();

            }

            if (!context.People.Any())
            {
                context.People.AddOrUpdate(
                    x => x.PersonName,
                    new Person { PersonName = "Fred Flintstone", Address = "1 Rock Gulch", Phone = "123 123 123" },
                    new Person { PersonName = "Barney Rubble", Address = "3 Rock Gulch", Phone = "234 234 234" }
                    );

                context.SaveChanges();

            }

            var at2 = context
                .AssignmentTypes
                .OrderBy(x => x.AssignmentTypeId).FirstOrDefault();

            foreach (var p in context.People
                .Include(p => p.Assignments).Take(2)) //include assignments so you get to check for Any()
            {
                if (!p.Assignments.Any())
                {
                    p.Assignments.Add(new Assignment { AssignmentType = at2 });
                    context.SaveChanges();
                }
            }

        }


    }
}
