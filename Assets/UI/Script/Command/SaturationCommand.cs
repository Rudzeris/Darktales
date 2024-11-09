﻿namespace Assets.UI.Script.Command
{
    public class SaturationCommand : IUICommand
    {
        public int Saturation { get;private set; }
        public SaturationCommand (int saturation)
        {
            Saturation = saturation;
        }
    }
}
