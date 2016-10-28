namespace GarageApp
{
    class Car : Vehicle
    {
        public Car(string name, string color, string regnr, int weight) : base(name, color, regnr, weight)
        {
            this.wheelCount = 4;
        }

        public Car() : base()
        {
        }

    }
}
