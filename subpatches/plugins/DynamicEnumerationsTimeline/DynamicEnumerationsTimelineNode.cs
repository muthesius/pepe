#region usings
using System;
using System.ComponentModel.Composition;
using System.Linq;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "Timeline", Category = "Enumerations", Version = "Dynamic", Help = "Basic template with dynamic custom enumeration", Tags = "")]
	#endregion PluginInfo
	public class DynamicEnumerationsTimelineNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input", EnumName = "Timeline")]
		public IDiffSpread<EnumEntry> FInput;

		[Input("UpdateEnum", IsBang = true)]
		public ISpread<bool> FChangeEnum;

		[Input("Enum Entries")]
		public ISpread<string> FEnumStrings;

		[Output("Name")]
		public ISpread<string> FNameOutput;

		[Output("Index")]
		public ISpread<int> FOrdOutput;

		[Import()]
		public ILogger Flogger;
		#endregion fields & pins

		//add some entries to the enum in the constructor
		[ImportingConstructor()]
		public DynamicEnumerationsTimelineNode()
		{
			var s = new string[] {
				"value_LINE_00"
			};
			//Please rename your Enum Type to avoid 
			//numerous "MyDynamicEnum"s in the system
			EnumManager.UpdateEnum("Timeline", "two", s);
		}

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FNameOutput.SliceCount = FInput.SliceCount;
			FOrdOutput.SliceCount = FInput.SliceCount;

			if ((FChangeEnum[0]) && (FEnumStrings.SliceCount > 0)) {
				EnumManager.UpdateEnum("Timeline", FEnumStrings[0], FEnumStrings.ToArray());
			}

			if (FInput.IsChanged) {
				for (int i = 0; i < SpreadMax; i++) {
					FNameOutput[i] = FInput[i].Name;
					FOrdOutput[i] = FInput[i].Index;
				}

				Flogger.Log(LogType.Debug, "Input was changed");
			}
		}
	}
}
