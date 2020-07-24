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
