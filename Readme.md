<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128628198/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2732)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms Data Grid - Draw a thick border under a data cell (PaintEx event)

This example shows how to draw a thick border around a grid cell under the mouse pointer:

![WinForms Data Grid - Draw thick cell borders by handling the PaintX event](https://raw.githubusercontent.com/DevExpress-Examples/how-to-draw-thick-cell-borders-by-handling-the-paint-event-e2732/17.2.3%2B/media/winforms-grid-paintx.gif)

The example handles the grid's [PaintEx](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.GridControl.PaintEx) event to draw a border. The `PaintEx` event allows you to use DirectX-compatible APIs (`e.Cache`):

```csharp
private void GridControl_PaintEx(object sender, DevExpress.XtraGrid.PaintExEventArgs e) {
    DrawHotTrackedCell(e.Cache);
}

private void DrawHotTrackedCell(GraphicsCache cache) {
    Rectangle bounds = GetCellBounds(HotTrackedCell);
    cache.DrawRectangle(new Pen(Brushes.Black, _BorderWidth), bounds);
}
```


## Files to Review

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))


## Documentation

* [DirectX Hardware Acceleration](https://docs.devexpress.com/WindowsForms/119441/common-features/graphics-performance-and-high-dpi/directx-hardware-acceleration)
