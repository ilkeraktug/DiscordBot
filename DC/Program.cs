namespace DC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new BoshBot().Run().ConfigureAwait(false).GetAwaiter().GetResult();
		}
	}
}
