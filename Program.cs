using System;
using System.Text;
using System.Linq;


namespace ReportMultiplicationGamesForKids
{
    class Program
    {
        static void Main(string[] args)
        {   
            string local = String.Empty;

            do {
                Console.Write("Enter a country code (for example 'us') or 'help' for reference: \n");
                string line = Console.ReadLine();
                
                string[] checkLine = { line.ToLower(), line.ToUpper() };
                Local[] arrayLocal = Enum.GetValues(typeof(Local)).Cast<Local>().ToArray();

                foreach(Local e in arrayLocal) {
                    if (checkLine.Contains(e.ToString())) {
                        local = e.ToString();
                        break;
                    } else if (checkLine.Contains("help")) {
                        Program.Help();
                        local = String.Empty;
                        break;
                    }
                }
            } while(String.IsNullOrEmpty(local));

            Console.Write($"[SUCCESS] The region '{local}' was found! \n");
            Report report = new Report(local);
            report.Print();

            Console.Write($"\r\nPress any key to exit...\n\r");
            Console.ReadKey(true);
        }

        private static void Help() {
            Console.Clear();
            Console.WriteLine("\r\n[HELP]---------------------------------------------------\r");
            Console.WriteLine("The following regions are available:");

            StringBuilder sb = new StringBuilder();
            foreach (Local suit in (Local[]) Enum.GetValues(typeof(Local))) {
                sb.Append(String.Format("{0}, ", suit));
            }
            sb.Append("\r\n");

            Console.Write(sb);
            Console.WriteLine("---------------------------------------------------------\r\n");
        }
    }
}
