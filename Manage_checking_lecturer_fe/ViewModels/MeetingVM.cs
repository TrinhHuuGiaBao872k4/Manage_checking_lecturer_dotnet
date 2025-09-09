using System;
public class MeetingDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Datetime { get; set; } = DateTime.Now;
}

public class CreateMeetingDto
{
    public string Title { get; set; } = "";
    public DateTime Datetime { get; set; } = DateTime.Now;
}

public class UpdateMeetingDto
{
    public string Title { get; set; } = "";
    public DateTime Datetime { get; set; } = DateTime.Now;
}

public class ParticipantDto
{
    public string Fullname { get; set; } = "";
    public string Status { get; set; } = "PENDING"; // ATTENDING | ABSENT | PENDING
    public string? Reason { get; set; }
}

