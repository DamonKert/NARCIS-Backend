namespace NarcisKH.Class.Customer.DTO
{
	public class CategoryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<ChildDTO> Childs {  get; set; } = new List<ChildDTO>();
	}
	public class ChildDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
	}

}
