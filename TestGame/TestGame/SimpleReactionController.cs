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

        private int state = 0;

        private float time = 0;

        private float delayTime = 0;

        private float endTime = 0;

        private int counter = 0;

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
            this.state = 0;
            this.time = 0;
            this.delayTime = 0;
            this.endTime = 0;
            this.this_gui.SetDisplay("Insert coin");
        }

        public void CoinInserted()
        {
            if ((this.state == 0))
            {
                this.state = 1;
                this.this_gui.SetDisplay("Press GO!");
            }

        }
        public void GoStopPressed()
        {
            if ((this.state == 1))
            {
                this.delayTime = this.this_rng.GetRandom(100, 250);
                this.this_gui.SetDisplay("Wait...");
                this.state = 2;
            }
            else if ((this.state == 2))
            {
                this.Init();
            }
            else if ((this.state == 3))
            {
                this.endTime = this.time;
                this.time = 0;
                this.state = 4;
            }
            else if ((this.state == 4))
            {
                this.Init();
            }

        }
        public void Tick()
        {
            if ((this.state == 2))
            {
                this.time++;
                if ((this.time >= this.delayTime))
                {
                    this.state = 3;
                    this.time = 0;
                    String string_time = String.Format("{0:0.00}", (this.endTime / 100));
                    this.this_gui.SetDisplay(string_time);
                }

            }
            else if ((this.state == 3))
            {
                this.time++;
                String string_time = (this.time / 100).ToString();
                this.this_gui.SetDisplay(string_time);
                if ((this.time >= 200))
                {
                    this.endTime = 200;
                    this.time = 0;
                    this.state = 4;
                }

            }
            else if ((this.state == 4))
            {
                this.time++;
                counter = counter + 1;
                string string_time = string.Format("{0:0.00}", (this.endTime / 100));
                // string string_time = (this.endTime / 100).ToString();
                this.this_gui.SetDisplay(string_time);
                if ((this.time >= 300))
                {
                    this.Init();
                }

            }

        }

    }
}

//using SimpleReactionMachine;
//using System;

//public class EnhancedReactionController : IController
//{

//    private IGui this_gui;

//    private IRandom this_rng;

//    private int state = 0;

//    private float time = 0;

//    private float delayTime = 0;

//    private float endTime = 0;

//    private float[] scores = null;

//    private int index;

//    private float averageTime;

//    public EnhancedReactionController()
//    {

//    }

//    public void Connect(IGui gui, IRandom rng)
//    {
//        this.this_gui = gui;
//        this.this_rng = rng;
//    }

//    public void Init()
//    {
//        this.state = 0;
//        this.time = 0;
//        this.delayTime = 0;
//        this.endTime = 0;
//        this.scores = new float[3];
//        this.index = 0;
//        this.averageTime = 0;
//        this.this_gui.SetDisplay("Insert coin");
//    }


//    public void CoinInserted()
//    {
//        if ((this.state == 0))
//        {
//            this.state = 1;
//            this.this_gui.SetDisplay("Press GO!");
//        }

//    }

//    public void GoStopPressed()
//    {
//        if ((this.state == 1))
//        {
//            this.delayTime = this.this_rng.GetRandom(100, 250);
//            this.this_gui.SetDisplay("Wait...");
//            this.state = 2;
//        }
//        else if ((this.state == 2))
//        {
//            this.Init();
//        }
//        else if ((this.state == 3))
//        {
//            this.scores[this.index] = this.endTime;
//            this.index++;
//            this.time = 0;
//            this.state = 4;
//        }
//        else if ((this.state == 4))
//        {
//            if ((this.index < 3))
//            {
//                this.delayTime = this.this_rng.GetRandom(100, 250);
//                this.state = 2;
//            }
//            else
//            {
//                this.time = 0;
//                this.averageTime = ((float)(((this.scores[0]
//                            + (this.scores[1] + this.scores[2]))
//                            / 3)));
//                this.state = 5;
//            }

//        }
//        else if ((this.state == 5))
//        {
//            this.Init();
//        }

//    }
//    public void Tick()
//    {
//        if ((this.state == 1))
//        {
//            this.time++;
//            if ((this.time >= 1000))
//            {
//                this.Init();
//            }

//        }
//        else if ((this.state == 2))
//        {
//            this.time++;
//            this.this_gui.SetDisplay("Wait...");
//            if ((this.time >= this.delayTime))
//            {
//                this.state = 3;
//                this.time = 0;
//                String string_time = String.Format("{0:0.00}", (this.time / 100));
//                this.this_gui.SetDisplay(string_time);
//            }

//        }
//        else if ((this.state == 3))
//        {
//            this.time++;
//            String string_time = String.Format("{0:0.00}", (this.time / 100));
//            this.this_gui.SetDisplay(string_time);
//            this.endTime = this.time;
//            if ((this.time >= 200))
//            {
//                this.endTime = 200;
//                this.scores[this.index] = this.time;
//                this.index++;
//                this.time = 0;
//                this.state = 4;
//            }

//        }
//        else if ((this.state == 4))
//        {
//            this.time++;
//            this.this_gui.SetDisplay("Wait...");
//            if ((this.time >= 300))
//            {
//                if ((this.index < 3))
//                {
//                    this.delayTime = this.this_rng.GetRandom(100, 250);
//                    this.state = 2;
//                    this.time = 0;
//                }
//                else
//                {
//                    this.time = 0;
//                    this.averageTime = ((float)(((this.scores[0]
//                                + (this.scores[1] + this.scores[2]))
//                                / 3)));
//                    String string_time = String.Format("AverageScore= {0:0.00}", (this.averageTime / 100));
//                    this.this_gui.SetDisplay(string_time);
//                    this.state = 5;
//                }

//            }

//        }
//        else if ((this.state == 5))
//        {
//            this.time++;
//            if ((this.time >= 500))
//            {
//                this.Init();
//            }

//        }

//    }
//}