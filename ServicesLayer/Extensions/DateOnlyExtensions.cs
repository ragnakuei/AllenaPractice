namespace ServicesLayer.Extensions;

public static class DateOnlyExtensions
{
    public static int Days(this DateOnly from, DateOnly to)
    {
        return (to.ToDateTime(new TimeOnly()) - from.ToDateTime(new TimeOnly())).Days;
    }
}