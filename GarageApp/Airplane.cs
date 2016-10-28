namespace GarageApp
{
    class Airplane : Vehicle
    {
        public Airplane(string name, string color, string regnr, int weight) : base(name, color, regnr, weight)
        {
            //this.wheelCount = 4;
        }

        public Airplane() : base()
        {
        }

    }
}
