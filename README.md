# ContentSizeTransitionDemo

一个基于 WPF 的最小示例项目，用于演示**内容尺寸变化时的平滑过渡效果**。

项目中实现了一个自定义控件 `ContentSizeTransition`。当其 `Content` 的期望尺寸发生变化时，控件会对测量尺寸执行短时动画，从而避免界面在高度变化时“瞬间跳变”。

## 项目目的

在 WPF 中，如果容器开启了 `SizeToContent`，而内部内容的尺寸又会动态变化，窗口或容器通常会直接跳到新的大小，视觉上较为生硬。

本项目演示了一种简单实现方式：

- 监听内容测量结果的变化
- 在旧尺寸和新尺寸之间插入过渡动画
- 让外层布局在动画期间逐步更新，而不是一次性突变

## 效果说明

示例窗口中包含：

- 一个 `Toggle content` 按钮
- 一个带红色边框的内容容器

每次点击按钮，容器内部内容会在两种高度之间切换：

- `50`
- `150`

由于内容被放置在 `ContentSizeTransition` 中，因此高度变化会以动画方式过渡，而不是立即跳变。

## 技术实现

核心实现位于 `ContentSizeTransitionDemo/Components/ContentSizeTransition.cs`。

### 实现思路

`ContentSizeTransition` 继承自 `ContentControl`，并通过以下方式实现尺寸动画：

1. 在 `MeasureOverride()` 中先调用基类测量，得到内容当前真实需要的尺寸。
2. 如果检测到目标尺寸发生变化：
	- 清除上一次动画
	- 启动一个 `SizeAnimation`
	- 从当前 `DesiredSize` 过渡到新的测量尺寸
3. 使用依赖属性触发布局重新测量，使动画中的尺寸能够持续参与布局过程。
4. 在 `ArrangeOverride()` 中将唯一子元素按当前动画尺寸进行排列。

### 动画参数

当前实现使用：

- 时长：`0.15` 秒
- 缓动：`SineEase`
- 模式：`EaseOut`

这意味着动画会快速开始、平滑结束，适合作为轻量级的界面反馈。

## 示例窗口结构

示例窗口位于：

- `ContentSizeTransitionDemo/MainWindow.xaml`
- `ContentSizeTransitionDemo/MainWindow.xaml.cs`

窗口特点：

- `SizeToContent="Height"`
- `ResizeMode="NoResize"`
- 点击按钮后动态替换 `container.Content`

代码中通过切换 `TextBlock.MinHeight`，模拟内容尺寸变化：

- 偶数次点击高度为 `50`
- 奇数次点击高度为 `150`

## 项目结构

```text
ContentSizeTransitionDemo.slnx
README.md
ContentSizeTransitionDemo/
├─ App.xaml
├─ App.xaml.cs
├─ MainWindow.xaml
├─ MainWindow.xaml.cs
├─ Components/
│  └─ ContentSizeTransition.cs
└─ ContentSizeTransitionDemo.csproj
```

## 运行环境

- .NET 8
- Windows
- WPF

项目文件中目标框架为：

- `net8.0-windows`

## 如何运行

### 使用 Visual Studio 或 VS Code

1. 打开解决方案或项目目录
2. 还原 NuGet 依赖（如果 IDE 未自动完成）
3. 构建并运行项目

### 使用命令行

在项目根目录执行：

```bash
dotnet run --project .\ContentSizeTransitionDemo\ContentSizeTransitionDemo.csproj
```

## 可扩展方向

如果你想把这个控件用于实际项目，可以继续扩展：

- 支持可配置的动画时长
- 支持自定义缓动函数
- 同时过渡宽度与高度
- 避免频繁内容切换时的重复布局抖动
- 封装为可复用的通用动画容器控件

## 适用场景

`ContentSizeTransition` 适合用于以下场景：

- 折叠 / 展开面板
- 表单验证提示的动态出现
- 列表项详情区域展开
- 卡片内容增减时的平滑尺寸变化
- 基于 `SizeToContent` 的弹窗内容切换
