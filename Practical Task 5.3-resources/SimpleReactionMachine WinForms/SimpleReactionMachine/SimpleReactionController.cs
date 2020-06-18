using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReactionMachine
{
    public class SimpleReactionController : IController
    {

        private IGui this_gui;

        private IRandom this_rng;

        private int s = 0;

        private float t = 0;

        private float dt = 0;

        private float et = 0;

        private int c = 0;

        public SimpleReactionController()
        {

        }

        public void Connect(IGui gui, IRandom rng)
        {
            this.this_gui = gui;
            this.this_rng = rng;
        }

        public void Init()
        {
            this.s = 0;
            this.t = 0;
            this.dt = 0;
            this.et = 0;
            this.this_gui.SetDisplay("Insert coin");
        }

        public void CoinInserted()
        {
            if ((this.s == 0))
            {
                this.s = 1;
                this.this_gui.SetDisplay("Press GO!");
            }

        }
        public void GoStopPressed()
        {
            if ((this.s == 1))
            {
                this.dt = this.this_rng.GetRandom(100, 250);
                this.this_gui.SetDisplay("Wait...");
                this.s = 2;
            }
            else if ((this.s == 2))
            {
                this.Init();
            }
            else if ((this.s == 3))
            {
                this.et = this.t;
                this.t = 0;
                this.s = 4;
            }
            else if ((this.s == 4))
            {
                this.Init();
            }

        }
        public void Tick()
        {
            if ((this.s == 2))
            {
                this.t++;
                if ((this.t >= this.dt))
                {
                    this.s = 3;
                    this.t = 0;
                    String string_time = String.Format("{0:0.00}", (this.et / 100));
                    this.this_gui.SetDisplay(string_time);
                }

            }
            else if ((this.s == 3))
            {
                this.t++;
                String string_time = (this.t / 100).ToString();
                this.this_gui.SetDisplay(string_time);
                if ((this.t >= 200))
                {
                    this.et = 200;
                    this.t = 0;
                    this.s = 4;
                }

            }
            else if ((this.s == 4))
            {
                this.t++;
                c = c + 1;
                string string_time = string.Format("{0:0.00}", (this.et / 100));
                // string string_time = (this.endTime / 100).ToString();
                this.this_gui.SetDisplay(string_time);
                if ((this.t >= 300))
                {
                    this.Init();
                }

            }

        }

    }
}

