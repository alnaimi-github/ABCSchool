namespace Domain.Entities;

public class School
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedOn { get; set; }
    public string Address { get; set; }
}