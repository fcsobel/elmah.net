namespace c3o.Logger.Data
{
	public interface ISiteInstance
	{
		string SiteName { get; }
		ISiteRecord Site { get; }
		string CreateSite(ISiteRecord site);
	}
}