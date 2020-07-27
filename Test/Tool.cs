using Altseed2;
using Altseed2.ToolAuxiliary;
using System.Diagnostics;
using NUnit.Framework;

namespace Test
{
    public class Tool
    {
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Empty()
        {
            Engine.Initialize("Empry", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool1";
            ToolHelper.Size = new Vector2I(960, 720);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool1()
        {
            Engine.Initialize("Tool1", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool1";
            ToolHelper.Size = new Vector2I(250, 250);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            ToolHelper.AddComponent(new InputInt1("Int1", 0)
            {
                Max = 100,
                Min = 0
            });
            ToolHelper.AddComponent(new InputInt2("Int2", 0, 0)
            {
                Max1 = 100,
                Min1 = 0,
                Max2 = 100,
                Min2 = 0
            });
            ToolHelper.AddComponent(new InputFloat1("Float1", 0)
            {
                Max = 100f,
                Min = 0f
            });
            ToolHelper.AddComponent(new InputFloat2("Float2", 0, 0)
            {
                Max1 = 100f,
                Min1 = 0f,
                Max2 = 100f,
                Min2 = 0f
            });
            ToolHelper.AddComponent(new InputText("Text1", "Text")
            {
                MaxLength = 2000
            });
            var button = new Button("Button");
            button.Clicked += (x, y) =>
            {
                Debug.WriteLine("Button1_Clicked");
            };
            ToolHelper.AddComponent(new Text("Text2", new Color(255, 100, 100)));
            ToolHelper.AddComponent(button);
            ToolHelper.AddComponent(new ColorEdit("Color_3", new Color(255, 255, 100)) { EditAlpha = false });
            ToolHelper.AddComponent(new ColorEdit("Color_4", new Color(255, 255, 100)) { EditAlpha = true });
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool2()
        {
            Engine.Initialize("Tool2", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool2";
            ToolHelper.Size = new Vector2I(250, 500);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            ToolHelper.AddComponent(new Combo<AlphaBlendMode>("Combo", new [] { AlphaBlendMode.Add, AlphaBlendMode.Multiply, AlphaBlendMode.Normal, AlphaBlendMode.Opacity, AlphaBlendMode.Subtract }));
            var imageButton = new ImageButton()
            {
                Color = new Color(100, 255, 100),
                FrameSize = 3,
                Texture = Texture2D.LoadStrict("Resources/Texture.png")
            };
            imageButton.Clicked += (x, y) => Debug.WriteLine("ImageButton_Clicked");
            ToolHelper.AddComponent(imageButton);
            ToolHelper.AddComponent(new Image()
            {
                Color = new Color(100, 255, 255),
                FrameColor = new Color(255, 100, 100),
                Texture = Texture2D.LoadStrict("Resources/Texture.png")
            });
            ToolHelper.AddComponent(new ListBox<DrawMode>("List", new [] { DrawMode.Absolute, DrawMode.Fill, DrawMode.KeepAspect }));
            ToolHelper.AddComponent(new BulletTexts(new[] { "A", "B", "C" }));
            ToolHelper.AddComponent(new CheckBox("CheckBox", true));
            ToolHelper.AddComponent(new SliderInt1("Int1", 0));
            ToolHelper.AddComponent(new SliderInt2("Int2", 0, 0));
            ToolHelper.AddComponent(new SliderInt3("Int3", 0, 0, 0));
            ToolHelper.AddComponent(new SliderInt4("Int4", 0, 0, 0, 0));
            ToolHelper.AddComponent(new SliderFloat1("Float1", 0));
            ToolHelper.AddComponent(new SliderFloat2("Float2", 0, 0));
            ToolHelper.AddComponent(new SliderFloat3("Float3", 0, 0, 0));
            ToolHelper.AddComponent(new SliderFloat4("Float4", 0, 0, 0, 0));
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool3()
        {
            Engine.Initialize("Tool3", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool3";
            ToolHelper.Size = new Vector2I(250, 150);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            ToolHelper.AddComponent(new ArrowButton("Arrow", ToolDir.Right));
            ToolHelper.AddComponent(new SliderAngle("Angle", 0f));
            ToolHelper.AddComponent(new DragFloat("Float", 0) { Speed = 3f });
            ToolHelper.AddComponent(new DragInt("Int", 0));
            var menubar = ToolHelper.MenuBar = new MenuBar();
            var menu_File = new Menu("File");
            menu_File.AddComponent(new MenuItem("CreateNew", "Ctrl + N"));
            menu_File.AddComponent(new MenuItem("Open", "Ctrl + O"));
            menu_File.AddComponent(new MenuItem("Save", "Ctrl + S"));
            menu_File.AddComponent(new MenuItem("SaveNew", "Ctrl + Shift + S"));
            menubar.AddComponent(menu_File);
            var menu_Edit = new Menu("Edit");
            menu_Edit.AddComponent(new MenuItem("Encoding", string.Empty));
            menu_Edit.AddComponent(new MenuItem("Help", string.Empty));
            var menu_Edit_Option = new Menu("Option");
            menu_Edit_Option.AddComponent(new MenuItem("Position", string.Empty));
            menu_Edit_Option.AddComponent(new MenuItem("Size", string.Empty));
            menu_Edit.AddComponent(menu_Edit_Option);
            menubar.AddComponent(menu_Edit);
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool4()
        {
            Engine.Initialize("Tool4", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool4";
            ToolHelper.Size = new Vector2I(250, 350);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            var tabbar = new TabBar("TabBar");
            var tabItem1 = new TabItem("A");
            tabItem1.AddComponent(new Text("A1"));
            tabItem1.AddComponent(new Text("A2"));
            tabItem1.AddComponent(new Text("A3"));
            tabbar.AddTabItem(tabItem1);
            var tabItem2 = new TabItem("B");
            tabItem2.AddComponent(new InputInt1("B1", 0));
            tabItem2.AddComponent(new InputInt1("B2", 0));
            tabItem2.AddComponent(new InputInt1("B3", 0));
            tabbar.AddTabItem(tabItem2);
            var tabItem3 = new TabItem("C");
            tabItem3.AddComponent(new Combo<int>("C1", new [] { 1, 2, 3, 4, 5 }));
            tabItem3.AddComponent(new Combo<double>("C2", new [] { 0.1, 0.2, 0.3, 0.4, 0.5 }));
            tabItem3.AddComponent(new Combo<char>("C3", new [] { 'a', 'b', 'c', 'd', 'e' }));
            tabbar.AddTabItem(tabItem3);
            ToolHelper.AddComponent(tabbar);
            var node1 = new TreeNode("1");
            var node1_1 = new TreeNode("1-1");
            node1_1.AddComponent(new Text("1-1-1"));
            node1_1.AddComponent(new Text("1-1-2"));
            var node1_2 = new TreeNode("1-2");
            node1_2.AddComponent(new Text("1-2"));
            node1.AddComponent(node1_1);
            node1.AddComponent(node1_2);
            ToolHelper.AddComponent(node1);
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool5()
        {
            Engine.Initialize("Tool5", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool5";
            ToolHelper.Size = new Vector2I(250, 350);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            ToolHelper.AddComponent(new ProgressBar("ProgressBar", 0f, 1f) { AddProgressValue = true });
            var group = new Group();
            group.AddComponent(new Text("A"));
            group.AddComponent(new Text("B"));
            group.AddComponent(new Text("C"));
            ToolHelper.AddComponent(group);
            var collapsing = new CollapsingHeader("Collapsing");
            collapsing.AddComponent(new Button("A"));
            collapsing.AddComponent(new Button("B"));
            collapsing.AddComponent(new Button("C"));
            ToolHelper.AddComponent(collapsing);
            var tooltip = new Tooltip();
            tooltip.AddComponent(new ArrowButton("1", ToolDir.Down));
            tooltip.AddComponent(new ArrowButton("2", ToolDir.Left));
            tooltip.AddComponent(new ArrowButton("3", ToolDir.Right));
            tooltip.AddComponent(new ArrowButton("4", ToolDir.Up));
            ToolHelper.AddComponent(tooltip);
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void Tool6()
        {
            Engine.Initialize("Tool6", 960, 720, new Configuration()
            {
                ToolEnabled = true
            });
            ToolHelper.Name = "Tool6";
            ToolHelper.Size = new Vector2I(250, 350);
            ToolHelper.WindowFlags = ToolWindowFlags.NoResize | ToolWindowFlags.NoCollapse | ToolWindowFlags.NoMove;
            var button_OpenMono = new Button("OpenMono");
            var dialog_Open = new OpenFileDialog()
            {
                Filter = "jpg,png"
            };
            button_OpenMono.Clicked += (x, y) =>
            {
                if (dialog_Open.ShowDialog()) Debug.WriteLine(dialog_Open.FileName);
            };
            ToolHelper.AddComponent(button_OpenMono);
            var button_OpenMulti = new Button("OpenMulti");
            var dialog_OpenMulti = new OpenFileDialog()
            {
                Filter = "jpg,png",
                MultiSelect  = true
            };
            button_OpenMulti.Clicked += (x, y) =>
            {
                if (dialog_OpenMulti.ShowDialog())
                    foreach (var filename in dialog_OpenMulti.FileNames)
                    Debug.WriteLine(filename);
            };
            ToolHelper.AddComponent(button_OpenMulti);
            var button_Save = new Button("Save");
            var dialog_Save = new SaveFileDialog()
            {
                Filter = "jpg,png"
            };
            button_Save.Clicked += (x, y) =>
            {
                if (dialog_Save.ShowDialog()) Debug.WriteLine(dialog_Save.FileName);
            };
            ToolHelper.AddComponent(button_Save);
            var button_Folder = new Button("Folder");
            var dialog_Folder = new FolderDialog();
            button_Folder.Clicked += (x, y) =>
            {
                if (dialog_Folder.ShowDialog()) Debug.WriteLine(dialog_Folder.SelectedPath);
            };
            ToolHelper.AddComponent(button_Folder);
            while (Engine.DoEvents())
            {
                ToolHelper.Update();
                Engine.Update();
                if (Engine.Keyboard.GetKeyState(Key.Escape) == ButtonState.Push) break;
            }
            Engine.Terminate();
        }
    }
}
