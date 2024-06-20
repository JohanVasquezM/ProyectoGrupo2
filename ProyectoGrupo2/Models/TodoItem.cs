namespace ProyectoGrupo2.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
    }
    public enum TodoStatus
    {
        PorHacer,
        EnProgreso,
        Finalizada
    }
}