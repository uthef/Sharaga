using Microsoft.Maui.Layouts;
using StackLayoutManager = Microsoft.Maui.Layouts.StackLayoutManager;

namespace MAUI_TASK_6.Layouts
{
    public class HorizontalWrapLayoutManager : StackLayoutManager
    {
        HorizontalWrapLayout _layout;

        public HorizontalWrapLayoutManager(HorizontalWrapLayout horizontalWrapLayout) : base(horizontalWrapLayout)
        {
            _layout = horizontalWrapLayout;
        }

        public override Size Measure(double widthConstraint, double heightConstraint)
        {
            var padding = _layout.Padding;
            widthConstraint -= padding.HorizontalThickness;
            double currentRowWidth = 0,
                currentRowHeight = 0,
                totalWidth = 0,
                totalHeight = 0;

            for (int n = 0; n < _layout.Count; n++)
            {
                var child = _layout[n];

                if (child.Visibility is Visibility.Collapsed) continue;

                var measure = child.Measure(double.PositiveInfinity,
                    heightConstraint);

                if (currentRowWidth + measure.Width > widthConstraint)
                {
                    totalWidth = Math.Max(totalWidth, currentRowWidth);
                    totalHeight += currentRowHeight;

                    totalHeight += _layout.Spacing;

                    currentRowWidth = 0;
                    currentRowHeight = measure.Height;

                }
                else
                {
                    currentRowWidth += measure.Width;
                    currentRowHeight = Math.Max(currentRowHeight,
                        measure.Height);

                    if (n < _layout.Count - 1)
                        currentRowWidth += _layout.Spacing;

                }
            }

            totalWidth = Math.Max(totalWidth, currentRowWidth);
            totalHeight += currentRowHeight;
            totalWidth += padding.HorizontalThickness;
            totalHeight += padding.VerticalThickness;

            var finalHeight = ResolveConstraints(heightConstraint,
                Stack.Height, totalHeight, Stack.MinimumHeight,
                Stack.MaximumHeight);
            var finalWidth = ResolveConstraints(widthConstraint,
                Stack.Width, totalWidth, Stack.MinimumWidth,
                Stack.MaximumWidth);

            return new Size(finalWidth, finalHeight);
        }

        public override Size ArrangeChildren(Rect bounds)
        {
            var padding = Stack.Padding;
            double top = padding.Top + bounds.Top;
            double left = padding.Left + bounds.Left;
            double currentRowTop = top;
            double currentX = left;
            double currentRowHeight = 0;
            double maxStackWidth = currentX;

            for (int n = 0; n < _layout.Count; n++)
            {
                var child = _layout[n];
                if (child.Visibility == Visibility.Collapsed)
                {
                    continue;
                }

                if (currentX + child.DesiredSize.Width > bounds.Right)
                {
                    maxStackWidth = Math.Max(maxStackWidth, currentX);
                    currentX = left;
                    currentRowTop += currentRowHeight + _layout.Spacing;
                    currentRowHeight = 0;
                }

                var destination = new Rect(currentX, currentRowTop, child.DesiredSize.Width, child.DesiredSize.Height);
                child.Arrange(destination);

                currentX += destination.Width + _layout.Spacing;
                currentRowHeight = Math.Max(currentRowHeight, destination.Height);

            }

            var actual = new Size(maxStackWidth, currentRowTop + currentRowHeight);

            return actual.AdjustForFill(bounds, Stack);
        }
    }
}