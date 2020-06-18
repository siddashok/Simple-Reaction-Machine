using System;

namespace SimpleReactionMachine
{
    class Tester
    {
        private static IController controller;
        private static IGui gui;
        private static string displayText;
        private static int randomNumber;
        private static int passed = 0;

        static void Main(string[] args)
        {
            // run simple test
            SimpleTest();
            Console.WriteLine("\n=====================================\nSummary: {0} tests passed out of 49", passed);
            Console.ReadKey();
        }

        private static void SimpleTest()
        {
             //controller = new SimpleReactionController();
            controller = new EnhancedReactionController();
            gui = new DummyGui();

            //Connect them to each other
            gui.Connect(controller);
            controller.Connect(gui, new RndGenerator());

            //Reset the components()
            gui.Init();

            //Test the SimpleReactionController
            //IDLE
            DoReset('A', controller, "Insert coin");
            DoGoStop('B', controller, "Insert coin");
            DoTicks('C', controller, 1, "Insert coin");

            //coinInserted
            DoInsertCoin('D', controller, "Press GO!");

            //READY
            DoTicks('E', controller, 1, "Press GO!");
            DoInsertCoin('F', controller, "Press GO!");

            //goStop
            randomNumber = 117;
            DoGoStop('G', controller, "Wait...");

            //WAIT tick(s)
            DoTicks('H', controller, randomNumber - 1, "0.00");

            //RUN tick(s)
            DoTicks('I', controller, 1, "0.01");
            DoTicks('J', controller, 1, "0.02");
            DoTicks('K', controller, 11, "0.13");
            DoTicks('L', controller, 111, "1.24");

            //goStop
            DoGoStop('M', controller, "1.24");

            //STOP tick(s)
            DoTicks('N', controller, 299, "Wait...");
            // *********************************new game?
            //tick
            DoTicks('O', controller, 1, "Wait...");

            //IDLE coinInserted
            DoInsertCoin('P', controller, "Wait...");

            //READY goStop
            randomNumber = 167;
            DoGoStop('Q', controller, "Insert coin");
            // *********************************cheating?
            //WAIT tick(s) goStop
            DoTicks('R', controller, randomNumber - 1, "Insert coin");
            DoGoStop('S', controller, "Insert coin");
            // *********************************new game?
            //IDLE init
            gui.Init();
            DoReset('T', controller, "Insert coin");

            //IDLE -> READY init
            randomNumber = 123;
            DoInsertCoin('U', controller, "Press GO!");
            // *********************************new game?	
            gui.Init();
            DoReset('V', controller, "Insert coin");

            //IDLE -> READY ->WAIT init
            randomNumber = 123;
            DoInsertCoin('W', controller, "Press GO!");
            DoGoStop('X', controller, "Wait...");
            // *********************************new game?
            gui.Init();
            DoReset('Y', controller, "Insert coin");

            //IDLE -> READY -> WAIT -> RUN init
            randomNumber = 137;
            DoInsertCoin('Z', controller, "Press GO!");
            DoGoStop('a', controller, "Wait...");
            DoTicks('b', controller, randomNumber + 98, "0.98");
            // *********************************new game?
            gui.Init();
            DoReset('c', controller, "Insert coin");

            //IDLE -> READY -> WAIT -> RUN -> STOP init
            randomNumber = 119;
            DoInsertCoin('d', controller, "Press GO!");
            DoGoStop('e', controller, "Wait...");
            DoTicks('f', controller, randomNumber + 135, "1.35");
            DoGoStop('g', controller, "1.35");
            // *********************************new game?
            gui.Init();
            DoReset('h', controller, "Insert coin");

            //IDLE -> READY -> WAIT -> RUN (timeout) -> STOP
            randomNumber = 120;
            DoInsertCoin('i', controller, "Press GO!");
            DoGoStop('j', controller, "Wait...");
            DoTicks('k', controller, randomNumber + 199, "1.99");

            DoTicks('l', controller, 50, "Wait...");
            DoReset('m', controller, "Insert coin");
            DoInsertCoin('m', controller, "Press GO!");
            DoInsertCoin('0', controller, "Press GO!");
            DoGoStop('p', controller, "Wait...");
            DoTicks('q', controller, randomNumber + 200, "2.00");
            DoReset('r', controller, "Insert coin");
            DoInsertCoin('s', controller, "Press GO!");
            DoGoStop('t', controller, "Wait...");
            DoTicks('u', controller, randomNumber + 299, "Wait...");
            DoTicks('v', controller, randomNumber + 300, "0.99");
            DoTicks('w', controller, randomNumber + 399, "Wait...");
            DoTicks('w', controller, randomNumber + 400, "2.00");   //averageScore =2.00
        }

        private static void DoReset(char ch, IController controller, string msg)
        {
            try
            {
                controller.Init();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoGoStop(char ch, IController controller, string msg)
        {
            try
            {
                controller.GoStopPressed();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoInsertCoin(char ch, IController controller, string msg)
        {
            try
            {
                controller.CoinInserted();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void DoTicks(char ch, IController controller, int n, string msg)
        {
            try
            {
                for (int t = 0; t < n; t++) controller.Tick();
                GetMessage(ch, msg);
            }
            catch (Exception exception)
            {
                Console.WriteLine("test {0}: failed with exception {1})", ch, msg, exception.Message);
            }
        }

        private static void GetMessage(char ch, string msg)
        {
            if (msg.ToLower() == displayText.ToLower())
            {
                Console.WriteLine("test {0}: passed successfully", ch);
                passed++;
            }
            else
                Console.WriteLine("test {0}: failed with message ( expected {1} | received {2})", ch, msg, displayText);
        }

        private class DummyGui : IGui
        {

            private IController controller;

            public void Connect(IController controller)
            {
                this.controller = controller;
            }

            public void Init()
            {
                displayText = "?reset?";
            }

            public void SetDisplay(string msg)
            {
                displayText = msg;
            }
        }

        private class RndGenerator : IRandom
        {
            public int GetRandom(int from, int to)
            {
                return randomNumber;
            }
        }

    }

}


     
 
