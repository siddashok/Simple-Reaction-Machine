using SimpleReactionMachine;
using System;

public class EnhancedReactionController : IController
{

    private IGui this_gui;

    private IRandom this_rng;

    private int s = 0; //state

    private float t = 0;  //time

    private float dt = 0; //delay time

    private float et = 0; //end time

    private float at;  //averae time

    private int i; //index value

    private float[] scores = null;


    public EnhancedReactionController()
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
        this.scores = new float[3];
        this.i = 0;
        this.at = 0;
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
            this.scores[this.i] = this.et;
            this.i++;
            this.t = 0;
            this.s = 4;
        }
        else if ((this.s == 4))
        {
            if ((this.i < 3))
            {
                this.dt = this.this_rng.GetRandom(100, 250);
                this.s = 2;
            }
            else
            {
                this.t = 0;
                this.at = ((float)(((this.scores[0]
                            + (this.scores[1] + this.scores[2]))
                            / 3)));
                this.s = 5;
            }

        }
        else if ((this.s == 5))
        {
            this.Init();
        }

    }
    public void Tick()
    {
        if ((this.s == 1))
        {
            this.t++;
            if ((this.t >= 1000))
            {
                this.Init();
            }

        }
        else if ((this.s == 2))
        {
            this.t++;
            this.this_gui.SetDisplay("Wait...");
            if ((this.t >= this.dt))
            {
                this.s = 3;
                this.t = 0;
                String string_time = String.Format("{0:0.00}", (this.t / 100));
                this.this_gui.SetDisplay(string_time);
            }

        }
        else if ((this.s == 3))
        {
            this.t++;
            String string_time = String.Format("{0:0.00}", (this.t / 100));
            this.this_gui.SetDisplay(string_time);
            this.et = this.t;
            if ((this.t >= 200))
            {
                this.et = 200;
                this.scores[this.i] = this.t;
                this.i++;
                this.t = 0;
                this.s = 4;
            }

        }
        else if ((this.s == 4))
        {
            this.t++;
            this.this_gui.SetDisplay("Wait...");
            if ((this.t >= 300))
            {
                if ((this.i < 3))
                {
                    this.dt = this.this_rng.GetRandom(100, 250);
                    this.s = 2;
                    this.t = 0;
                }
                else
                {
                    this.t = 0;
                    this.at = ((float)(((this.scores[0]
                                + (this.scores[1] + this.scores[2]))
                                / 3)));
                    String string_time = String.Format("AverageScore= {0:0.00}", (this.at / 100));
                    this.this_gui.SetDisplay(string_time);
                    this.s = 5;
                }

            }

        }
        else if ((this.s == 5))
        {
            this.t++;
            if ((this.t >= 500))
            {
                this.Init();
            }

        }

    }
}