using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using Newtonsoft.Json;

namespace EnergyMonitor.Agent
{
    class Program
    {
        // 👉 Ajusta estos dos valores
        static int DeviceId = 1; // ID existente en dbo.Devices
        static string ApiBase = "http://localhost:51234"; // URL de tu IIS Express o IIS

        static async Task Main()
        {
            Console.WriteLine("EnergyMonitor.Agent iniciando...");
            Console.WriteLine($"Publicando a: {ApiBase}/api/ingest/reading   DeviceId={DeviceId}");

            var computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true, // toma NVIDIA/AMD si el driver expone sensores
                IsBatteryEnabled = true
            };
            computer.Open();

             var http = new HttpClient();

            while (true)
            {
                try
                {
                    computer.Accept(new UpdateVisitor());

                    // Leemos potencia CPU y GPU (si existen)
                    float cpuW = SumPower(computer, HardwareType.Cpu);
                    float gpuNvidiaW = SumPower(computer, HardwareType.GpuNvidia);
                    float gpuAmdW = SumPower(computer, HardwareType.GpuAmd);

                    decimal watts = (decimal)(cpuW + gpuNvidiaW + gpuAmdW);
                    if (watts <= 0) watts = 0;

                    var payload = new
                    {
                        DeviceId = DeviceId,
                        Watts = Math.Round(watts, 2),
                        Source = "LHM"
                    };

                    string json = JsonConvert.SerializeObject(payload);
                    var res = await http.PostAsync($"{ApiBase}/api/ingest/reading",
                        new StringContent(json, Encoding.UTF8, "application/json"));

                    if (!res.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[{DateTime.Now:T}] ERROR HTTP: {(int)res.StatusCode} {res.ReasonPhrase}");
                    }
                    else
                    {
                        Console.WriteLine($"[{DateTime.Now:T}] {watts:F2} W enviados");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now:T}] Excepción: {ex.Message}");
                }

                await Task.Delay(1000); // cada 1s
            }
        }

        // Suma todos los sensores de potencia (W) del tipo de hardware indicado
        static float SumPower(Computer pc, HardwareType type)
        {
            float sum = 0f;
            foreach (var hw in pc.Hardware.Where(h => h.HardwareType == type))
            {
                hw.Update();
                foreach (var s in hw.Sensors.Where(s => s.SensorType == SensorType.Power))
                {
                    if (s.Value.HasValue) sum += s.Value.Value;
                }

                // también revisar sub-hardware (algunas GPUs lo usan)
                foreach (var sub in hw.SubHardware)
                {
                    sub.Update();
                    foreach (var s in sub.Sensors.Where(s => s.SensorType == SensorType.Power))
                        if (s.Value.HasValue) sum += s.Value.Value;
                }
            }
            return sum;
        }
    }

    // Visitor que refresca todos los sensores
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer) { computer.Traverse(this); }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (var sub in hardware.SubHardware) sub.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
