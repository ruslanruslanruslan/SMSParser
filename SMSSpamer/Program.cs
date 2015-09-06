using System;
using System.Threading;
using System.Windows.Forms;

namespace SMSSpamer
{
  static class Program
  {
    private static int exitCode = 0; 

    public static void Exit(int code)
    {
      exitCode = code;
      Application.Exit();
    }

    [STAThread]
    static int Main()
    {
      using (var mutex = new Mutex(false, @"Global\{FAB04460-1078-49DF-955F-511531648AD9}"))
      {
        if (mutex.WaitOne(0, false))
        {
          GC.Collect();
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Application.Run(new frmMain());
        }
        return exitCode;
      }
    }
  }
}
