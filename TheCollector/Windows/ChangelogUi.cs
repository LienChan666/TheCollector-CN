using System;
using OtterGui.Services;
using OtterGui.Widgets;
using TheCollector.Utility;

namespace TheCollector.Windows;

public class ChangelogUi : IUiService
{
    public const int LastChangelogVersion = 0;
    public readonly Changelog Changelog;
    private readonly Configuration _config;
    private readonly PlogonLog _log;
    public ChangelogUi(Configuration config, PlogonLog log)
    {
        _log = log;
        _config = config;
        Changelog = new Changelog("TheCollector 更新日志", ConfigData , Save );
        Add0_28(Changelog);
        Add0_29(Changelog);
        Add0_30(Changelog);
        Add0_31(Changelog);
        Add0_32(Changelog);
        Add0_33(Changelog);
        Add0_34(Changelog);
        Add0_35(Changelog);
        Add0_36(Changelog);
        Add0_37(Changelog);
        Add0_38(Changelog);
        Add0_39(Changelog);
        Add0_40(Changelog);
        Add_0_4_4(Changelog);
        Add_0_4_5(Changelog);
        Add_0_4_6(Changelog);
        
    }

    private void Add_0_4_6(Changelog changeLog)
    {
        changeLog.NextVersion("版本 0.4.6")
            .RegisterEntry("更新读取工票数量的逻辑，修复若干问题");
    }

    private void Add_0_4_5(Changelog log)
    {
        log.NextVersion("版本 0.4.5")
            .RegisterEntry("针对未解锁工票商店子页面导致无法找到物品的情况做了临时修复——现在会强制遍历所有子页面寻找物品");
    }
    private void Add_0_4_4(Changelog log)
    {
        log.NextVersion("版本 0.4.4")
            .RegisterEntry("工票商店物品现在按 ItemId 匹配而非字符串，支持更多语言（可能需要重新添加要购买的物品以正确显示）");
    }
    private void Add0_40(Changelog changelog)
    {
        changelog.NextVersion("版本 0.40")
                 .RegisterEntry("完成工票购买后，插件会交纳剩余的收藏品")
                 .RegisterEntry("如果再次触及工票上限，收藏品交纳会提前停止");
    }

    public static void Add0_39(Changelog log) =>
        log.NextVersion("版本 0.39")
           .RegisterHighlight("功能已重新启用")
           .RegisterHighlight("将 ArtisanBuddy 功能合并到 TheCollector：自动采集结束后可制作指定的 Artisan 列表")
           .RegisterEntry("移除了“自动采集结束后自动交纳收藏品”的功能");
    public static void Add0_38(Changelog log) =>
        log.NextVersion("版本 0.38")
           .RegisterImportant(
               "在 GatherBuddyReborn 最新测试版中已实现自动交纳收藏品及工票购买功能。\n因此我会暂时禁用本插件的相关功能，直到下个版本（届时将移除采集相关收藏品功能，仅保留制作）。\n预计几天内发布，感谢所有使用和支持插件的人！♡");

    public static void Add0_37(Changelog log) =>
        log.NextVersion("版本 0.37")
           .RegisterImportant("如果你的首选商店设置为格里达尼亚旧街，请先选择其他地点再重新选择格里达尼亚旧街，以确保正常工作，谢谢！")
           .RegisterEntry("现在在 Artisan 制作列表结束时会正确检查是否可传送")
           .RegisterEntry("修复在住宅区内不会传送的问题")
           .RegisterEntry(
               "如果位于同一地区且距离不远，将改为直接移动而非传送");
    public static void Add0_36(Changelog log) =>
        log.NextVersion("版本 0.36")
           .RegisterHighlight("为工票购买增加保险措施：当在商店页签中找不到所选物品时，不再购买错误物品")
           .RegisterEntry("工票商店物品数据改为从 Git 仓库拉取，无需更新插件即可调整");
    public static void Add0_35(Changelog log) =>
        log.NextVersion("版本 0.35")
           .RegisterHighlight("新增石匠研磨剂并修复部分物品索引");
    public static void Add0_34(Changelog log) =>
        log.NextVersion("版本 0.34")
           .RegisterHighlight(
               "！！！重要！！！如果你的采集/制作等级足够，但尚未解锁对应的工票兑换页（例如“紫票兑换 - 80级 材料/鱼饵/代币”），插件可能会购买错误物品。\n请先在收藏品/工票NPC处解锁相应页签，再设置高等级物品。")
           .RegisterEntry("已完全修复背包中收藏品排序问题");
    public static void Add0_33(Changelog log) =>
        log.NextVersion("版本 0.33")
           .RegisterEntry(
               "由于 Lumina 的 IsCollectable 标记错误地把“瞪羚革”识别为收藏品，现在已将其从列表中过滤。");
    public static void Add0_32(Changelog log) =>
        log.NextVersion("版本 0.32")
           .RegisterEntry(
               "延长交纳收藏品的超时时间，现在应可完成整包交纳")
           .RegisterEntry("修复已购数量不正确的问题");
    public static void Add0_31(Changelog log) =>
        log.NextVersion("版本 0.31")
           .RegisterHighlight("新增 Lifestream 联动，并新增收藏品地点：九号解决方案与格里达尼亚旧街")
           .RegisterEntry("进一步优化自动化流程");
    public static void Add0_30(Changelog log) =>
        log.NextVersion("版本 0.30")
           .RegisterHighlight("新增工票商店物品紫电灵砂！")
           .RegisterEntry("修复工票商店自动化失效的问题，抱歉！");
    
    public static void Add0_29(Changelog log) =>
        log.NextVersion("版本 0.29")
           .RegisterHighlight("重构了整个自动化流程")
           .RegisterHighlight("新增 /collector stop 命令用于停止自动化，并在自动化运行时显示带停止按钮的窗口")
           .RegisterHighlight("新增配置项：钓鱼结束后自动开始交纳收藏品")
           .RegisterEntry("通过 EzIPC 暴露了部分功能");
    
    private static void Add0_28(Changelog log)=>
        log.NextVersion("版本 0.28")
           .RegisterHighlight("新增更新日志窗口！")
           .RegisterEntry("将九号解决方案的传送标记为不可用并禁用交互，同时将游末邦设为默认")
           .RegisterEntry("修复当数量设置过高时无法购买的问题")
           .RegisterEntry("当必需插件未安装时，相关设置将不可操作");
    
    private (int, ChangeLogDisplayType) ConfigData()
        => (_config.LastSeenVersion, _config.ChangeLogDisplayType);

    private void Save(int version, ChangeLogDisplayType type)
    {
        if (_config.LastSeenVersion != version)
        {
            _config.LastSeenVersion = version;
            _config.Save();
        }

        if (_config.ChangeLogDisplayType != type)
        {
            _config.ChangeLogDisplayType = type;
            _config.Save();
        }
    }
}
