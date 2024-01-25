using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleAppQ
{
    class Program
    {
        // решил использовать потокобезопасную очередь для работы паралельно с 3 мя окнами(потоками)
        static ConcurrentQueue<Ticket> queue = new ConcurrentQueue<Ticket>();
        // услуги
        static List<Service> services = new List<Service>();
        // окна обслуживания 
        static Window[] windows = new Window[3];
        // начальный билет
        static int numberOfTicket = 1;
        
        static void Main(string[] args)
        {
            // Создаем три рабочих окна по 8 часов работы
            windows[0] = new Window { Title = "Окно 1", WorkingHours = 8};
            windows[1] = new Window { Title = "Окно 2", WorkingHours = 8};
            windows[2] = new Window { Title = "Окно 3", WorkingHours = 8};
            
            // Создаем четыре разных услуги с продолжительностью
            services.Add(
                new Service
                {
                    Title = "Услуга 1",
                    Duration = 5
                }
            );    
            
            services.Add(
                new Service
                {
                    Title = "Услуга 2",
                    Duration = 7
                }
            );            
            
            services.Add(
                new Service
                {
                    Title = "Услуга 3",
                    Duration = 10
                }
            );            
            
            services.Add(
                new Service
                {
                    Title = "Услуга 4",
                    Duration = 15
                }
            );
            
            
            // Начинаем обслуживать население
            
            // Запускаем потоки для каждого окна
            foreach (var window in windows)
            {
                new Thread(() => ServeCustomers(window)).Start();
            }
            
            // Добавляем талоны в очередь выбирая наименьший из доступных в очереди(которые не в процессе обслуживания)
            List<int> serveTimes = new List<int>();
            foreach (var el in services)
            {
                serveTimes.Add(el.Duration);
            }
            
            for (int i = 0; i < 8 * 60 / serveTimes.Min(); i++) // здесь тоже такая себе симуляция набора очереди для 8 часов по продолжительности услуги
            {
                foreach (Service service in services)
                {
                    Ticket ticket = new Ticket
                    {
                        Service = service,
                        Number = numberOfTicket,
                        StartTime = DateTime.Now, // тут по факту надо смещать время от предыдущего, но я для простоты и симуляции очереди оставил так
                        EndTime = DateTime.Now.AddMinutes(service.Duration)
                    };
                    queue.Enqueue(ticket);
                    
                    numberOfTicket++;
                }
            }

            // Ждем завершения работы всех окон
            while (!queue.IsEmpty)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Очередь пуста. Работа завершена.");
            Console.ReadLine();
            
            static void ServeCustomers(Window window)
            {
                while (true)
                {
                    // Забираем с очереди услугу
                    if (queue.TryDequeue(out Ticket ticket))
                    {
                    
                        Console.WriteLine($"{window.Title}, талон {ticket.Number}: Обслуживание клиента на {ticket.Service.Duration} минут. \n" +
                                          $"Начало обработки талона {ticket.StartTime} \n" +
                                          $"Окончание обработки талона {ticket.EndTime}");
                        Console.WriteLine();
                        Thread.Sleep(ticket.Service.Duration); // Здесь надо минуты в миллисекунды * 60000, НО ДЛЯ СИМУЛЯЦИИ не умножаю
                    
                    }
                }
            }

            
        }
    }
}