using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSSpamer
{
  static class Program
  {
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    /// 

    private static int exitCode = 0; 

    public static void Exit(int code)
    {
      exitCode = code;
      Application.Exit();
    }

    [STAThread]
    static int Main()
    {
      const string appGuid = "{FAB04460-1078-49DF-955F-511531648AD9}";
      using (Mutex mutex = new Mutex(false, @"Global\" + appGuid))
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
