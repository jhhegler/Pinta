// 
// ToolManager.cs
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
using System.Collections.Generic;
using Gtk;

namespace Pinta.Core
{
	public class ToolManager : IEnumerable<BaseTool>
	{
		int index = -1;
		int prev_index = -1;
		
		private List<BaseTool> Tools;

		public ToolManager ()
		{
			Tools = new List<BaseTool> ();
		}

		public void AddTool (BaseTool tool)
		{
			tool.ToolItem.Clicked += HandlePbToolItemClicked;
			tool.ToolItem.Sensitive = tool.Enabled;
			
			Tools.Add (tool);	
		}
		
		void HandlePbToolItemClicked (object sender, EventArgs e)
		{
			ToggleToolButton tb = (ToggleToolButton)sender;

			BaseTool t = FindTool (tb.Label);
		
			// Don't let the user unselect the current tool	
			if (t.Name == CurrentTool.Name) {
				//tb.Active = true;
				//return;
			}

			SetCurrentTool (t);
		}

		private BaseTool FindTool (string name)
		{
			foreach (BaseTool tool in Tools) {
				if (tool.Name == name) {
					return tool;
				}
			}
			
			return null;
		}
		
		public BaseTool CurrentTool {
			get { return Tools[index]; }
		}
		
		public BaseTool PreviousTool {
			get { return Tools[prev_index]; }
		}

		public void SetCurrentTool (BaseTool tool)
		{
			int i = Tools.IndexOf (tool);
			
			if (index == i)
				return;
			
			// Unload previous tool if needed
			if (index >= 0) {
				Tools[index].DoClearToolBar (PintaCore.Chrome.ToolToolBar);
				Tools[index].DoDeactivated ();
				Tools[index].ToolItem.Active = false;
				prev_index = index;
			}
			
			// Load new tool
			index = i;
			tool.DoBuildToolBar (PintaCore.Chrome.ToolToolBar);
			tool.ToolItem.Active = true;
			
			PintaCore.Chrome.SetStatusBarText (string.Format (" {0}: {1}", tool.Name, tool.StatusBarText));
		}
		
		#region IEnumerable<BaseTool> implementation
		public IEnumerator<BaseTool> GetEnumerator ()
		{
			return Tools.GetEnumerator ();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return Tools.GetEnumerator ();
		}
		#endregion
	}
}