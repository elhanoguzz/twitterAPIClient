using System;
public class Rootobject
{
    public Event _event { get; set; }
}

public class Event
{
    public string type { get; set; }
    public Message_Create message_create { get; set; }
}

public class Message_Create
{
    public Target target { get; set; }
    public Message_Data message_data { get; set; }
}

public class Target
{
    public string recipient_id { get; set; }
    public string recipient_id { get; set; }
}

public class Message_Data
{
    public string text { get; set; }
}




