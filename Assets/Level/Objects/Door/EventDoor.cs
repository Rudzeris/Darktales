using System;

namespace Assets.Level.Objects.Door
{
    public class EventDoor : EventArgs
    {
        public Door Door { get; private set; }
        public DoorState PreviousState { get; private set; }
        public DoorState CurrentState { get; private set; }
        public EventDoor(Door door, DoorState previousState, DoorState currentState)
        {
            Door = door;
            PreviousState = previousState;
            CurrentState = currentState;
        }
    }
}
