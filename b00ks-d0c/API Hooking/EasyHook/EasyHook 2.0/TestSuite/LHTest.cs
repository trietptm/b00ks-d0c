/*
Copyright (c) 2008 by Christoph Husse, SecurityRevolutions e.K.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial 
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Visit http://www.codeplex.com/easyhook for more information.
*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using EasyHook;

namespace Examples
{
    public class LHTest
    {
        static DMethodA LHTestHookA = new DMethodA(MethodAHooked);
        static DMethodB LHTestHookB = new DMethodB(MethodBHooked);
        static DMethodA LHTestMethodADelegate;
        static DMethodB LHTestMethodBDelegate;
        static IntPtr LHTestMethodA;
        static IntPtr LHTestMethodB;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate bool DMethodA(Int32 InParam1);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate Int64 DMethodB(Int32 InParam1, Int32 InParam2, String InParam3);

        static bool MethodAHooked(Int32 InParam1)
        {
            LHTestMethodBDelegate.Invoke(0, 1, "Hallo");

            Interlocked.Increment(ref LHTestCounterMAH);

            return true;
        }

        static Int64 MethodBHooked(Int32 InParam1, Int32 InParam2, String InParam3)
        {
            LHTestMethodADelegate.Invoke(0);

            Interlocked.Increment(ref LHTestCounterMBH);

            return 0;
        }

        static bool MethodA(Int32 InParam1)
        {
            Interlocked.Increment(ref LHTestCounterMA);


            return true;
        }


        static Int64 MethodB(Int32 InParam1, Int32 InParam2, String InParam3)
        {
            LHTestMethodADelegate.Invoke(0);

            Interlocked.Increment(ref LHTestCounterMB);

            return 0;
        }

        static void LHTestThread()
        {
            for (int x = 0; x < 10000; x++)
            {
                LHTestMethodBDelegate.Invoke(0, 0, "");
            }

            Interlocked.Increment(ref LHTestThreadCounter);


            if (LHTestThreadCounter == LHTestThreadCount)
                LHTestCompleted.Set();
        }

        static Int32 LHTestThreadCounter = 0;
        static Int32 LHTestCounterMA = 0;
        static Int32 LHTestCounterMB = 0;
        static Int32 LHTestCounterMAH = 0;
        static Int32 LHTestCounterMBH = 0;
        static ManualResetEvent LHTestCompleted = new ManualResetEvent(false);

        const Int32 LHTestThreadCount = 30;


        public static void Run()
        {

            DMethodA MethodADelegate = new DMethodA(MethodA);
            DMethodB MethodBDelegate = new DMethodB(MethodB);

            GC.KeepAlive(MethodADelegate);
            GC.KeepAlive(MethodBDelegate);

            LHTestMethodA = Marshal.GetFunctionPointerForDelegate(MethodADelegate);
            LHTestMethodB = Marshal.GetFunctionPointerForDelegate(MethodBDelegate);

            // install hooks
            LocalHook[] MyHooks = new LocalHook[6];
            Int32 Index = 0;
            
            LocalHook.BeginUpdate(true);
            {
                LocalHook.BeginUpdate(true);
                {
                    LocalHook.BeginUpdate(true);
                    {
                        MyHooks[Index++] = LocalHook.Create(
                           LHTestMethodA,
                           LHTestHookA,
                           Index);

                        MyHooks[Index++] = LocalHook.Create(
                            LHTestMethodB,
                            LHTestHookB,
                            Index);
                    }
                    LocalHook.CancelUpdate();

                    MyHooks[Index++] = LocalHook.Create(
                       LHTestMethodA,
                       LHTestHookA,
                       Index);

                    MyHooks[Index++] = LocalHook.Create(
                        LHTestMethodB,
                        LHTestHookB,
                        Index);
                }
                LocalHook.CancelUpdate();

                MyHooks[Index++] = LocalHook.Create(
                   LHTestMethodA,
                   LHTestHookA,
                   Index);

                MyHooks[Index++] = LocalHook.Create(
                    LHTestMethodB,
                    LHTestHookB,
                    Index);
            }
            LocalHook.EndUpdate();

            // we want to intercept all threads...
            MyHooks[4].ThreadACL.SetExclusiveACL(null);
            MyHooks[5].ThreadACL.SetExclusiveACL(null);

            LHTestMethodADelegate = (DMethodA)Marshal.GetDelegateForFunctionPointer(LHTestMethodA, typeof(DMethodA));
            LHTestMethodBDelegate = (DMethodB)Marshal.GetDelegateForFunctionPointer(LHTestMethodB, typeof(DMethodB));

            /*
             * This is just to make sure that all related objects are referenced.
             * At the beginning there were several objects like delegates that have
             * been collected during execution! The NET-Framework will produce bugchecks
             * in such cases...
             */
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            IntPtr t = Marshal.GetFunctionPointerForDelegate(LHTestHookA);

            Int64 t1 = System.Diagnostics.Stopwatch.GetTimestamp();

            for (int i = 0; i < LHTestThreadCount; i++)
            {
                new Thread(new ThreadStart(LHTestThread)).Start();

            }

            LHTestCompleted.WaitOne();

            t1 = ((System.Diagnostics.Stopwatch.GetTimestamp() - t1) * 1000) / System.Diagnostics.Stopwatch.Frequency;

            // verify results
            if ((LHTestCounterMA != LHTestCounterMAH) || (LHTestCounterMAH != LHTestCounterMB) ||
                    (LHTestCounterMB != LHTestCounterMBH) || (LHTestCounterMB != LHTestThreadCount * 10000))
                throw new Exception("LocalHook test failed.");

            Console.WriteLine("Localhook test passed in {0} ms.", t1);
        }
    }
}
