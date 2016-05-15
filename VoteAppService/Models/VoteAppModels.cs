using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace VoteAppService.Models
{
    public class VoteAppDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }

    public static class ConstraintsConstants
    {
        public const int VoteScoreRangeMin = 0;
        public const int VoteScoreRangeMax = 5;
    }

    public class Team
    {
        public int TeamId { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(450, MinimumLength = 1), Index()]
        public string TeamName { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(450, MinimumLength = 1), Index()]
        public string TeamMember { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string ImageUri { get; set; }

        [Required(), Index()]
        public int SortOrder { get; set; }

        [JsonIgnore()]
        public virtual List<Vote> Votes { get; set; }
    }

    public class Vote
    {
        public int VoteId { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(64, MinimumLength = 1), Index("IX_VoterAndTeam", 1, IsUnique = true), Index()]
        public string Voter { get; set; }

        [Required, Range(ConstraintsConstants.VoteScoreRangeMin, ConstraintsConstants.VoteScoreRangeMax)]
        public int Score { get; set; }

        [Required, Index("IX_VoterAndTeam", 2, IsUnique = true), Index()]
        public virtual Team Team { get; set; }
    }
}