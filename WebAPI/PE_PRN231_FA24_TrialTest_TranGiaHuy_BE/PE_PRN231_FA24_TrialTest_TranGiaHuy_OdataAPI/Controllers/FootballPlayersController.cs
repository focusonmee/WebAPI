using BOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repos;

namespace PE_PRN231_FA24_TrialTest_TranGiaHuy_OdataAPI.Controllers
{
  
    public class FootballPlayersController : ODataController
    {
        private readonly IFootballPlayerService _footballPlayerService;

        public FootballPlayersController(IFootballPlayerService footballPlayerService)
        {
            _footballPlayerService = footballPlayerService;
        }

        [EnableQuery]

        public async Task <ActionResult<IEnumerable<FootballPlayer>>> GetFootballPlayers()
        {
            try
            {
                var players = await _footballPlayerService.GetFootballPlayers();
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpGet("/api/FootballClubs")]
        public async Task<ActionResult<List<FootballClub>>> GetClub()
        {
            try
            {
                var clubs = await _footballPlayerService.GetFootballClubs();
                return Ok(clubs);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpPost("/api/FootballPlayers")]
        public async Task<ActionResult<FootballPlayer>> AddPlayer([FromBody] FootballPlayer player)
        {
            if (player == null)
            {
                return BadRequest("Player data cannot be null");
            }

            try
            {
                var newPlayer = await _footballPlayerService.AddFootballPlayer(player);
                return Ok(newPlayer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPut("/api/FootballPlayers/{id}")]
        public async Task<ActionResult<FootballPlayer>> UpdatePlayer(string id,[FromBody] FootballPlayer player)
        {
            try
            {
                var updatedPlayer = await _footballPlayerService.UpdateFootballPlayer(player);
                return Ok(updatedPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }


        [HttpDelete("/api/FootballPlayers/{id}")]
        public async Task<ActionResult<FootballPlayer>> DeletePlayer(string id)
        {
            try
            {
                var detletedPlayer = await _footballPlayerService.DeleFootballPlayer(id);
                return Ok(detletedPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpGet("/api/FootballPlayers/{id}")]
        public async Task<ActionResult<FootballPlayer>> GetFootBallPlayer(string id)
        {
            try
            {
                var footballplayer = await _footballPlayerService.GetFootballPlayer(id);
                return Ok(footballplayer);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }
    }
}
