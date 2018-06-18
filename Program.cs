using ApplicationServices;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            EventManager TelephoneEventManager = EventManager.Instance;
            TelephoneActivities telephoneActivities = new TelephoneActivities();
            Console.WriteLine("Hello World!");
            TelephoneEventManager.RegisterEvent("TelephoneUiEvent", telephoneActivities);
            TelephoneEventManager.RegisterEvent("TelephoneDeviceEvent", telephoneActivities);

            TelephoneEventManager.SubscribeEvent("TelephoneUiEvent", "ViewCommandHandler", telephoneActivities);
            TelephoneEventManager.SubscribeEvent("TelephoneDeviceEvent", "DeviceCommandHandler", telephoneActivities);

            telephoneActivities.ActionBellRings();
            Console.ReadKey();
        }


    }

    public class TelephoneActivities
    {
        // Public members
        // Events to communicate from state machine to managers - wiring will be done via via event manager
        public event EventHandler<StateMachineEventArgs> TelephoneUiEvent;
        public event EventHandler<StateMachineEventArgs> TelephoneDeviceEvent;


        public void ActionBellRings()
        {
            // Error handling - do not do it  here, instead in device or UI!
            // Raising an event normally does not fail!
            RaiseDeviceEvent("Bell", "Rings");
        }

        public void RaiseTelephoneUiEvent(string command)
        {
            var telArgs = new StateMachineEventArgs(command, "UI command", StateMachineEventType.Command, "State machine action", "View Manager");
            TelephoneUiEvent(this, telArgs);
        }

        public void RaiseDeviceEvent(string target, string command)
        {
            var telArgs = new StateMachineEventArgs(command, "Device command", StateMachineEventType.Command, "State machine action", target);
            TelephoneDeviceEvent(this, telArgs);
        }

        public void ViewCommandHandler(object sender, StateMachineEventArgs args)
        {
        }

        public void DeviceCommandHandler(object sender, StateMachineEventArgs args)
        {
        }
    }
}

//http://www.marco.panizza.name/dispenseTM/slides/exerc/eventNotifier/eventNotifier.html