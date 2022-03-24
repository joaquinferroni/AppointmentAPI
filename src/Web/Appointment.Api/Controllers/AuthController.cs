﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Appointment.Api.Infrastructure.HttpResponses;
using Appointment.Application.AuthUseCases.Authenticate;
using Appointment.Application.AuthUseCases.AuthenticateExternal;
using Appointment.Application.AuthUseCases.CreateUser;
using Appointment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Api.Controllers
{
    [Route("Api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(LoginResult), 200)]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommand login)
            => (await _mediator.Send(login)).ToHttpResponse();

        [HttpPost("External")]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(LoginResult), 200)]
        public async Task<IActionResult> AuthenticateExternal([FromBody] LoginExternalCommand login)
            => (await _mediator.Send(login)).ToHttpResponse();


        [HttpPost("TZ")]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(LoginResult), 200)]
        public async Task<IActionResult> MyTz(test myTz)
        {
            CultureInfo culture = new CultureInfo("ES-es");

            return Ok(new
                {
                    utc = myTz.myTz.ToUniversalTime(),
                    received = myTz.myTz,
                    shortLd = myTz.myTz.ToShortTimeString(),
                    local = myTz.myTz.ToLocalTime(),
                    diff = myTz.myTz.ToUniversalTime() - myTz.myTz.ToLocalTime(),
                    a= myTz.myTz.ToString("dddd, dd MMMM yyyy HH:mm", culture),
                }
            );
        }

    }

    public class test
    {
        public DateTime myTz { get; set; }
    }
}