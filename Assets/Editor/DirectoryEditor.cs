using UnityEditor;
using System.IO;
using System.Linq;

public class DirectoryEditor : Editor
{
    private static string[] _simpleDirectoriesName = new[]
    {
        "Scripts",
        "Scenes",
        "Shaders",
    };
    
    private static string[] _basicDirectoriesName = new[]
    {
        "Editor",
        "Materials",
        "Resources",
        "Prefabs",
        "Textures"
    };
    
    private static string[] _fullDirectoriesName = new[]
    {
        "Animations",
        "Audios",
    };
    
    [MenuItem("Assets/Create/CreateDirectories/Simple")]
    public static void CreateSimpleDirectories()
    {
        CreateDirectories(_simpleDirectoriesName);
    }
    
    [MenuItem("Assets/Create/CreateDirectories/Basic")]
    public static void CreateBasicDirectories()
    {
        var directoriesName = _simpleDirectoriesName
            .Concat(_basicDirectoriesName)
            .ToArray();
        CreateDirectories(directoriesName);
    }

    [MenuItem("Assets/Create/CreateDirectories/Full")]
    public static void CreateFullDirectories()
    {
        var directoriesName = _simpleDirectoriesName
            .Concat(_basicDirectoriesName)
            .Concat(_fullDirectoriesName)
            .ToArray();
        CreateDirectories(directoriesName);
    }

    private static void CreateDirectories(string[] directoriesName)
    {
        var targetPath = GetTargetDirectoryPath();
        
        if (targetPath == null) return;

        for (int i = 0; i < directoriesName.Length; i++)
        {
            CreateDirectory(targetPath, directoriesName[i]);
        }
    }

    private static void CreateDirectory(string targetPath, string name)
    {
        var directoryPath = Path.Combine(targetPath, name);
        
        if (!AssetDatabase.IsValidFolder(directoryPath))
        {
            AssetDatabase.CreateFolder(targetPath, name);
            AssetDatabase.ImportAsset(directoryPath);
        }
    }
    
    private static string GetTargetDirectoryPath()
    {
        var instanceID = Selection.activeInstanceID;
        var path = AssetDatabase.GetAssetPath(instanceID);
        
        if (!AssetDatabase.IsValidFolder(path)) return null;
        return path;
    }
}
