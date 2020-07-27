namespace Altseed2.ToolAuxiliary
{
    internal static class FlagCalculator
    {
        internal static ToolColorEditFlags CalcToolColorEdit(this IToolColorEdit colorEdit)
        {
            if (colorEdit == null) return default;
            var result = ToolColorEditFlags.None;
            if (!colorEdit.EditAlpha) result |= ToolColorEditFlags.NoAlpha;
            switch (colorEdit.InputType)
            {
                case IToolColorEdit.ColorEditInputType.None: result |= ToolColorEditFlags.NoInputs; break;
                case IToolColorEdit.ColorEditInputType.RGB: result |= ToolColorEditFlags.DisplayRGB; break;
                case IToolColorEdit.ColorEditInputType.HSV: result |= ToolColorEditFlags.DisplayHSV; break;
                case IToolColorEdit.ColorEditInputType.ColorCode: result |= ToolColorEditFlags.DisplayHex; break;
            }
            switch (colorEdit.PickerType)
            {
                case IToolColorEdit.ColorEditPickerType.Bar: result |= ToolColorEditFlags.PickerHueBar; break;
                case IToolColorEdit.ColorEditPickerType.Wheel: result |= ToolColorEditFlags.PickerHueWheel; break;
            }
            if (colorEdit.ShowAlphaBar) result |= ToolColorEditFlags.AlphaBar;
            if (!colorEdit.ShowInputOption) result |= ToolColorEditFlags.NoOptions;
            if (!colorEdit.ShowLabel) result |= ToolColorEditFlags.NoLabel;
            if (!colorEdit.ShowPicker) result |= ToolColorEditFlags.NoPicker;
            if (!colorEdit.ShowSmallPreview) result |= ToolColorEditFlags.NoSmallPreview;
            switch (colorEdit.ValueType)
            {
                case IToolColorEdit.ColorEditValueType.Float: result |= ToolColorEditFlags.Float; break;
                case IToolColorEdit.ColorEditValueType.UInt8: result |= ToolColorEditFlags.Uint8; break;
            }
            return result;
        }
        internal static ToolInputTextFlags CalcToolInputText(this IToolInputText inputText)
        {
            if (inputText == null) return default;
            var result = ToolInputTextFlags.None;
            if (inputText.AutoSelectAll) result |= ToolInputTextFlags.AutoSelectAll;
            if ((inputText.CharType & IToolInputText.InputTextCharType.Scientific) == (IToolInputText.InputTextCharType)1) result |= ToolInputTextFlags.CharsScientific;
            if ((inputText.CharType & (IToolInputText.InputTextCharType)2) == (IToolInputText.InputTextCharType)2) result |= ToolInputTextFlags.CharsDecimal;
            if ((inputText.CharType & IToolInputText.InputTextCharType.Hexadecimal) == (IToolInputText.InputTextCharType)4) result |= ToolInputTextFlags.CharsHexadecimal;
            if (!inputText.EnableHorizontalScroll) result |= ToolInputTextFlags.NoHorizontalScroll;
            if (inputText.IsOverwriteMode) result |= ToolInputTextFlags.AlwaysInsertMode;
            if (inputText.IsPasswordMode) result |= ToolInputTextFlags.Password;
            if (inputText.IsReadOnly) result |= ToolInputTextFlags.ReadOnly;
            if (inputText.NoBlank) result |= ToolInputTextFlags.CharsNoBlank;
            if (inputText.TabSpace) result |= ToolInputTextFlags.AllowTabInput;
            if (inputText.UpperCaseFilter) result |= ToolInputTextFlags.CharsUppercase;
            return result;
        }
        internal static ToolSelectableFlags CalcToolSelectable(this IToolSelectable selectable)
        {
            if (selectable == null) return default;
            var result = ToolSelectableFlags.None;
            if (selectable.AllowDoubleClick) result |= ToolSelectableFlags.AllowDoubleClick;
            if (!selectable.Enabled) result |= ToolSelectableFlags.Disabled;
            if (selectable.KeepOpenPopups) result |= ToolSelectableFlags.DontClosePopups;
            return result;
        }
        internal static ToolTabBarFlags CaclToolTabBar(this IToolTabBar tabBar)
        {
            if (tabBar == null) return default;
            var result = ToolTabBarFlags.None;
            switch (tabBar.FittingType)
            {
                case IToolTabBar.TabBarFittingType.Default: result |= ToolTabBarFlags.FittingPolicyScroll | ToolTabBarFlags.NoTabListScrollingButtons; break;
                case IToolTabBar.TabBarFittingType.ReSize: result |= ToolTabBarFlags.FittingPolicyResizeDown; break;
                case IToolTabBar.TabBarFittingType.ScrollBar: result |= ToolTabBarFlags.FittingPolicyScroll; break;
            }
            if (tabBar.IsReorderable) result |= ToolTabBarFlags.Reorderable;
            if (tabBar.ShowListUpButton) result |= ToolTabBarFlags.TabListPopupButton;
            return result;
        }
        internal static ToolTreeNodeFlags CalcToolTreeNode(this IToolTreeNode treeNode)
        {
            if (treeNode == null) return default;
            var result = ToolTreeNodeFlags.None;
            if (treeNode.AlwaysOpen) result |= ToolTreeNodeFlags.Leaf;
            if (treeNode.Bullet) result |= ToolTreeNodeFlags.Bullet;
            if (treeNode.DefaultOpened) result |= ToolTreeNodeFlags.DefaultOpen;
            switch (treeNode.FrameType)
            {
                case IToolTreeNode.TreeNodeFrameType.Framed: result |= ToolTreeNodeFlags.Framed; break;
                case IToolTreeNode.TreeNodeFrameType.FramePadding: result |= ToolTreeNodeFlags.FramePadding; break;
            }
            if (treeNode.OpenOnArrow) result |= ToolTreeNodeFlags.OpenOnArrow;
            if (treeNode.OpenOnDoubleClick) result |= ToolTreeNodeFlags.OpenOnDoubleClick;
            if (treeNode.Selected) result |= ToolTreeNodeFlags.Selected;
            if (treeNode.SpanFullWidth) result |= ToolTreeNodeFlags.SpanFullWidth;
            return result;
        }
    }
}
