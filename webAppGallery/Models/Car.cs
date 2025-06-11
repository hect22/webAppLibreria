using MongoDB.Bson;

namespace webAppGallery.Models
{
    public class Car
    {
        public ObjectId id { get; set; }

        public string marca { get; set; }
    public string modelo { get; set; }
    public string descripcion { get; set; }
    public string color { get; set; }

    public decimal precio { get; set; }

    public string imagen { get; set; }

}
}
