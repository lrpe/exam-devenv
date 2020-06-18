namespace DevEnvExam.Resources
{
	public class ProductResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Unit { get; set; }
		public int Amount { get; set; }
		public double Price { get; set; }
		public int QuantityInStock { get; set; }
		public int CategoryId { get; set; }
		public int SupplierId { get; set; }
	}
}
