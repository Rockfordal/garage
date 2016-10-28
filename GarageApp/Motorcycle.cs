namespace GarageApp
{
    class Motorcycle : Vehicle
    {
        public Motorcycle() : base()
        {
            this.wheelCount = 2;
        }

        public Motorcycle(string name, string color, string regnr, int weight) : base(name, color, regnr, weight)
        {
            this.wheelCount = 2;
        }
    }
}
