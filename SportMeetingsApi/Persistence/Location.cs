using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportMeetingsApi.Persistence;
public class Location {
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Street { get; set; }

    [RegularExpression(@"\d{2}-\d{3}")]
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public ICollection<SportEvent> SportEvents { get; set; } = new List<SportEvent>();
}
