using System;
using System.Collections.Generic;
using System.Linq;
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
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new frmMain());
      return exitCode;
    }
  }
}
