
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class MCHEditorWindow : EditorWindow
{
    /// <summary>
    /// This is a test object, just to showcase how this could be used.
    /// </summary>
    private class Enemy
    {
        public string Name { get; set; }
        public float Health { get; set; }
        public Color SkinColor { get; set; }

        public Enemy(string name, float health, Color skinColor)
        {
            this.Name = name;
            this.Health = health;
            this.SkinColor = skinColor;
        }
    }

    [MenuItem(itemName: "Tools/MCH Editor Window")]
    public static MCHEditorWindow Open()
    {
        MCHEditorWindow commentsNotebookEditorWindow = EditorWindow.GetWindow<MCHEditorWindow>(
            title: "MCH Editor Window",
            focus: true
        );

        commentsNotebookEditorWindow.minSize = new Vector2(x: 450.0f, y: 100.0f);

        commentsNotebookEditorWindow.Show();

        return commentsNotebookEditorWindow;
    }

    private MultiColumnHeaderState _multiColumnHeaderState;
    private MultiColumnHeader _multiColumnHeader;

    private MultiColumnHeaderState.Column[] _columns;

    // Create a few test subjects.
    private Enemy[] _testObjects = new Enemy[]
    {
        new Enemy("Orc", 25.0f, Color.green),
        new Enemy("Fairy", 10.0f, Color.cyan),
        new Enemy("Mech Golem", 57.0f, Color.grey),
        new Enemy("Orc", 25.0f, Color.green),
        new Enemy("Fairy", 10.0f, Color.cyan),
        new Enemy("Mech Golem", 57.0f, Color.grey),
        new Enemy("Orc", 25.0f, Color.green),
        new Enemy("Fairy", 10.0f, Color.cyan),
        new Enemy("Mech Golem", 57.0f, Color.grey),
        new Enemy("Orc", 25.0f, Color.green),
        new Enemy("Fairy", 10.0f, Color.cyan),
        new Enemy("Mech Golem", 57.0f, Color.grey),
    };

    private void Initialize()
    {
        // We can move these columns into some ScriptableObject or some other data saving object/file to save their properties there, otherwise because of some events these settings will be recreated and state of the window won't be saved as expected.
        this._columns = new MultiColumnHeaderState.Column[]
        {
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = false, // At least one column must be there.
                autoResize = true,
                minWidth = 250.0f,
                canSort = true,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Name", "A name of an enemy."),
                headerTextAlignment = TextAlignment.Left,
            },
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = true,
                autoResize = true,
                minWidth = 300.0f,
                canSort = false,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Health", "A health of an enemy."),
                headerTextAlignment = TextAlignment.Center,
            },
            new MultiColumnHeaderState.Column()
            {
                allowToggleVisibility = true,
                autoResize = true,
                minWidth = 125.0f,
                maxWidth = 175.0f,
                canSort = false,
                sortingArrowAlignment = TextAlignment.Right,
                headerContent = new GUIContent("Skin Color", "A color of an enemy skin."),
                headerTextAlignment = TextAlignment.Center,
            },
        };

        this._multiColumnHeaderState = new MultiColumnHeaderState(columns: this._columns);

        this._multiColumnHeader = new MultiColumnHeader(state: this._multiColumnHeaderState);

        // When we chagne visibility of the column we resize columns to fit in the window.
        this._multiColumnHeader.visibleColumnsChanged += (multiColumnHeader) => multiColumnHeader.ResizeToFit();

        // Initial resizing of the content.
        this._multiColumnHeader.ResizeToFit();
    }

    private readonly Color _lighterColor = Color.white * 0.3f;
    private readonly Color _darkerColor = Color.white * 0.1f;

    private Vector2 _scrollPosition;
    
    private void OnGUI()
    {
        // After compilation and some other events data of the window is lost if it's not saved in some kind of container. Usually those containers are ScriptableObject(s).
        if (this._multiColumnHeader == null)
        {
            this.Initialize();
        }

        // Basically we just draw something. Empty space. Which is `FlexibleSpace` here on top of the window.
        // We need this for - `GUILayoutUtility.GetLastRect()` because it needs at least 1 thing to be drawn before it.
        GUILayout.FlexibleSpace();

        // Get automatically aligned rect for our multi column header component.
        Rect windowRect = GUILayoutUtility.GetLastRect();

        // Here we are basically assigning the size of window to our newly positioned `windowRect`.
        windowRect.width = this.position.width;
        windowRect.height = this.position.height;

        float columnHeight = EditorGUIUtility.singleLineHeight;

        // This is a rect for our multi column table.
        Rect columnRectPrototype = new Rect(source: windowRect)
        {
            height = columnHeight, // This is basically a height of each column including header.
        };

        // Just enormously large view if you want it to span for the whole window. This is how it works [shrugs in confusion].
        Rect positionalRectAreaOfScrollView = GUILayoutUtility.GetRect(0, float.MaxValue, 0, float.MaxValue);

        // Create a `viewRect` since it should be separate from `rect` to avoid circular dependency.
        Rect viewRect = new Rect(source: windowRect)
        {
            xMax = this._columns.Sum((column) => column.width) // Scroll max on X is basically a sum of width of columns.
        };

        this._scrollPosition = GUI.BeginScrollView(
            position: positionalRectAreaOfScrollView,
            scrollPosition: this._scrollPosition,
            viewRect: viewRect,
            alwaysShowHorizontal: false,
            alwaysShowVertical: false
        );

        // Draw header for columns here.
        this._multiColumnHeader.OnGUI(rect: columnRectPrototype, xScroll: 0.0f);

        // For each element that we have in object that we are modifying.
        //? I don't have an appropriate object here to modify, but this is just an example. In real world case I would probably use ScriptableObject here.
        for (int a = 0; a < this._testObjects.Length; a++)
        {
            //! We draw each type of field here separately because each column could require a different type of field as seen here.
            // This can be improved if we want to have a more robust system. Like for example, we could have logic of drawing each field moved to object itself.
            // Then here we would be able to just iterate through array of these objects and call a draw methods for these fields and use this window for many types of objects.
            // But example with such a system would be too complicated for gamedev.stackexchange, so I have decided to not overengineer and just use hard coded indices for columns - `columnIndex`.

            Rect rowRect = new Rect(source: columnRectPrototype);

            rowRect.y += columnHeight * (a + 1);

            // Draw a texture before drawing each of the fields for the whole row.
            if (a % 2 == 0)
                EditorGUI.DrawRect(rect: rowRect, color: this._darkerColor);
            else
                EditorGUI.DrawRect(rect: rowRect, color: this._lighterColor);

            // Name field.
            int columnIndex = 0;

            if (this._multiColumnHeader.IsColumnVisible(columnIndex: columnIndex))
            {
                int visibleColumnIndex = this._multiColumnHeader.GetVisibleColumnIndex(columnIndex: columnIndex);

                Rect columnRect = this._multiColumnHeader.GetColumnRect(visibleColumnIndex: visibleColumnIndex);

                // This here basically is a row height, you can make it any value you like. Or you could calculate the max field height here that your object has and store it somewhere then use it here instead of `EditorGUIUtility.singleLineHeight`.
                // We move position of field on `y` by this height to get correct position.
                columnRect.y = rowRect.y;

                GUIStyle nameFieldGUIStyle = new GUIStyle(GUI.skin.label)
                {
                    padding = new RectOffset(left: 10, right: 10, top: 2, bottom: 2)
                };

                EditorGUI.LabelField(
                    position: this._multiColumnHeader.GetCellRect(visibleColumnIndex: visibleColumnIndex, columnRect),
                    label: new GUIContent(this._testObjects[a].Name),
                    style: nameFieldGUIStyle
                );
            }

            // Health slider field.
            columnIndex = 1;

            if (this._multiColumnHeader.IsColumnVisible(columnIndex: columnIndex))
            {
                int visibleColumnIndex = this._multiColumnHeader.GetVisibleColumnIndex(columnIndex: columnIndex);

                Rect columnRect = this._multiColumnHeader.GetColumnRect(visibleColumnIndex: visibleColumnIndex);

                columnRect.y = rowRect.y;
                
                this._testObjects[a].Health = EditorGUI.Slider(
                    position: this._multiColumnHeader.GetCellRect(visibleColumnIndex: visibleColumnIndex, columnRect),
                    value: this._testObjects[a].Health,
                    leftValue: 0.0f,
                    rightValue: 100.0f
                );
            }

            // Skin color field.
            columnIndex = 2;

            if (this._multiColumnHeader.IsColumnVisible(columnIndex: columnIndex))
            {
                int visibleColumnIndex = this._multiColumnHeader.GetVisibleColumnIndex(columnIndex: columnIndex);

                Rect columnRect = this._multiColumnHeader.GetColumnRect(visibleColumnIndex: visibleColumnIndex);

                columnRect.y = rowRect.y;
                
                this._testObjects[a].SkinColor = EditorGUI.ColorField(
                    position: this._multiColumnHeader.GetCellRect(visibleColumnIndex: visibleColumnIndex, columnRect),
                    label: GUIContent.none,
                    value: this._testObjects[a].SkinColor,
                    showEyedropper: true,
                    showAlpha: false,
                    hdr: false
                );
            }
        }

        GUI.EndScrollView(handleScrollWheel: true);
    }

    private void Awake()
    {
        this.Initialize();
    }
}
#endif
