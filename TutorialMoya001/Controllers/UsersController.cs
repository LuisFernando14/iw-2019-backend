using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;
using TutorialMoya001.Utils;

namespace TutorialMoya001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly Util util;

        public UsersController(IUsersRepository usersRepository, Util util)
        {
            this.usersRepository = usersRepository;
            this.util = util;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await usersRepository.GetAll();
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Usuarios obtenidos correctamente",
                userData = response,
                fullStackTrace = "",
            });
        }

        // GET: api/users/email/partitionkey
        [HttpGet("{Email}", Name = "GetUser")]
        public async Task<IActionResult> Get(string Email)
        {
            var response = await usersRepository.GetById(Email);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Usuario obtenido correctamente",
                userData = response,
                fullStackTrace = "",
            });
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var response = await usersRepository.SaveAsync(user);
            var tokenHash = util.GetToken(user);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Registro exitoso",
                userData = user,
                fullStackTrace = "",
                token = tokenHash
            });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            var response = await usersRepository.Update(user);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Usuario actualizado correctamente",
                userData = user,
                fullStackTrace = "",
            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(User user)
        {
            var response = await usersRepository.Update(user);
            return Ok(new
            {
                correct = response,
                title = "Mensaje del sistema",
                message = "Usuario eliminado correctamente",
                userData = response,
                fullStackTrace = "",
            });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] Authentication data)
        {
            var user = await usersRepository.Login(data);
            if(user == null)
            {
                return Ok(new {
                    correct = false,
                    title = "Error al iniciar sesión",
                    message = "Usuario y/o contraseña incorrectos",
                    userData = user,
                    fullStackTrace = "",
                });
            }
            var tokenHash = util.GetToken(user);
            return Ok(new {
                correct = true,
                title = "Mensaje del sistema",
                message = "Ha iniciado sesión correctamente",
                userData = user,
                fullStackTrace = "",
                token = tokenHash
            });
        }
    }
}
