using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using TheCollector.CollectableManager;
using TheCollector.Data;
using TheCollector.Utility;

namespace TheCollector.Windows;

public class StopUi : Window, IDisposable
{
    private readonly AutomationHandler _automation;
    private readonly CollectableAutomationHandler _collectableHandler;

    public StopUi(AutomationHandler automation, CollectableAutomationHandler collectableHandler)
        : base("收藏品助手##CollectorStop",
               ImGuiWindowFlags.NoScrollbar
               | ImGuiWindowFlags.NoScrollWithMouse
               | ImGuiWindowFlags.NoResize
               | ImGuiWindowFlags.NoCollapse
               | ImGuiWindowFlags.NoSavedSettings
               | ImGuiWindowFlags.NoMove
               | ImGuiWindowFlags.AlwaysAutoResize)
    {
        _automation = automation;
        _collectableHandler = collectableHandler;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(350, 140),
            MaximumSize = new Vector2(350, 800)
        };

    }

    public override void PreOpenCheck()
    {
        if (_automation.IsRunning)
            IsOpen = true;
        else IsOpen = false;
    }

    public override void PreDraw()
    {
        var io = ImGui.GetIO();
        var center = io.DisplaySize / 2f;
        ImGui.SetNextWindowPos(center, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
        ImGui.SetNextWindowFocus();
    }

    public override void Draw()
    {
        ImGuiHelper.Panel("StatusInfo", DrawStatusInfo);
        DrawStopButton();
    }

    private void DrawStopButton()
    {
        if (ImGui.Button("停止", new Vector2(ImGui.GetContentRegionAvail().X, 100)))
            _automation?.ForceStop("已由用户停止");

    }
    private void DrawStatusInfo()
    {
        switch (Plugin.State)
        {
            case PluginState.Idle:
                ImGui.TextUnformatted("空闲中...");
                break;
            case PluginState.Teleporting:
                ImGui.TextUnformatted("传送中...");
                break;
            case PluginState.MovingToCollectableVendor:
                ImGui.TextUnformatted("前往兑换员...");
                break;
            case PluginState.ExchangingItems:
                ImGui.TextUnformatted("正在交纳物品：");
                if (_collectableHandler.TurnInQueue.Length != 0)
                {
                    for (int i = 0; i < _collectableHandler.TurnInQueue.Length; i++)
                    {
                        var item = _collectableHandler.TurnInQueue[i];
                        if (_collectableHandler.CurrentItemName is not null && _collectableHandler.CurrentItemName == item.name)
                        {
                            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0f, 1f, 0f, 1f)); //green
                            ImGui.TextUnformatted("当前物品：");
                            ImGui.PopStyleColor();
                            ImGui.SameLine();

                        }

                        ImGui.TextUnformatted($"{item.name} : {item.left}");
                    }
                }
                break;
            case PluginState.SpendingScrip:
                ImGui.TextUnformatted("正在使用工票...");
                break;
        }
    }


    public void Dispose()
    {
        _automation.Dispose();
    }
}
