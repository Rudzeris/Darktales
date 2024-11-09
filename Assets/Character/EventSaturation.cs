using System;

namespace Assets.Character
{
    public class EventSaturation : EventArgs
    {
        public int Saturation { get; private set; }
        public EventSaturation(int saturation)
        {
            Saturation = saturation;
        }
    }
}
