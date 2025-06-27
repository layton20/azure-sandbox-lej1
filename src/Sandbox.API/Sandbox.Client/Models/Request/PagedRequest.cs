using System.ComponentModel.DataAnnotations;

namespace Sandbox.Client.Models.Request;

public class PagedRequest
{
    [Required]
    public int PageNumber { get; set; }

    [Required]
    public int PageSize { get; set; }
}