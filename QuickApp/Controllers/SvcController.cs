// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using AutoMapper;
using DAL;
using DAL.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickApp.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SvcController : Controller
    {

        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;


        public SvcController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: svc/values
        [HttpGet]
        public IActionResult Get(DateTime? from, DateTime? to)
        {
            if (from == null || to == null)
            {
                var svcs = _unitOfWork.Svcs.GetAll();
                return Ok(Mapper.Map<IEnumerable<SvcViewModel>>(svcs));
            }
            else
            {
                var svcs = _unitOfWork.Svcs.Find(s => s.Date >= from && s.Date <= to).ToList();
                return Ok(Mapper.Map<IEnumerable<SvcViewModel>>(svcs));
            }
        }

        // GET: svc/values
        [HttpGet("locked/{id:int}")]
        public IActionResult IsLocked(int id)
        {
            var userId = Utilities.GetUserId(this.User);
            var svc = _unitOfWork.Svcs.Get(id);
            if (svc == null)
            {
                return BadRequest(false);
            }
            else
            {
                if (!string.IsNullOrEmpty(svc.LockedBy) && svc.LockedBy != userId)
                {
                    var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == svc.LockedBy);
                    if (user != null)
                    {
                        return Ok(user.UserName);
                    }
                }
            }
            return Ok(string.Empty);
        }


        // GET: svc/values
        [HttpGet("lock/{id:int}")]
        public IActionResult Lock(int id)
        {
            var userId = Utilities.GetUserId(this.User);
            var svc = _unitOfWork.Svcs.Get(id);
            if(svc == null)
            {
                return BadRequest(string.Empty);
            }
            else
            {
                svc.LockedBy = userId;
                _unitOfWork.SaveChanges();
                return Ok(userId);
            }
        }






        // GET: svc/values
        [HttpGet("delete/{id:int}")]
        public IActionResult DeleteSvc(int id)
        {
            try
            {
                var svc = _unitOfWork.Svcs.Get(id);
                if (svc == null)
                {
                    return BadRequest(false);
                }
                else
                {
                    _unitOfWork.Svcs.Remove(svc);
                    return Ok(true);
                }
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }


        [HttpPut("update/{id:int}")]
        public IActionResult UpdateSvc(int id, [FromBody] SvcViewModel svc)
        {
            if (ModelState.IsValid)
            {
                if (svc == null)
                {
                    return BadRequest($"{nameof(svc)} cannot be null");
                }

                if (id != svc.Id)
                {
                    return BadRequest("Conflicting role id in parameter and model data");
                }

                var upateSvc = _unitOfWork.Svcs.Get(id);

                if (upateSvc == null)
                {
                    return NotFound(id);
                }

                svc.ProcessModel();
                Mapper.Map(svc, upateSvc);

                _unitOfWork.Svcs.Update(upateSvc);
                _unitOfWork.SaveChanges();
                return Ok(true);
            }
            return BadRequest(ModelState);
        }




        [HttpPut("create")]
        public IActionResult CreateSvc([FromBody] SvcViewModel svc)
        {
            if (ModelState.IsValid)
            {
                var createSvc = new Svc();
                Mapper.Map(svc, createSvc);
                svc.Date = DateTime.Parse(svc.ReadableDate);
                _unitOfWork.Svcs.Add(svc);
                _unitOfWork.SaveChanges();
                return Ok(true);
            }
            return BadRequest(ModelState);
        }
    }
}
