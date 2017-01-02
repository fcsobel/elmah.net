namespace c3o.Logger.Data
{
	public interface ISiteRecord
	{
		string ConnectionString { get; set; }
		bool UpdateDb();

	}
}