namespace GarageApp
{
    class OneWheeler : Vehicle
    {
        public OneWheeler() : base()
        {
        }

        public OneWheeler(string name, string color, string regnr, int weight) : base(name, color, regnr, weight)
        {
            this.wheelCount = 1;
        }
    }
}
