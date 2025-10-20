namespace MinimalMapping.Features.ModMenu.Dialogue;

internal class ModMenuDialogue : GenericDialogue
{
    private ComposableGuiTab _currentTab;
    private readonly List<ComposableGuiTab> _tabs = [];

    private static string T(string code, params object[] args) 
        => G.T("ModMenu", code, args);

    public ModMenuDialogue(ICoreGantryAPI gantry, List<ComposableGuiTab> tabs, ModFileScope scope) : base(gantry)
    {
        Title = T("Title");
        Alignment = EnumDialogArea.CenterMiddle;
        ModalTransparency = 0.6f;
        ShowTitleBar = true;
        Modal = true;

        var generalTab = new GeneralSettingsGuiTab(Gantry, scope);
        foreach (var tab in tabs.OrderBy(p => p.Name).Prepend(generalTab))
        {
            tab.Active = false;
            tab.DataInt = _tabs.Count;
            _tabs.Add(tab);
        }
        _currentTab = _tabs.First().To<ComposableGuiTab>();
    }

    private double CalculatedTabWidth()
    {
        var maxWidth = 200.0;
        foreach (var tab in _tabs)
        {
            var font = CairoFont.WhiteDetailText().WithFontSize(17f);
            var width = font.GetTextExtents(tab.Name).Width;
            maxWidth = Math.Max(width + 1.0 + 2.0 * GuiElement.scaled(3.0), maxWidth);
        }
        return maxWidth;
    }

    protected override void ComposeBody(GuiComposer composer)
    {
        var tabBounds = ElementBounds.FixedOffseted(EnumDialogArea.LeftTop, -221f, 20f, CalculatedTabWidth(), _tabs.Count * 29f);
        composer.AddVerticalTabs([.. _tabs], tabBounds, OnTabClicked, "tabMenuLeft");
        composer.AddComposablePart(this, _currentTab);
    }

    protected override void RefreshValues()
    {
        _currentTab.RefreshValues(SingleComposer);
        var tabs = SingleComposer.GetVerticalTab("tabMenuLeft");
        tabs.SetValue(_currentTab.DataInt, triggerHandler: false);
    }

    private void OnTabClicked(int arg1, GuiTab tab)
    {
        _tabs.ForEach(t => t.Active = false);
        _currentTab = tab.To<ComposableGuiTab>();
        _currentTab.Active = true;
        Recompose();
    }
}