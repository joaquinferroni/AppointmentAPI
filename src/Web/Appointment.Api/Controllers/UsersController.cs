﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment.Api.Infrastructure.HttpResponses;
using Appointment.Application.AuthUseCases.Authenticate;
using Appointment.Application.AuthUseCases.AuthenticateExternal;
using Appointment.Application.AuthUseCases.CreateUser;
using Appointment.Application.UsersUseCase.GetUserByRole;
using Appointment.Domain;
using Appointment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Api.Controllers
{
    [Route("Api/Users")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
            => (await _mediator.Send(command)).ToHttpResponse();

        [HttpGet("hosts")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetHosts()
            => (await _mediator.Send(new GetUserByRoleQuery(RolesEnum.HOST))).ToHttpResponse();

        [HttpGet("patients")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> GetCommon()
            => (await _mediator.Send(new GetUserByRoleQuery(RolesEnum.COMMON))).ToHttpResponse();

    }
}