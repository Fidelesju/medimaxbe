using MediMax.Business.CoreServices.Interfaces;
using MediMax.Business.Exceptions;
using MediMax.Business.RealTimeServices.Interfaces;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IO.Ports;

namespace MediMax.Application.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class FirmwareController : BaseController<FirmwareController>
    {
        private readonly ILoggerService _loggerService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private static SerialPort _serialPort;

        public FirmwareController (
            ILogger<FirmwareController> logger,
            ILoggerService loggerService) : base(logger, loggerService)
        {
            InitializeSerialPort("COM7");  // Substitua "COM7" pela porta correta
        }

        private void InitializeSerialPort ( string portName )
        {
            if (_serialPort == null)
            {
                _serialPort = new SerialPort(portName, 9600);
            }

            if (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                    Console.WriteLine($"Porta {portName} aberta com sucesso.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Acesso à porta {portName} negado: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao abrir a porta {portName}: {ex.Message}");
                }
            }
        }

        [HttpPost("led/on")]
        public async Task<ActionResult<BaseResponse<bool>>> OnLed ( LedsRequestModel leds )
        {
            try
            {
                foreach (var led in leds.leds)
                {
                    _serialPort.WriteLine(led.ToString()); // Envia o número do LED para o Arduino
                }

                var response = BaseResponse<bool>
                    .Builder()
                    .SetMessage("LED(s) aceso(s)")
                    .SetData(true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao acender LEDs: {ex.Message}");
            }
        }

        [HttpPost("led/off")]
        public async Task<ActionResult<BaseResponse<bool>>> OffLed ( LedsRequestModel leds )
        {
            try
            {
                foreach (var led in leds.leds)
                {
                    _serialPort.WriteLine((-led).ToString()); // Envia o número negativo do LED para o Arduino apagar
                }

                var response = BaseResponse<bool>
                    .Builder()
                    .SetMessage("LED(s) apagado(s)")
                    .SetData(true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao apagar LEDs: {ex.Message}");
            }
        }


        private string GenerateLedCommand ( List<int> leds, char action )
        {
            char[] commandArray = new char[leds.Max() + 1]; // Assumindo que `leds` tem o índice mais alto do LED
            Array.Fill(commandArray, '0'); // Preenche a string com '0'

            foreach (var led in leds)
            {
                if (led < commandArray.Length)
                {
                    commandArray[led] = action; // Define o estado ('1' para on, '0' para off) para o LED específico
                }
            }

            return new string(commandArray);
        }


        [HttpGet("sensor")]
        public IActionResult GetSensorData ( )
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write("S");  // Envia o comando para solicitar o status do sensor

                    // Aguarde até que os dados estejam disponíveis
                    if (_serialPort.BytesToRead > 0)
                    {
                        string data = _serialPort.ReadLine();
                        string cleanedData = data.Trim();
                        return Ok(new { sensorData = cleanedData });
                    }
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, new { error = "A porta está fechada." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
