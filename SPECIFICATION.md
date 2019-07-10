# Librame Specification
Updated in 2019-07-10

## 1. 基本代码规范

遵循 C# 基本代码规范。

## 2. 专用代码规范

### 2.1 命名规范

**类命名**

* 接口类：强制以 **I** 为前缀命名（通常位于抽象层。如：**IBuilder**）。
* 抽象类：强制以 **Abstract** 为前缀命名（通常位于抽象层。如：**AbstractBuilder**）。
* 基础类：强制以 **Base** 为后缀命名（用于抽象类或接口的基类实现，通常位于实现层。如：**BuilderBase**）。
* 内部类：强制以 **Internal** 为前缀命名（通常用于抽象类或接口的实现，通常位于实现层。如：**InternalBuilder**）。
* 静态扩展类：强制以程序集命名后缀单数形式为前缀（原型命名或没有则省略），并在功能名称+被扩展类型名后附加 **.Extensions** 为后缀命名（如：**BuidlerExtensions**、**AbstractionBuidlerExtensions**、**AbstractionBuilderServiceCollectionExtensions**）。

**类命名空间**

* 各种类：在大程序集中，强制以程序集命名空间后附加 **.Children** 子级目录复数形式为后缀命名；在模块化程序集中，强制以程序集命名空间命名（可按子级目录复数形式分类便于管理，但不用做命名空间）。
* 静态扩展类：在原生静态扩展类中，强制以原生被扩展类命名空间命名（如：**Librame.Extensions.Core[.AbstractionBuidlerExtensions]**）；在第三方静态扩展类中，如果被扩展类为流式扩展，则采用第三方命名空间命名（如：**Microsoft.Extensions.DependencyInjection[.BuilderServiceCollectionExtensions]**），反之则统一以 **.Extensions** 命名空间命名（如：**Librame.Extensions**）。
* 引用命名空间：强制同解决方案的命名空间在当前命名空间内引用（可省略初始引用名。如：--Librame.--**Extensions**），第三方（含系统）则在当前命名空间外（头部）引用。

**程序集命名**

* 抽象程序集名称：强制在原型名 **Assembly** 后附加 **.Abstractions** 复数形式为后缀命名（如：**Librame.Extensions.Core.Abstractions**）。
* 抽象命名空间：强制以原型 **Assembly** 命名（如：**Librame.Extensions.Core**）。
* 实现（原生）程序集名称：强制以原型 **Assembly** 命名（如：**Librame.Extensions.Core**）。
* 实现（原生）命名空间：强制以原型 **Assembly** 命名（如：**Librame.Extensions.Core**）。
* 实现（第三方）程序集名称：强制在原型名 **Assembly** 后附加 **.Library** 第三方程序集为后缀命名（如：**Librame.Extensions.Data.EntityFrameworkCore**）。
* 实现（第三方）命名空间：强制以原型 **Assembly** 命名（如：**Librame.Extensions.Data**）。

### 2.2 布局规范

**接口**

* 标记与泛型接口可存在于单页（如：**IConverter**、**IConverter<T>**）。
* 选项与子级选项可存在于单页（如：**DrawingBuilderOptions**、**WatermarkOptions**）。

**空行**

* 单行：字段、属性、方法等成员具有重载或直接关系时以单行间隔。
* 双行：字段、属性、方法等成员不具有直接关系时以双行间隔（类）。

**域**

* 分域：当单页成员过多时，可采用域分隔（各域间以双行间隔，域外以单行间隔）。

### 2.1 注释规范

**顶部版权声明**

    #region License

    /* **************************************************************************************
     * Copyright (c) Librame Pang All rights reserved.
     * 
     * http://librame.net
     * 
     * You must not remove this notice, or any other, from this software.
     * **************************************************************************************/

    #endregion

### 2.1 使用规范

**语言特性**

* this：新版已经不推荐使用，特殊情况下除外（推荐直接访问成员）。
* base: 子类重写时强制使用。
* var：强制在各场合使用（除限制返回指定基本类型外；如：限制返回 **IList<T>**）。
* 字符串组合：强制使用字符串内插形式（如：$"Hello {Name}"）。
