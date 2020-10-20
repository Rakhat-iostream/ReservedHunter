using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndustrialStudentPositionHunters.DTO;
using IndustrialStudentPositionHunters.Models;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;

namespace IndustrialStudentPositionHunters.Controllers
{
    [Route("{controller}/")]
    [Authorize]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IEntityRepository<Advertisement> _repos;
        private readonly IPositionsRepository _positionRepos;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public AdvertisementsController(IEntityRepository<Advertisement> repos, IPositionsRepository positionRepos)
        {
            _repos = repos;
            _positionRepos = positionRepos;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAllAdvertisements()
        {
            return await _repos.GetAll();
        }

        [HttpGet("pos={id}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAdvertisementsForPosition(int id)
        {
            var position = await _positionRepos.GetPositionById(id);
            return await (_repos as AdvertisementsRepository).GetAdvertisementsForPosition(position.Name);
        }
        [HttpGet("emp={id}")]
        [AllowAnonymous]
        public async Task<IList<Advertisement>> GetAllAdvertisementsByEmployer(int id)
        {
            logger.Info($"Getting all advertisements of employer with id = {id}");
            var ads = await (_repos as AdvertisementsRepository).GetAdvertisementsByEmployerId(id);
            return ads;
        }
       
        [HttpPost("emp={id}/add")]
        public async Task<IActionResult> AddAdvertisement(AdvertisementDTO adv, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Position pos = await _positionRepos.GetPositionByName(adv.PositionName);
            if (pos == null) return BadRequest("Such position is not in database");
            var advertisement = new Advertisement()
            {
                Title = adv.Title,
                Salary = adv.Salary.GetValueOrDefault(),
                Description = adv.Description,
                PositionName = adv.PositionName,
                PositionId = pos.PositionId,
                EmployerId = id
            };
            if (await _repos.HasEntity(advertisement)) return BadRequest("Ads with this title already exists");
            logger.Info($"Adding new advertisement for employer with id = {id}");
            if (await _repos.Create(advertisement)) return Ok("Added new ads");
            return BadRequest("Failed to add ads");
        }


        [HttpGet("id={id}")]
        [AllowAnonymous]
        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            logger.Info($"Getting advertisement with id = {id}");
            return await _repos.GetById(id);
        }
        [HttpPut("id={id}/update")]
        public async Task<IActionResult> UpdateAdvertisement(AdvertisementDTO advertisement, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");

            Advertisement ad = await _repos.GetById(id);
            if(ad != null)
            {
                ad.Title = advertisement.Title;
                ad.Description = advertisement.Description;
                ad.Salary = advertisement.Salary.GetValueOrDefault();
                ad.PositionName = advertisement.PositionName;
                logger.Info($"Updating advertisement with id = {id}");
                if (await _repos.Update(ad)) return Ok("Updated ads");
            }
            return BadRequest("This ads is not in database");
        }

        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            Advertisement ad = await _repos.GetById(id);
            if (ad == null) return BadRequest("This ads already deleted");
            logger.Info($"Deleting advertisement with id = {id}");
            if (await _repos.Delete(ad)) return Ok("Deleted ads");
            return BadRequest("Failed to delete ads");
        }
    }
}
