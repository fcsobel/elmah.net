namespace Elmah.Net.Logger.Data
{
	public interface ISiteRecord
	{
		string Name { get; set; }
		int Id { get; set; }
		string ConnectionString { get; set; }
		bool UpdateDb();
	}
}