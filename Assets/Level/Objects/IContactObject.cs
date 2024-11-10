using System;

namespace Assets.Level.Objects
{
    public interface IContactObject
    {
        void Contact(object sender, EventArgs args);
    }
}
