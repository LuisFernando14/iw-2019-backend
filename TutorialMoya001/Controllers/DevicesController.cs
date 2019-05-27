using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TutorialMoya001.Hubs;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;

// https://fontawesome.com/icons?d=gallery&q=cloud

namespace TutorialMoya001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesRepository devicesRepository;
        private readonly IHubContext<ActionHub, IActionHub> hubContext;

        public DevicesController(IDevicesRepository devicesRepository, IHubContext<ActionHub, IActionHub> devicesHub)
        {
            this.devicesRepository = devicesRepository;
            this.hubContext = devicesHub;
        }

        // GET: api/Devices/partitionKey
        [HttpGet("{partitionKey}")]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(string partitionKey)
        {
            var response = await devicesRepository.GetAll(partitionKey);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Dispositivos obtenidos correctamente",
                deviceData = response,
                fullStackTrace = "",
            });
        }

        // GET: api/Devices/partitionKey/rowKey
        
        [HttpGet("{partitionKey}/{rowKey}", Name = "Get")]
        public async Task<IActionResult> Get(string partitionKey, string rowKey)
        {
            var response = await devicesRepository.GetById(partitionKey, rowKey);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Dispositivo obtenido correctamente",
                deviceData = response,
                fullStackTrace = "",
            });
        }
        

        // POST: api/Devices
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Device device)
        {
            var response = await devicesRepository.Save(device);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Dispositivo agregado exitosamente",
                deviceData = response,
                fullStackTrace = "",
            });
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Device device)
        {
            var response = await devicesRepository.Update(device);
            // Notify clients in the group
            await this.hubContext.Clients.Group(device.UserEmail).DeviceIsOnChange(response);
            // Notify every client
            await this.hubContext.Clients.All.DeviceStatusChange(response.Id, response.UserEmail, response.IsOn);
            return Ok(new
            {
                correct = true,
                title = "Mensaje del sistema",
                message = "Dispositivo actualizado correctamente",
                deviceData = response,
                fullStackTrace = "",
            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            var response = await devicesRepository.Delete(partitionKey, rowKey);
            return Ok(new
            {
                correct = response,
                title = "Mensaje del sistema",
                message = "Dispositivo eliminado correctamente",
                deviceData = response,
                fullStackTrace = "",
            });
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Device device)
        {
            if (device == null)
            {
                return Ok(new
                {
                    correct = false,
                    title = "Petición inválida.",
                    message = "No se permite enviar solicitud vacía",
                    deviceData = device,
                    fullStackTrace = "",
                });
            }
            var response = await devicesRepository.TurnOnOffDevice(device);
            return Ok(new
            {
                correct = false,
                title = "Petición inválida.",
                message = "No se permite enviar solicitud vacía",
                deviceData = response,
                fullStackTrace = "",
            });
        }
    }
}
