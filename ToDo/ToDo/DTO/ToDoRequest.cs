namespace ToDo.DTO
{
    public class ToDoRequest
    {
        public Guid TaskId { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }

    }
}
