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
	public enum PinType
	{
		DigitalInput,
		DigitalOutput
	}
	public enum MyEnum
	{
		A,
		B,
		C,
		D
	}
	public enum MyEnum2
	{
		eins,
		zwei,
		drei,
		vier
	}
	//Please rename your Enum Type to avoid 
	//numerous "MyEnum"s in the system

	#region PluginInfo
	[PluginInfo(Name = "PinMode", Category = "Enumerations", Version = "Static", Help = "Basic template with native .NET enum type", Tags = "")]
	#endregion PluginInfo
	public class StaticEnumerationsPinModeNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Type", DefaultEnumEntry = "DigitalInput")]
		public IDiffSpread<PinType> FType;

		[Input("Input", DefaultEnumEntry = "B")]
		public IDiffSpread<MyEnum> FInput;
		
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
					FNameOutput[i] = Enum.GetName(typeof(MyEnum), FInput[i]);
					FOrdOutput[i] = (int)FInput[i];
				}

				Flogger.Log(LogType.Debug, "Input was changed");
			}
			
			
			
			
			if (FType.IsChanged) {
				for (int i = 0; i < SpreadMax; i++) {
					FNameOutput[i] = Enum.GetName(typeof(MyEnum), FInput[i]);
					FOrdOutput[i] = (int)FInput[i];
				}

				Flogger.Log(LogType.Debug, "FType was changed");
			}
			
			
			
			
		}
	}
}
