namespace GarageApp
{
    class Motorcycle : Vehicle
    {
        public Motorcycle() : base()
        {
            this.wheelCount = 2;
        }

        public Motorcycle(int id, string name, string color, string regnr, int weight) : base(id, name, color, regnr, weight)
        {
            this.wheelCount = 2;
        }
    }
}
