namespace netcoreAPI.Domain
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Model> Models { get; set; } = new List<Model>();
    }
}
