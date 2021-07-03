using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace sccoresystems
{


    public struct sccsincrementteststruct0
    {
        unsafe int* incrementercounter;
        unsafe int* incrementercountermax;
        unsafe int* incrementercounterreset;
    }



    public class sccsincrementtest0
    {
        int somereset = 0;
        static int collectgcmax = 10;
        unsafe int collectgcreset = 0;
        static int collectgccounter = 0;

        unsafe int* incrementercounter;
        unsafe int* incrementercountermax;
        unsafe int* incrementercounterreset;
        //int* collectgcpointer;

        unsafe void setinitvarsunsafetest()
        {
            //incrementercountermax = &collectgcmax;
            fixed (int* test = &collectgcmax)
            {
                incrementercountermax = test;
                //int* tester = &someres;
            }

            fixed (int* test = &collectgcreset)
            {
                incrementercounterreset = test;
                //int* tester = &someres;
            }
        }

        unsafe void incrementgccollecttest()
        {
            //is this working like a stack alloc of c# 7.3++ ? as in, is it better than not using pointers? i am still new to using the visual studio debugger so i 
            //have yet to know. but i had a 0.1 memory leak in the diagnostics tools window. testing gccollect.
            fixed (int* incrementercountermax = &collectgcmax)
            {
                fixed (int* incrementercounter = &collectgccounter)
                {
                    fixed (int* incrementercounterreset = &collectgcreset)
                    {
                        if (collectgccounter >= collectgcmax)
                        {
                            GC.Collect();
                            //Program.MessageBox((IntPtr)0, "GC.Collect is called.", "sccs error", 0);
                            collectgccounter = collectgcreset;
                        }
                        collectgccounter++;
                    }
                }
            }

            /*fixed (int* incrementercounterreset = &collectgcreset)
            {
                fixed (int* incrementercounter = &collectgccounter)
                {
                    fixed (int* incrementercountermax = &collectgcmax)
                    {
                        /*if (collectgccounter >= incrementercounterreset)
                        {
                            Program.MessageBox((IntPtr)0, "GC.Collect is called.", "sccs error", 0);
                            GC.Collect();
                            collectgccounter = incrementercounterreset;
                        }
                        collectgccounter++;
                        incrementercounter++
                        //GC.Collect();
                        //incrementercounter++;
                    }
                }
            }*/

            /*unsafe
            {
                int* numbers = stackalloc int[] { 0, 1, 2 };
                int* p1 = &numbers[0];
                int* p2 = p1;
                Console.WriteLine($"Before operation: p1 - {(long)p1}, p2 - {(long)p2}");
                Console.WriteLine($"Postfix increment of p1: {(long)(p1++)}");
                Console.WriteLine($"Prefix increment of p2: {(long)(++p2)}");
                Console.WriteLine($"After operation: p1 - {(long)p1}, p2 - {(long)p2}");
            }*/

            /*
            fixed (int* collectgcpointer = &collectgc)
            {
                int x = 0;
                int* reseter = &x;
                if (collectgc >= collectgcmax)
                {
                    GC.Collect();
                    collectgc = reset;
                }
                incrementervar++;
            }*/
        }
    }
}
