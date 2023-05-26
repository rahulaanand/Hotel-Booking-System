
using Hotel_Booking_System_2.Models;

namespace APIcodefirst.Repository
{
    public interface IRoomRepository
    {
        public IEnumerable<Rooms> GetRoom();

        public Rooms GetRoomByid(int id);

        public Rooms PostRoom(Rooms rooms);

        public void PutRoom(Rooms rooms);

        public void DeleteRoom(int id);

    }
}