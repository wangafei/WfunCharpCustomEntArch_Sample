# WfunCharpCustomEnt
这个包可以帮助cad二次开发人员快速的用C#实现自定义实体实体，不需要学习复杂的C++，
[样例代码仓库](https://github.com/wangafei/WfunCharpCustomEntArch_Sample)
由于本包内部使用了C++，所以需要严格版本对应，具体对应关系：

| net版本 | 对应CAD版本 | Rx版本 |
| --- | --- | --- |
| net35 | 2010 | R18 |
| net40 | 2013-2014 | R19 |
| net45 | 2015-2016 | R20 |
| net46 | 2017 | R21 |
| net461 | 2018 | R22 |
| net47 | 2019-2020 | R23 |
| net48 | 2021-2024 | R24 |

<img width="248" height="153" alt="image" src="https://github.com/user-attachments/assets/08505e46-d190-4b01-b785-61dfdd415b3f" />

仅支持x64，32位暂未有支持计划
## 目前支持的cad版本：
- [X] Cad2010
- [X] Cad2011
- [X] Cad2012
- [X] Cad2013
- [X] Cad2014
- [X] Cad2015
- [X] Cad2016
- [X] Cad2017
- [X] Cad2018
- [X] Cad2019
- [X] Cad2020
- [X] Cad2021
- [X] Cad2022
- [X] Cad2023
- [X] Cad2024
- [ ] Cad2025
- [ ] Cad2026
## 浩辰CAD版本
- [ ] zwCad2024
- [ ] zwCad2025
- [ ] zwCad2026
## 中望AD版本
- [ ] gsCad2024
- [ ] gsCad2025
- [ ] gsCad2026

# 支持的自定实体功能
## 1.自定义数据归档
``
protected override void SubDataOut(DataIO data)
{
    base.SubDataOut(data);
    DataIO dataThis = new DataIO();
    // dataThis.SetData(value);
    data.SetData(dataThis);
}
protected override void SubDataIn(DataIO data)
{
    base.SubDataIn(data);
    DataIO dataThis = data.GetData();
    // dataThis.GetData(ref value);
}
``
## 2.自定义绘制
``
protected override bool SubDraw(DrawEnt mode)
{
    mode.Draw(new Circle() {Center = Center, Radius = Radius });
    return true;
}
``
## 3.自定义打印信息到命令行
`` 
protected override void SubEntList()
{
    ed.WriteMessage($"\n 半径:{Radius}");
}
``
## 3.自定义矩阵变换
``
protected override bool SubEntTransformBy(Matrix3d xform)
{
    Center = Center.TransformBy(xform);
    return true;
}
``
## 4.自定义夹点
``
protected override bool SubEntGetGripPoints(List<Point3d> gripPts)
{
    gripPts.Add(Center);
    return true;
}
protected override bool SubEntSetGripPoints(Vector3d offset, int nIndex)
{
    if (0 == nIndex)
    {
        Center = Center + offset;
    }
    return true;
}
``

# 其他注意事项参考样例工程


