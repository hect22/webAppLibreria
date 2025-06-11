using MongoDB.Bson;
using MongoDB.Driver;
using webAppGallery.Models;

namespace webAppGallery.Services
{
    public class carServices
    {
        private readonly IMongoCollection<Car> cars;

        public carServices(IConfiguration configuration)
        {

            MongoClient cliente = new MongoClient
                (configuration.GetConnectionString("AppGalleryDB"));
            IMongoDatabase database = cliente.GetDatabase("galleryDB");
            cars = database.GetCollection<Car>("car");

        }
        public List<Car> GetCars()
        {
            return cars.Find(Car => true).ToList();
        }
        public Car get(ObjectId id)
        {
            return cars.Find(Car => Car.id==id).FirstOrDefault();
        }

        public Car create(Car car)
        {
            cars.InsertOne(car);
            return car;
        }
       public void delete (ObjectId id)
        {

            cars.DeleteOne(car=> car.id==id);
        }

        public void update (ObjectId id, Car carId)
        {
            cars.ReplaceOne(car => car.id== id, carId);
        }
    }
}