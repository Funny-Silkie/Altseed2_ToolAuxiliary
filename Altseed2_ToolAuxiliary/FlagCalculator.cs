namespace Altseed2.ToolAuxiliary
{
    internal static class FlagCalculator
    {
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
