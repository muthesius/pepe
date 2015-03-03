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
	[PluginInfo(Name = "InputPepe Builder", Category = "Enumerations", Version = "Dynamic", Help = "Basic template with dynamic custom enumeration", Tags = "")]
	#endregion PluginInfo
	public class DynamicEnumerationsInputPepeNode : IPluginEvaluate
	{
		#region fields & pins
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
		public DynamicEnumerationsInputPepeNode()
		{
			var s = new string[] {
				"Digital_02",
				"Digital_03",
				"Digital_04",
				"Digital_05",
				"Digital_06",
				"Digital_07",
				"Digital_08",
				"Digital_09",
				"Digital_10",
				"Digital_11",
				"Digital_12",
				"Digital_13",
				"Analog_A0",
				"Analog_A1",
				"Analog_A2",
				"Analog_A3",
				"Analog_A4",
				"Analog_A5",
				"42"
			};
			//Please rename your Enum Type to avoid 
			//numerous "MyDynamicEnum"s in the system
			EnumManager.UpdateEnum("InputPepe", "42", s);
		}

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{

			if ((FChangeEnum[0]) && (FEnumStrings.SliceCount > 0)) {
				EnumManager.UpdateEnum("InputPepe", FEnumStrings[0], FEnumStrings.ToArray());
			}

	}
	}
}
