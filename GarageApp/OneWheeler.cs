namespace GarageApp
{
    class OneWheeler : Vehicle
    {
        public OneWheeler() : base()
        {
        }

        public OneWheeler(int id, string name, string color, string regnr, int weight) : base(id, name, color, regnr, weight)
        {
            this.wheelCount = 1;
        }
    }
}
