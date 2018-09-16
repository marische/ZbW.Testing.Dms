using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Views;

namespace ZbW.Testing.Dms.Client
{
    interface ILoginView
    {
        LoginView OnCanLogin();

        LoginView OnCmdAbbrechen();

        LoginView OnCmdLogin();
    }
}
