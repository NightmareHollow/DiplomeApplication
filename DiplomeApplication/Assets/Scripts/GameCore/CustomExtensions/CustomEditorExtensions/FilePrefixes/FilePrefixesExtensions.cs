namespace GameCore.CustomExtensions.CustomEditorExtensions.FilePrefixes
{
	public static class FilePrefixesExtensions
	{

		public static string PrefixesConversion(FilePrefixType prefixType)
		{
			return prefixType switch
			{
				FilePrefixType.Prefab => ".prefab",
				FilePrefixType.Asset => ".asset",
				FilePrefixType.Script => ".cs",
				FilePrefixType.Png => ".png",
				FilePrefixType.Animation => ".anim",
				FilePrefixType.Controller => ".controller",
				FilePrefixType.Meta => ".meta",
				_ => ".asset"
			};
		}
		
	}
}