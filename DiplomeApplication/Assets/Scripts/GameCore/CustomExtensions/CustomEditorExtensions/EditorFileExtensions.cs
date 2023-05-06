#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameCore.CustomExtensions.CustomEditorExtensions
{
	public static class EditorFileExtensions
	{
#if UNITY_EDITOR
		
		public static string CreateFolder(string parentFolder, string folderName)
		{
			if (string.IsNullOrEmpty(parentFolder) || string.IsNullOrEmpty(folderName))
				return string.Empty;

			string guid = AssetDatabase.CreateFolder(parentFolder, folderName);
			string folderPath = AssetDatabase.GUIDToAssetPath(guid);

			return folderPath;
		}

		public static bool DeleteFolder(string folderPath)
		{
			bool deleteResult = FileUtil.DeleteFileOrDirectory(folderPath);

			return deleteResult;
		}
		
		public static bool ContainsFolder(string assetPath)
		{
			bool contains = AssetDatabase.IsValidFolder(assetPath);
			return contains;
		}
		
#endif
	}
}