using System;
using System.Text;

namespace Relauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Main main = new Main();

                main.Start(args);
                
                main = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("### EXCEPTION Type = {0};", ex.GetType().Name);
                Console.WriteLine("### EXCEPTION Message = {0};", ex.Message);
            }
            finally {
                Console.WriteLine("Press Any Key to Continue.");
                Console.ReadKey();
            }
        }
    }
}
