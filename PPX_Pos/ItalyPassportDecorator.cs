using System;

namespace PPX_Pos
{
    public class ItalyPassportDecorator : IPOS
    {
        private readonly IPOS _pos;

        public ItalyPassportDecorator(IPOS pos)
        {
            _pos = pos;
        }

        public string DisplayWelcomeScreen()
        {
            return _pos.DisplayWelcomeScreen() + " Italy customer";
        }

        public Guid CreateCustomerOrder()
        {
            return _pos.CreateCustomerOrder();
        }
    }
}