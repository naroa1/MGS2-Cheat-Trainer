﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace MGS2_MC
{
    internal static class MGS2Injector
    {
        #region Native Methods
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        /*[DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);*/ //this may be useful for slapping the GUI on top of MGS2
        #endregion

        public class TrainerMenuArgs : EventArgs
        {
            public bool ActivateMenu;
        }

        static readonly ControllerHook ControllerHook = new ControllerHook();

        internal static void EnableInjector()
        {
            Task.Run(StartControllerHook);
        }

        private static void StartControllerHook()
        {
            ControllerHook.TrainerMenu += OpenTrainerMenuEventHandler;
            ControllerHook.Start(CancellationToken.None); //TODO: maybe do a proper cancellation token in the future?
        }

        private static void OpenTrainerMenuEventHandler(object o, EventArgs e)
        {
            
            TrainerMenuArgs trainerMenuArgs = (TrainerMenuArgs)e;

            if (trainerMenuArgs.ActivateMenu)
            {
                try
                {
                    SuspendMgs2();
                    //bool gotMgs2Window = GetWindowRect(Program.MGS2Process.MainWindowHandle, out Rectangle mgs2WindowRectangle);
                    //TODO: open a GUI over MGS2 that lets the user do their desired modifications... for now, just enable navigating the GUI w/ buttons
                    GUI.CanNavigateWithController = true;
                    GUI.ShowGui();
                }
                catch(Exception ex)
                {
                    //TODO: something
                }
            }
            else
            {
                try
                {
                    GUI.CanNavigateWithController = false;
                    GUI.HideGui();
                    ResumeMgs2();
                }
                catch(Exception ex)
                {
                    //TODO: something
                }
            }
        }

        private static void SuspendMgs2()
        {
            //https://stackoverflow.com/a/71457 for how to do this
            foreach (ProcessThread mgs2Thread in Program.MGS2Process?.Threads)
            {
                IntPtr mgs2OpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)mgs2Thread.Id);

                if (mgs2OpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(mgs2OpenThread);
                CloseHandle(mgs2OpenThread);
            }
        }

        private static void ResumeMgs2()
        {
            //https://stackoverflow.com/a/71457 for how to do this
            foreach (ProcessThread mgs2Thread in Program.MGS2Process?.Threads)
            {
                IntPtr mgs2OpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)mgs2Thread.Id);

                if (mgs2OpenThread == IntPtr.Zero)
                {
                    continue;
                }

                int suspendCount;
                do
                {
                    suspendCount = ResumeThread(mgs2OpenThread);
                } while (suspendCount > 0);

                CloseHandle(mgs2OpenThread);
            }
        }
    }
}
