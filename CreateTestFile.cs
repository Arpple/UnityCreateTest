using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateTestFile : MonoBehaviour {

	public static readonly string SCRIPT_ROOT = Path.Combine("Assets", "Scripts");
	public static readonly string TEST_ROOT = Path.Combine("Assets", "Editor", "Test");

	private const string TEMPLATE = @"using NSubstitute;
using NUnit.Framework;

public class <{CLASS}> {

	public override void SetUp() {
		base.SetUp();
	}

	[Test]
	public void TestMethod() {
	}
}";

	[MenuItem("Assets/Create Test")]
	private static void CreateTest() {

		var selected = Selection.activeObject;
		if (selected == null) {
			Debug.LogError("cannot create test for empty");
			return;
		}

		var assetPath = AssetDatabase.GetAssetPath(selected.GetInstanceID());
		if (IsDirectory(assetPath)) {
			Debug.LogError("cannot create test for directory");
			return;
		}

		var sourceDirPath = Path.GetDirectoryName(assetPath);
		if (!sourceDirPath.Contains(SCRIPT_ROOT)) {
			Debug.LogError("cannot resolve path for script outside of SCRIPT_ROOT");
			return;
		}

		var sourceFileName = Path.GetFileNameWithoutExtension(assetPath);

		var targetDirPath = Path.Combine(TEST_ROOT, sourceDirPath.Substring(SCRIPT_ROOT.Length + 1));
		if (!Directory.Exists(targetDirPath)) {
			Directory.CreateDirectory(targetDirPath);
		}

		var targetFileName = sourceFileName + "Test";
		var targetFullPath = Path.Combine(targetDirPath, targetFileName + ".cs");

		CreateFileWithContent(targetFullPath, TEMPLATE.Replace("<{CLASS}>", targetFileName));

		AssetDatabase.Refresh();
		Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(targetFullPath);
	}

	private static bool IsDirectory(string path) {
		return Directory.Exists(path);
	}

	private static void CreateFileWithContent(string filePath, string content) {
		var file = File.CreateText(filePath);
		file.Write(content);
		file.Close();
	}
}
