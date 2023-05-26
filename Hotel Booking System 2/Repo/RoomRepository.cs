using Hotel_Booking_System_2.Db;
using Hotel_Booking_System_2.Models;

namespace APIcodefirst.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingContext _Context;

        public RoomRepository(HotelBookingContext con)
        {
            _Context = con;
        }
        public Rooms GetRoomByid(int id)
        {
            return _Context.Rooms.FirstOrDefault(x => x.RoomId == id);
        }
        public IEnumerable<Rooms> GetRoom()
        {
            return _Context.Rooms.ToList();
        }
        public Rooms PostRoom(Rooms rooms)
        {
            _Context.Rooms.Find(rooms.RoomId);
            _Context.Rooms.Add(rooms);
            _Context.SaveChanges();
            return rooms;
        }
        public void PutRoom(Rooms rooms)
        {
            _Context.Entry(rooms).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _Context.SaveChanges();
        }
        public void DeleteRoom(int id)
        {
            Rooms e = _Context.Rooms.FirstOrDefault(x => x.RoomId == id);
            _Context.Rooms.Remove(e);
            _Context.SaveChanges();
        }
    }
}