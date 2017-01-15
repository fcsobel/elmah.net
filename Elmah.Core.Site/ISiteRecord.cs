namespace Elmah.Net.Logger.Data
{
	public interface ISiteRecord
	{
		string ConnectionString { get; set; }
		bool UpdateDb();

	}
}