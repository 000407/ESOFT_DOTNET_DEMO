namespace demo_crud.Models.DTO
{
    public class Message
    {
        public readonly Status Status;

        public readonly string Text;

        public Message(Status status, string text) {
            Status = status;
            Text = text;
        }
    }

    public enum Status {
        INFO,
        WARN,
        ERROR,
        FATAL
    }
}