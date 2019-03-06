namespace DatingAppOrleans.Grains.UserGrain
{
    public enum UserState { New, Registered }
    public enum UserTriggers { Register, Login }


    public class User
    {
        public int Id { get; set; }
        public UserState State { get; set; } = UserState.New;
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
