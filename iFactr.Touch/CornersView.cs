using System;
using System.Drawing;

using CoreGraphics;
using UIKit;

namespace iFactr.Touch
{
	internal enum MGCornersPosition
	{
		LeadingVertical, // top of screen for a left/right split.
		TrailingVertical, // bottom of screen for a left/right split.
		LeadingHorizontal, // left of screen for a top/bottom split.
		TrailingHorizontal // right of screen for a top/bottom split.
	}
	
	class MGSplitCornersView : UIView
	{
		internal nfloat CornerRadius { get; set; }
		internal MGCornersPosition CornersPosition { get; set; }
		internal UIColor CornerBackgroundColor { get; set; }
		
		internal MGSplitCornersView ()
		{
			ContentMode = UIViewContentMode.Redraw;
			UserInteractionEnabled = false;
			Opaque = false;
			//BackgroundColor = UIColor.Clear;
			CornerRadius = 0; // actual value is set by the splitViewController.
			CornersPosition = MGCornersPosition.LeadingVertical;
		}
		
		
		float DegreesToRadians(float degrees)
		{
			// Converts degrees to radians.
			return degrees * ((float)Math.PI / 180);
		}
		
		
		float RadiansToDegrees(float radians)
		{
			// Converts radians to degrees.
			return radians * (180 / (float)Math.PI);
		}
		
		
		void DrawRect(CGRect rect)
		{
			// Draw two appropriate corners, with cornerBackgroundColor behind them.
			if (CornerRadius > 0) {
		
				nfloat maxX = Bounds.GetMaxX();
				nfloat maxY = Bounds.GetMaxY();
				UIBezierPath path = UIBezierPath.Create();
				CGPoint pt = CGPoint.Empty;
				switch (CornersPosition) {
					case MGCornersPosition.LeadingVertical: // top of screen for a left/right split
						path.MoveTo(pt);
						pt.Y += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(90), 0, true));
						pt.X += CornerRadius;
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						path.AddLineTo(CGPoint.Empty);
						path.ClosePath();
		
						pt.X = maxX - CornerRadius;
						pt.Y = 0;
						path.MoveTo(pt);
						pt.Y = maxY;
						path.AddLineTo(pt);
						pt.X += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(180), DegreesToRadians(90), true));
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						break;
		
					case MGCornersPosition.TrailingVertical: // bottom of screen for a left/right split
						pt.Y = maxY;			
						path.MoveTo(pt);
						pt.Y -= CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(270), DegreesToRadians(360), false));
						pt.X += CornerRadius;
						pt.Y += CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						pt.X = maxX - CornerRadius;
						pt.Y = maxY;
						path.MoveTo(pt);
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						pt.X += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(180), DegreesToRadians(270), false));
						pt.Y += CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						break;
		
					case MGCornersPosition.LeadingHorizontal: // left of screen for a top/bottom split
						pt.X = 0;
						pt.Y = CornerRadius;
						path.MoveTo(pt);
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						pt.X += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(180), DegreesToRadians(270), false));
						pt.Y += CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						pt.X = 0;
						pt.Y = maxY - CornerRadius;
						path.MoveTo(pt);
						pt.Y = maxY;
						path.AddLineTo(pt);
						pt.X += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(180), DegreesToRadians(90), true));
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						break;
		
					case MGCornersPosition.TrailingHorizontal: // right of screen for a top/bottom split
						pt.Y = CornerRadius;
						path.MoveTo(pt);
						pt.Y -= CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(270), DegreesToRadians(360), false));
						pt.X += CornerRadius;
						pt.Y += CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
		
						pt.Y = maxY - CornerRadius;
						path.MoveTo(pt);
						pt.Y += CornerRadius;
						path.AppendPath(UIBezierPath.FromArc(pt, CornerRadius, DegreesToRadians(90), 0, true));
						pt.X += CornerRadius;
						pt.Y -= CornerRadius;
						path.AddLineTo(pt);
						pt.X -= CornerRadius;
						path.AddLineTo(pt);
						path.ClosePath();
					
						break;
		
					default:
						break;
				}
		
				CornerBackgroundColor.SetColor();
				path.Fill();
			}
		}
		
		
		void SetCornerRadius(float newRadius)
		{
			if (newRadius != CornerRadius) {
				CornerRadius = newRadius;
				SetNeedsDisplay();
			}
		}
		
		
//		void SetSplitViewController(MGSplitViewController controller)
//		{
//			if (controller != splitViewController) {
//				splitViewController = controller;
//				SetNeedsDisplay();
//			}
//		}
		
		
		void SetCornersPosition(MGCornersPosition pos)
		{
			if (CornersPosition != pos) {
				CornersPosition = pos;
				SetNeedsDisplay();
			}
		}
		
		
		void SetCornerBackgroundColor(UIColor color)
		{
			if (color != CornerBackgroundColor) {
				CornerBackgroundColor = color;
				SetNeedsDisplay();
			}
		}
	}
}

