using System;

namespace LittleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!Veryfier.Verify()) throw new Exception("Ur program has been expired");
                Console.WriteLine("I'm working!");
                string text = Console.ReadLine();
                Console.WriteLine("ur text:{0}", text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
            
            
        }
    }
}
