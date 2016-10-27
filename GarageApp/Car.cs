namespace GarageApp
{
    class Car : Vehicle
    {
        //public int id { get; set; }
        //public string name { get; set; }
        //public string color { get; set; }
        //public int weight { get; set; }

        public Car(int id, string name, string color, string regnr, int weight) : base(id, name, color, regnr, weight)
        {
        }

        //public Car(int id, string name, string color, int weight)
        //{
        //    this.id = id;
        //    this.name = name;
        //    this.color = color;
        //    this.weight = weight;
        //}

    }
}
