namespace GarageApp
{
    class Boat : Vehicle
    {
        public Boat(string name, string color, string regnr, int weight) : base(name, color, regnr, weight)
        {
            this.wheelCount = 0;
        }
    }
}
