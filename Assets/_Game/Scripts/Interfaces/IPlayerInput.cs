
using System;

namespace _Game.Scripts.Interfaces
{
    public interface IPlayerInput
    {
        public event Action<IClickable> Clicked;
    }
}