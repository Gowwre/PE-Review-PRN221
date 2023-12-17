namespace BusinessObjects.DTO {
    public class SearchParam {
       public string? Name { get; set; }
       public string? Id { get; set; }
       public int? Page { get; set; } = 1;
       public int? PageSize { get; set; } = 4;
    }
}