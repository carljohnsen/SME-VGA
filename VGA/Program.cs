using SME;

namespace VGA
{
    class Program
    {
        static void Main(string[] args)
        {
            new Simulation()
                //.BuildCSVFile()
                //.BuildVHDL()
                .Run(typeof(Program).Assembly);
        }
    }
}
