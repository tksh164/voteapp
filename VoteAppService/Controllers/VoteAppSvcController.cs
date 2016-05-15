using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using VoteAppService.Models;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace VoteAppService.Controllers
{
    [Authorize]
    public class VoteAppSvcController : ApiController
    {
        private VoteAppDbContext db = new VoteAppDbContext();

        // GET: api/VoteAppSvc/Teams
        [HttpGet]
        [ActionName("Teams")]
        public IHttpActionResult GetTeams()
        {
            List<Team> teams = db.Teams.OrderBy(t => t.SortOrder).ToList();
            string voter = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<object> results = new List<object>();

            foreach (Team team in teams)
            {
                int score = 0;
                Vote vote = team.Votes.Where(v => v.Voter == voter).FirstOrDefault();
                if (vote != null) score = vote.Score;

                results.Add(new
                {
                    TeamId = team.TeamId,
                    TeamName = team.TeamName,
                    TeamMember = team.TeamMember,
                    Description = team.Description,
                    ImageUri = team.ImageUri,
                    Score = score,
                    MaxScore = ConstraintsConstants.VoteScoreRangeMax,
                });
            }

            return Ok(results);
        }

        // GET: api/VoteAppSvc/TeamScore/5
        [HttpGet]
        [ActionName("TeamScore")]
        public IHttpActionResult GetTeamScore([FromUri]int id)
        {
            Team team = db.Teams.Where(t => t.TeamId == id).FirstOrDefault();
            if (team == null) return BadRequest();

            string voter = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            int score = 0;
            Vote vote = team.Votes.Where(v => v.Voter == voter).FirstOrDefault();
            if (vote != null) score = vote.Score;

            object result = new
            {
                TeamId = team.TeamId,
                Score = score,
                MaxScore = ConstraintsConstants.VoteScoreRangeMax,
            };

            return Ok(result);
        }

        // PUT: api/VoteAppSvc/TeamScore/5
        [HttpPut]
        [ActionName("TeamScore")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateTeamScore([FromUri]int id, [FromBody]JObject teamData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Team team = new Team { TeamId = teamData["TeamId"].ToObject<int>() };
            if (id != team.TeamId) return BadRequest();

            int score = teamData["Score"].ToObject<int>();
            string voter = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            db.Entry(team).State = EntityState.Unchanged;
            Vote vote = db.Votes.Where(v => v.Voter == voter && v.Team.TeamId == team.TeamId).FirstOrDefault();
            if (vote == null)
            {
                db.Votes.Add(new Vote
                {
                    Team = team,
                    Voter = voter,
                    Score = score,
                });
            }
            else
            {
                vote.Score = score;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teams
        //[ResponseType(typeof(Team))]
        //public IHttpActionResult PostTeam(Team team)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Teams.Add(team);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = team.TeamId }, team);
        //}

        // DELETE: api/Teams/5
        //[ResponseType(typeof(Team))]
        //public IHttpActionResult DeleteTeam(int id)
        //{
        //    Team team = db.Teams.Find(id);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Teams.Remove(team);
        //    db.SaveChanges();

        //    return Ok(team);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}