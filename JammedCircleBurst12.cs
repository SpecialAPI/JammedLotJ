using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brave.BulletScript;
using System.Collections;

namespace JammedLotJ
{
	public class JammedCircleBurst12 : Script
	{
		protected override IEnumerator Top()
		{
			float num = RandomAngle();
			float num2 = 30f;
			for (int i = 0; i < 12; i++)
			{
				Fire(new Direction(num + i * num2, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new Bullet(null, false, false, true));
			}
			return null;
		}
	}
}
