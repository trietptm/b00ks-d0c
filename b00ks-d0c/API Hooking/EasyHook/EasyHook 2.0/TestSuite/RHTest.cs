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
using System.Text;
using System.Diagnostics;
using System.Security.Principal;
using EasyHook;

namespace Examples
{
    public class Entry : IEntryPoint
    {
        public Entry(RemoteHooking.IContext InContext)
        {
        }

        public void Run(RemoteHooking.IContext InContext)
        {
        }
    }

    public class RHTest
    {
        [Serializable]
        public class ProcessInfo
        {
            public String FileName;
            public Int32 Id;
            public Boolean Is64Bit;
            public String Identity;
        }

        public static ProcessInfo[] Enum()
        {
            List<ProcessInfo> Result = new List<ProcessInfo>();
            Process[] ProcList = Process.GetProcesses();

            for (int i = 0; i < ProcList.Length; i++)
            {
                Process Proc = ProcList[i];

                try
                {
                    ProcessInfo Info = new ProcessInfo();

                    Info.FileName = Proc.MainModule.FileName;
                    Info.Id = Proc.Id;
                    Info.Is64Bit = RemoteHooking.IsX64Process(Proc.Id);
                    Info.Identity = RemoteHooking.GetProcessIdentity(Proc.Id).Owner.ToString();

                    Result.Add(Info);
                }
                catch
                {
                }
            }

            return Result.ToArray();
        }

        public static void Run()
        {
            Config.Install(typeof(Config).Assembly.Location);
            Config.Register("", "TestSuite.exe");

            ProcessInfo[] Result = (ProcessInfo[])RemoteHooking.ExecuteAsService<RHTest>("Enum");
            /*
            try
            {
                RemoteHooking.Inject(
                    5416,
                    //2892,
                    //RemoteHooking.GetCurrentProcessId(),
                    InjectionOptions.None, "TestSuite.exe", "TestSuite.exe");
            }
            catch
            {
            }*/
        }
    }
}
