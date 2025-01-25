using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JSON.Database.Manager
{
	[Serializable]
	public class JSONCentroPuebloDatabaseScript
	{
		public bool _DATAOneWays;
		public bool _DATAPedestrians;
		public bool _DATANoLeftTurns;

		public int _DATAOneWaysValue;
		public int _DATAPedestriansValue;
		public int _DATANoLeftTurnsValue;
	}
}

