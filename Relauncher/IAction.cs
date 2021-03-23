using System;

namespace Relauncher
{
    interface IAction
    {
        int LoadParm(String parm, string[] args);
        int Execute();
    }
}

