using System;
using System.Threading.Tasks;
using SME;
using System.Windows.Forms;

namespace VGA
{
    [InitializedBus]
    public interface Address : IBus
    {
        uint addr { get; set; }
    }

    [InitializedBus]
    public interface Data : IBus
    {
        uint data { get; set; }
    }

    [InitializedBus]
    public interface FrameBuffer : IBus
    {
        bool flg { get; set; }
    }

    
    public class VGAComponent : Process
    {
        [InputBus] Address address;
        [InputBus] Data data;
        [InputBus] FrameBuffer buffer;

        const int width = 640;
        const int height = 480;
        const int size = width * height;

        uint[] Buffer1 = new uint[size];
        uint[] Buffer2 = new uint[size];
        int countX = 0;
        int countY = 0;

        const int maxX = 800;
        const int maxY = 525;

        public async override Task Run()
        {
            await ClockAsync();
            // Open window
            Console.WriteLine("1");
            Monitor monitor = new Monitor(width + 40, height + 60);
            Task.Run(() => Application.Run(monitor));
            while (!monitor.IsHandleCreated) { }
            monitor.Invoke(new Action(monitor.aoeu));

            Console.WriteLine("2");
            while (true)
            {
                Console.WriteLine("3");
                await ClockAsync();
                Console.WriteLine("4");
                countX = (countX + 1) % maxX;
                if (countX == 0)
                    countY = (countY + 1) % maxY;

                if (countX < width && countY < height)
                {
                    // Send color
                }
                else
                {
                    // Send black
                }

                monitor.Invoke(new Action(monitor.aoeu));
                Console.WriteLine("a");
            }

        }
    }

    public class Tester : SimulationProcess
    {
        [OutputBus] Address addr;
        [OutputBus] Data data;
        [OutputBus] FrameBuffer buff;

        public override async Task Run()
        {
            while (true)
            {
                await ClockAsync();
                addr.addr = 0;
                data.data = 0;
                buff.flg = false;
            }
            return;
        }
    }
}
