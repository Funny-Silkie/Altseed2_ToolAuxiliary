namespace Altseed2.ToolAuxiliary
{
    internal static class FlagCalculator
    {
        internal static ToolInputText CalcToolInputText(this IToolInputText inputText)
        {
            if (inputText == null) return default;
            var result = ToolInputText.None;
            if (inputText.AutoSelectAll) result |= ToolInputText.AutoSelectAll;
            if ((inputText.CharType & IToolInputText.InputTextCharType.Scientific) == (IToolInputText.InputTextCharType)1) result |= ToolInputText.CharsScientific;
            if ((inputText.CharType & (IToolInputText.InputTextCharType)2) == (IToolInputText.InputTextCharType)2) result |= ToolInputText.CharsDecimal;
            if ((inputText.CharType & IToolInputText.InputTextCharType.Hexadecimal) == (IToolInputText.InputTextCharType)4) result |= ToolInputText.CharsHexadecimal;
            if (!inputText.EnableHorizontalScroll) result |= ToolInputText.NoHorizontalScroll;
            if (inputText.IsOverrideMode) result |= ToolInputText.AlwaysInsertMode;
            if (inputText.IsPasswordMode) result |= ToolInputText.Password;
            if (inputText.IsReadOnly) result |= ToolInputText.ReadOnly;
            if (inputText.NoBlank) result |= ToolInputText.CharsNoBlank;
            if (inputText.TabSpace) result |= ToolInputText.AllowTabInput;
            if (inputText.UpperCaseFilter) result |= ToolInputText.CharsUppercase;
            return result;
        }
        internal static ToolSelectable CalcToolSelectable(this IToolSelectable selectable)
        {
            if (selectable == null) return default;
            var result = ToolSelectable.None;
            if (selectable.AllowDoubleClick) result |= ToolSelectable.AllowDoubleClick;
            if (!selectable.Enabled) result |= ToolSelectable.Disabled;
            if (selectable.KeepOpenPopups) result |= ToolSelectable.DontClosePopups;
            return result;
        }
        internal static ToolTabBar CaclToolTabBar(this IToolTabBar tabBar)
        {
            if (tabBar == null) return default;
            var result = ToolTabBar.None;
            switch (tabBar.FittingType)
            {
                case IToolTabBar.TabBarFittingType.Default: result |= ToolTabBar.FittingPolicyScroll | ToolTabBar.NoTabListScrollingButtons; break;
                case IToolTabBar.TabBarFittingType.ReSize: result |= ToolTabBar.FittingPolicyResizeDown; break;
                case IToolTabBar.TabBarFittingType.ScrollBar: result |= ToolTabBar.FittingPolicyScroll; break;
            }
            if (tabBar.IsReorderable) result |= ToolTabBar.Reorderable;
            if (tabBar.ShowListUpButton) result |= ToolTabBar.TabListPopupButton;
            return result;
        }
        internal static ToolTreeNode CalcToolTreeNode(this IToolTreeNode treeNode)
        {
            if (treeNode == null) return default;
            var result = ToolTreeNode.None;
            if (treeNode.AlwaysOpen) result |= ToolTreeNode.Leaf;
            if (treeNode.Bullet) result |= ToolTreeNode.Bullet;
            if (treeNode.DefaultOpened) result |= ToolTreeNode.DefaultOpen;
            switch (treeNode.FrameType)
            {
                case IToolTreeNode.TreeNodeFrameType.Framed: result |= ToolTreeNode.Framed; break;
                case IToolTreeNode.TreeNodeFrameType.FramePadding: result |= ToolTreeNode.FramePadding; break;
            }
            if (treeNode.OpenOnArrow) result |= ToolTreeNode.OpenOnArrow;
            if (treeNode.OpenOnDoubleClick) result |= ToolTreeNode.OpenOnDoubleClick;
            if (treeNode.Selected) result |= ToolTreeNode.Selected;
            if (treeNode.SpanFullWidth) result |= ToolTreeNode.SpanFullWidth;
            return result;
        }
    }
}
