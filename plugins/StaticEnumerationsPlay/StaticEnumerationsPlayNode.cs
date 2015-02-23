#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	//create your own enum type or use any other .net enum
	public enum PlayNum
	{
		Pin0,
		Pin1,
		Pin2,
		Pin3,
		Pin4,
		Pin5,
		Pin6,
		Pin7
	}
	//Please rename your Enum Type to avoid 
	//numerous "MyEnum"s in the system

	#region PluginInfo
	[PluginInfo(Name = "Play", Category = "Enumerations", Version = "Static", Help = "Basic template with native .NET enum type", Tags = "")]
	#endregion PluginInfo
	public class StaticEnumerationsPlayNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input", DefaultEnumEntry = "B")]
		public IDiffSpread<PlayNum> FInput;

		[Output("Name")]
		public ISpread<string> FNameOutput;

		[Output("Index")]
		public ISpread<int> FOrdOutput;

		[Import()]
		public ILogger Flogger;
		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FNameOutput.SliceCount = SpreadMax;
			FOrdOutput.SliceCount = SpreadMax;

			if (FInput.IsChanged) {
				for (int i = 0; i < SpreadMax; i++) {
					FNameOutput[i] = Enum.GetName(typeof(PlayNum), FInput[i]);
					FOrdOutput[i] = (int)FInput[i];
				}

				Flogger.Log(LogType.Debug, "Input was changed");
			}
		}
	}
}
