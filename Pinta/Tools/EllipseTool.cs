// 
// EllipseTool.cs
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Copyright (c) 2010 Jonathan Pobst
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Cairo;
using Pinta.Core;

namespace Pinta
{
	public class EllipseTool : ShapeTool
	{
		public override string Name {
			get { return "Ellipse"; }
		}
		public override string Icon {
			get { return "Tools.Ellipse.png"; }
		}
		public override string StatusBarText {
			get { return "Click and drag to draw an ellipse (right click for secondary color). Hold shift to constrain to a circle."; }
		}
		public override bool Enabled {
			get { return true; }
		}
		
		public EllipseTool ()
		{
		}
		
		protected override Rectangle DrawShape (Rectangle rect, Layer l)
		{
			Rectangle dirty;
			
			using (Context g = new Context (l.Surface)) {
				g.Antialias = Antialias.Subpixel;
				dirty = g.FillStrokedEllipse (rect, fill_color, outline_color, BrushWidth);
			}
			
			return dirty;
		}
	}
}