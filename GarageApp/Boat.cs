namespace GarageApp
{
    class Boat : Vehicle
    {
        public Boat(int id, string name, string color, string regnr, int weight) : base(id, name, color, regnr, weight)
        {
            this.wheelCount = 0;
        }
    }
}
