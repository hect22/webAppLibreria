using MongoDB.Bson;
using MongoDB.Driver;
using webAppGallery.Models;

namespace webAppGallery.Services
{
    public class orderServices
    {
        private readonly IMongoCollection<Order> order;

        public orderServices(IConfiguration configuration)
        {

            MongoClient cliente = new MongoClient
                (configuration.GetConnectionString("AppGalleryDB"));
            IMongoDatabase database = cliente.GetDatabase("galleryDB");
            order = database.GetCollection<Order>("order");

        }
        public List<Order> GetOrders()
        {
            return order.Find(Order => true).ToList();
        }
        public Order get(ObjectId id)
        {
            return order.Find(Order => Order.id == id).FirstOrDefault();
        }

        public Order create(Order orders)
        {
            order.InsertOne(orders);
            return orders;
   
        }
        public void delete(ObjectId id)
        {

            order.DeleteOne(car => car.id == id);
        }

        public void update(ObjectId id, Order orderId)
        {
            order.ReplaceOne(car => car.id == id, orderId);
        }

    }

}
