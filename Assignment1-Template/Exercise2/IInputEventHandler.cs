using System;

namespace Exercise2
{
    interface IInputEventHandler
    {
        event EventHandler<string> InputEntered;
        void StartInputLoop();
    }
}
