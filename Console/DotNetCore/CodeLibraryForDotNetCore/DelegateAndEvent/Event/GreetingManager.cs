using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeLibraryForDotNetCore.DelegateAndEvent.Event
{
    public class GreetingManager
    {
        public delegate void GreetingDelegate(string name);
        private GreetingDelegate MakeGreet { get; set; }

        public void GreetPeople(string name)
        {
            if (MakeGreet != null)
            {
                MakeGreet(name);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddMakeGreet(GreetingDelegate value)
        {
            this.MakeGreet = (GreetingDelegate)System.Delegate.Combine(this.MakeGreet, value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveMakeGreet(GreetingDelegate value)
        {
            this.MakeGreet = (GreetingDelegate)System.Delegate.Remove(this.MakeGreet, value);
        }
    }
}
