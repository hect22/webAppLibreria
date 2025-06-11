using MongoDB.Bson;

namespace webAppGallery.Models
{
    public class Order
    {

        public ObjectId id { get; set; }

        public DateTime fecha { get; set; }
        public ObjectId idCar { get; set; }
        public string nomliente { get; set; }
        public string apCliente { get; set; }

        public string numTel { get; set; }

        public string dirCliente { get; set; }

        public string email { get; set; }
    }
}

