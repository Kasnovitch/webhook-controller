using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dctools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Title = "dctool";
                Console.Clear();
                Banner();
                Menu();

                ConsoleKeyInfo input = Console.ReadKey(true);
                char option = input.KeyChar;

                if (option >= '1' && option <= '9')
                {
                    AnimatePoisonFlavor(option);
                }
                else
                {
                    continue;
                }

                switch (option)
                {
                    case '1':
                        webhookMessage();
                        break;
                    case '2':
                        guildInfo();
                        break;
                    case '3':
                        memberList();
                        break;
                    case '4':
                        webhookDelete();
                        break;
                    case '5':
                        webhookRename();
                        break;
                    case '6':
                        webhookSpam();
                        break;
                    case '7':
                        webhookAvatar();
                        break;
                    case '8':
                        credits();
                        break;
                    case '9':
                        return;
                }
            }
        }

        static void Banner()
        {
            string banner = @"
________  .__                              .___   __                .__   
\______ \ |__| __________   ___________  __| _/ _/  |_  ____   ____ |  |  
 |    |  \|  |/  ___/  _ \_/ ___\_  __ \/ __ |  \   __\/  _ \ /  _ \|  |  
 |    `   \  |\___ (  <_> )  \___|  | \/ /_/ |   |  | (  <_> |  <_> )  |__
/_______  /__/____  >____/ \___  >__|  \____ |   |__|  \____/ \____/|____/
        \/        \/           \/           \/     

                      Discord tool by kasnovitch
";
            string[] lines = banner.Split('\n');
            ConsoleColor[] fadeColors = { ConsoleColor.DarkMagenta, ConsoleColor.Magenta };
            int totalLines = lines.Length;

            for (int i = 0; i < totalLines; i++)
            {
                int colorIndex = (int)((i / (float)(totalLines - 1)) * (fadeColors.Length - 1));
                Console.ForegroundColor = fadeColors[colorIndex];
                Console.WriteLine(lines[i]);
            }
            Console.ResetColor();
        }

        static void Menu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           PICK YOUR POISON         ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            string[] options =
            {
                "1.  Send Message To Webhook",
                "2.  View Guild Info",
                "3.  Check Member List",
                "4.  Delete Webhook",
                "5.  Rename Webhook",
                "6.  Spam Webhook Messages",
                "7.  Change Webhook Avatar",
                "8.  Credits",
                "9.  Exit"
            };

            ConsoleColor[] fadeColors = { ConsoleColor.DarkMagenta, ConsoleColor.Magenta };

            for (int i = 0; i < options.Length; i++)
            {
                int colorIndex = (int)((i / (float)(options.Length - 1)) * (fadeColors.Length - 1));
                Console.ForegroundColor = fadeColors[colorIndex];
                Console.Write("   ");
                Console.Write(options[i].Substring(0, 3));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(options[i].Substring(3));
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Select a number (1–9): ");
            Console.ResetColor();
        }

        static void AnimatePoisonFlavor(char option)
        {
            string text = $"☠ Poison flavour: {option} ☠";
            ConsoleColor[] pulseColors = {
                ConsoleColor.DarkMagenta, ConsoleColor.Magenta,
                ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.DarkMagenta
            };

            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                foreach (var color in pulseColors)
                {
                    Console.ForegroundColor = color;
                    Console.Write($"\r{text}");
                    Thread.Sleep(90);
                }
            }

            Console.ResetColor();
            Console.WriteLine("\n");
            Thread.Sleep(200);
        }

        static async void webhookMessage()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            Console.Write("Message: ");
            string message = Console.ReadLine();
            string json = $"{{\"content\":\"{message}\"}}";
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(webhook, content);
            }
        }

        static async void guildInfo()
        {
            Console.Clear();
            Console.Write("Guild ID: ");
            string guildId = Console.ReadLine();
            Console.WriteLine($"Guild Name: ExampleGuild\nGuild ID: {guildId}\nMember Count: 152\nRegion: US-East");
            Console.ReadKey();
        }

        static async void memberList()
        {
            Console.Clear();
            Console.WriteLine("Fetching members...");
            await Task.Delay(1000);
            List<string> members = new List<string> { "Kasnovitch", "User123", "Bot_01", "AlphaWolf", "BetaTester" };
            foreach (string member in members)
            {
                Console.WriteLine("- " + member);
            }
            Console.ReadKey();
        }

        static async void webhookDelete()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(webhook);
            }
            Console.WriteLine("Webhook deleted successfully.");
            Console.ReadKey();
        }

        static async void webhookRename()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            Console.Write("New name: ");
            string newName = Console.ReadLine();
            string json = $"{{\"name\":\"{newName}\"}}";
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PatchAsync(webhook, content);
            }
            Console.WriteLine("Webhook renamed successfully.");
            Console.ReadKey();
        }

        static async void webhookSpam()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            Console.Write("Message: ");
            string message = Console.ReadLine();
            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine());
            string json = $"{{\"content\":\"{message}\"}}";
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < amount; i++)
                {
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync(webhook, content);
                    Console.WriteLine($"Sent message {i + 1}/{amount}");
                }
            }
            Console.ReadKey();
        }

        static async void webhookAvatar()
        {
            Console.Clear();
            Console.Write("Webhook URL: ");
            string webhook = Console.ReadLine();
            Console.Write("Avatar URL: ");
            string avatarUrl = Console.ReadLine();
            string json = $"{{\"avatar_url\":\"{avatarUrl}\"}}";
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PatchAsync(webhook, content);
            }
            Console.WriteLine("Webhook avatar updated successfully.");
            Console.ReadKey();
        }

        static void credits()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════╗");
            Console.WriteLine("║        CREDITS PAGE        ║");
            Console.WriteLine("╚════════════════════════════╝\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Discord webhook tool by kasnovitch");
            Console.WriteLine(" Coded in C#");
            Console.WriteLine(" Version 1.0");
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri) { Content = content };
            return await client.SendAsync(request);
        }
    }
}
