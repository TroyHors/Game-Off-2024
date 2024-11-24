using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ItemRecipeBatchCreator : EditorWindow {
    // 滚动视图的滚动位置
    private Vector2 scrollPosition = Vector2.zero;

    // Item 属性
    private List<ItemData_SO> itemsToCreate = new List<ItemData_SO>();
    private int itemCount = 1;

    // Recipe 属性
    private List<Recipe> recipesToCreate = new List<Recipe>();
    private int recipeCount = 1;

    // 保存路径
    private string itemSavePath = "Assets/Items";
    private string recipeSavePath = "Assets/Recipes";

    [MenuItem( "Tools/Batch Item & Recipe Creator" )]
    public static void ShowWindow() {
        GetWindow<ItemRecipeBatchCreator>( "Batch Item & Recipe Creator" );
    }

    private void OnGUI() {
        // 添加滚动视图
        scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition );

        GUILayout.Label( "Batch Create Items" , EditorStyles.boldLabel );

        // 选择 Item 保存路径
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Item Save Path:" , GUILayout.Width( 100 ) );
        if (GUILayout.Button( "Select Folder" , GUILayout.Width( 100 ) )) {
            string selectedPath = EditorUtility.OpenFolderPanel( "Select Folder for Items" , itemSavePath , "" );
            if (!string.IsNullOrEmpty( selectedPath )) {
                itemSavePath = ConvertToRelativePath( selectedPath );
            }
        }
        GUILayout.Label( itemSavePath );
        GUILayout.EndHorizontal();

        itemCount = EditorGUILayout.IntField( "Number of Items" , itemCount );
        if (GUILayout.Button( "Generate Item Fields" )) {
            GenerateItemFields();
        }

        for (int i = 0 ; i < itemsToCreate.Count ; i++) {
            GUILayout.Label( $"Item {i + 1}" , EditorStyles.boldLabel );
            itemsToCreate[ i ].itemName = EditorGUILayout.TextField( "Name" , itemsToCreate[ i ].itemName );
            itemsToCreate[ i ].itemType = (ItemType) EditorGUILayout.EnumPopup( "Type" , itemsToCreate[ i ].itemType );
            itemsToCreate[ i ].itemIcon = (Sprite) EditorGUILayout.ObjectField( "Icon" , itemsToCreate[ i ].itemIcon , typeof( Sprite ) , false );
            itemsToCreate[ i ].maxStack = EditorGUILayout.IntField( "Max Stack" , itemsToCreate[ i ].maxStack );
            itemsToCreate[ i ].stackable = EditorGUILayout.Toggle( "Stackable" , itemsToCreate[ i ].stackable );

            if (itemsToCreate[ i ].itemType == ItemType.Bait) {
                itemsToCreate[ i ].baitsLevel = EditorGUILayout.IntSlider( "Baits Level" , itemsToCreate[ i ].baitsLevel , 1 , 3 );
                itemsToCreate[ i ].targetFish = (ItemData_SO) EditorGUILayout.ObjectField( "Target Fish" , itemsToCreate[ i ].targetFish , typeof( ItemData_SO ) , false );
            }

            if (itemsToCreate[ i ].itemType == ItemType.Eq) {
                GUILayout.Label( "Special Bonus" , EditorStyles.boldLabel );
                itemsToCreate[ i ].bonusEffect = (ItemBonusEffect) EditorGUILayout.ObjectField( "Bonus Effect" , itemsToCreate[ i ].bonusEffect , typeof( ItemBonusEffect ) , false );
            }
        }

        if (GUILayout.Button( "Create Items" )) {
            CreateItems();
        }

        GUILayout.Space( 20 );

        GUILayout.Label( "Batch Create Recipes" , EditorStyles.boldLabel );

        // 选择 Recipe 保存路径
        GUILayout.BeginHorizontal();
        GUILayout.Label( "Recipe Save Path:" , GUILayout.Width( 100 ) );
        if (GUILayout.Button( "Select Folder" , GUILayout.Width( 100 ) )) {
            string selectedPath = EditorUtility.OpenFolderPanel( "Select Folder for Recipes" , recipeSavePath , "" );
            if (!string.IsNullOrEmpty( selectedPath )) {
                recipeSavePath = ConvertToRelativePath( selectedPath );
            }
        }
        GUILayout.Label( recipeSavePath );
        GUILayout.EndHorizontal();

        recipeCount = EditorGUILayout.IntField( "Number of Recipes" , recipeCount );
        if (GUILayout.Button( "Generate Recipe Fields" )) {
            GenerateRecipeFields();
        }

        for (int i = 0 ; i < recipesToCreate.Count ; i++) {
            GUILayout.Label( $"Recipe {i + 1}" , EditorStyles.boldLabel );

            // 文件名输入
            recipesToCreate[ i ].name = EditorGUILayout.TextField( "File Name" , recipesToCreate[ i ].name );

            // 动态调整材料数量
            int ingredientCount = EditorGUILayout.IntField( "Number of Ingredients" , recipesToCreate[ i ].ingredients.Count );
            while (recipesToCreate[ i ].ingredients.Count < ingredientCount) {
                recipesToCreate[ i ].ingredients.Add( new Recipe.Ingredient { itemData = null , requiredAmount = 0 } );
            }
            while (recipesToCreate[ i ].ingredients.Count > ingredientCount) {
                recipesToCreate[ i ].ingredients.RemoveAt( recipesToCreate[ i ].ingredients.Count - 1 );
            }

            // 编辑每个材料及其数量
            for (int j = 0 ; j < recipesToCreate[ i ].ingredients.Count ; j++) {
                GUILayout.BeginHorizontal();
                recipesToCreate[ i ].ingredients[ j ].itemData = (ItemData_SO) EditorGUILayout.ObjectField( $"Ingredient {j + 1}" , recipesToCreate[ i ].ingredients[ j ].itemData , typeof( ItemData_SO ) , false );
                recipesToCreate[ i ].ingredients[ j ].requiredAmount = EditorGUILayout.IntField( "Amount" , recipesToCreate[ i ].ingredients[ j ].requiredAmount );
                GUILayout.EndHorizontal();
            }

            // 设置合成结果
            recipesToCreate[ i ].resultItem = (ItemData_SO) EditorGUILayout.ObjectField( "Result Item" , recipesToCreate[ i ].resultItem , typeof( ItemData_SO ) , false );
            recipesToCreate[ i ].resultAmount = EditorGUILayout.IntField( "Result Amount" , recipesToCreate[ i ].resultAmount );
        }

        if (GUILayout.Button( "Create Recipes" )) {
            CreateRecipes();
        }

        // 结束滚动视图
        EditorGUILayout.EndScrollView();
    }

    private void GenerateItemFields() {
        itemsToCreate.Clear();
        for (int i = 0 ; i < itemCount ; i++) {
            itemsToCreate.Add( CreateInstance<ItemData_SO>() );
        }
    }

    private void CreateItems() {
        for (int i = 0 ; i < itemsToCreate.Count ; i++) {
            string path = $"{itemSavePath}/{itemsToCreate[ i ].itemName}.asset";
            AssetDatabase.CreateAsset( itemsToCreate[ i ] , path );
        }

        AssetDatabase.SaveAssets();
        Debug.Log( $"{itemsToCreate.Count} Items created successfully!" );
    }

    private void GenerateRecipeFields() {
        recipesToCreate.Clear();
        for (int i = 0 ; i < recipeCount ; i++) {
            recipesToCreate.Add( CreateInstance<Recipe>() );
        }
    }

    private void CreateRecipes() {
        for (int i = 0 ; i < recipesToCreate.Count ; i++) {
            // 使用用户输入的文件名，如果为空则自动生成默认名称
            string fileName = string.IsNullOrEmpty( recipesToCreate[ i ].name )
                ? $"Recipe_{i + 1}"
                : recipesToCreate[ i ].name;

            // 更新主对象名称
            recipesToCreate[ i ].name = fileName;

            // 使用文件名创建资产
            string path = $"{recipeSavePath}/{fileName}.asset";
            AssetDatabase.CreateAsset( recipesToCreate[ i ] , path );
        }

        AssetDatabase.SaveAssets();
        Debug.Log( $"{recipesToCreate.Count} Recipes created successfully!" );
    }

    private string ConvertToRelativePath( string absolutePath ) {
        if (absolutePath.StartsWith( Application.dataPath )) {
            return "Assets" + absolutePath.Substring( Application.dataPath.Length );
        }
        return absolutePath;
    }
}
