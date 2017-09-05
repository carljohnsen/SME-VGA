using System;
using System.Threading.Tasks;
using SME;

namespace VGA
{
    [InitializedBus]
    public interface CounterX : IBus
    {
        short val { get; set; }
    }

    [InitializedBus]
    public interface CounterY : IBus
    {
        short val { get; set; }
    }

    [InitializedBus, TopLevelOutputBus]
    public interface SyncBus : IBus
    {
        bool HSync { get; set; }
        bool VSync { get; set; }
    }

    [InitializedBus, TopLevelOutputBus]
    public interface RGB : IBus
    {
        bool R { get; set; }
        bool G { get; set; }
        bool B { get; set; }
    }

    [ClockedProcess]
    public class CountX : SimpleProcess
    {
        [OutputBus]
        CounterX output;

        short current = 0;
        short line = 800; // spec

        protected override void OnTick()
        {
            output.val = current;
            current = (short)((current + 1) % line);
        }
    }

    [ClockedProcess]
    public class CountY : SimpleProcess
    {
        [InputBus]
        CounterX cx;

        [OutputBus]
        CounterY output;

        short current = 0;
        short frame = 525; // spec

        protected override void OnTick()
        {
            if (cx.val == 0)
                current = (short)((current + 1) % frame);
            output.val = current;
        }
    }

    public class Sync : SimpleProcess
    {
        [InputBus]
        CounterX cx;
        [InputBus]
        CounterY cy;

        [OutputBus]
        SyncBus output;

        // spec
        short visH = 640;
        short frontH = 16;
        short syncH = 96;
        short backH = 48;

        short visV = 480;
        short frontV = 10;
        short syncV = 2;
        short backV = 33;

        protected override void OnTick()
        {
            /*output.HSync = cx.val < visH + frontH || cx.val > visH + frontH + syncH;
            output.VSync = cy.val < visV + frontV || cy.val > visV + frontV + syncV;*/
            output.HSync = cx.val < frontH || cx.val > frontH + syncH;
            output.VSync = cy.val < frontV || cy.val > frontV + syncV;
        }
    }

    public class Color : SimpleProcess
    {
        [InputBus]
        CounterX cx;
        [InputBus]
        CounterY cy;

        [OutputBus]
        RGB output;

        short visH = 640;
        short frontH = 16;
        short syncH = 96;
        short backH = 48;

        short visV = 480;
        short frontV = 10;
        short syncV = 2;
        short backV = 33;

        protected override void OnTick()
        {
            if (cx.val > frontH + syncH + backH && cy.val > frontV + syncV + backV)
            {
                output.R = true;
                output.G = true;
                output.B = true;
            }
            else
            {
                output.R = false;
                output.G = false;
                output.B = false;
            }
        }
    }

    public class Tester : SimulationProcess
    {
        public override async Task Run()
        {
            await ClockAsync();
            return;
        }
    }
}
