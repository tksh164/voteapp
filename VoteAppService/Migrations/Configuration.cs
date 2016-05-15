namespace VoteAppService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VoteAppService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<VoteAppService.Models.VoteAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VoteAppService.Models.VoteAppDbContext context)
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
            //

            //context.Teams.AddOrUpdate(
            //    new Team {
            //        TeamName = @"Team A",
            //        Description = @"The description for the Team A.​",
            //        TeamMember = @"Andrew Peters",
            //        ImageUri = @"",
            //    },
            //    new Team
            //    {
            //        TeamName = @"Team B",
            //        Description = @"The description for the Team B​",
            //        TeamMember = @"Brice Lambson, Rowan Miller",
            //        ImageUri = @"",
            //    },
            //    new Team
            //    {
            //        TeamName = @"Team C",
            //        Description = @"The description for the Team C​",
            //        TeamMember = @"Rowan Miller, Andrew Peters, Brice Lambson",
            //        ImageUri = @"",
            //    }
            //);

            //context.Votes.AddOrUpdate(
            //    new Vote {
            //        Voter = @"rbzZ1CJoH3CH-Y8_TdrEBEoBmWN8KkGvNqwnUko_RsQ",
            //        Score = 2,
            //        Team = new Team {
            //            TeamId = 1,
            //        },
            //    }
            //);
        }
    }
}
