using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Staff_time.Helpers
{
    public static class DumpMaker
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_EXCEPTION_INFORMATION
        {
            public uint ThreadId;
            public IntPtr ExceptionPointers;
            public int ClientPointers;
        }

        private static class MINIDUMP_TYPE
        {
            public const int MiniDumpNormal = 0x00000000;
            public const int MiniDumpWithCodeSegs = 0x00002000;
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            CreateMiniDump();
            System.Windows.Forms.MessageBox.Show(ex.Message + ". " + ex.InnerException.Message + " - " + ex.Source + "Будет создан мини-дамп", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("Dbghelp.dll")]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint ProcessId, IntPtr hFile, int DumpType, ref MINIDUMP_EXCEPTION_INFORMATION ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

        public static void CreateMiniDump()
        {
            using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
            {
                string FileName = string.Format(@"MINIDUMP_{0}_{1}_{2}.dmp", System.Environment.MachineName, DateTime.Today.ToShortDateString(), DateTime.Now.Ticks);

                MINIDUMP_EXCEPTION_INFORMATION Mdinfo = new MINIDUMP_EXCEPTION_INFORMATION();

                Mdinfo.ThreadId = GetCurrentThreadId();
                Mdinfo.ExceptionPointers = Marshal.GetExceptionPointers();
                Mdinfo.ClientPointers = 1;

                MessageBox.Show($"Произошла ошибка! Будет создан мини-дамп: {FileName}", "Ошибка");
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    {
                        MiniDumpWriteDump(process.Handle, (uint)process.Id, fs.SafeFileHandle.DangerousGetHandle(), MINIDUMP_TYPE.MiniDumpNormal,
               ref Mdinfo,
               IntPtr.Zero,
               IntPtr.Zero);
                    }
                }
            }
        }
    }
}
