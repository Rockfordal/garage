namespace GarageApp
{
    class Car : Vehicle
    {
        public Car(int id, string name, string color, string regnr, int weight) : base(id, name, color, regnr, weight)
        {
            this.wheelCount = 4;
        }

        public Car() : base()
        {
        }

    }
}
